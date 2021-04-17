using NoteKeeper.Services;
using NoteKeeper.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteKeeper
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataCoursesStore>();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockDataNoteStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
