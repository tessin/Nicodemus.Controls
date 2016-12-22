using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using Microsoft.LightSwitch.Client;
using Microsoft.LightSwitch.Runtime.Shell.View;
using Microsoft.LightSwitch.Runtime.Shell.View.Internal;
using Microsoft.VisualStudio.ExtensibilityHosting;

namespace Nicodemus.Controls
{
    [Obsolete("This particular approach only seemed to work but does in fact not. Replace with BusyWaiter for long running background tasks.", false)]
    public class BusyIndicator
    {

        // These may have to changed in the future. Disassemble 
        // 'Microsoft.LightSwitch.ExportProvider.dll' and look 
        // in the static Scopes class for these Guids.
        private static readonly Guid GlobalGuid = new Guid("229C0B13-97A2-41BE-B96D-3CDDB9E8E389");
        private static readonly Guid LocalGuid = new Guid("92E1D6FA-CAD7-400f-914D-E265294841B4");

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

        public void Show()
        {
            
            IsBusy = true;
            // Strangely seems required to 
            // provoke the busy indicator 
            // to show.
            Thread.Sleep(1000);
        }

        public void Hide()
        {
            IsBusy = false;
        }

        public BusyIndicator(IScreenObject screenObject)
        {
            _busyIndicatorHost = (IBusyIndicatorHost) ScreenViewService.GetScreenView(screenObject);
        }

        private static VsExportProvisionScope GetScope()
        {
            var globalScope = new VsExportProvisionScope(GlobalGuid);
            return new VsExportProvisionOuterScope(LocalGuid, globalScope);
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
