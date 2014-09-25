using System.Collections.Generic;
using System.Windows;

namespace musicvmcompiler.Model
{
    public class MainWindowModel : DependencyObject
    {
        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof (string), typeof (MainWindowModel),
                new PropertyMetadata(default(string)));

        public string Input
        {
            get { return (string) GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty OutputProperty =
            DependencyProperty.Register("Output", typeof (List<InstructionModel>), typeof (MainWindowModel),
                new PropertyMetadata(default(string)));

        public List<InstructionModel> Output
        {
            get { return (List<InstructionModel>) GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }

        public static readonly DependencyProperty FloatConstsProperty =
            DependencyProperty.Register("FloatConsts", typeof (string), typeof (MainWindowModel),
                new PropertyMetadata(default(string)));

        public string FloatConsts
        {
            get { return (string) GetValue(FloatConstsProperty); }
            set { SetValue(FloatConstsProperty, value); }
        }

        public static readonly DependencyProperty IntConstsProperty =
            DependencyProperty.Register("IntConsts", typeof (string), typeof (MainWindowModel),
                new PropertyMetadata(default(string)));

        public string IntConsts
        {
            get { return (string) GetValue(IntConstsProperty); }
            set { SetValue(IntConstsProperty, value); }
        }

        public static readonly DependencyProperty TickConstsProperty =
            DependencyProperty.Register("TickConsts", typeof (string), typeof (MainWindowModel),
                new PropertyMetadata(default(string)));

        public string TickConsts
        {
            get { return (string) GetValue(TickConstsProperty); }
            set { SetValue(TickConstsProperty, value); }
        }

        public static readonly DependencyProperty UnoptimizedBytesProperty =
            DependencyProperty.Register("UnoptimizedBytes", typeof (int?), typeof (MainWindowModel),
                new PropertyMetadata(default(int?)));

        public int? UnoptimizedBytes
        {
            get { return (int?) GetValue(UnoptimizedBytesProperty); }
            set { SetValue(UnoptimizedBytesProperty, value); }
        }

        public static readonly DependencyProperty CompileTimeProperty =
            DependencyProperty.Register("CompileTime", typeof (int), typeof (MainWindowModel),
                new PropertyMetadata(default(int)));

        public int CompileTime
        {
            get { return (int) GetValue(CompileTimeProperty); }
            set { SetValue(CompileTimeProperty, value); }
        }

        public static readonly DependencyProperty OptimizedBytesProperty =
            DependencyProperty.Register("OptimizedBytes", typeof (int?), typeof (MainWindowModel),
                new PropertyMetadata(default(int?)));

        public int? OptimizedBytes
        {
            get { return (int?) GetValue(OptimizedBytesProperty); }
            set { SetValue(OptimizedBytesProperty, value); }
        }

        public static readonly DependencyProperty ParameterSlotsProperty = DependencyProperty.Register(
            "ParameterSlots", typeof (string), typeof (MainWindowModel), new PropertyMetadata(default(string)));

        public string ParameterSlots
        {
            get { return (string) GetValue(ParameterSlotsProperty); }
            set { SetValue(ParameterSlotsProperty, value); }
        }

        public static readonly DependencyProperty OpcodesProperty = DependencyProperty.Register("Opcodes",
            typeof (string), typeof (MainWindowModel), new PropertyMetadata(default(string)));

        public string Opcodes
        {
            get { return (string) GetValue(OpcodesProperty); }
            set { SetValue(OpcodesProperty, value); }
        }

        public static readonly DependencyProperty StatisticsProperty =
            DependencyProperty.Register("BytecodeStatistics", typeof (List<StatisticsModel>), typeof (MainWindowModel),
                new PropertyMetadata(default(List<StatisticsModel>)));

        public List<StatisticsModel> BytecodeStatistics
        {
            get { return (List<StatisticsModel>) GetValue(StatisticsProperty); }
            set { SetValue(StatisticsProperty, value); }
        }
    }
}