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

            Assert.AreEqual("N050.18.04.000 E019.22.22.000 N050.18.02.000 E019.21.47.000\nN050.18.02.000 E019.21.47.000 N050.18.01.000 E019.21.12.000\n", result);
        }

        [TestMethod]
        public void ClosedPolygonToSctTest()
        {
            Polygon poly = new Polygon();
            poly.Add(new GeoPoint(new Longitude(69742), new Latitude(181084)));
            poly.Add(new GeoPoint(new Longitude(69707), new Latitude(181082)));
            poly.Add(new GeoPoint(new Longitude(69672), new Latitude(181081)));
            poly.Close();

            string result = poly.ToSct();

            Assert.AreEqual("N050.18.04.000 E019.22.22.000 N050.18.02.000 E019.21.47.000\nN050.18.02.000 E019.21.47.000 N050.18.01.000 E019.21.12.000\nN050.18.01.000 E019.21.12.000 N050.18.04.000 E019.22.22.000\n", result);
        }

        [TestMethod]
        public void NamedPolygonToSctTest()
        {
            Polygon poly = new Polygon();
            poly.Add(new GeoPoint(new Longitude(69742), new Latitude(181084)));
            poly.Add(new GeoPoint(new Longitude(69707), new Latitude(181082)));
            poly.Add(new GeoPoint(new Longitude(69672), new Latitude(181081)));
            poly.Close();
            poly.Comment = "new TMA element";
            poly.Title = "EPWA_TMA_X";

            string result = poly.ToSct();

            Assert.AreEqual(";new TMA element\nEPWA_TMA_X N050.18.04.000 E019.22.22.000 N050.18.02.000 E019.21.47.000\nN050.18.02.000 E019.21.47.000 N050.18.01.000 E019.21.12.000\nN050.18.01.000 E019.21.12.000 N050.18.04.000 E019.22.22.000\n", result);
        }

        [TestMethod]
        public void PolygonToEseTest()
        {
            Polygon poly = new Polygon();
            poly.Add(new GeoPoint(new Longitude(69742), new Latitude(181084)));
            poly.Add(new GeoPoint(new Longitude(69707), new Latitude(181082)));
            poly.Add(new GeoPoint(new Longitude(69672), new Latitude(181081)));

            string result = poly.ToEse();

            Assert.AreEqual("COORD:N050.18.04.000:E019.22.22.000\nCOORD:N050.18.02.000:E019.21.47.000\nCOORD:N050.18.01.000:E019.21.12.000\n", result);
        }

        [TestMethod]
        public void ClosedPolygonToEseTest()
        {
            Polygon poly = new Polygon();
            poly.Add(new GeoPoint(new Longitude(69742), new Latitude(181084)));
            poly.Add(new GeoPoint(new Longitude(69707), new Latitude(181082)));
            poly.Add(new GeoPoint(new Longitude(69672), new Latitude(181081)));
            poly.Close();

            string result = poly.ToEse();

            Assert.AreEqual("COORD:N050.18.04.000:E019.22.22.000\nCOORD:N050.18.02.000:E019.21.47.000\nCOORD:N050.18.01.000:E019.21.12.000\nCOORD:N050.18.04.000:E019.22.22.000\n", result);
        }
        
        [TestMethod]
        public void NamedPolygonToEseTest()
        {
            Polygon poly = new Polygon();
            poly.Add(new GeoPoint(new Longitude(69742), new Latitude(181084)));
            poly.Add(new GeoPoint(new Longitude(69707), new Latitude(181082)));
            poly.Add(new GeoPoint(new Longitude(69672), new Latitude(181081)));
            poly.Close();
            poly.Comment = "new TMA element";
            poly.Title = "EPWA_TMA_X";

            string result = poly.ToEse();

            Assert.AreEqual(";new TMA element\nEPWA_TMA_X\nCOORD:N050.18.04.000:E019.22.22.000\nCOORD:N050.18.02.000:E019.21.47.000\nCOORD:N050.18.01.000:E019.21.12.000\nCOORD:N050.18.04.000:E019.22.22.000\n", result);
        }
    }
}
