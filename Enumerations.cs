namespace SUELIB.Enumerations
{
    using System.ComponentModel;

    /// <summary>
    /// Global enumerations.
    /// </summary>
    public static class Enumerations
    {
        public enum eActionType
        {
            [ShortDescription("Create")]
            Create,
            [ShortDescription("Skip")]
            Skip,
            [ShortDescription("Update")]
            Update
        }

        public enum eLoginAction
        {
            Login,
            LogOut
        }

        public enum eEventTiming
        {
            Before,
            After,
            Abort
        }

    }
}
