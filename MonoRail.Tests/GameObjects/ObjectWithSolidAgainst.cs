using MonoRail.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Tests.GameObjects
{
    public class SolidTest : GameObject
    {
        public SolidTest()
        {
            Width = 30;
            Height = 30;
        }

        Action<GameObject> onUpdate;
        public void OnUpdate(Action<GameObject> onUpdate)
        {
            this.onUpdate = onUpdate;
        }

        protected override void Update()
        {
            onUpdate?.Invoke(this);

            base.Update();
        }
    }

    [SolidAgainst(typeof(SolidBlock), Side.All, false)]
    [SolidAgainst(typeof(SolidFromTop), Side.Top, false)]
    [SolidAgainst(typeof(SolidFromTopLeft), Side.Top | Side.Left, false)]
    [SolidAgainst(typeof(SolidFromLeft), Side.Left, false)]
    [SolidAgainst(typeof(SolidFromRight), Side.Right, false)]
    [SolidAgainst(typeof(SolidFromBottom), Side.Bottom, false)]
    public class ObjectWithSolidAgainst : SolidTest
    {
        
    }

    public class SolidFromTop : SolidTest { }
    public class SolidFromTopLeft : SolidTest { }

    public class SolidFromLeft : SolidTest { }
    public class SolidFromRight : SolidTest { }
    public class SolidFromBottom : SolidTest { }
    public class SolidBlock : SolidTest { }
}
