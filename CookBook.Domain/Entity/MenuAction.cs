using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.Domain.Common;

namespace CookBook.Domain.Entity
{
    public class MenuAction : BaseEntity
    {
       // public int Id { get; set; }
        public string Name { get; set; }
        public string MenuName { get; set; }

        public MenuAction(int id, string name, string menuname) 
        {
            Id = id;
            Name = name;
            MenuName = menuname;
        }
        //public MenuAction(int id, string name) //* zamiana z menu actonservice linia20
        //{
        //    Id = id;
        //    Name = name;
        //}
        //public MenuAction(int id)
        //{
        //    Id = id;
        //}
    }

}
