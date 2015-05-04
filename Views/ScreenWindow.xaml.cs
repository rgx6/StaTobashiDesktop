using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using StaTobashi.Models;

namespace StaTobashi.Views
{
    public class StaImage : Image
    {
        public bool IsClicked { get; set; }

        public StaImage()
            : base()
        {
            this.IsClicked = false;
        }
    }

    public partial class ScreenWindow : Window
    {
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int VK_F4 = 0x73;

        DispatcherTimer dispatcherTimer;
        ImageSelector imageSelector;
        Random random;

        public ScreenWindow()
        {
            InitializeComponent();

            imageSelector = new ImageSelector();

            random = new Random();

            SetAreaConfig();
            SetTopmost();

            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
            SetIntervalConfig();
            dispatcherTimer.Tick += delegate(object sender, EventArgs e)
            {
                Launch();
            };
            dispatcherTimer.Start();

            Launch();

            Loaded += delegate(object sender, RoutedEventArgs e)
            {
                var source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
                source.AddHook(new HwndSourceHook(WndProc));
            };
        }

        public void SetAreaConfig()
        {
            this.Left = Config.Current.Left;
            this.Top = Config.Current.Top;
            this.Width = Config.Current.Width;
            this.Height = Config.Current.Height;
        }

        public void SetTopmost()
        {
            this.Topmost = Config.Current.Topmost;
        }

        public void SetIntervalConfig()
        {
            if (dispatcherTimer == null) throw new InvalidOperationException();

            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(Config.Current.LaunchIntervalSeconds * 1000));
        }

        public void Launch()
        {
            var id = Guid.NewGuid().ToString("N");

            var name = "sta_" + id;

            var starchoo = new StaImage();
            starchoo.Name = name;
            starchoo.Source = GetRandomImage();
            starchoo.Width = starchoo.Source.Width * Config.Current.Scale;

            this.RegisterName(starchoo.Name, starchoo);

            var staWidth = starchoo.Source.Width * Config.Current.Scale;
            var staHeight = starchoo.Source.Height * Config.Current.Scale;

            var staLeft = this.Width;
            var staTop = GetRandomTop(staHeight);
            var staDuration = GetRandomDuration();
            Canvas.SetLeft(starchoo, staLeft);
            Canvas.SetTop(starchoo, staTop);
            Canvas.SetZIndex(starchoo, -(int)staDuration);

            var anime = new DoubleAnimation();
            anime.From = staLeft;
            anime.To = -staWidth;
            anime.Duration = new Duration(TimeSpan.FromSeconds(staDuration));

            var sb = new Storyboard();
            sb.Children.Add(anime);
            Storyboard.SetTargetName(anime, starchoo.Name);
            Storyboard.SetTargetProperty(anime, new PropertyPath("(Canvas.Left)"));

            canvas.Children.Add(starchoo);

            anime.Completed += delegate(object sender, EventArgs e)
            {
                canvas.Children.Remove(starchoo);
            };

            starchoo.Loaded += delegate(object sender, RoutedEventArgs e)
            {
                sb.Begin(this, true);
            };

            starchoo.MouseDown += delegate(object sender, MouseButtonEventArgs e)
            {
                if (starchoo.IsClicked) return;

                starchoo.IsClicked = true;
                Launch();
            };
        }

        private BitmapImage GetRandomImage()
        {
            return imageSelector.GetRandomImage();
        }

        private int GetRandomTop(double staHeight)
        {
            return this.Height <= staHeight ? 0 : random.Next(0, (int)this.Height - (int)staHeight);
        }

        private double GetRandomDuration()
        {
            var rBase = Config.Current.LaunchDurationSecondsMin;
            var rRange = Config.Current.LaunchDurationSecondsMax - Config.Current.LaunchDurationSecondsMin;

            return rBase + random.NextDouble() * rRange;
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if ((msg == WM_SYSKEYDOWN) &&
                (wParam.ToInt32() == VK_F4))
            {
                handled = true;
            }

            return IntPtr.Zero;
        }
    }
}
