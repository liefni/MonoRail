using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoRail.Testing
{
    public class TestSprite : IStaticSprite
    {
        public static Color[] CreateColorFromString(string bitmapDataString)
        {
            return bitmapDataString.Where(c => c != '\n' && c != '\r').Select(c => Char.IsWhiteSpace(c) ? Color.Transparent : Color.Black).ToArray();
        }

        public TestSprite(string resourceName, int width, int height, bool[] data = null)
        {
            ResourceName = resourceName;
            Width = width;
            Height = height;
            if (data == null)
                this.data = new bool[width * height].Populate(true);
            else
                this.data = data;
        }

        public int TimesUpdated { get; private set; }
        public int TimesDrawn { get; private set; }
        public int LastDrawnX { get; private set; }
        public int LastDrawnY { get; private set; }

        public bool ResourceIsLoaded { get; private set; }

        public int Height { get; }

        public string ResourceName { get; set; }

        public int Width { get; }

        public void Draw(int screenX, int screenY)
        {
            TimesDrawn++;
            LastDrawnX = screenX;
            LastDrawnY = screenY;
        }


        private bool[] data;
        public bool[] GetCollisionData()
        {
            return data;
        }

        public void Update()
        {
            TimesUpdated++;
        }

        public void SetResources(params object[] resource)
        {
            ResourceIsLoaded = true;
        }
    }
}
