namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.All)]
    public class NameAttribute : Attribute
    {

        public static readonly NameAttribute Default;

        public NameAttribute() { }

        public NameAttribute(string name)
        {
            Name = name;
        }

        public virtual string Name { get; }
    }
}