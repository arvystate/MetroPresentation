using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MetroPresentation.Pages
{
    public partial class PageVideo
    {
        public PageVideo ()
        {
            InitializeComponent ();

            Intro.Opacity = 1;

            myStoryboard.BeginTime = new TimeSpan (0, 0, 1);
            myStoryboard.Begin ();
        }

        private void LayoutRootMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            App.NavigateToPage (App.GetPresentation () ? new MainPage (8) : new MainPage ());
        }

        private void MediaElementMediaFailed (object sender, ExceptionRoutedEventArgs e)
        {
        }

        private void MyStoryboardCompleted (object sender, EventArgs e)
        {
            Intro.Visibility = Visibility.Collapsed;
        }

        private void PlayVideoClick (object sender, RoutedEventArgs e)
        {
            if (video.CurrentState == MediaElementState.Playing)
            {
                video.Pause ();
                play_video.Content = "PLAY";
            }
            else
            {
                video.Play ();
                play_video.Content = "PAUSE";
            }
        }

        private void StopVideoClick (object sender, RoutedEventArgs e)
        {
            video.Stop ();
        }
    }
}