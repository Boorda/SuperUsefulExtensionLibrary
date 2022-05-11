namespace VaultDocumentImporter.Extensions
{
    using SUELIB.StringExtensions;

    using System.Xml.Linq;

    public static class XmlExtensions
    {

        /// <summary>
        /// Gets the value of the first top level or sub-level <c>xElement</c> or <c>xAttribute</c> matching the specified name starting 
        /// at the <c>xElement</c> in which the method was called on. <para/>
        /// Returns <c>String.Empty</c> if no match exists.
        /// </summary>
        /// <param name="xElement"></param>
        /// <param name="xName"></param>
        /// <param name="ignoreCase"></param>
        public static string TryGetValue(this XElement xElement, string xName, bool ignoreCase = true)
        {
            /* Check the Top element name. */
            if (ignoreCase)
            {
                if (xElement.Name.LocalName.ToUpper() == xName.ToUpper().Trim()) { return xElement.Value; };
            }
            else
            {
                if (xElement.Name.LocalName == xName.Trim()) { return xElement.Value; };
            }

            /* Take care of the top level attributes.*/
            foreach (XAttribute att in xElement.Attributes())
            {
                if (ignoreCase)
                {
                    if (att.Name.LocalName.ToUpper() == xName.ToUpper().Trim()) { return att.Value; };
                }
                else
                {
                    if (att.Name.LocalName == xName.Trim()) { return att.Value; };
                }
            }

            /* Now check all descending elements and attributes*/

            foreach (XElement ele in xElement.Descendants())
            {
                string val =  ele.TryGetValue(xName);
                if (!val.IsEmpty()) { return val; }
            }

            /* Well shoot, guess we didn't find anything.*/
            return string.Empty;
        }

    }
}
