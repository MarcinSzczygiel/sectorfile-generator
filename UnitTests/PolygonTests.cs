using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coordinates;
using SectorfileObjects;

namespace UnitTests
{
    [TestClass]
    public class PolygonTests
    {
        [TestMethod]
        public void PolygonToSctTest()
        {
            Polygon poly = new Polygon();
            poly.Add(new GeoPoint(new Longitude(69742), new Latitude(181084)));
            poly.Add(new GeoPoint(new Longitude(69707), new Latitude(181082)));
            poly.Add(new GeoPoint(new Longitude(69672), new Latitude(181081)));

            string result = poly.ToSct();
            //N050.18.04.931 E019.22.22.007 N050.18.02.628 E019.21.47.519

            Assert.AreEqual("N050.18.04.000 E019.22.22.000 N050.18.02.000 E019.21.47.000\nN050.18.02.000 E019.21.47.000 N050.18.01.000 E019.21.12.000\n", result);
        }

        [TestMethod]
        public void PolygonToEseTest()
        {
            Polygon poly = new Polygon();
            poly.Add(new GeoPoint(new Longitude(69742), new Latitude(181084)));
            poly.Add(new GeoPoint(new Longitude(69707), new Latitude(181082)));
            poly.Add(new GeoPoint(new Longitude(69672), new Latitude(181081)));

            string result = poly.ToEse();
            //COORD: N050.18.04.931:E019.22.22.007
            //COORD: N050.18.02.628:E019.21.47.519

            Assert.AreEqual("COORD:N050.18.04.000:E019.22.22.000\nCOORD:N050.18.02.000:E019.21.47.000\nCOORD:N050.18.01.000:E019.21.12.000\n", result);
        }

    }
}
