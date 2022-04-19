using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Helpers
{
    public class Constants
    {
        public static string Directory = "\\uploads\\";
        public static string Image = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss"); 

        public static string OrderId = "order" + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
        public static string reviewId = "review" + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
    }
}
