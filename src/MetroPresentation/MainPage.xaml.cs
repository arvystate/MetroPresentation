using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MetroPresentation.Pages;

namespace MetroPresentation
{
    public partial class MainPage
    {
        private readonly Stack<SolidColorBrush> _colorStack = new Stack<SolidColorBrush> ();

        private readonly DispatcherTimer _eventTimer = new DispatcherTimer ();
        private readonly int _nextPage = -1;
        private readonly Color _tileHighlight = Color.FromArgb (255, 222, 222, 222);

        public MainPage ()
        {
            SetupMain ();
        }

        public MainPage (int next)
        {
            SetupMain ();
            _nextPage = next;

            _eventTimer.Interval = new TimeSpan (0, 0, 0, 0, 2000);
            _eventTimer.Tick += EventTimerTick;

            _eventTimer.Start ();
        }

        private void SetupMain ()
        {
            InitializeComponent ();

            // Change background randomly
            Random rnd = new Random ();

            ImageBrush bg1Brush = new ImageBrush ();
            ImageBrush bg2Brush = new ImageBrush ();

            int random = rnd.Next (0, 6);

            switch (random)
            {
                case 1:
                    bg1Brush.ImageSource = new BitmapImage(new Uri("Images/bg1_blue.png", UriKind.Relative));
                    bg2Brush.ImageSource = new BitmapImage(new Uri("Images/bg2_blue.png", UriKind.Relative));

                    break;
                case 2:
                    bg1Brush.ImageSource = new BitmapImage(new Uri("Images/bg1_black.png", UriKind.Relative));
                    bg2Brush.ImageSource = new BitmapImage(new Uri("Images/bg2_black.png", UriKind.Relative));

                    break;
                case 3:
                    bg1Brush.ImageSource = new BitmapImage(new Uri("Images/bg1_red.png", UriKind.Relative));
                    bg2Brush.ImageSource = new BitmapImage(new Uri("Images/bg2_red.png", UriKind.Relative));

                    break;
                case 4:
                    bg1Brush.ImageSource = new BitmapImage(new Uri("Images/bg1_violet.png", UriKind.Relative));
                    bg2Brush.ImageSource = new BitmapImage(new Uri("Images/bg2_violet.png", UriKind.Relative));

                    break;
                case 5:
                    bg1Brush.ImageSource = new BitmapImage (new Uri ("Images/bg1_yellow.png", UriKind.Relative));
                    bg2Brush.ImageSource = new BitmapImage (new Uri ("Images/bg2_yellow.png", UriKind.Relative));

                    break;

                default:
                    bg1Brush.ImageSource = new BitmapImage (new Uri ("Images/bg1.png", UriKind.Relative));
                    bg2Brush.ImageSource = new BitmapImage (new Uri ("Images/bg2.png", UriKind.Relative));

                    break;
            }

            bg1Brush.Stretch = Stretch.UniformToFill;
            bg2Brush.Stretch = Stretch.UniformToFill;

            bg1.Fill = bg1Brush;
            bg2.Fill = bg2Brush;

            if (App.GetPresentation ())
            {
                presentation.Text = GetLanguageString ("lang_presentation_off");
            }

            if (Application.Current.Host.Content.IsFullScreen)
            {
                fullscreen.Text = GetLanguageString ("lang_fullscreen_off");
            }

            Menu.Opacity = 0;

            myStoryboard.BeginTime = new TimeSpan (0, 0, 0, 0, 200);
            myStoryboard.Begin ();
        }

        private void EventTimerTick (object sender, EventArgs e)
        {
            _eventTimer.Stop ();

            if (_nextPage >= 0)
            {
                switch (_nextPage)
                {
                    case 0:
                        App.NavigateToPage (new PageOne ());
                        break;
                    case 1:
                        App.NavigateToPage (new PageTwo ());
                        break;
                    case 2:
                        App.NavigateToPage (new PageThree ());
                        break;
                    case 3:
                        App.NavigateToPage (new PageFour ());
                        break;
                    case 4:
                        App.NavigateToPage (new PageFive ());
                        break;
                    case 5:
                        App.NavigateToPage (new PageSix ());
                        break;
                    case 6:
                        App.NavigateToPage (new PageSeven ());
                        break;
                    case 7:
                        App.NavigateToPage (new PageVideo ());
                        break;
                    case 8:
                        App.NavigateToPage (new PageConclusion ());
                        break;
                    case 9:
                        App.NavigateToPage (new PageAuthor ());
                        break;
                }
            }
        }

        private void LayoutRootKeyDown (object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Application.Current.Host.Content.IsFullScreen = true;

                fullscreen.Text = GetLanguageString ("lang_fullscreen_off");
            }
            else if (e.Key == Key.Escape)
            {
                Application.Current.Host.Content.IsFullScreen = false;
                fullscreen.Text = GetLanguageString ("lang_fullscreen_on");
            }
        }

