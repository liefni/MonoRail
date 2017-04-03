using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoRail.Tests.GameObjects;
using MonoRail.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Tests
{
    [TestClass]
    public class GameObjectTests
    {
        [TestMethod]
        public void get_Right_Test()
        {
            GameObject gameObject = new GameObject { X = 20, Width = 30 };
            Assert.AreEqual(50, gameObject.Right);
        }

        [TestMethod]
        public void set_Right_Test()
        {
            GameObject gameObject = new GameObject { Width = 25 };
            gameObject.Right = 60;
            Assert.AreEqual(35, gameObject.X);
        }

        [TestMethod]
        public void get_Bottom_Test()
        {
            GameObject gameObject = new GameObject { Y = 40, Height = 25 };
            Assert.AreEqual(65, gameObject.Bottom);
        }

        [TestMethod]
        public void set_Bottom_Test()
        {
            GameObject gameObject = new GameObject { Height = 35 };
            gameObject.Bottom = 80;
            Assert.AreEqual(45, gameObject.Y);
        }

        [TestMethod]
        public void PreviousX_BehaviourTest()
        {
            Level level = new Level();
            PreviousXYTester gameObject = new PreviousXYTester { X = 25, HSpeed = 10 };
            level.GameObjects.Add(gameObject);
            Simulate.UpdateCycle(level);
            Simulate.UpdateCycle(level);

            Assert.AreEqual(25, gameObject.PreviousX1, "error at 1");
            Assert.AreEqual(35, gameObject.PreviousX2, "error at 2");
            Assert.AreEqual(35, gameObject.PreviousX3, "error at 3");
        }

        [TestMethod]
        public void PreviousY_BehaviourTest()
        {
            Level level = new Level();
            PreviousXYTester gameObject = new PreviousXYTester { Y = 20, VSpeed = 5 };
            level.GameObjects.Add(gameObject);
            Simulate.UpdateCycle(level);
            Simulate.UpdateCycle(level);

            Assert.AreEqual(20, gameObject.PreviousY1, "error at 1");
            Assert.AreEqual(25, gameObject.PreviousY2, "error at 2");
            Assert.AreEqual(25, gameObject.PreviousY3, "error at 3");
        }

        [TestMethod]
        public void HSpeed_Test()
        {
            GameObject gameObject = new GameObject();
            gameObject.HSpeed = 5;
            Assert.AreEqual(5, gameObject.HSpeed);
        }

        [TestMethod]
        public void HSpeed_SetSpeedTest()
        {
            GameObject gameObject = new GameObject();
            gameObject.VSpeed = 3;
            gameObject.HSpeed = 4;
            Assert.AreEqual(5, gameObject.Speed);
        }

        [TestMethod]
        public void HSpeed_SetDirectionTest()
        {
            GameObject gameObject = new GameObject();

            gameObject.HSpeed = 10;        
            Assert.AreEqual(0, gameObject.Direction);

            gameObject.HSpeed = -10;
            Assert.AreEqual(180, gameObject.Direction);

            gameObject.VSpeed = 5;
            gameObject.HSpeed = 5;
            Assert.AreEqual(45, gameObject.Direction);
        }

        [TestMethod]
        public void VSpeed_Test()
        {
            GameObject gameObject = new GameObject();
            gameObject.VSpeed = 5;
            Assert.AreEqual(5, gameObject.VSpeed);
        }

        [TestMethod]
        public void VSpeed_SetSpeedTest()
        {
            GameObject gameObject = new GameObject();
            gameObject.VSpeed = 3;
            gameObject.HSpeed = 4;
            Assert.AreEqual(5, gameObject.Speed);
        }

        [TestMethod]
        public void VSpeed_SetDirectionTest()
        {
            GameObject gameObject = new GameObject();

            gameObject.VSpeed = 10;
            Assert.AreEqual(90, gameObject.Direction);

            gameObject.VSpeed = -10;
            Assert.AreEqual(-90, gameObject.Direction);

            gameObject.HSpeed = 5;
            gameObject.VSpeed = 5;
            Assert.AreEqual(45, gameObject.Direction);
        }

        [TestMethod]
        public void Speed_Test()
        {
            GameObject gameObject = new GameObject();
            gameObject.Speed = 10;
            Assert.AreEqual(10, gameObject.Speed);
        }

        [TestMethod]
        public void Speed_SetHSpeedVSpeedTest()
        {
            GameObject gameObject = new GameObject();
            gameObject.Direction = 30;
            gameObject.Speed = 10;
            Assert.AreEqual(5, Math.Round(gameObject.VSpeed, 3), "Set VSpeed failed");

            gameObject.Direction = 60;
            gameObject.Speed = 8;
            Assert.AreEqual(4, Math.Round(gameObject.HSpeed, 3), "Set HSpeed failed");
        }

        [TestMethod]
        public void Direction_Test()
        {
            GameObject gameObject = new GameObject();
            gameObject.Direction = 30;
            Assert.AreEqual(30, gameObject.Direction);
        }

        [TestMethod]
        public void Direction_SetHSpeedVSpeedTest()
        {
            GameObject gameObject = new GameObject();
            gameObject.Speed = 10;
            gameObject.Direction = 30;
            Assert.AreEqual(5, Math.Round(gameObject.VSpeed, 3), "Set VSpeed failed");

            gameObject.Direction = 60;
            Assert.AreEqual(5, Math.Round(gameObject.HSpeed, 3), "Set HSpeed failed");
        }

        [TestMethod]
        public void Speed_BehaviourTest()
        {
            GameObject gameObject = new GameObject { X = 100, Y = 200, HSpeed = 5, VSpeed = 10 };
            Level level = new Level();
            level.GameObjects.Add(gameObject);

            Simulate.UpdateCycle(level);
            Assert.AreEqual(105, gameObject.X, "on X1");
            Assert.AreEqual(210, gameObject.Y, "on Y1");

            Simulate.UpdateCycle(level);
            Assert.AreEqual(110, gameObject.X, "on X2");
            Assert.AreEqual(220, gameObject.Y, "on Y2");
        }

        [TestMethod]
        public void Gravity_BehaviourTest()
        {
            GameObject gameObject = new GameObject { Y = 200, VSpeed = 3, Gravity = 1 };
            Level level = new Level();
            level.GameObjects.Add(gameObject);

            Simulate.UpdateCycle(level);
            Assert.AreEqual(4, gameObject.VSpeed);

            Simulate.UpdateCycle(level);
            Assert.AreEqual(5, gameObject.VSpeed);
        }

        [TestMethod]
        public void TerminalVelosity_BehaviourTest()
        {
            GameObject gameObject = new GameObject { Y = 200, VSpeed = 9, Gravity = 1, TerminalVelosity = 10 };
            Level level = new Level();
            level.GameObjects.Add(gameObject);

            Simulate.UpdateCycle(level);
            Assert.AreEqual(10, gameObject.VSpeed, "at 1");

            Simulate.UpdateCycle(level);
            Assert.AreEqual(10, gameObject.VSpeed, "at 2");
        }

        [TestMethod]
        public void EnableSolid_FunctionTest()
        {
            Level level = new Level();
            var primary = new ObjectWithSolidAgainst { Width = 30, Height = 30 };
            var other = new SolidBlock { X = 35, Y = 0, Width = 30, Height = 30 };
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);

            Simulate.UpdateCycle(level);
            primary.OnUpdate(p => p.X = 10);

            Simulate.UpdateCycle(level);
            Assert.AreEqual(5, primary.X);

            other.EnableSolid = false;
            Simulate.UpdateCycle(level);
            Assert.AreEqual(10, primary.X);
        }

        [TestMethod]
        public void ContainingLevel_Test()
        {
            GameObject gameObject = new GameObject { Y = 200, VSpeed = 9, Gravity = 1, TerminalVelosity = 10 };
            Level level = new Level();
            level.GameObjects.Add(gameObject);
            Assert.AreEqual(level, gameObject.ContainingLevel);
        }

        [TestMethod]
        public void Sprite_Test()
        {
            GameObject gameObject = new GameObject();
            gameObject.Sprite = TestSprites.Diamond20x10;
            Assert.AreEqual("Diamond20x10", gameObject.Sprite.ResourceName);
            Assert.AreEqual(20, gameObject.Width);
            Assert.AreEqual(10, gameObject.Height);
        }

        [TestMethod]
        public void Draw_VisableTest()
        {
            var gameObject = new GameObject();
            var sprite = TestSprites.Diamond20x10;
            gameObject.Sprite = sprite;
            var level = new Level();
            level.GameObjects.Add(gameObject);
            Assert.AreEqual(0, sprite.TimesDrawn);

            Simulate.UpdateCycle(level);
            Simulate.Draw(level);
            Assert.AreEqual(1, sprite.TimesDrawn);
        }

        [TestMethod]
        public void Draw_NotVisibleTest()
        {
            var gameObject = new GameObject();
            var sprite = TestSprites.Diamond20x10;
            gameObject.Sprite = sprite;
            gameObject.Visible = false;
            var level = new Level();
            level.GameObjects.Add(gameObject);
            Assert.AreEqual(0, sprite.TimesDrawn);

            Simulate.UpdateCycle(level);
            Simulate.Draw(level);
            Assert.AreEqual(0, sprite.TimesDrawn);
        }

        [TestMethod]
        public void CollidesWith_Test()
        {
            GameObject primary = new GameObject { X = 100, Y = 200, Width = 30, Height = 50 };
            GameObject other = new GameObject { X = 100, Y = 200, Width = 35, Height = 55 };
            Level level = new Level();
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);

            Assert.IsTrue(primary.CollidesWith(other, false), "at 1");

            other.X = 129;
            other.Y = 249;
            Assert.IsTrue(primary.CollidesWith(other, false), "at 2");

            other.X = 130;
            Assert.IsFalse(primary.CollidesWith(other, false), "at 3");

            other.X = 129;
            other.Y = 250;
            Assert.IsFalse(primary.CollidesWith(other, false), "at 4");

            other.X = 66;
            other.Y = 146;
            Assert.IsTrue(primary.CollidesWith(other, false), "at 5");

            other.X = 65;
            Assert.IsFalse(primary.CollidesWith(other, false), "at 6");

            other.X = 66;
            other.Y = 145;
            Assert.IsFalse(primary.CollidesWith(other, false), "at 7");
        }

        [TestMethod]
        public void CollidesWith_AtCoordinateTest()
        {
            GameObject primary = new GameObject { X = 0, Y = 5, Width = 30, Height = 50 };
            GameObject other = new GameObject { X = 100, Y = 200, Width = 35, Height = 55 };
            Level level = new Level();
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);

            Assert.IsFalse(primary.CollidesWith(other, false, 200, 100), "at 1");
            Assert.IsTrue(primary.CollidesWith(other, false, 100, 200), "at 2");

            Assert.AreEqual(0, primary.X, "on x");
            Assert.AreEqual(5, primary.Y, "on y");
        }

        [TestMethod]
        public void CollidesWith_CheckPixelsTest()
        {
            GameObject primary = new GameObject { };
            GameObject other = new GameObject { X = 100, Y = 200 };
            Level level = new Level();
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);
            primary.Sprite = TestSprites.Diamond20x10;
            other.Sprite = TestSprites.Diamond20x10;

            Assert.IsFalse(primary.CollidesWith(other, true, 112, 206), "at 1");
            Assert.IsFalse(primary.CollidesWith(other, true, 111, 207), "at 2");

            Assert.IsFalse(primary.CollidesWith(other, true, 102, 209), "at 3");
            Assert.IsTrue(primary.CollidesWith(other, true, 100, 209), "at 4");

            Assert.IsFalse(primary.CollidesWith(other, true, 119, 202), "at 5");
            Assert.IsTrue(primary.CollidesWith(other, true, 119, 200), "at 6");
        }

        [TestMethod]
        public void SolidAgainst_SolidBlockTest()
        {
            var primary = new ObjectWithSolidAgainst { };
            var other = new SolidBlock { X = 100, Y = 200 };
            Level level = new Level();
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);

            SolidTest(level, primary, 050, 150, 080, 180, 070, 170, "top left");
            SolidTest(level, primary, 100, 150, 100, 180, 100, 170, "top");
            SolidTest(level, primary, 150, 150, 120, 180, 130, 170, "top right");
            SolidTest(level, primary, 150, 200, 120, 200, 130, 200, "right");
            SolidTest(level, primary, 150, 250, 120, 220, 130, 230, "bottom right");
            SolidTest(level, primary, 100, 250, 100, 220, 100, 230, "bottom");
            SolidTest(level, primary, 050, 250, 080, 220, 070, 230, "bottom left");
            SolidTest(level, primary, 050, 200, 080, 200, 070, 200, "left");

            level.GameObjects.Add(new SolidBlock { X = 130, Y = 200 });
            level.GameObjects.Add(new SolidBlock { X = 100, Y = 230 });
            SolidTest(level, primary, 150, 250, 120, 220, 130, 230, "inside corner");
        }

        [TestMethod]
        public void SolidAgainst_SolidFromTopTest()
        {
            var primary = new ObjectWithSolidAgainst { };
            var other = new SolidFromTop { X = 100, Y = 200 };
            Level level = new Level();
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);

            SolidTest(level, primary, 100, 150, 100, 180, 100, 170, "top");
            SolidTest(level, primary, 150, 200, 120, 200, 120, 200, "right");
            SolidTest(level, primary, 100, 250, 100, 220, 100, 220, "bottom");
            SolidTest(level, primary, 050, 200, 080, 200, 080, 200, "left");
        }

        [TestMethod]
        public void SolidAgainst_SolidFromRightTest()
        {
            var primary = new ObjectWithSolidAgainst { };
            var other = new SolidFromRight { X = 100, Y = 200 };
            Level level = new Level();
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);

            SolidTest(level, primary, 100, 150, 100, 180, 100, 180, "top");
            SolidTest(level, primary, 150, 200, 120, 200, 130, 200, "right");
            SolidTest(level, primary, 100, 250, 100, 220, 100, 220, "bottom");
            SolidTest(level, primary, 050, 200, 080, 200, 080, 200, "left");
        }

        [TestMethod]
        public void SolidAgainst_SolidFromBottomTest()
        {
            var primary = new ObjectWithSolidAgainst { };
            var other = new SolidFromBottom { X = 100, Y = 200 };
            Level level = new Level();
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);

            SolidTest(level, primary, 100, 150, 100, 180, 100, 180, "top");
            SolidTest(level, primary, 150, 200, 120, 200, 120, 200, "right");
            SolidTest(level, primary, 100, 250, 100, 220, 100, 230, "bottom");
            SolidTest(level, primary, 050, 200, 080, 200, 080, 200, "left");
        }

        [TestMethod]
        public void SolidAgainst_SolidFromLeftTest()
        {
            var primary = new ObjectWithSolidAgainst { };
            var other = new SolidFromLeft { X = 100, Y = 200 };
            Level level = new Level();
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);

            SolidTest(level, primary, 100, 150, 100, 180, 100, 180, "top");
            SolidTest(level, primary, 150, 200, 120, 200, 120, 200, "right");
            SolidTest(level, primary, 100, 250, 100, 220, 100, 220, "bottom");
            SolidTest(level, primary, 050, 200, 080, 200, 070, 200, "left");
        }

        [TestMethod]
        public void SolidAgainst_FromTopLeftTest()
        {
            var primary = new ObjectWithSolidAgainst { };
            var other = new SolidFromTopLeft { X = 100, Y = 200 };
            Level level = new Level();
            level.GameObjects.Add(primary);
            level.GameObjects.Add(other);

            SolidTest(level, primary, 050, 150, 080, 180, 070, 170, "top left");
            SolidTest(level, primary, 100, 150, 100, 180, 100, 170, "top");
            SolidTest(level, primary, 150, 150, 120, 180, 120, 170, "top right");
            SolidTest(level, primary, 150, 200, 120, 200, 120, 200, "right");
            SolidTest(level, primary, 150, 250, 120, 220, 120, 220, "bottom right");
            SolidTest(level, primary, 100, 250, 100, 220, 100, 220, "bottom");
            SolidTest(level, primary, 050, 250, 080, 220, 070, 220, "bottom left");
            SolidTest(level, primary, 050, 200, 080, 200, 070, 200, "left");
        }

        public void SolidTest(Level level, SolidTest primary, int startX, int startY, int moveToX, int moveToY, int expectedX, int expectedY, string testName)
        {
            primary.X = startX;
            primary.Y = startY;
            primary.OnUpdate(null);
            Simulate.UpdateCycle(level);

            primary.OnUpdate(p =>
            {
                p.X = moveToX;
                p.Y = moveToY;
            });
            Simulate.UpdateCycle(level);
            Assert.AreEqual(expectedX, primary.X, testName + " on X");
            Assert.AreEqual(expectedY, primary.Y, testName + " on Y");
        }
    }
}
