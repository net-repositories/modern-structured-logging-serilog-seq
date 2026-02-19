using CustomEnricher;
using Serilog;
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

namespace PlaceOrder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ApplicationDetailsEnricher())
                        .MinimumLevel.Verbose()
                        .WriteTo.Seq("http://localhost:5341")
                        .WriteTo.Console()
                        .CreateLogger();
        }

        private void PlaceOrder_OnClick(object sender, RoutedEventArgs e)
        {
            var id = OrderId.Text;
            var item = OrderItem.Text;
            var quantity = int.Parse(OrderQuantity.Text);
            var customer = CustName.Text;

            var corrLog = Log.Logger.ForContext("OrderId", id).ForContext<MainWindow>();

            corrLog.Debug("Place order button clicked");

            corrLog.Verbose("Opening database connection");

            // add to database            
            corrLog.Information("{Customer} ordered {Quantity} {Item}", customer, quantity, item);
        }

        private void SimulateError_OnClick(object sender, RoutedEventArgs e)
        {
            Log.Error("Simulated exception");
        }
    }
}