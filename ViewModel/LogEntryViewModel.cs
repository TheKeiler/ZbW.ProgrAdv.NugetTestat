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

        public List<LogEntry> LogEntriesList { get; set; }

        public LogEntry SelectedEntry { get; set; }
        public LogEntry NewEntry { get; set; }

        public LogEntryViewModel()
        {
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = ...";
            this.LogEntriesList = new List<LogEntry>();
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
                var logentryRepository = new LogEntryRepository();
                this.LogEntriesList = logentryRepository.GetAll().ToList<LogEntry>();
                if (LogEntriesList.Any())
                {
                    this.SelectedEntry = this.LogEntriesList.First();
                }
                OnPropertyChanged("LogEntriesList");
                OnPropertyChanged("SelectedEntry");
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
                var logentryRepository = new LogEntryRepository();
                logentryRepository.ExecuteLogClear(SelectedEntry);
                LogEntriesList = logentryRepository.GetAll().ToList<LogEntry>();
                if (LogEntriesList.Any())
                {
                    this.SelectedEntry = this.LogEntriesList.First();
                }
                OnPropertyChanged("LogEntriesList");
                OnPropertyChanged("SelectedEntry");
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
                    var logentryRepository = new LogEntryRepository();
                    logentryRepository.ExecuteLogMessageAdd(this.NewEntry);
                    this.LogEntriesList = logentryRepository.GetAll().ToList<LogEntry>();
                    OnPropertyChanged("LogEntriesList");
                }
            }
        }

        private void RunDuplicatesChecker()
        {
            var dubChecker = new DuplicateChecker();
            var logRepo = new LogEntryRepository();
            var logList = logRepo.GetAll().ToList<LogEntry>();
            var dubList = dubChecker.FindDuplicates(logList);

            if (dubList.Any())
            {
                for (int i = 0; i < dubList.Count(); i++)
                {
                    var log = (LogEntry)dubList.ElementAt(i);
                    for (int j = 0; j < logList.Count(); j++)
                    {
                        if (logList.ElementAt(j).Id == log.Id)
                        {
                            logList.ElementAt(j).IsDuplicate = true;
                        }
                    }
                }
            }
            this.LogEntriesList = logList;
            OnPropertyChanged("LogEntriesList");
        }
    }

}
