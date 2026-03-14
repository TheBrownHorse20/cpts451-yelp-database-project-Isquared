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
using Npgsql;

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
        public ObservableCollection<string> States { get; } = new ObservableCollection<string>();

        public ObservableCollection<City> Cities { get; } = new ObservableCollection<City>()
        {
            new City("Lake Stevens", "WA"),
            new City("Las Vegas", "NV")
        };

        public ObservableCollection<Business> Businesses { get; } = new ObservableCollection<Business>()
        {
            new Business("Rice Up", "Lake Stevens", "WA"),
            new Business("Tom's Pizzaria", "Las Vegas", "NV")
        };

        string postGresString = "Host=localhost; Username=postgres; Password=pugs; Database = milestone1db";

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            AddStates();
        }

        public void AddStates()
        {
            using (var conn = new NpgsqlConnection(postGresString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT distinct state FROM business ORDER BY state;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            States.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
        }

        private void OnStateClick(object sender, RoutedEventArgs e)
        {
            this.cityBox.ItemsSource = null; // Clear the city box
            this.busBox.ItemsSource = null;  // Clear the business box
            
            if (this.stateBox.SelectedIndex < 0)
            {
                return;
            }

            string? selectedState = this.stateBox.SelectedItem.ToString();

            // Populate cityBox with distinct cities from the database for the selected state
            ObservableCollection<City> subCities = new ObservableCollection<City>();

            using (var conn = new NpgsqlConnection(postGresString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT DISTINCT city, state FROM business WHERE state = @state ORDER BY city;";
                    cmd.Parameters.AddWithValue("@state", selectedState);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subCities.Add(new City(reader.GetString(0), reader.GetString(1)));
                        }
                    }
                }
                conn.Close();
            }

            this.cityBox.ItemsSource = subCities;

            // Also populate businesses based on the selected state
            using (var conn = new NpgsqlConnection(postGresString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT name, city, state FROM business WHERE state = @state;";
                    cmd.Parameters.AddWithValue("@state", selectedState);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Businesses.Add(new Business(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                        }
                    }
                }
                conn.Close();
            }
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