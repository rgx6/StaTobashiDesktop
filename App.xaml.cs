using System.Windows;
using StaTobashi.Models;

namespace StaTobashi
{
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Config.Load();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Config.Current.Save();
        }
    }
}
