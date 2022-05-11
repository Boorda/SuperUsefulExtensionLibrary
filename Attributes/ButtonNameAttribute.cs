namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ButtonNameAttribute : Attribute
    {

        public static readonly ButtonNameAttribute Default;

        public ButtonNameAttribute() { }

        public ButtonNameAttribute(string name)
        {
            Name = name;
        }

        public virtual string Name { get; }

        protected string NameValue { get; set; }
    }
}