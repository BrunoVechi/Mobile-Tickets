
using Xamarin.Forms;

namespace People
{
    public partial class App : Application
    {
        string dbPath => FileAccessHelper.GetLocalFilePath("people.db3");

        public static PersonRepository PersonRepo { get; private set; }

        public App()
        {
            InitializeComponent();

            PersonRepo = new PersonRepository(dbPath);

            //MainPage = new People.MainPage();

            var mainPage = new TabbedPage();
            mainPage.Children.Add(new MainPage());
            mainPage.Children.Add(new ScanQR());

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
