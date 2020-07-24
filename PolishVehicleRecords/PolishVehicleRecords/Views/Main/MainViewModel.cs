using PolishVehicleRecords.Models.Entities;
using PolishVehicleRecords.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using LiveCharts;
using LiveCharts.Wpf;

using PolishVehicleRecords.Views.Commands;
using System.Windows.Input;
using PolishVehicleRecords.Models.Builders;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using LiveCharts.Helpers;

namespace PolishVehicleRecords.Views.Main
{
    public class MainViewModel : Base.BaseViewModel
    {
        private IDictionariesValues DictionariesSource;
        private ICarsData CarsData;

        public ICommand SearchCommand { get; set; }

        private ObservableCollection<Voivodeship> _voivodeships;
        public ObservableCollection<Voivodeship> Voivodeships
        {
            get => _voivodeships;
            set
            {
                _voivodeships = value;
                OnPropertyChanged();
            }
        }
        public List<CarType> CarTypes;

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection _fuelSeries;
        public SeriesCollection FuelSeries
        {
            get => _fuelSeries;
            set
            {
                _fuelSeries = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IDictionariesValues dictionaries, ICarsData cars)
        {
            DictionariesSource = dictionaries;
            CarsData = cars;
            Voivodeships = new ObservableCollection<Voivodeship>();
            FuelSeries = new SeriesCollection();

            GetDictionaries().ConfigureAwait(false);

            SearchCommand = new Command(async (object arg) => await SearchData());
        }

        private async Task GetDictionaries()
        {
            await GetVoivodeships();
        }

        private async Task GetVoivodeships()
        {
            await DictionariesSource.GetVoivodeships().ContinueWith(result =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var v in result.Result)
                        Voivodeships.Add(v);
                });                
            });
        }

        private async Task SearchData()
        {
            var cars = new List<Car>();
            var search = new CarsSearchBuilder()
                    .SetItemsLimit(500)
                    .SetVoivodeiships(
                        Voivodeships.Where(s => s.IsSelected == true).Select(v => v.Code).ToList<string>());
            var tasks = new List<Task<List<Car>>>();

            var startDate = StartDate;

            while (startDate.AddDays(1).AddMonths(2) < EndDate)
            {
                var endDate = startDate.AddMonths(1);
                search.SetDateRange(startDate, endDate);
                tasks.Add(GetCars(search));
                startDate = endDate.AddDays(1);
                endDate = startDate.AddMonths(1);
                search.SetDateRange(startDate, endDate);
                tasks.Add(GetCars(search));
                var result = await Task.WhenAll(tasks);
                foreach(var r in result)
                {
                    cars.AddRange(r);
                }

                var t = cars.GroupBy(f => f.Fuel)
                    .Select(group => new PieSeries()
                    {
                        Values = new ChartValues<int> { group.Count() },
                        Title = String.IsNullOrEmpty(group.Key) ? "NIEZNANE" : group.Key
                    }).ToList();

                FuelSeries.Clear();
                FuelSeries.AddRange(t);

                startDate.AddDays(1).AddMonths(2);
                await Task.Delay(1000);
            }

            if(startDate < EndDate)
            {
                search.SetDateRange(startDate, EndDate);
                cars.AddRange(await GetCars(search));
            }

            var test = cars.GroupBy(f => f.Fuel)
                .Select(group => new PieSeries()
                {
                    Values = new ChartValues<int> { group.Count() },
                    Title = String.IsNullOrEmpty(group.Key) ? "NIEZNANE" : group.Key
                }).ToList();

            FuelSeries.Clear();
            FuelSeries.AddRange(test);
        }

        private async Task<List<Car>> GetCars(CarsSearchBuilder search)
        {
            var cars = new List<Car>();
            int page = 1;
            do
            {
                search.SetPage(page);
                cars.AddRange(await CarsData.GetCars(search));
                page++;
                await Task.Delay(1000);
            } while (cars.Count != 0 && cars.Count % 500 == 0);

            return cars;
        }
    }
}
