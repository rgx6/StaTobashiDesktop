using System;
using System.ComponentModel;
using StaTobashi.Models;

namespace StaTobashi.ViewModels
{
    public class ConfigViewModel : INotifyPropertyChanged
    {
        private double _launchIntervalSeconds;
        public double LaunchIntervalSeconds
        {
            get { return _launchIntervalSeconds; }
            set
            {
                if (_launchIntervalSeconds != value)
                {
                    _launchIntervalSeconds = value;
                    NotifyPropertyChanged("LaunchIntervalSeconds");
                }
            }
        }

        public double IntervalRangeMin
        {
            get { return Config.IntervalRangeMin; }
        }

        public double IntervalRangeMax
        {
            get { return Config.IntervalRangeMax; }
        }

        public double IntervalStep
        {
            get { return Config.IntervalStep; }
        }

        private int _launchDurationMillisecondsMin;
        public int LaunchDurationMillisecondsMin
        {
            get { return _launchDurationMillisecondsMin; }
            set
            {
                if (_launchDurationMillisecondsMin != value)
                {
                    _launchDurationMillisecondsMin = value;
                    NotifyPropertyChanged("LaunchDurationMillisecondsMin");
                    NotifyPropertyChanged("LaunchDurationMillisecondsMax");

                    if (this.LaunchDurationMillisecondsMax < this.LaunchDurationMillisecondsMin)
                    {
                        this.LaunchDurationMillisecondsMax = this.LaunchDurationMillisecondsMin;
                    }
                }
            }
        }

        private int _launchDurationMillisecondsMax;
        public int LaunchDurationMillisecondsMax
        {
            get { return _launchDurationMillisecondsMax; }
            set
            {
                if (_launchDurationMillisecondsMax != value)
                {
                    _launchDurationMillisecondsMax = value;
                    NotifyPropertyChanged("LaunchDurationMillisecondsMin");
                    NotifyPropertyChanged("LaunchDurationMillisecondsMax");

                    if (this.LaunchDurationMillisecondsMax < this.LaunchDurationMillisecondsMin)
                    {
                        this.LaunchDurationMillisecondsMin = this.LaunchDurationMillisecondsMax;
                    }
                }
            }
        }

        public int DurationRangeMin
        {
            get { return Config.DurationRangeMin; }
        }

        public int DurationRangeMax
        {
            get { return Config.DurationRangeMax; }
        }

        public int DurationStep
        {
            get { return Config.DurationStep; }
        }

        private double _scale;
        public double Scale
        {
            get { return _scale; }
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    NotifyPropertyChanged("Scale");
                }
            }
        }

        public double ScaleRangeMin
        {
            get { return Config.ScaleRangeMin; }
        }

        public double ScaleRangeMax
        {
            get { return Config.ScaleRangeMax; }
        }

        public double ScaleStep
        {
            get { return Config.ScaleStep; }
        }

        public ConfigViewModel()
        {
            this.LaunchIntervalSeconds = Config.Current.LaunchIntervalSeconds;
            this.LaunchDurationMillisecondsMin = Config.Current.LaunchDurationMillisecondsMin;
            this.LaunchDurationMillisecondsMax = Config.Current.LaunchDurationMillisecondsMax;
            this.Scale = Config.Current.Scale;
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
