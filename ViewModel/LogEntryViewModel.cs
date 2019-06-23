using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ZbW.ProgrAdv.NugetTestat.Model;
using ZbW.ProgrAdv.NugetTestat.Persistence;

namespace ZbW.ProgrAdv.NugetTestat.ViewModel
{
    public class LogEntryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ConnectionString { get; set; }

        private ICommand _laden;
        private ICommand _confirm;
        private ICommand _logMessageAdd;
        private ICommand _findDuplicates;

        public IQueryable<LogEntry> LogEntriesList { get; set; }

        public LogEntry SelectedEntry { get; set; }
        public LogEntry NewEntry { get; set; }

        public LogEntryViewModel()
        {
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = ...";
            this.LogEntriesList = Enumerable.Empty<LogEntry>().AsQueryable();
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
                var logentryRepository = new LogEntryRepository(ConnectionString);
                this.LogEntriesList = logentryRepository.GetAll();
                if (LogEntriesList.Any())
                {
                    this.SelectedEntry = this.LogEntriesList.First();
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
                var logentryRepository = new LogEntryRepository(ConnectionString);
                logentryRepository.ExecuteLogClear(SelectedEntry);
                LogEntriesList = logentryRepository.GetAll();
                if (LogEntriesList.Any())
                {
                    this.SelectedEntry = this.LogEntriesList.First();
                }
                PropertyChanged(this, new PropertyChangedEventArgs("LogEntriesList"));
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedEntry"));
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
                    var logentryRepository = new LogEntryRepository(this.ConnectionString);
                    logentryRepository.ExecuteLogMessageAdd(this.NewEntry);
                    this.LogEntriesList = logentryRepository.GetAll();
                    PropertyChanged(this, new PropertyChangedEventArgs("LogEntriesList"));
                }
            }
        }

        private void RunDuplicatesChecker()
        {
            var logentryRepository = new LogEntryRepository(this.ConnectionString);
            this.LogEntriesList = logentryRepository.GetAll();
            var dubChecker = new DuplicateChecker();
            var dubList = dubChecker.FindDuplicates(this.LogEntriesList);

            if (dubList.Any())
            {
                for (int i = 0; i < dubList.Count(); i++)
                {
                    var log = (LogEntry)dubList.ElementAt(i);
                    for (int j = 0; j < LogEntriesList.Count(); j++)
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
