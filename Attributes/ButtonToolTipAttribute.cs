namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ButtonToolTipAttribute : Attribute
    {

        public static readonly ButtonToolTipAttribute Default;

        public ButtonToolTipAttribute() { }

        public ButtonToolTipAttribute(string toolTip)
        {
            ToolTip = toolTip;
        }

        public virtual string ToolTip { get; }

        protected string ToolTipValue { get; set; }
    }
}