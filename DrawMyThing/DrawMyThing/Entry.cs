using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMyThing
{
    [Serializable]
    public class Entry
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public Entry(string Name,int Points)
        {
            this.Name = Name;
            this.Points = Points;
        }
    }
}
