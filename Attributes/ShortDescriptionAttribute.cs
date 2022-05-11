namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.All)]
    public class ShortDescriptionAttribute : Attribute
    {

        public static readonly ShortDescriptionAttribute Default;

        public ShortDescriptionAttribute() { }

        public ShortDescriptionAttribute(string description) {
            Description = description;
        }

        public virtual string Description { get; }

        protected string DescriptionValue { get; set; }
    }
}