using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MonoRail.MonoGame
{
    /// <summary>
    /// Represents a bitmap based sprite that is not animated.
    /// </summary>
    public class StaticSprite : IStaticSprite
    {
        /// <summary>
        /// Gets the name of the image resource to load for the sprite.
        /// </summary>
        public string ResourceName { get; set; }
        public Texture2D Image { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// Draws the sprite to the screen.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to use draw the sprite to the screen.</param>
        /// <param name="screenX">The X Coordinate to draw the sprite onto the screen.</param>
        /// <param name="screenY">The Y Coordinate to draw the sprite onto the screen.</param>
        public void Draw(int screenX, int screenY)
        {
            if (Image != null)
                SpriteBatch.Draw(Image, new Vector2(screenX, screenY), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Updates the sprite once per game cycle.
        /// </summary>
        public void Update()
        {
            
        }

        private bool[] data;
        /// <summary>
        /// Gets an array indicating where visable pixels are in the sprite to enable collision checking.
        /// </summary>
        /// <returns>An array of boolean values going left to right, top to bottom, indicating if there is a pixel at the corrosponding location</returns>
        public bool[] GetCollisionData()
        {
            if (data == null)
            {
                Color[] colorData = new Color[Image.Width * Image.Height];
                Image.GetData(colorData);

                data = new bool[colorData.Length];
                for (int i = 0; i < colorData.Length; i++)
                    data[i] = colorData[i].A != 0;
            }
            return data;
        }

        /// <summary>
        /// Set the image resource that this sprite will draw.
        /// </summary>
        /// <param name="resource">An array containing a Texture2D and a SpriteBatch</param>
        public void SetResources(params object[] resource)
        {
            Image = (Texture2D)resource[0];
            SpriteBatch = (SpriteBatch)resource[1];
        }

        /// <summary>
        /// Gets the height of the sprite.
        /// </summary>
        public int Height
        {
            get { return Image.Bounds.Height; }
        }

        /// <summary>
        /// Gets the width of the sprite.
        /// </summary>
        public int Width
        {
            get { return Image.Bounds.Width; }
        }

        
    }
}
