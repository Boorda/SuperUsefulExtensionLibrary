namespace SUELIB.StringExtensions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    public static class StringExtensions
    {

        /// <summary>
        /// Case insensitive comparison of two strings to see if they are a match.  
        /// </summary>
        /// <param name="StrA">First string to compare.</param>
        /// <param name="StrB">Second string to compare.</param>
        /// <param name="trimWhiteSpace">If true (default) then trims outer whitespace from bath strings.</param>
        /// <returns></returns>
        public static bool CompareText(this string StrA, string StrB, bool trimWhiteSpace = true)
        {
            var sA = StrA.ToUpper();
            var sB = StrB.ToUpper();
            if (trimWhiteSpace) { sA = sA.Trim(); sB = sB.Trim(); }
            return StrA == StrB;
        }

        /// <summary>
        /// Creates a delimited string list of the values in the collection using the specified delimiter. <para/>
        /// Skips any items in the collection that are not type of <c>string</c>.
        /// </summary>
        /// <param name="collection">The collection used to generate the string.</param>
        /// <param name="delimiter">The delimiter to be used.</param>
        /// <returns></returns>
        public static string GetDelimitedStringFromCollection(this ICollection collection, char delimiter)
        {
            string delString = string.Empty;
            foreach (var item in collection)
            {
                if (item.GetType() == typeof(string))
                {
                    if (delString.IsEmpty()) { delString = (string)item; } else { delString += $"{delimiter}{(string)item}"; }
                }
            }
            return delString;
        }

        /// <summary>
        /// Checks to see if the specified string is empty.
        /// </summary>
        /// <param name="Str"><c>String</c> to be checked.</param>
        /// <returns><c>True</c> if the <c>String</c> is empty, otherwise <c>False</c></returns>
        [DebuggerHidden]
        public static bool IsEmpty(this string Str)
        {
            if (Str == null | Str == string.Empty || Str.Trim() == "")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks to see if the specified string is a number.
        /// </summary>
        /// <param name="Str"><c>String</c> to be checked.</param>
        /// <returns><c>True</c> if the <c>String</c> is a number, otherwise <c>False</c></returns>
        public static bool IsNumeric(this string str)
        {
            List<char> chars = str.ToCharArray().ToList();
            foreach (var c in chars)
            {
                if (!char.IsNumber(c)) { return false; }
            }
            return true;
        }

        /// <summary>
        /// Creates a string by repeating the current string the specified number of times.
        /// </summary>
        /// <param name="str">String or Character to repeat.</param>
        /// <param name="count">Number of times to repeat.</param>
        /// <param name="newLines">If <c>true</c> creates the repeated string on a new line.</param>
        public static string Repeat(this string str, int count, bool newLines = false)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (newLines) { sb.AppendLine(str); } else { sb.Append(str); }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns a string[] containing string split by the specified delimiter.
        /// </summary>
        /// <param name="input">The string to be split by the specified delimiters.</param>
        /// <param name="delimiters">Array of characters to be used as a delimiter to split the string.</param>
        /// <returns></returns>
        public static string[] SplitDelimitedString(this string input, char[] delimiters)
        {
            var sList = new List<string>();
            foreach (var item in input.Split(delimiters))
            {
                var clnItem = item.Trim();
                if (!clnItem.IsEmpty() && !sList.Contains(clnItem)) { sList.Add(clnItem); }
            }
            return sList.ToArray();
        }

    }
}
