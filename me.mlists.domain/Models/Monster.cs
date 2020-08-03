using System;
using System.Collections.Generic;
using System.Text;

namespace me.mlists.domain.Models
{
    public class Monster
    {
        public int Id { get; private set; }
        public string Url { get; set; }
        public IList<Lista> Listas { get; private set; }

        public Monster() 
        { 
        }

        public Monster(int id, string url)
        {
            Id = id;
            Url = url;
        }

        public Monster(string url)
        {
            Url = url;
        }
    }
}
