using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Tests.GameObjects
{
    public class PreviousXYTester : GameObject
    {
        public PreviousXYTester()
        {
            Width = 30;
            Height = 30;
        }

        public int PreviousX1 { get; set; }
        public int PreviousY1 { get; set; }
        public int PreviousX2 { get; set; }
        public int PreviousY2 { get; set; }
        public int PreviousX3 { get; set; }
        public int PreviousY3 { get; set; }

        protected override void BeforeUpdate()
        {
            PreviousX1 = PreviousX;
            PreviousY1 = PreviousY;

            base.BeforeUpdate();
        }

        protected override void Update()
        {
            PreviousX2 = PreviousX;
            PreviousY2 = PreviousY;

            base.Update();
        }

        protected override void AfterUpdate()
        {
            PreviousX3 = PreviousX;
            PreviousY3 = PreviousY;

            base.AfterUpdate();
        }
    }
}
