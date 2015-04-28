using System;
using System.ComponentModel;
using StaTobashi.Models;

namespace StaTobashi.ViewModels
{
    public class AreaConfigViewModel : INotifyPropertyChanged
    {
        private double _left;
        public double Left
        {
            get { return _left; }
            set
            {
                if (_left != value)
                {
                    _left = value;
                    NotifyPropertyChanged("Left");
                }
            }
        }

        private double _top;
        public double Top
        {
            get { return _top; }
            set
            {
                if (_top != value)
                {
                    _top = value;
                    NotifyPropertyChanged("Top");
                }
            }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    NotifyPropertyChanged("Width");
                }
            }
        }

        public double WidthMin
        {
            get { return Config.AreaWidthMin; }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    NotifyPropertyChanged("Height");
                }
            }
        }

        public double HeightMin
        {
            get { return Config.AreaHeightMin; }
        }

        public bool Topmost { get; set; }

        public AreaConfigViewModel()
        {
            this.Left = Config.Current.Left;
            this.Top = Config.Current.Top;
            this.Width = Config.Current.Width;
            this.Height = Config.Current.Height;
            this.Topmost = Config.Current.Topmost;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
