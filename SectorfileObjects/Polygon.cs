using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coordinates;

namespace SectorfileObjects
{
    public class Polygon
    {
        string name;
        bool isClosed;
        List<GeoPoint> points;

        public GeoPoint FirstPoint
        {
            get
            {
                if (points.Count > 0) return points[0];
                else return null;
            }
        }

        public GeoPoint LastPoint { get
            {
                if (points.Count > 0) return points[points.Count-1];
                else return null;
            }
        }

        public Polygon()
        {
            points = new List<GeoPoint>();
        }

        public Polygon(List<GeoPoint> points)
        {
            this.points = points;
            isClosed = false;
        }

        public Polygon(List<GeoPoint> points, bool closed)
        {
            this.points = points;
            isClosed = closed;
        }

        public Polygon Close()
        {
            //Polygon must be at least a triangle to be closed.
            if (this.points.Count > 2)
            {
                isClosed = true;
            }
            return this;
        }

        public Polygon Open()
        {
            //If the last point is identical to the first point, a polygon is closed anyway.
            if (this.FirstPoint != this.LastPoint)
            {
                isClosed = false;
            }
            return this;
        }

        public Polygon Add(GeoPoint point)
        {
            points.Add(point);
            return this;
        }

        public string ToSct() {
            StringBuilder result = new StringBuilder();
            if (this.points.Count == 0)
                return result.ToString();
            GeoPoint current, previous;
            bool first = true;
            current = this.points[0];
            foreach (GeoPoint point in this.points)
            {
                if (first)
                {
                    current = point;
                    first = false;
                    continue;
                }
                previous = current;
                current = point;

                result.Append(previous.Latitude.ToString() + " "
                    + previous.Longitude.ToString() + " "
                    + current.Latitude.ToString() + " "
                    + current.Longitude.ToString() + "\n");
            }
            if (this.isClosed && this.FirstPoint != this.LastPoint)
            {
                result.Append(this.LastPoint.Latitude.ToString() + " "
                    + this.LastPoint.Longitude.ToString() + " "
                    + this.FirstPoint.Latitude.ToString() + " "
                    + this.FirstPoint.Longitude.ToString() + "\n");
            }
            return result.ToString();
        }

        public string ToEse()
        {
            StringBuilder result = new StringBuilder();
            if (this.points.Count == 0)
                return result.ToString()                    ;
            foreach (GeoPoint point in this.points)
            {
                result.Append("COORD:" + point.Latitude.ToString() + ":" + point.Longitude.ToString() + "\n");
            }
            if (this.isClosed && this.FirstPoint!=this.LastPoint)
            {
                result.Append("COORD:" + this.FirstPoint.Latitude.ToString() + ":" + this.FirstPoint.Longitude.ToString() + "\n");
            }
            return result.ToString();
        }
    }
}
