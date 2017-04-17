using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }

    public abstract class Coordinate
    {
        //internally stored in seconds
        protected decimal value;
        public virtual decimal MaxValue { get { return 1; } }
        public virtual decimal MinValue { get { return -1; } }

        public decimal Value { get { return value; } }

        public Coordinate(decimal value)
        {
            if (IsValid(value))
            {
                this.value = value;
            }
        }

        public Coordinate(GeoCoordinatesSign sign, byte deg, decimal min)
        {
            decimal newvalue = deg * 3600 + min;
            if (sign == GeoCoordinatesSign.Minus)
            {
                newvalue *= -1m;
            }
            if (IsValid(newvalue))
            {
                this.value = newvalue;
            }
        }

        public Coordinate(GeoCoordinatesSign sign, byte deg, byte min, byte sec)
        {
            decimal newvalue = deg * 3600 + min * 60 +sec;
            if (sign == GeoCoordinatesSign.Minus)
            {
                newvalue *= -1m;
            }
            if (IsValid(newvalue))
            {
                this.value = newvalue;
            }
        }

        private bool IsValid(decimal newvalue)
        {
            return newvalue<=MaxValue && newvalue>=MinValue;
        }

        public DegMinSec ToDegMinSec()
        {
            DegMinSec x = new DegMinSec();
            decimal valueToConvert = this.value;
            if (this.value>=0)
            {
                x.Sign = GeoCoordinatesSign.Plus;
            }
            else
            {
                x.Sign = GeoCoordinatesSign.Minus;
                valueToConvert = -valueToConvert;
            }
            x.Seconds = (byte)(valueToConvert % 60);
            x.Minutes = (byte)((valueToConvert - x.Seconds) % 3600 / 60);
            x.Degrees = (byte)((valueToConvert - x.Minutes * 60 - x.Seconds) / 3600);
            return x;
        }
    }

    public class Latitude:Coordinate
    {
        public Latitude(decimal value) : base(value) { }
        public Latitude(GeoCoordinatesSign sign, byte deg, decimal min) : base(sign, deg, min) { }
        public Latitude(GeoCoordinatesSign sign, byte deg, byte min, byte sec) : base(sign, deg, min, sec) { }
        public override decimal MaxValue { get { return 324000; } }
        public override decimal MinValue { get { return -324000; } }

    }

    public class Longitude : Coordinate
    {
        public Longitude(decimal value) : base(value) { }
        public Longitude(GeoCoordinatesSign sign, byte deg, decimal min) : base(sign, deg, min) { }
        public Longitude(GeoCoordinatesSign sign, byte deg, byte min, byte sec) : base(sign, deg, min, sec) { }
        public override decimal MaxValue { get { return 648000; } }
        public override decimal MinValue { get { return -648000; } }
    }

    public struct DegMinSec
    {
        public GeoCoordinatesSign Sign;
        public byte Degrees, Minutes, Seconds;
        public DegMinSec(GeoCoordinatesSign sign, byte degrees, byte minutes, byte seconds)
        {
            this.Sign = sign;
            this.Degrees = degrees;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }
    }

    public enum GeoCoordinatesSign
    {
        /// <summary>
        /// Stands for nothern latitude and eastern longitude
        /// </summary>
        Plus = 1,
        /// <summary>
        /// Stands for southern latitude and western longitude
        /// </summary>
        Minus =-1
    }

}
