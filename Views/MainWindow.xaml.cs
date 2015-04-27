using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace StaTobashi.Views
{
    public partial class MainWindow : Window
    {
        private NotifyIcon notifyIcon;

        private ScreenWindow screenWindow;
        private ConfigWindow configWindow;
        private AreaConfigWindow areaConfigWindow;

        public MainWindow()
        {
            InitializeComponent();

            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "すたとばし";
            notifyIcon.Icon = Properties.Resources.ApplicationIcon;
            notifyIcon.Visible = true;

            var menuStrip = new ContextMenuStrip();

            var configItem = new ToolStripMenuItem();
            configItem.Text = "すた設定";
            configItem.Click += new EventHandler(configItem_Click);
            menuStrip.Items.Add(configItem);

            var areaConfigItem = new ToolStripMenuItem();
            areaConfigItem.Text = "範囲設定";
            areaConfigItem.Click += new EventHandler(areaConfigItem_Click);
            menuStrip.Items.Add(areaConfigItem);

            menuStrip.Items.Add(new ToolStripSeparator());

            var staItem = new ToolStripMenuItem();
            staItem.Text = "すた";
            staItem.Click += new EventHandler(staItem_Click);
            menuStrip.Items.Add(staItem);

            menuStrip.Items.Add(new ToolStripSeparator());

            var exitItem = new ToolStripMenuItem();
            exitItem.Text = "終了";
            exitItem.Click += new EventHandler(exitItem_Click);
            menuStrip.Items.Add(exitItem);

            notifyIcon.ContextMenuStrip = menuStrip;

            screenWindow = new ScreenWindow();
            screenWindow.Show();
        }

        private void configItem_Click(object sender, EventArgs e)
        {
            DisableTaskTrayMenu();

            configWindow = new ConfigWindow();
            configWindow.ShowDialog();

            screenWindow.SetIntervalConfig();

            EnableTaskTrayMenu();
        }

        private void areaConfigItem_Click(object sender, EventArgs e)
        {
            DisableTaskTrayMenu();

            areaConfigWindow = new AreaConfigWindow();
            areaConfigWindow.ShowDialog();

            screenWindow.SetAreaConfig();

            EnableTaskTrayMenu();
        }

        private void staItem_Click(object sender, EventArgs e)
        {
            screenWindow.Launch();
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            try
            {
                notifyIcon.Dispose();
                System.Windows.Application.Current.Shutdown();
            }
            catch
            {
            }
        }

        private void EnableTaskTrayMenu()
        {
            foreach (object item in notifyIcon.ContextMenuStrip.Items)
            {
                if (!(item is ToolStripMenuItem)) continue;
                if (((ToolStripMenuItem)item).Text == "終了") continue;

                ((ToolStripMenuItem)item).Enabled = true;
            }
        }

        private void DisableTaskTrayMenu()
        {
            foreach (object item in notifyIcon.ContextMenuStrip.Items)
            {
                if (!(item is ToolStripMenuItem)) continue;
                if (((ToolStripMenuItem)item).Text == "終了") continue;

                ((ToolStripMenuItem)item).Enabled = false;
            }
        }
    }
}
