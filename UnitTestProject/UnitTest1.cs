using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Math3d;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Identity()
        {
            Point[] srcs = new Point[3]{
                new  Point{ x = -1, y = -1, z = 0 },
                new  Point{ x = -1, y = +1, z = 0 },
                new  Point{ x = +1, y = -1, z = 0 },
            };
            Point[] expects = new Point[3]{
                new  Point{ x = -1, y = -1, z = 0 },
                new  Point{ x = -1, y = +1, z = 0 },
                new  Point{ x = +1, y = -1, z = 0 },
            };

            CoordinateTransform tr = new CoordinateTransform();
            tr.initialize(expects, srcs);

            Point src = new Point { x = +1, y = +1, z = 0 };
            Point expected = new Point { x = +1, y = +1, z = 0 };

            Point dest = tr.convert(src);

            double err = 0.000001;
            Assert.IsTrue((dest.x - err) < expected.x, "the value on the x axis is too small");
            Assert.IsTrue((dest.y - err) < expected.y, "the value on the y axis is too small");
            Assert.IsTrue((dest.z - err) < expected.z, "the value on the z axis is too small");
            Assert.IsTrue(expected.x < (dest.x + err), "the value on the x axis is too large");
            Assert.IsTrue(expected.y < (dest.y + err), "the value on the y axis is too large");
            Assert.IsTrue(expected.z < (dest.z + err), "the value on the z axis is too large");
        }

        [TestMethod]
        public void Transform()
        {
            Point[] srcs = new Point[3]{
                new  Point{ x = -1, y = -1, z = 0 },
                new  Point{ x = -1, y = +1, z = 0 },
                new  Point{ x = +1, y = -1, z = 0 },
            };
            Point[] expects = new Point[3]{
                new  Point{ x = -1, y = -1, z = 1 },
                new  Point{ x = -1, y = +1, z = 1 },
                new  Point{ x = +1, y = -1, z = 1 },
            };

			CoordinateTransform tr = new CoordinateTransform();
            tr.initialize(expects, srcs);

            Point src = new Point { x = +1, y = +1, z = 0 };
            Point expected = new Point { x = +1, y = +1, z = 1};

            Point dest = tr.convert(src);

			double err = 0.000001;
            Assert.IsTrue((dest.x - err) < expected.x, "the value on the x axis is too small");
            Assert.IsTrue((dest.y - err) < expected.y, "the value on the y axis is too small");
            Assert.IsTrue((dest.z - err) < expected.z, "the value on the z axis is too small");
            Assert.IsTrue(expected.x < (dest.x + err), "the value on the x axis is too large");
            Assert.IsTrue(expected.y < (dest.y + err), "the value on the y axis is too large");
            Assert.IsTrue(expected.z < (dest.z + err), "the value on the z axis is too large");
        }

        [TestMethod]
        public void Rotate()
        {
            Point[] srcs = new Point[3]{
                new  Point{ x = -1, y = 0, z = 0 },
                new  Point{ x = 0, y = +1, z = 0 },
                new  Point{ x = +1, y = 0, z = 0 },
            };
            Point[] expects = new Point[3]{
                new  Point{ x = 0, y = +1, z = 0 },
                new  Point{ x = +1, y = 0, z = 0 },
                new  Point{ x = 0, y = -1, z = 0 },
            };

			CoordinateTransform tr = new CoordinateTransform();
            tr.initialize(expects, srcs);

            Point src = new Point { x = 0, y = -1, z = 0 };
            Point expected = new Point { x = -1, y = 0, z = 0 };

            Point dest = tr.convert(src);

			double err = 0.000001;
            Assert.IsTrue((dest.x - err) < expected.x, "the value on the x axis is too small");
            Assert.IsTrue((dest.y - err) < expected.y, "the value on the y axis is too small");
            Assert.IsTrue((dest.z - err) < expected.z, "the value on the z axis is too small");
            Assert.IsTrue(expected.x < (dest.x + err), "the value on the x axis is too large");
            Assert.IsTrue(expected.y < (dest.y + err), "the value on the y axis is too large");
            Assert.IsTrue(expected.z < (dest.z + err), "the value on the z axis is too large");
        }
    }
}
