﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Grib.Api.Interop;
using System.Text.RegularExpressions;

namespace Grib.Api.Tests
{
    [TestFixture]
    public class Get
    {
        [Test]
        public void TestGetBox() 
        {
            using (GribFile file = new GribFile(Settings.GAUSS)) 
            {
                Assert.IsTrue(file.MessageCount > 0);
                foreach (var msg in file)
                {
                    var pts = msg.Box(new GeoCoordinate(60, -10), new GeoCoordinate(10, 30));
                    foreach (var val in pts.Latitudes)
                    {
                        Assert.GreaterOrEqual(60, val);
                        Assert.LessOrEqual(10, val);
                    }

                    foreach (var val in pts.Longitudes)
                    {
                        Assert.GreaterOrEqual(val, -10);
                        Assert.LessOrEqual(val, 30);
                    }
                }
            }
        }

        [Test]
        public void TestOpenPng()
        {
            using (GribFile file = new GribFile(Settings.PNG_COMPRESSION))
            {
                Assert.IsTrue(file.MessageCount > 0);
                foreach (var msg in file)
                {
                    Assert.IsTrue(msg["packingType"].AsString().ToLower().Contains("png"));
                    Assert.IsTrue(msg.ValuesCount > 0);
                }
            }
        }

        [Test]
        public void TestOpenComplex ()
        {
            using (GribFile file = new GribFile(Settings.COMPLEX_GRID))
            {
                Assert.IsTrue(file.MessageCount > 0);
                foreach (var msg in file)
                {
                    Assert.IsTrue(msg["packingType"].AsString().ToLower().Contains("complex"));
                    Assert.IsTrue(msg.ValuesCount > 0);
                }
            }
        }

        [Test]
        public void TestGetCounts()
        {
            using (GribFile file = new GribFile(Settings.GRIB))
            {
                Assert.IsTrue(file.MessageCount > 0);
                foreach(var msg in file)
                {
                    Assert.AreNotEqual(msg.DataPointsCount, 0);
                    Assert.AreNotEqual(msg.ValuesCount, 0);
                    Assert.AreEqual(msg.ValuesCount, msg["numberOfCodedValues"].AsInt());
                    Assert.IsTrue(msg["numberOfCodedValues"].IsReadOnly);
                    Assert.AreEqual(msg.DataPointsCount, msg.ValuesCount + msg.MissingCount);
                }
            }
        }

        [Test]
        public void TestGetVersion()
        {
            Regex re = new Regex(@"^(\d+\.)?(\d+\.)?(\*|\d+)$");
            Assert.IsTrue(re.IsMatch(GribEnvironment.GribApiVersion));
        }

        [Test]
        public void TestGetNativeType()
        {
            using (GribFile file = new GribFile(Settings.REG_LATLON_GRB1))
            {
                var msg = file.First();
                Assert.AreEqual(msg["packingType"].NativeType, GribValueType.String);
                Assert.AreEqual(msg["longitudeOfFirstGridPointInDegrees"].NativeType, GribValueType.Double);
                Assert.AreEqual(msg["numberOfPointsAlongAParallel"].NativeType, GribValueType.Int);
                Assert.AreEqual(msg["values"].NativeType, GribValueType.DoubleArray);

                // TODO: test other types
            }
        }

        [Test]
        public void TestCanConvertToDegress ()
        {
            using (GribFile file = new GribFile(Settings.REDUCED_LATLON_GRB2))
            {
                var msg = file.First();

                // true
                Assert.IsTrue(msg["latitudeOfFirstGridPointInDegrees"].CanConvertToDegrees);
                Assert.IsTrue(msg["latitudeOfFirstGridPoint"].CanConvertToDegrees);
                Assert.IsTrue(msg["longitudeOfFirstGridPointInDegrees"].CanConvertToDegrees);
                Assert.IsTrue(msg["longitudeOfFirstGridPoint"].CanConvertToDegrees);
                Assert.IsTrue(msg["latitudeOfLastGridPointInDegrees"].CanConvertToDegrees);
                Assert.IsTrue(msg["latitudeOfLastGridPoint"].CanConvertToDegrees);
                Assert.IsTrue(msg["jDirectionIncrement"].CanConvertToDegrees);
                Assert.IsTrue(msg["iDirectionIncrement"].CanConvertToDegrees);

                // false
                Assert.IsFalse(msg["numberOfPointsAlongAParallel"].CanConvertToDegrees);
                Assert.IsFalse(msg["numberOfPointsAlongAParallelInDegrees"].CanConvertToDegrees);
                Assert.IsFalse(msg["numberOfPointsAlongAMeridian"].CanConvertToDegrees);
                Assert.IsFalse(msg["numberOfPointsAlongAMeridianInDegrees"].CanConvertToDegrees);
                Assert.IsFalse(msg["packingType"].CanConvertToDegrees);
            }
        }

