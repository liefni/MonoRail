using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    /// <summary>
    /// Specifies common methods and properties that sprites must implement.
    /// </summary>
    public interface ISprite
    {
        /// <summary>
        /// Updates the sprite once per game cycle.
        /// </summary>
        void Update();

        /// <summary>
        /// Draws the sprite to the screen.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to use to draw the sprite.</param>
        /// <param name="screenX">The X coordinate on the screen to draw the sprite.</param>
        /// <param name="screenY">The Y coordinate on the screen to draw the sprite.</param>
        void Draw(int screenX, int screenY);

        /// <summary>
        /// Gets the width of the sprite.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Gets the height of the sprite.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the name of the image resource to load for the sprite.
        /// </summary>
        string ResourceName { get; set; }
        /// <summary>
        /// Set the resources that the sprite needs to draw itself.
        /// </summary>
        /// <param name="resource">An array of resources used by the sprite.</param>
        void SetResources(params object[] resources);

        /// <summary>
        /// Gets an array indicating where visable pixels are in the sprite to enable collision checking.
        /// </summary>
        /// <returns>An array of boolean values going left to right, top to bottom, indicating if there is a pixel at the corrosponding location</returns>
        bool[] GetCollisionData();
    }
}
