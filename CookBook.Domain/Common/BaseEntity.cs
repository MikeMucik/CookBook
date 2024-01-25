using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Domain.Common
{
    public class BaseEntity : AudiTableModel
    {
        public int Id { get; set; }
    }
}
