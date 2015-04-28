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
    }
}
