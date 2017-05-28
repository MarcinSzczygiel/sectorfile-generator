using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Coordinates;
using SectorfileObjects;

namespace SectorfileGeneratorUI
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        string input, converted;
        public string InputCoordinates
        {
            get { return input; }
            set
            {
                input = value;
                OnPropertyChanged("InputCoordinates");
            }
        }

        public string ConvertedCoordinates
        {
            get { return converted; }
            set
            {
                converted = value;
                OnPropertyChanged("ConvertedCoordinates");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand ToEse { get { return new RelayCommand(ToEseExcute, CanToEseExcute); } }
        public ICommand ToSct { get { return new RelayCommand(ToSctExcute); } }

        private void ToEseExcute()
        {
            Polygon poly = new Polygon();
            foreach (string line in input.Split('\n'))
            {
                if (line == string.Empty) continue;
                GeoPoint point;
                if (GeoPoint.TryParse(line, out point))
                {
                    poly.Add(point);
                }
                else if (line.StartsWith(";")) poly.Comment = line.Substring(1);
                else poly.Title = line;
            }
            ConvertedCoordinates = poly.ToEse();
        }
        private void ToSctExcute()
        {
            Polygon poly = new Polygon();
            foreach (string line in input.Split('\n'))
            {
                if (line == string.Empty) continue;
                GeoPoint point;
                if (GeoPoint.TryParse(line, out point))
                {
                    poly.Add(point);
                }
                else if (line.StartsWith(";")) poly.Comment = line.Substring(1);
                else poly.Title = line;
            }
            ConvertedCoordinates = poly.ToSct();
        }

        private bool CanToEseExcute()
        {
            return true;
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Func<Boolean> _canExecute;
        private readonly Action _execute;

        public RelayCommand(Action execute)
          : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<Boolean> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(Object parameter)
        {
            _execute();
        }
    }
}
