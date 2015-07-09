using System;
using System.Windows;
using System.Windows.Controls;

namespace MetroPresentation
{
    public partial class App
    {
        public Grid NavigationUi = new Grid ();

        public App ()
        {
            Startup += ApplicationStartup;
            Exit += Application_Exit;
            UnhandledException += ApplicationUnhandledException;

            InitializeComponent ();
        }

        public bool Presentation
        {
            get;
            set;
        }

        private void ApplicationStartup (object sender, StartupEventArgs e)
        {
            RootVisual = NavigationUi;
            NavigationUi.Children.Add (new MainPage ());
        }

        private void Application_Exit (object sender, EventArgs e)
        {
        }

        private void ApplicationUnhandledException (object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke (() => ReportErrorToDOM (e));
            }
        }

        private void ReportErrorToDOM (ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace ('"', '\'').Replace ("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval ("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        public static void NavigateToPage (UserControl page)
        {
            App application = (App) Current;
            application.NavigationUi.Children.Clear ();
            application.NavigationUi.Children.Add (page);
        }

        public static bool GetPresentation ()
        {
            App application = (App) Current;
            return application.Presentation;
        }

        public static void SetPresentation (bool set)
        {
            App application = ((App) Current);
            application.Presentation = set;
        }
    }
}