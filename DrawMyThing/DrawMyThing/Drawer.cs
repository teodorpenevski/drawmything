using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Drawing.Drawing2D;

namespace DrawMyThing
{
    public class Drawer
    {   
        public ConcurrentQueue<Action> Actions;
        public Drawer()
        {
            Actions = new ConcurrentQueue<Action>();
        }

        public void Draw(Graphics g)
        {
            foreach (Action a in Actions)
            {
                Pen pn = new Pen(a.Colour,a.Width);
                g.DrawLine(pn,a.Start, a.End);
                pn.Dispose();
            }
        }
        public void AddNewAction(Action a)
        {
            Actions.Enqueue(a);
        }
    }
}
