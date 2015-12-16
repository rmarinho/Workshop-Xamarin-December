using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HousesDemo
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IGpsService gps, HousePropertyService service, INavigation navigation)
        {
            this.Navigation = navigation;
            this.GpsService = gps;
            this.HousePropertyService = service;
        }

        /// <summary>
        /// Contains the navigation object from Xamarin.Forms
        /// </summary>
        public INavigation Navigation { get; set; }
        

        private IGpsService GpsService { get; set; }

        public HousePropertyService HousePropertyService { get; set; }

		public static string HousePropertiesPropertyName = "HouseProperties";

        private ObservableCollection<HouseProperty> _houseProperties;

        public ObservableCollection<HouseProperty> HouseProperties
        {
            get
            {
                return _houseProperties;
            }
            set
            {
                Set(() => HouseProperties, ref _houseProperties, value);
            }
        }

        private RelayCommand _LoadHousePropertiesCommand;
        public RelayCommand LoadHousePropertiesCommand
        {
            get
            {
                return _LoadHousePropertiesCommand
                    ?? (_LoadHousePropertiesCommand = new RelayCommand(
                                          async () =>
                                          {
                                              var items = await HousePropertyService.Load();
                                              this.HouseProperties = new ObservableCollection<HouseProperty>(items);
                                          }));
            }
        }

        private RelayCommand _CurrentLocationCommand;

        /// <summary>
        /// Gets the CurrentLocationCommand.
        /// </summary>
        public RelayCommand CurrentLocationCommand
        {
            get
            {
                return _CurrentLocationCommand
                    ?? (_CurrentLocationCommand = new RelayCommand(
                                            async () =>
                                            {
                                                this.CurrentLocation = await GpsService.GetLocation();
                                            }));
            }
        }

        /// <summary>
        /// The <see cref="CurrentLocation" /> property's name.
        /// </summary>
        public const string CurrentLocationPropertyName = "CurrentLocation";
        private Location _CurrentLocation;

        /// <summary>
        /// Sets and gets the CurrentLocation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Location CurrentLocation
        {
            get
            {
                return _CurrentLocation;
            }
            set
            {
                Set(() => CurrentLocation, ref _CurrentLocation, value);
            }
        }
        
        private RelayCommand<int> _HeritagePropertySelectedCommand;

        /// <summary>
        /// Gets the HeritagePropertySelectedCommand.
        /// </summary>
        public RelayCommand<int> HeritagePropertySelectedCommand
        {
            get
            {
                return _HeritagePropertySelectedCommand
                    ?? (_HeritagePropertySelectedCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              ServiceLocator.Current.GetInstance<DetailsViewModel>().SelectedItem = this.HouseProperties[p];
                                          }));
            }
        }

        private RelayCommand<HouseProperty> _HousesPropertyItemSelectedCommand;

        /// <summary>
        /// Gets the HeritagePropertyItemSelectedCommand.
        /// </summary>
        public RelayCommand<HouseProperty> HeritagePropertyItemSelectedCommand
        {
            get
            {
                return _HousesPropertyItemSelectedCommand
                    ?? (_HousesPropertyItemSelectedCommand = new RelayCommand<HouseProperty>(
                                          p =>
                                          {
                                              ServiceLocator.Current.GetInstance<DetailsViewModel>().SelectedItem = p;
                                              Navigation.PushAsync(new DetailsPage());
                                          }));
            }
        }

	
	}
}
