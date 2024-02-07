using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Domain.Common
{
    public class AudiTableModel
    {
        public int CreatedById { get; set; }
        public DateTime CreatedDateTime {  get; set; }
        public int? ModyfiedById { get; set; }
        public DateTime ModyfiedDateTime { get; set; }
    }
}
