using System;
using System.Windows;

namespace musicvmcompiler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        [STAThread]
        [LoaderOptimization(LoaderOptimization.SingleDomain)]
        public static void Main()
        {
			var app = new App();
			app.Run();
		}
		
        protected override void OnStartup(StartupEventArgs e) {
			var mainWindow = new MainWindow();
			mainWindow.Show();
		}
	}
}
