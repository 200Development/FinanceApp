
using System.Web.Mvc;

namespace FinanceApp.Data
{
    public class Global
    {
        private static volatile Global _instance;
        private static readonly object Lock = new object();


        private Global()
        {
            User = new ApplicationUser();
        }


        public ApplicationUser User { get; set; }


        public static Global Instance
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new Global();
                }

                return _instance;
            }
        }
    }
}
