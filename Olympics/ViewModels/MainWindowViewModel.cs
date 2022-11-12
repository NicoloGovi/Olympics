using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympics.Controllers;
using Olympics.Models;

namespace Olympics.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        //PROPRIETA CON BINDING
        
        private string _pagination;
        public string Pagination
        {
            get { return _pagination; }
            set { _pagination = value;
                NotifyPropertyChanged("Pagination");
            }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value;
                SetPagination();        
            }
        }

        private int _totalPage;
        public int TotalPage
        {
            get { return _totalPage; }
            set {
                _totalPage = value;
                SetPagination();
                }
        }

        private List<Partecipation> _datiPartecipation;
        public  List<Partecipation> DatiPartecipation
        {
            get { return _datiPartecipation; }
            set { _datiPartecipation = value;
                NotifyPropertyChanged("DatiPartecipation");
            }
        }

        private List<string> _genders;
        public List<string> Genders
        {
            get { return _genders; }
            set { _genders = value;
                NotifyPropertyChanged("Genders");
            }
        }

        private List<string> _games;
        public List<string> Games
        {
            get { return _games; }
            set { _games = value;
                NotifyPropertyChanged("Games");
            }
        }

        private List<string> _sports;
        public List<string> Sports
        {
            get { return _sports; }
            set
            {
                _sports = value;
                NotifyPropertyChanged("Sports");
            }
        }

        private List<string> _events;
        public List<string> Events
        {
            get { return _events; }
            set { _events = value;
                NotifyPropertyChanged("Events");
            }
        }

        private List<string> _medals;
        public List<string> Medals
        {
            get { return _medals; }
            set { _medals = value;
                NotifyPropertyChanged("Medals");
            }
        }




        private string _filtroGame;
        public string FiltroGame
        {
            get { return _filtroGame; }
            set { _filtroGame = value;
                NotifyPropertyChanged("FiltroGame");
                LoadFiltroSports();
                LoadDataPartecipations();
            }
        }

        private string _filtroSport;
        public string FiltroSport
        {
            get { return _filtroSport; }
            set { _filtroSport = value;
                NotifyPropertyChanged("FiltroSport");
                LoadFiltroEvents();
                LoadDataPartecipations();
            }

        }

        private string _filtroEvent;
        public string FiltroEvent
        {
            get { return _filtroEvent; }
            set { _filtroEvent = value;
                NotifyPropertyChanged("FiltroEvent");
                LoadDataPartecipations();
            }
        }

        private string _filtroMedal;
        public string FiltroMedal
        {
            get { return _filtroMedal; }
            set { _filtroMedal = value;
                NotifyPropertyChanged("FiltroMedal");
                LoadDataPartecipations();
            }
        }

        private string _filtroName;
        public string FiltroName
        {
            get { return _filtroName; }
            set { _filtroName = value;
                NotifyPropertyChanged("FiltroName");
                LoadDataPartecipations();
            }
        }

        private string _filtroSex;
        public string FiltroSex
        {
            get { return _filtroSex; }
            set { _filtroSex = value;
                NotifyPropertyChanged("FiltroSex");
                LoadDataPartecipations();
            }
        }












        //COSTRUTTORE

        public MainWindowViewModel()
        {
            SetPagination();
            this.Genders = Partecipations.getGenders();
            this.Games = Partecipations.getGames();
            this.Medals = Partecipations.getMedals();
            this.DatiPartecipation = Partecipations.getAll("", "", "", "", "", "");
            
        }


        //METODI

        public void LoadFiltroSports()
        {
            if (this.FiltroGame != null)
                this.Sports = Partecipations.getSports(this.FiltroGame);
            this.FiltroEvent = null;
            this.FiltroSport = null;
        }

        public void LoadFiltroEvents()
        {
            if (this.FiltroSport != null)
                this.Events = Partecipations.getEvents(this.FiltroGame, this.FiltroSport);
            this.FiltroEvent = null;
        }

        public void LoadDataPartecipations()
        {
            this.DatiPartecipation = Partecipations.getAll(FiltroName, FiltroSex, FiltroGame, FiltroSport, FiltroEvent, FiltroMedal);
        }

        public void SetPagination()
        {
            this.Pagination = $"pagina {this.CurrentPage} di {this.TotalPage}";
        }


    }
}
