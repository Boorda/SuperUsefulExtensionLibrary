namespace SUELIB.WindowExtensions
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    public static class WindowExtensions
    {

        public static Action EmptyDelegate = () => { };

        /// <summary>
        /// Refreshes the current Window.
        /// </summary>
        /// <param name="window">Window to refresh.</param>
        public static void RefreshWindow(this Window window)
        {
            window.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window">Window to save location from.</param>
        /// <param name="settingName">Name of the System.Drawing.Point application setting used to store the windows location on screen.</param>
        public static void SaveLocation(this Window window, string settingName)
        {
            try
            {
                System.Drawing.Point loc = new System.Drawing.Point(Convert.ToInt32(window.Left), Convert.ToInt32(window.Top));
                var saved = (System.Drawing.Point)Properties.Settings.Default[settingName];
                if (loc != saved)
                {
                    Properties.Settings.Default[settingName] = loc;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception)
            {
                // Do Nothing
            }

        }

        /// <summary>
        /// Restores the <see cref="Window"/> to a saved location using the specified appSetting name.
        /// </summary>
        /// <param name="window">Window to set location of.</param>
        /// <param name="settingName">Name of the System.Drawing.Point application setting used to restore the window's location on screen.</param>
        public static void RestoreLocation(this Window window, string settingName)
        {
            try
            {
                var loc = (System.Drawing.Point)Properties.Settings.Default[settingName];
                window.Left = loc.X;
                window.Top = loc.Y;
            }
            catch
            {
                /* Do Nothing */
            }

        }

    }
}
