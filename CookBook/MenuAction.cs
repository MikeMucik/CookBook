using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    public class MenuAction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MenuName { get; set; }
        public MenuAction(int id, string name) //* zamiana z menu actonservice linia20
        {
            Id = id;
            Name = name;
        }
        //public MenuAction(int id)
        //{
        //    Id= id;
        //}
    }

}
