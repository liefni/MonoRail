using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Testing
{
    public static class Simulate
    {
        public static void Initialize(Level level)
        {
            if (level.ResourceManager == null)
                level.ResourceManager = new TestResourceManager();
            level.DoInit();
        }

        public static void UpdateCycle(Level level)
        {
            Initialize(level);
            level.Update();
        }

        public static void Draw(Level level)
        {
            Initialize(level);
            level.Draw();
        }
    }
}
