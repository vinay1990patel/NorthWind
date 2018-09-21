using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;
namespace Northwind.Entities.Models
{
   public partial class ExceptionLogger: Entity
    {
        public int id { get; set; }
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ExceptionStackTrace { get; set;}
        public string LogTime { get; set; }
        public string RequestMethod { get; set; }
    }
}
