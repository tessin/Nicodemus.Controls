using com.bodurov.SilverlightControls.XmlCodeEditor;
using Microsoft.LightSwitch.Presentation.Implementation;
using Nicodemus.Controls.Common;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace Nicodemus.Controls.Editors
{
    public class XmlTextEditor : UserContentItemControl
    {
        private readonly RoutedEventHandler _lostFocus;
        private readonly XmlCodeEditorBox _editor;

        public bool IsXmlValidationEnabled { get; set; }
        public event Action<XmlException> XmlValidationError;

        public XmlTextEditor()
        {
            _editor = new XmlCodeEditorBox();
            _lostFocus = new RoutedEventHandler(LostFocusHandler);
            Content = _editor;
        }

        private void LostFocusHandler(object sender, RoutedEventArgs e)
        {
            if (ContentItem != null)
            {
                var text = _editor.Text;

                // validate XML, let the exception propagate
                if (IsXmlValidationEnabled)
                {
                    try
                    {
                        XDocument.Load(new StringReader(text), LoadOptions.SetLineInfo);
                    }
                    catch (XmlException ex)
                    {
                        var xmlValidationError = XmlValidationError;
                        if (xmlValidationError != null)
                        {
                            xmlValidationError(ex);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                var text2 = NormalizeLineEndings(ContentItem.Value as string);
                if (text != text2) // update only if changed
                {
                    ContentItem.Value = text;
                }
            }
        }

        protected override void OnLoaded()
        {
            _editor.LostFocus += _lostFocus;
        }

        protected override void OnContentItemSubscribed(ContentItem contentItem)
        {
            SetEditorText(contentItem.Value as string);
        }

        protected override void OnContentItemValueChanged(ContentItem contentItem, object value)
        {
            SetEditorText(contentItem.Value as string);
        }

        protected override void OnContentItemUnsubscribed(ContentItem contentItem)
        {
            SetEditorText(null);
        }

        private void SetEditorText(string text)
        {
            // for some reason the text is stored with CR only line endings
            // whatever the case is we fix that here
            string text2 = NormalizeLineEndings(text);
            _editor.Text = text2;
        }

        protected override void OnUnloaded()
        {
            _editor.LostFocus -= _lostFocus;
        }

        private static string NormalizeLineEndings(string text)
        {
            // todo: if no line ending normalization is necessary we don't need to rebuild the string
            //       it is much cheaper just to check that than to actually do the conversion

            var sb = new StringBuilder();

            var buf = (text ?? string.Empty).ToCharArray();
            for (int i = 0, len = buf.Length; i < len;)
            {
                var ch = buf[i];
                switch (ch)
                {
                    case '\r':
                        if (i + 1 < len && buf[i + 1] == '\n')
                        {
                            i++;
                        }
                        sb.Append('\r');
                        sb.Append('\n');
                        i++;
                        break;
                    case '\n':
                        sb.Append('\r');
                        sb.Append('\n');
                        i++;
                        break;
                    default:
                        sb.Append(ch);
                        i++;
                        break;
                }
            }

            var text2 = sb.ToString();
            return text2;
        }
    }
}
