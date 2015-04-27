using System.Windows;
using System.Windows.Controls;
using StaTobashi.Models;
using StaTobashi.ViewModels;

namespace StaTobashi.Views
{
    public partial class ConfigWindow : Window
    {
        private ConfigViewModel configViewModel;

        public ConfigWindow()
        {
            InitializeComponent();

            configViewModel = new ConfigViewModel();

            this.DataContext = configViewModel;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValid(this))
            {
                MessageBox.Show(this, "設定にエラーがあります", "確認", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Config.Current.LaunchIntervalSeconds = configViewModel.LaunchIntervalSeconds;
            Config.Current.LaunchDurationMillisecondsMin = configViewModel.LaunchDurationMillisecondsMin;
            Config.Current.LaunchDurationMillisecondsMax = configViewModel.LaunchDurationMillisecondsMax;
            Config.Current.Scale = configViewModel.Scale;

            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsValid(DependencyObject node)
        {
            if (node != null && Validation.GetHasError(node)) return false;

            foreach (object subnode in LogicalTreeHelper.GetChildren(node))
            {
                if (subnode is DependencyObject && !IsValid((DependencyObject)subnode)) return false;
            }

            return true;
        }
    }
}
