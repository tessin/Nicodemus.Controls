using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using Microsoft.LightSwitch.Client;
using Microsoft.LightSwitch.Runtime.Shell.View;
using Microsoft.LightSwitch.Runtime.Shell.View.Internal;
using Microsoft.VisualStudio.ExtensibilityHosting;

namespace Nicodemus.Controls
{
    public class BusyIndicator
    {
        private static readonly IScreenViewService ScreenViewService =
            VsExportProviderService.GetServiceFromCache<IScreenViewService>(GetScope());

        private readonly IBusyIndicatorHost _busyIndicatorHost;
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                SetBusyState();
            }
        }

        public BusyIndicator(IScreenObject screenObject)
        {
            _busyIndicatorHost = (IBusyIndicatorHost)ScreenViewService.GetScreenView(screenObject);
        }

        private static VsExportProvisionScope GetScope()
        {
            const string assemblyName = "Microsoft.LightSwitch.ExportProvider";
            var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.FullName.StartsWith(assemblyName));
            if (assembly == null)
                throw new DllNotFoundException($"\"{assemblyName}\" is not found.");

            const string typeName = "Microsoft.VisualStudio.ExtensibilityHosting.Scopes";
            var type = assembly.GetType(typeName);
            if (type == null)
                throw new TypeLoadException($"Cannot load \"{typeName}\"");

            var localGuid = (Guid)type.GetField(
                "localGuid", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);
            var global = (VsExportProvisionScope)type.GetProperty(
                "Global", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null, null);
            return new VsExportProvisionOuterScope(localGuid, global);
        }

        private void SetBusyState()
        {
            if (!Deployment.Current.Dispatcher.CheckAccess())
                Deployment.Current.Dispatcher.BeginInvoke(SetBusyState);
            else
                _busyIndicatorHost.SetBusyState(IsBusy);
        }
    }
}
