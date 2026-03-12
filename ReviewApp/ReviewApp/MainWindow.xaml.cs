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
            "WA"
        };

        public ObservableCollection<string> Cities { get; } = new ObservableCollection<string>()
        {
            "Lake Stevens"
        };

        public ObservableCollection<Business> Businesses { get; } = new ObservableCollection<Business>()
        {
            new Business("Rice Up", "Lake Stevens", "WA")
        };

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void OnCityClick(object sender, RoutedEventArgs e)
        {
            // Populate Businesses box (busBox) with corresponding businesses
            var selectedItem = this.cityBox.SelectedItem;
        }
    }
}