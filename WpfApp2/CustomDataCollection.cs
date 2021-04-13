using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DataLibrary;

namespace WpfApp2
{
    class CustomDataCollection: IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public V4MainCollection V4Item { get; set; }
        private double _minValue;
        public double minValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                OnPropertyChanged("minValue");
                OnPropertyChanged("maxValue");
            }
        }

        private double _maxValue;
        public double maxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                OnPropertyChanged("maxValue");
                OnPropertyChanged("minValue");
            }
        }

        private int _num;
        public int num
        {
            get { return _num; }
            set
            {
                _num = value;
                OnPropertyChanged("num");
            }
        }

        private string _info;
        public string info
        {
            get { return _info; }
            set
            {
                _info = value;
                OnPropertyChanged("info");
            }
        }


        public CustomDataCollection(V4MainCollection V4Item)
        {
            this.V4Item = V4Item;
        }

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void AddCustom()
        {
            float freq = 2;
            V4DataCollection item = new V4DataCollection(info, freq);
            item.InitRandom(num, 5, 5, minValue, maxValue);
            V4Item.Add(item);
            OnPropertyChanged("info");
        }

        public string Error
        {
            get { return "Error text"; }
        }

        public string this[string property]
        {
            get
            {
                string message = null;
                switch(property)
                {
                    case "info":
                        if (info == null || info.Length == 0 || V4Item.Contains(info))
                            message = "Main Collection contains element with the same information";
                        break;
                    case "num":
                        if (num < 1 || num > 5)
                            message = "Number of elements should be >=1 and <=5";
                        break;
                    case "minValue":
                    case "maxValue":
                        if (maxValue <= minValue)
                            message = "maxValue should be greater than minValue";
                        break;
                    default:
                        break;
                }
                return message;
            }
        }

    }
}
