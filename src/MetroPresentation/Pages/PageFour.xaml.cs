using System;
using System.Windows.Input;

namespace MetroPresentation.Pages
{
    public partial class PageFour
    {
        public PageFour ()
        {
            InitializeComponent ();

            Intro.Opacity = 1;

            myStoryboard.BeginTime = new TimeSpan (0, 0, 1);
            myStoryboard.Begin ();
        }

        private void LayoutRootMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            App.NavigateToPage (App.GetPresentation () ? new MainPage (4) : new MainPage ());
        }
    }
}