using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Tests
{
    [TestClass]
    public class HelpersTests
    {
        [TestMethod]
        public void Intersects_Test()
        {
            Rectangle r1 = new Rectangle(100, 200, 30, 40);
            Rectangle r2;

            //Top Left Corner
            r2 = new Rectangle(66, 155, 35, 45);
            Assert.IsFalse(Helpers.Intersects(r1, r2), "at 1");

            r2 = new Rectangle(65, 156, 35, 45);
            Assert.IsFalse(Helpers.Intersects(r1, r2), "at 2");

            r2 = new Rectangle(66, 156, 35, 45);
            Assert.IsTrue(Helpers.Intersects(r1, r2), "at 3");

            //Top Right Corner
            r2 = new Rectangle(129, 155, 35, 45);
            Assert.IsFalse(Helpers.Intersects(r1, r2), "at 4");

            r2 = new Rectangle(130, 156, 35, 45);
            Assert.IsFalse(Helpers.Intersects(r1, r2), "at 5");

            r2 = new Rectangle(129, 156, 35, 45);
            Assert.IsTrue(Helpers.Intersects(r1, r2), "at 6");

            //Bottom Right Corner
            r2 = new Rectangle(129, 240, 35, 45);
            Assert.IsFalse(Helpers.Intersects(r1, r2), "at 7");

            r2 = new Rectangle(130, 239, 35, 45);
            Assert.IsFalse(Helpers.Intersects(r1, r2), "at 8");

            r2 = new Rectangle(129, 239, 35, 45);
            Assert.IsTrue(Helpers.Intersects(r1, r2), "at 9");

            //Top Left Corner
            r2 = new Rectangle(66, 240, 35, 45);
            Assert.IsFalse(Helpers.Intersects(r1, r2), "at 10");

            r2 = new Rectangle(65, 239, 35, 45);
            Assert.IsFalse(Helpers.Intersects(r1, r2), "at 11");

            r2 = new Rectangle(66, 239, 35, 45);
            Assert.IsTrue(Helpers.Intersects(r1, r2), "at 12");
        }

        [TestMethod]
        public void Populate_Test()
        {
            var stringArray = new string[50].Populate("hello world");
            foreach (var str in stringArray)
                Assert.AreEqual("hello world", str);
        }

        [TestMethod]
        public void GetRight_Test()
        {
            Rectangle r1 = new Rectangle(100, 200, 30, 40);
            Assert.AreEqual(130, Helpers.GetRight(r1));
        }

        [TestMethod]
        public void GetBottom_Test()
        {
            Rectangle r1 = new Rectangle(100, 200, 30, 40);
            Assert.AreEqual(240, Helpers.GetBottom(r1));
        }
    }
}
