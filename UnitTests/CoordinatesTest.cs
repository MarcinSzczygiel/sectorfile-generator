using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coordinates;

namespace UnitTests
{
    [TestClass]
    public class CoordinatesTest
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"TestSourceData\CoordinatesInitTestObjectList.csv",
            "CoordinatesInitTestObjectList#csv", DataAccessMethod.Sequential)]
        public void TestLongitudeInitByDegMinSec()
        {
            //arrange
            decimal ResultLon = decimal.Parse(TestContext.DataRow["ResultLon"].ToString());
            GeoCoordinatesSign sign = (GeoCoordinatesSign)decimal.Parse(TestContext.DataRow["LonSign"].ToString());
            byte deg = byte.Parse(TestContext.DataRow["LonDeg"].ToString());
            byte min = byte.Parse(TestContext.DataRow["LonMin"].ToString());
            byte sec = byte.Parse(TestContext.DataRow["LonSec"].ToString());
            GeoPoint coord = new GeoPoint(new Longitude(sign, deg, min, sec), new Latitude(GeoCoordinatesSign.Plus, 10, 2, 3));

            //assert
            Assert.AreEqual(ResultLon, coord.Longitude.Value);

        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"TestSourceData\CoordinatesInitTestObjectList.csv",
            "CoordinatesInitTestObjectList#csv", DataAccessMethod.Sequential)]
        public void TestLatitudeInitByDegMinSec()
        {
            //arrange
            decimal ResultLat = decimal.Parse(TestContext.DataRow["ResultLat"].ToString());
            GeoCoordinatesSign sign = (GeoCoordinatesSign)decimal.Parse(TestContext.DataRow["LatSign"].ToString());
            byte deg = byte.Parse(TestContext.DataRow["LatDeg"].ToString());
            byte min = byte.Parse(TestContext.DataRow["LatMin"].ToString());
            byte sec = byte.Parse(TestContext.DataRow["LatSec"].ToString());
            GeoPoint coord = new GeoPoint(new Longitude(GeoCoordinatesSign.Plus, 10, 2, 3), new Latitude(sign, deg, min, sec));

            //assert
            Assert.AreEqual(ResultLat, coord.Latitude.Value);

        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"TestSourceData\CoordinatesInitTestObjectList.csv",
            "CoordinatesInitTestObjectList#csv", DataAccessMethod.Sequential)]
        public void TestGetDegMinSecFromLongitude()
        {
            //arrange
            decimal GivenLon = decimal.Parse(TestContext.DataRow["ResultLon"].ToString());
            GeoCoordinatesSign sign = (GeoCoordinatesSign)decimal.Parse(TestContext.DataRow["LonSign"].ToString());
            byte deg = byte.Parse(TestContext.DataRow["LonDeg"].ToString());
            byte min = byte.Parse(TestContext.DataRow["LonMin"].ToString());
            byte sec = byte.Parse(TestContext.DataRow["LonSec"].ToString());
            DegMinSec expected = new DegMinSec(sign, deg, min, sec);
            GeoPoint coord = new GeoPoint(new Longitude(GivenLon), new Latitude(3661));

            //act
            DegMinSec actual = coord.Longitude.ToDegMinSec();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"TestSourceData\CoordinatesInitTestObjectList.csv",
            "CoordinatesInitTestObjectList#csv", DataAccessMethod.Sequential)]
        public void TestGetDegMinSecFromLatitude()
        {
            //arrange
            decimal GivenLat = decimal.Parse(TestContext.DataRow["ResultLat"].ToString());
            GeoCoordinatesSign sign = (GeoCoordinatesSign)decimal.Parse(TestContext.DataRow["LatSign"].ToString());
            byte deg = byte.Parse(TestContext.DataRow["LatDeg"].ToString());
            byte min = byte.Parse(TestContext.DataRow["LatMin"].ToString());
            byte sec = byte.Parse(TestContext.DataRow["LatSec"].ToString());
            DegMinSec expected = new DegMinSec(sign, deg, min, sec);
            GeoPoint coord = new GeoPoint(new Longitude(3661), new Latitude(GivenLat));

            //act
            DegMinSec actual = coord.Latitude.ToDegMinSec();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestParseCoordinatesString()
        {
            //arrange
            string given = "52°41'55''N 020°59'26''E";
            GeoPoint expected = new GeoPoint(new Longitude(GeoCoordinatesSign.Plus, 20, 59, 26),
                new Latitude(GeoCoordinatesSign.Plus, 52, 41, 55));

            //act
            GeoPoint coord = GeoPoint.Parse(given);

            //assert
            Assert.AreEqual(expected, coord);
        }

        public TestContext TestContext { get; set; }
    }
}