        [Test]
        public void TestGetGrib2 ()
        {
            double delta = 0.1d;

            using (GribFile file = new GribFile(Settings.REDUCED_LATLON_GRB2))
            {
                var msg = file.First();

                // "InDegrees" is a magic token that converts coordinate double values to degrees
                // explicit degree conversion via key name
                double latitudeOfFirstGridPointInDegrees = msg["latitudeOfFirstGridPoint"].AsDouble();
                Assert.AreEqual(latitudeOfFirstGridPointInDegrees, 90, delta);

                // degree conversion via accessor
                double longitudeOfFirstGridPointInDegrees = msg["longitudeOfFirstGridPoint"].AsDouble(/* inDegrees == true */);
                Assert.AreEqual(longitudeOfFirstGridPointInDegrees, 0, delta);

                // degree conversion via accessor
                double latitudeOfLastGridPointInDegrees = msg["latitudeOfLastGridPoint"].AsDouble(/* inDegrees == true */);
                Assert.AreEqual(latitudeOfLastGridPointInDegrees, -90, delta);

                // degree conversion via accessor
                double longitudeOfLastGridPointInDegrees = msg["longitudeOfLastGridPoint"].AsDouble(/* inDegrees == true */);
                Assert.AreEqual(longitudeOfLastGridPointInDegrees, 360, .5);

                // degree conversion via accessor
                double jDirectionIncrementInDegrees = msg["jDirectionIncrement"].AsDouble(/* inDegrees == true */);
                Assert.AreEqual(jDirectionIncrementInDegrees, 0.36, delta);

                // degree conversion via accessor
                double iDirectionIncrementInDegrees = msg["iDirectionIncrement"].AsDouble(/* inDegrees == true */);
                Assert.AreEqual(iDirectionIncrementInDegrees, -1.0E+100, delta);

                int numberOfPointsAlongAParallel = msg["numberOfPointsAlongAParallel"].AsInt();
                Assert.AreEqual(numberOfPointsAlongAParallel, -1);

                int numberOfPointsAlongAMeridian = msg["numberOfPointsAlongAMeridian"].AsInt();
                Assert.AreEqual(numberOfPointsAlongAMeridian, 501);

                string packingType = msg["packingType"].AsString();
                Assert.AreEqual("grid_simple", packingType);             
            }
        }

                [Test]
        public void TestGetParallel ()
        {
            var files = new[] { Settings.REDUCED_LATLON_GRB2, Settings.BIN, Settings.COMPLEX_GRID, Settings.PNG_COMPRESSION };
            object l = new object();
            Parallel.ForEach(files, (path, s) =>
            {
                using (var file = new GribFile(path))
                {
                    //GribMessage m = null;
                    //lock (l)
                    //{
                    //    m = ((IEnumerable<GribMessage>) file).First();
                    //}
                    //foreach (var val in m.GeoSpatialValues)
                    //{
                    //    if (val.IsMissing) continue;
                    //    Console.WriteLine(" Lat: {0} Lon: {1} Val: {2}", val.Latitude, val.Longitude, val.Value);
                    //}
                    var msgs = (IEnumerable<GribMessage>) file;
                //    var foo = msgs.First();
                    Parallel.ForEach(msgs, (msg, s2) =>
{
    //Console.WriteLine(msg.ToString());
    Console.WriteLine(path);
    Console.WriteLine(msg.Index);
    //foreach (var val in msg.GeoSpatialValues)
    //{
    //    if (val.IsMissing) continue;
    //    Console.WriteLine(path);
    //    Console.WriteLine(msg.Index);
    //   // Console.WriteLine(" Lat: {0} Lon: {1} Val: {2}", val.Latitude, val.Longitude, val.Value);
    //}
});
                    //foreach (var msg in file)
                    //{

                    //    foreach (var val in msg.GeoSpatialValues)
                    //    {
                    //        if (val.IsMissing) continue;
                    //        Console.WriteLine(path);
                    //        Console.WriteLine(" Lat: {0} Lon: {1} Val: {2}", val.Latitude, val.Longitude, val.Value);
                    //    }
                    //}
                }

            });
        }
    }
}
