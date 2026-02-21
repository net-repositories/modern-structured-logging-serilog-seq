using Raven.Client.Documents;
using Serilog;
using System.Windows;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var logDocumentStore = new DocumentStore
            {
                Urls = ["http://localhost:8080/"],
                Database = "logs"
            };

            logDocumentStore.Initialize();


            ILogger logger = new LoggerConfiguration()
                        .WriteTo.RavenDB(logDocumentStore)
                        .WriteTo.Console()
                        .CreateLogger();

            Log.Logger = logger;
        }

        private void AddUser_OnClick(object sender, RoutedEventArgs e)
        {
            Log.Information("Button Clicked");

            // add user to database            
            MessageBox.Show("User added");

            var userToAdd = new User
            {
                Name = Name.Text,
                Age = int.Parse(Age.Text)
            };

            Log.Information("Added user {@NewUser}", userToAdd);
        }
    }
}