using System.Windows;
using System.Windows.Media;

namespace musicvmcompiler.Model
{
    public class InstructionModel : DependencyObject
    {
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof (Brush), typeof (InstructionModel),
                new PropertyMetadata(default(Brush)));

        public Brush Foreground
        {
            get { return (Brush) GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (InstructionModel),
                new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}