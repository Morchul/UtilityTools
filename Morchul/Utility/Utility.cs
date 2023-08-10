using System;
namespace Morchul.Utility
{
    public static class Utility
    {
        public static string GetCurrentSaveDateTime()
        {
            return DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
        }
    }

}
