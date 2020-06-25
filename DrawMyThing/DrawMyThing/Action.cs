using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMyThing
{
    [Serializable]
    public class Action
    {
        public int Id { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public Color Colour { get; set; }
        public float Width { get; set; }
        public Action(Point p1,Point p2,Color Colour,float Width)
        {
            this.Width = Width;
            this.Colour = Colour;
            Start = p1;
            End = p2;
        }
    }
}
