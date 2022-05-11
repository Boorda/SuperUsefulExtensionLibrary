namespace SUELIB.EnumerationExtensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public static class EnumerationExtensions
    {

        /// <summary>
        /// Gets the Description attribute of an enum field.
        /// </summary>
        /// <param name="field">The enum value to retrieve the description of.</param>
        /// <param name="extendedMessage">If string is not empty and the enum description has an {ex} place holder then the message is added to the description.</param>
        public static string GetDescriptionSting(this Enum field, string extendedMessage = null)
        {
            DescriptionAttribute[] customAttributes = (DescriptionAttribute[])field.GetType().GetField(field.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (customAttributes.Length == 0) { return string.Empty; }
            var descStr = customAttributes[0].Description;
            if (extendedMessage is null) { descStr.Replace("{ex}", ""); }
            else
            {
                descStr.Replace("{ex}", $"{Environment.NewLine}{extendedMessage}");
            }
            return descStr;
        }

        /// <summary>
        /// Gets the Short Description attribute of an enum field.
        /// </summary>
        /// <param name="field">The enum value to retrieve the description of.</param>
        /// <returns></returns>
        public static string GetShortDescriptionSting(this Enum field)
        {
            ShortDescriptionAttribute[] customAttributes = (ShortDescriptionAttribute[])field.GetType().GetField(field.ToString()).GetCustomAttributes(typeof(ShortDescriptionAttribute), false);
            return ((customAttributes.Length != 0) ? customAttributes[0].Description : string.Empty);
        }

        /// <summary>
        /// Gets the Name attribute of an enum field.
        /// </summary>
        /// <param name="field">The enum value to retrieve the name.</param>
        /// <returns></returns>
        public static string GetNameSting(this Enum field)
        {
            NameAttribute[] customAttributes = (NameAttribute[])field.GetType().GetField(field.ToString()).GetCustomAttributes(typeof(NameAttribute), false);
            return ((customAttributes.Length != 0) ? customAttributes[0].Name : string.Empty);
        }

        /// <summary>
        /// Attempts to find a string match within an enum's description attribute.<para/>
        /// The description can be split by a delimiter allowing for match lists to be created withing the enum description.
        /// </summary>
        /// <param name="field">The enum field in review.</param>
        /// <param name="text">The text to search for.</param>
        /// <param name="delimiter">A delimiter used to split the description string.</param>
        /// <returns></returns>
        public static bool DescriptionContains(this Enum field, string text, char delimiter = ';')
        {
            DescriptionAttribute[] customAttributes = (DescriptionAttribute[])field.GetType().GetField(field.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (customAttributes.Length == 0) { return false; }
            var desStr = customAttributes[0].Description.Split(delimiter);
            var found = desStr.Select(x => x.Trim().ToUpper() == text.Trim().ToUpper());
            // If we found anything then its a match...
            if (found.Contains(true)) { return true; }
            return false;
        }


    }
}
