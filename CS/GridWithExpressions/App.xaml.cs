using DevExpress.Xpf.Core;
using System.Windows;

namespace GridWithExpressions {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            ThemeManager.ApplicationThemeName = "MetropolisDark";
        }
    }
}
