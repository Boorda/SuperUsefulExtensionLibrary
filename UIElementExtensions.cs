namespace SUELIB.Extensions
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Threading;

    public static class UIElementExtensions
    {
        /// <summary>
        /// Closes an open Tool Tip.
        /// </summary>
        /// <param name="toolTip">Tool Tip object to be closed.</param>
        public static void Close(this ToolTip toolTip)
        {
            //BROKEN: Fix, not working.
            if (toolTip is null) { return; }
            toolTip.IsOpen = false;
            toolTip.Refresh();
        }

        /// <summary>
        /// Displays a custom Tool-Tip message on the UI Element then returns the Tool-Tip to it's original text.
        /// </summary>
        public static void DisplayToolTipMessage(this Control control, string message, short displayMilliseconds, PlacementMode placementMode = PlacementMode.RelativePoint)
        {
            ToolTip orgTT = null;
            Type ttType = control.ToolTip.GetType();
            if (ttType == typeof(ToolTip))
            {
                // Get the original tool tip.
                orgTT = (ToolTip)control.ToolTip;
            }

            if (ttType == typeof(string))
            {
                // Get the original tool tip text.
                string orgText = (string)control.ToolTip;
                orgTT = new ToolTip() { Content = orgText };
            }

            // Create the new tool tip;
            ToolTip newTT = new ToolTip
            {
                Content = message,
                Placement = PlacementMode.Top,
                PlacementTarget = control
            };
            // Assign the tool tip to the control.
            control.ToolTip = newTT;
            // Subscribe to the tool tip open event so we can control open time.
            newTT.Opened += async delegate (object obj, RoutedEventArgs args)
            {
                // Grab the tool tip passed in through the delegate.
                var tt = (ToolTip)obj;
                // Set the amount of time to keep the tool tip open, then close it.
                await Task.Delay(displayMilliseconds);
                tt.IsOpen = false;
                // Let the custom tool tip fully close then reset to the original tool tip.
                await Task.Delay(1000);
            };
            // Open the tool tip to initiate the event delegate we created above.
            newTT.IsOpen = true;
            control.ToolTip = orgTT;
        }

        private static Action EmptyDelegate = delegate () { };

        /// <summary>
        /// Finds the first instance of a parent matching the specified type or null if no matching type is found.
        /// </summary>
        /// <typeparam name="T">Type of the parent control to find.</typeparam>
        /// <param name="child">Child object in which to start searching the visual tree.</param>
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }

        public static ToolTip GetToolTip(this Control control)
        {
            if (control.ToolTip is null) { return null; }
            Type ttType = control.ToolTip.GetType();
            if (ttType == typeof(ToolTip))
            {
                // Get the original tool tip.
                return (ToolTip)control.ToolTip;
            }

            if (ttType == typeof(string))
            {
                // Get the original tool tip text.
                string orgText = (string)control.ToolTip;
                return new ToolTip() { Content = orgText };
            }
            return new ToolTip();
        }

        /// <summary>
        /// Forces the UIElement to refresh.
        /// </summary>
        /// <param name="uiElement">Element to refresh on the Dispatcher thread.</param>
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }

    }
}
