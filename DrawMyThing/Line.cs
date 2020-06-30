using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMyThing
{
    [Serializable]
    public class Line
    {
        List<Point> points;
        Color color;
        int width;

        public Line(Color color, int width)
        {
            points = new List<Point>();
            this.color = color;
            this.width = width;
        }

        public void addPoint(Point p)
        {
            points.Add(p);
        }

        public void Draw(Graphics g)
        {
            Pen pn = new Pen(color, width);
            pn.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(pn, points[i], points[i + 1]);
            }
            pn.Dispose();
        }


    }
}
