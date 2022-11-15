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

        private int _currentPage = 1;
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

        private int _dimensionPage;
        public int DimensionPage
        {
            get { return _dimensionPage; }
            set { _dimensionPage = value;
                NotifyPropertyChanged("DimensionPage");
                this.CurrentPage = 1;
                LoadDataPartecipations();

            }
        }

        private int[] _dimensionsPage;
        public int[] DimensionsPage
        {
            get { return _dimensionsPage; }
            set { _dimensionsPage = value;
                NotifyPropertyChanged("DimensionsPage");
            }
        }


        private string _filtroGame = null;
        public string FiltroGame
        {
            get { return _filtroGame; }
            set { _filtroGame = value;
                NotifyPropertyChanged("FiltroGame");
                this.CurrentPage = 1;
                LoadFiltroSports();
                LoadDataPartecipations();
            }
        }

        private string _filtroSport = null;
        public string FiltroSport
        {
            get { return _filtroSport; }
            set { _filtroSport = value;
                NotifyPropertyChanged("FiltroSport");
                this.CurrentPage = 1;
                LoadFiltroEvents();
                LoadDataPartecipations();
            }

        }

        private string _filtroEvent = null;
        public string FiltroEvent
        {
            get { return _filtroEvent; }
            set { _filtroEvent = value;
                NotifyPropertyChanged("FiltroEvent");
                this.CurrentPage = 1;
                LoadDataPartecipations();
            }
        }

        private string _filtroMedal = null;
        public string FiltroMedal
        {
            get { return _filtroMedal; }
            set { _filtroMedal = value;
                NotifyPropertyChanged("FiltroMedal");
                this.CurrentPage = 1;
                LoadDataPartecipations();
            }
        }

        private string _filtroName = null;
        public string FiltroName
        {
            get { return _filtroName; }
            set { _filtroName = value;
                NotifyPropertyChanged("FiltroName");
                this.CurrentPage = 1;
                LoadDataPartecipations();
            }
        }

        private string _filtroSex = null;
        public string FiltroSex
        {
            get { return _filtroSex; }
            set { _filtroSex = value;
                NotifyPropertyChanged("FiltroSex");
                this.CurrentPage = 1;
                LoadDataPartecipations();
            }
        }

        private bool _canGoAhead = true;

        public bool CanGoAhead
        {
            get { return _canGoAhead; }
            set { _canGoAhead = value;
                NotifyPropertyChanged("CanGoAhead"); 
            }
        }

        private bool _canGoBack = true;

        public bool CanGoBack
        {
            get { return _canGoBack; }
            set { _canGoBack = value;
                NotifyPropertyChanged("CanGoBack");
            }
        }




        //attributo per tenere traccia del count(command2) passato come ref al get all
        int paginetotali;









        //COSTRUTTORE

        public MainWindowViewModel()
        {
            SetPagination();
            this.Genders = Partecipations.getGenders();
            this.Games = Partecipations.getGames();
            this.Medals = Partecipations.getMedals();

            DimensionsPageLoad();

            this.DatiPartecipation = Partecipations.getAll(null, null, null, null, null, null, CurrentPage, DimensionPage,ref paginetotali);

            
            
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
            
            this.DatiPartecipation = Partecipations.getAll(FiltroName, FiltroSex, FiltroGame, FiltroSport, FiltroEvent, FiltroMedal, CurrentPage, DimensionPage,ref paginetotali);

            if (DimensionPage > paginetotali)
                this.TotalPage = (paginetotali / DimensionPage) + 1;
            else
                this.TotalPage = paginetotali / DimensionPage;

            if (CurrentPage == TotalPage && TotalPage != 1)
            {
                this.CanGoAhead = false;
                this.CanGoBack = true;
            }
            else if(CurrentPage == TotalPage && TotalPage == 1)
            {
                CanGoAhead = false;
                CanGoBack = false;
            }
            else if(CurrentPage == 1)
            {
                this.CanGoAhead = true;
                this.CanGoBack = false;
            }
            else
            {
                this.CanGoAhead = true;
                this.CanGoBack = true;
            }
                
        }

        public void SetPagination()
        {
            this.Pagination = $"pagina {this.CurrentPage} di {this.TotalPage}";
        }

        public void DimensionsPageLoad()
        {
            this.DimensionsPage = new int[]
            {
                10, 20, 30, 40, 50
            };
            this.DimensionPage = this.DimensionsPage[0];
        }

        internal void nextPage()
        {
            CurrentPage++;
            LoadDataPartecipations();
        }

        internal void previousPage()
        {
            CurrentPage--;
            LoadDataPartecipations();
        }

        internal void LastPage()
        {
            CurrentPage = TotalPage;
            LoadDataPartecipations();
        }

        internal void FirstPage()
        {
            CurrentPage = 1;
            LoadDataPartecipations();
        }

        internal void AzzeraFiltri()
        {
            this.FiltroName = null;
            this.FiltroSex = null;
            this.FiltroGame = null;
            this.FiltroEvent = null;
            this.FiltroSport = null;
            this.FiltroMedal = null;

            this.Sports = null;
            this.Events = null; ;

            

            LoadDataPartecipations();
        }


    }
}
