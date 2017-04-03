using MonoRail.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Tests
{
    public static class TestSprites
    {
        public static TestSprite Diamond20x10
        {
            get
            {
                string bitmap = "";
                bitmap += "         XX         ";
                bitmap += "       XXXXXX       ";
                bitmap += "     XXXXXXXXXX     ";
                bitmap += "  XXXXXXXXXXXXXXXX  ";
                bitmap += "XXXXXXXXXXXXXXXXXXXX";
                bitmap += "XXXXXXXXXXXXXXXXXXXX";
                bitmap += "  XXXXXXXXXXXXXXXX  ";
                bitmap += "     XXXXXXXXXX     ";
                bitmap += "       XXXXXX       ";
                bitmap += "         XX         ";

                var collisionData = bitmap.Where(c => c != '\n' && c != '\r').Select(c => !char.IsWhiteSpace(c)).ToArray();
                return new TestSprite("Diamond20x10", 20, 10, collisionData);
            }
        }
    }
}
