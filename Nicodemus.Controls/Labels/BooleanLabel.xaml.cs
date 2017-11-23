using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nicodemus.Controls.Labels
{
    public partial class BooleanLabel : UserControl
    {
        public static readonly DependencyProperty CustomForegroundProperty =
            DependencyProperty.Register(nameof(CustomForeground), typeof(Brush), typeof(BooleanLabel), new PropertyMetadata(null));

        public Brush CustomForeground
        {
            get { return (Brush)GetValue(CustomForegroundProperty); }
            set { SetValue(CustomForegroundProperty, value); }
        }

        public static readonly DependencyProperty CustomBackgroundProperty =
            DependencyProperty.Register(nameof(CustomBackground), typeof(Brush), typeof(BooleanLabel), new PropertyMetadata(null));

        public Brush CustomBackground
        {
            get { return (Brush)GetValue(CustomBackgroundProperty); }
            set { SetValue(CustomBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CustomFontWeightProperty =
            DependencyProperty.Register(nameof(CustomFontWeight), typeof(FontWeight), typeof(BooleanLabel), new PropertyMetadata(default(FontWeight)));

        public FontWeight CustomFontWeight
        {
            get { return (FontWeight)GetValue(CustomFontWeightProperty); }
            set { SetValue(CustomFontWeightProperty, value); }
        }

        public BooleanLabel()
        {
            InitializeComponent();
        }

        public void ChangeToCustomStyle(
            SolidColorBrush foreground = null,
            SolidColorBrush background = null,
            FontWeight fontWeight = default(FontWeight)
            )
        {
            CustomForeground = foreground;
            CustomBackground = background;
            CustomFontWeight = fontWeight;
        }

        public void ChangeToSuccessStyle()
        {
            ChangeToCustomStyle(
                foreground: new SolidColorBrush(Colors.White),
                background: new SolidColorBrush(Colors.Green),
                fontWeight: FontWeights.Bold
            );
        }

        public void ChangeToWarningStyle()
        {
            ChangeToCustomStyle(
                foreground: new SolidColorBrush(Colors.Black),
                background: new SolidColorBrush(Colors.Yellow),
                fontWeight: FontWeights.Bold
            );
        }

        public void ChangeToErrorStyle()
        {
            ChangeToCustomStyle(
                foreground: new SolidColorBrush(Colors.White),
                background: new SolidColorBrush(Colors.Red),
                fontWeight: FontWeights.Bold
            );
        }
    }
}
