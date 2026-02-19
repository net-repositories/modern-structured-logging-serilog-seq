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

namespace ShipOrder
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

        private void ShipOrder_OnClick(object sender, RoutedEventArgs e)
        {
            var orderId = OrderId.Text;

            Log.Information("Order {OrderId} shipped", orderId);
        }
    }
}