using System;
using System.ComponentModel;
using StaTobashi.Models;

namespace StaTobashi.ViewModels
{
    public class ConfigViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _launchIntervalSeconds;
        public int LaunchIntervalSeconds
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
                }
            }
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

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "LaunchIntervalSeconds":
                        if (this.LaunchIntervalSeconds < Config.IntervalRangeMin ||
                            Config.IntervalRangeMax < this.LaunchIntervalSeconds)
                        {
                            return Config.IntervalRangeMin + " から " + Config.IntervalRangeMax + " までの整数を入力してください";
                        }
                        break;
                    case "LaunchDurationMillisecondsMin":
                        if (this.LaunchDurationMillisecondsMin < Config.DurationRangeMin ||
                            Config.DurationRangeMax < this.LaunchDurationMillisecondsMin)
                        {
                            return Config.DurationRangeMin + " から " + Config.DurationRangeMax + " までの整数を入力してください";
                        }
                        else if (this.LaunchDurationMillisecondsMax < this.LaunchDurationMillisecondsMin)
                        {
                            return "最速 ≦ 最遅 にしてください";
                        }
                        break;
                    case "LaunchDurationMillisecondsMax":
                        if (this.LaunchDurationMillisecondsMax < Config.DurationRangeMin ||
                            Config.DurationRangeMax < this.LaunchDurationMillisecondsMax)
                        {
                            return Config.DurationRangeMin + " から " + Config.DurationRangeMax + " までの整数を入力してください";
                        }
                        else if (this.LaunchDurationMillisecondsMax < this.LaunchDurationMillisecondsMin)
                        {
                            return "最速 ≦ 最遅 にしてください";
                        }
                        break;
                    case "Scale":
                        if (this.Scale < Config.ScaleRangeMin ||
                            Config.ScaleRangeMax < this.Scale)
                        {
                            return Config.ScaleRangeMin + " から " + Config.ScaleRangeMax + " までの数値を入力してください";
                        }
                        break;
                }

                return null;
            }
        }
    }
}
