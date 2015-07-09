using System;
using System.Windows.Input;

namespace MetroPresentation.Pages
{
    public partial class PageOne
    {
        public PageOne ()
        {
            InitializeComponent ();

            Intro.Opacity = 1;

            myStoryboard.BeginTime = new TimeSpan (0, 0, 1);
            myStoryboard.Begin ();
        }

        private void LayoutRootMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            App.NavigateToPage (App.GetPresentation () ? new MainPage (1) : new MainPage ());
        }
    }
}