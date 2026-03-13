using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace ReviewApp
{
    public class City
    {
        public string Name { get; set; }
        public string State { get; set; }

        public City(string name, string state)
        {
            this.Name = name;
            this.State = state;
        }
    }

    public class Business
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public Business(string name, string city, string state)
        {
            this.Name = name;
            this.City = city;
            this.State = state;
        }
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<string> States { get; } = new ObservableCollection<string>()
        {
            "WA",
            "NE"
        };

        public ObservableCollection<City> Cities { get; } = new ObservableCollection<City>()
        {
            new City("Lake Stevens", "WA"),
            new City("Las Vegas", "NE")
        };

        public ObservableCollection<Business> Businesses { get; } = new ObservableCollection<Business>()
        {
            new Business("Rice Up", "Lake Stevens", "WA"),
            new Business("Tom's Pizzaria", "Las Vegas", "NE")
        };

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void OnStateClick(object sender, RoutedEventArgs e)
        {
            // Populate cityBox with corresponding cities
            string? selectedState = this.stateBox.SelectedItem.ToString();   // string containing selected state's abbreviation

            if (selectedState == null)
            {
                return;
            }

            ObservableCollection<City> subCities = new ObservableCollection<City>();

            foreach (var city in this.Cities)
            {
                if (city.State == selectedState)
                {
                    subCities.Add(city);
                }
            }

            this.cityBox.ItemsSource = subCities;
            this.busBox.ItemsSource = null;
        }

        private void OnCityClick(object sender, RoutedEventArgs e)
        {
            // Populate Businesses box (busBox) with corresponding businesses
            City selectedItem = (City)this.cityBox.SelectedItem;

            if (selectedItem == null)
            {
                return;
            }

            if (selectedItem is City)
            {
                ObservableCollection<Business> subBus = new ObservableCollection<Business>();

                foreach (var business in this.Businesses)
                {
                    if (business.City == selectedItem.Name)
                    {
                        subBus.Add(business);
                    }
                }

                this.busBox.ItemsSource = subBus;
            }
        }
    }
}