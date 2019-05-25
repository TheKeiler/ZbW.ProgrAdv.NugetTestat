using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ZbW.ProgrAdv.NugetTestat.Model;
using ZbW.ProgrAdv.NugetTestat.Persistence;

namespace ZbW.ProgrAdv.NugetTestat.ViewModel
{
    public class NugetTestatViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ConnectionString { get; set; }

        private ICommand _laden;
        private ICommand _confirm;
        private ICommand _logMessageAdd;
        private ICommand _findDuplicates;

        public ObservableCollection<LogEntry> LogEntriesList { get; set; }

        public LogEntry SelectedEntry { get; set; }
        public LogEntry NewEntry { get; set; }

        public NugetTestatViewModel()
        {
            this.ConnectionString= "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = ...";
            this.LogEntriesList = new ObservableCollection<LogEntry>();
            this.NewEntry = new LogEntry();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand Laden
        {
            get
            {
                if (_laden == null)
                {
                    _laden = new RelayCommand(
                        param => this.LoadFilesFromDB()
                    );
                }
                return _laden;
            }
        }

        public ICommand Confirm
        {
            get
            {
                if (_confirm == null)
                {
                    _confirm = new RelayCommand(
                        param => this.ExecuteLogClear()
                    );
                }
                return _confirm;
            }
        }

        public ICommand Add
        {
            get
            {
                if (_logMessageAdd == null)
                {
                    _logMessageAdd = new RelayCommand(
                        param => this.ExecuteLogMessageAdd()
                    );
                }
                return _logMessageAdd;
            }
        }

        public ICommand FindDuplicates
        {
            get
            {
                if (_findDuplicates == null)
                {
                    _findDuplicates = new RelayCommand(
                        param => this.RunDuplicatesChecker()
                    );
                }
                return _findDuplicates;
            }
        }

        private void LoadFilesFromDB()
        {
            if (this.ConnectionString == null && this.ConnectionString.Equals(""))
            {
                MessageBox.Show("Bitte Connectionstring eingeben!");
            }

            else
            {
                var dataBaseConnector = new DataBaseConnector(ConnectionString);
                this.LogEntriesList = dataBaseConnector.Read();
                if (LogEntriesList.Count > 0)
                {
                    this.SelectedEntry = this.LogEntriesList[0];
                }

                PropertyChanged(this, new PropertyChangedEventArgs("LogEntriesList"));
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedEntry"));
            }

        }

        private void ExecuteLogClear()
        {
            if (this.ConnectionString == null && this.ConnectionString.Equals(""))
            {
                MessageBox.Show("Bitte Connectionstring eingeben!");
            }
            else
            {
                var dataBaseConnector = new DataBaseConnector(ConnectionString);
                dataBaseConnector.LogClear(SelectedEntry);
                LogEntriesList = dataBaseConnector.Read();
                PropertyChanged(this, new PropertyChangedEventArgs("LogEntriesList"));
            }

        }

        private void ExecuteLogMessageAdd()
        {
            if (this.ConnectionString == null && this.ConnectionString.Equals(""))
            {
                MessageBox.Show("Bitte Connectionstring eingeben!");
            }
            else
            {
                if (NewEntry.Pod == null || NewEntry.Hostname == null || NewEntry.Severity < 0 ||
                    NewEntry.Message == null)
                {
                    MessageBox.Show("Bitte alle Felder abfüllen!");
                }
                else
                {
                    var dataBaseConnector = new DataBaseConnector(this.ConnectionString);
                    dataBaseConnector.LogMessageAdd(this.NewEntry);
                    this.LogEntriesList = dataBaseConnector.Read();
                }
                PropertyChanged(this, new PropertyChangedEventArgs("LogEntriesList"));
            }
        }

        private void RunDuplicatesChecker()
        {
            var dataBaseConnector = new DataBaseConnector(this.ConnectionString);
            this.LogEntriesList = dataBaseConnector.Read();
            var dubChecker = new DuplicateChecker();
            var dubList = dubChecker.FindDuplicates(this.LogEntriesList);

            if (dubList.Any())
            {
                for (int i = 0; i < dubList.Count(); i++)
                {
                    var log = (LogEntry)dubList.ElementAt(i);
                    for (int j = 0; j < LogEntriesList.Count; j++)
                    {
                        if (LogEntriesList.ElementAt(j).Id == log.Id)
                        {
                            LogEntriesList.ElementAt(j).IsDuplicate = true;
                        }
                    }
                }
            }
            PropertyChanged(this, new PropertyChangedEventArgs("LogEntriesList"));
        }
    }

}
