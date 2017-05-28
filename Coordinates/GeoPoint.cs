using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Coordinates
{
    public class GeoPoint
    {
        private Longitude longitude;
        private Latitude latitude;

        public Longitude Longitude { get { return longitude; } }
        public Latitude Latitude { get { return latitude; } }
        public GeoPoint(Longitude lon, Latitude lat)
        {
            this.longitude = lon;
            this.latitude = lat;
        }

        public override string ToString()
        {
            return "";
        }

        /// <summary>
        /// Converts the string representation of coordinates to its Coordinates.GeoPoint equivalent.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>GeoPoint representation of coordinates fiven as an input.</returns>
        /// <exception cref="FormatException">Input is not of the correct format.</exception>
        /// <remarks>The correct string format is: dd°mm'ss[''|"][N|S] dd°mm'ss[''|"][E|W]</remarks>
        public static GeoPoint Parse(string input)
        {
            string pattern = "^([0-9]{1,2})°([0-9]{1,2})'([0-9]{1,2})(''|\")([NS]{1}) ([0-9]{1,3})°([0-9]{1,2})'([0-9]{1,2})(''|\")([EW]{1})";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(input);
            if (match.Success)
            {
                byte latDeg = Byte.Parse(match.Groups[1].Value);
                byte latMin = Byte.Parse(match.Groups[2].Value);
                byte latSec = Byte.Parse(match.Groups[3].Value);
                GeoCoordinatesSign latSign = GeoCoordinatesSign.Plus;
                if (match.Groups[5].Value == "S")
                {
                    latSign = GeoCoordinatesSign.Minus;
                }
                byte lonDeg = Byte.Parse(match.Groups[6].Value);
                byte lonMin = Byte.Parse(match.Groups[7].Value);
                byte lonSec = Byte.Parse(match.Groups[8].Value);
                GeoCoordinatesSign lonSign = GeoCoordinatesSign.Plus;
                if (match.Groups[10].Value == "W")
                {
                    lonSign = GeoCoordinatesSign.Minus;
                }
                GeoPoint coord = new GeoPoint(new Longitude(lonSign, lonDeg, lonMin, lonSec),
                    new Latitude(latSign, latDeg, latMin, latSec));
                return coord;
            }
            else
            {
                throw new FormatException("Unsupported format. Pass coordinates in following format: dd°mm'ss[''|\"][N|S] dd°mm'ss[''|\"][E|W]");
            }
        }

        public static bool TryParse(string input, out GeoPoint point)
        {
            try
            {
                GeoPoint pnt = GeoPoint.Parse(input);
                point = pnt;
                return true;
            }
            catch (Exception)
            {
                point = null;
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GeoPoint)) { return false; }
            return ((GeoPoint)obj).Latitude.Value == this.Latitude.Value && ((GeoPoint)obj).Longitude.Value == this.Longitude.Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
