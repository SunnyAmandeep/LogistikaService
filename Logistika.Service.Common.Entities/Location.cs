using System.Collections.Generic;

namespace Logistika.Service.Common.Entities
{
    public class Location
    {
        private double Latitude;
        private double Longitude;

        public double Lon
        {
            get { return Longitude; }
            set { Longitude = value; }
        }

        public double Lat
        {
            get { return Latitude; }
            set { Latitude = value; }
        }

        public Location(double lt, double lg)
        {
            this.Latitude = lt;
            this.Longitude = lg;
        }

        public static bool PointInPolygon(Location p, List<Location> poly)
        {

            int n = poly.Count;

            poly.Add(new Location(poly[0].Lat, poly[0].Lon));
            Location[] v = poly.ToArray();

            int wn = 0;    // the winding number counter

            // loop through all edges of the polygon
            for (int i = 0; i < n; i++)
            {   // edge from V[i] to V[i+1]
                if (v[i].Lat <= p.Lat)
                {         // start y <= P.y
                    if (v[i + 1].Lat > p.Lat)      // an upward crossing
                        if (isLeft(v[i], v[i + 1], p) > 0)  // P left of edge
                            ++wn;            // have a valid up intersect
                }
                else
                {                       // start y > P.y (no test needed)
                    if (v[i + 1].Lat <= p.Lat)     // a downward crossing
                        if (isLeft(v[i], v[i + 1], p) < 0)  // P right of edge
                            --wn;            // have a valid down intersect
                }
            }
            if (wn != 0)
                return true;
            else
                return false;
        }

        private static int isLeft(Location P0, Location P1, Location P2)
        {
            double calc = ((P1.Lon - P0.Lon) * (P2.Lat - P0.Lat)
                    - (P2.Lon - P0.Lon) * (P1.Lat - P0.Lat));
            if (calc > 0)
                return 1;
            else if (calc < 0)
                return -1;
            else
                return 0;
        }
    }
}