        private string GetLanguageString (string key)
        {
            if (Application.Current.Resources.Contains (key))
            {
                return (string) Application.Current.Resources[key];
            }

            return null;
        }

        private void FullscreenMouseEnter (object sender, MouseEventArgs e)
        {
            fullscreen.Foreground = new SolidColorBrush (Colors.Black);
            fullscreen.FontWeight = FontWeights.Bold;
        }

        private void FullscreenMouseLeave (object sender, MouseEventArgs e)
        {
            fullscreen.Foreground = new SolidColorBrush (Colors.White);
            fullscreen.FontWeight = FontWeights.Normal;
        }

        private void FullscreenMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.Host.Content.IsFullScreen)
            {
                Application.Current.Host.Content.IsFullScreen = false;
                fullscreen.Text = GetLanguageString ("lang_fullscreen_on");
            }
            else
            {
                Application.Current.Host.Content.IsFullScreen = true;
                fullscreen.Text = GetLanguageString ("lang_fullscreen_off");
            }
        }

        private void PresentationMouseEnter (object sender, MouseEventArgs e)
        {
            presentation.Foreground = new SolidColorBrush (Colors.Black);
            presentation.FontWeight = FontWeights.Bold;
        }

        private void PresentationMouseLeave (object sender, MouseEventArgs e)
        {
            presentation.Foreground = new SolidColorBrush (Colors.White);
            presentation.FontWeight = FontWeights.Normal;
        }

        private void PresentationMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            if (App.GetPresentation ())
            {
                presentation.Text = GetLanguageString ("lang_presentation_on");
                App.SetPresentation (false);
            }
            else
            {
                presentation.Text = GetLanguageString ("lang_presentation_off");
                App.SetPresentation (true);
            }
        }

        #region Tile clicks

        private void Tile1MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageOne ());
        }

        private void Tile4MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageTwo ());
        }

        private void Tile5MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageFive ());
        }

        private void Tile6MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageThree ());
        }

        private void Tile7MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageFour ());
        }

        private void Tile8MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageSix ());
        }

        private void Tile9MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageSeven ());
        }

        private void Tile14MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageVideo ());
        }

        private void Tile15MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageConclusion ());
        }

        private void Tile16MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            _eventTimer.Stop ();

            App.NavigateToPage (new PageAuthor ());
        }

        #endregion

        #region Tile hover

        private void Tile1MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile1.Stroke;
            _colorStack.Push (brush);
            tile1.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile1MouseLeave (object sender, MouseEventArgs e)
        {
            tile1.Stroke = _colorStack.Pop ();
        }

        private void Tile4MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile4.Stroke;
            _colorStack.Push (brush);
            tile4.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile4MouseLeave (object sender, MouseEventArgs e)
        {
            tile4.Stroke = _colorStack.Pop ();
        }

        private void Tile5MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile5.Stroke;
            _colorStack.Push (brush);
            tile5.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile5MouseLeave (object sender, MouseEventArgs e)
        {
            tile5.Stroke = _colorStack.Pop ();
        }

        private void Tile6MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile6.Stroke;
            _colorStack.Push (brush);
            tile6.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile6MouseLeave (object sender, MouseEventArgs e)
        {
            tile6.Stroke = _colorStack.Pop ();
        }

        private void Tile7MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile7.Stroke;
            _colorStack.Push (brush);
            tile7.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile7MouseLeave (object sender, MouseEventArgs e)
        {
            tile7.Stroke = _colorStack.Pop ();
        }

        private void Tile8MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile8.Stroke;
            _colorStack.Push (brush);
            tile8.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile8MouseLeave (object sender, MouseEventArgs e)
        {
            tile8.Stroke = _colorStack.Pop ();
        }

        private void Tile9MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile9.Stroke;
            _colorStack.Push (brush);
            tile9.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile9MouseLeave (object sender, MouseEventArgs e)
        {
            tile9.Stroke = _colorStack.Pop ();
        }

        private void Tile14MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile14.Stroke;
            _colorStack.Push (brush);
            tile14.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile14MouseLeave (object sender, MouseEventArgs e)
        {
            tile14.Stroke = _colorStack.Pop ();
        }

        private void Tile15MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile15.Stroke;
            _colorStack.Push (brush);
            tile15.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile15MouseLeave (object sender, MouseEventArgs e)
        {
            tile15.Stroke = _colorStack.Pop ();
        }

        private void Tile16MouseEnter (object sender, MouseEventArgs e)
        {
            SolidColorBrush brush = (SolidColorBrush) tile16.Stroke;
            _colorStack.Push (brush);
            tile16.Stroke = new SolidColorBrush (_tileHighlight);
        }

        private void Tile16MouseLeave (object sender, MouseEventArgs e)
        {
            tile16.Stroke = _colorStack.Pop ();
        }

        #endregion
    }
}