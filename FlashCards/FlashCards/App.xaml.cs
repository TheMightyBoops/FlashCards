using System;
using System.IO;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FlashCards
{
    public partial class App : Application
    {
        static QuestionDB database;

        public static QuestionDB Database
        {
            get
            {
                if(database == null)
                {
                    database = new QuestionDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuestionSQLite.db"));
                }

                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
