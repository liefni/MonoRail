using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.MonoGame
{
    /// <summary>
    /// Represents an area of a level that should be displayed onscreen.
    /// </summary>
    public class View : IView
    {
        GraphicsDevice graphicsDevice;
        Viewport viewport;
        SpriteBatch spriteBatch;

        public View(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            viewport = graphicsDevice.Viewport;
        }
        
        /// <summary>
        /// Gets or sets the level this view is applied to.
        /// </summary>
        public Level Level { get; set; }

        /// <summary>
        /// Gets or sets the offset of the view from the top of the level.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the offset of the view from the right of the level.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the width of the area in the level to display.
        /// </summary>
        public int Width { get; set; } = 800;
        /// <summary>
        /// Gets or sets the height of the area in the level to display.
        /// </summary>
        public int Height { get; set; } = 480;

        /// <summary>
        /// Gets or sets the GameObject that the view will follow.
        /// </summary>
        public GameObject ObjectToFollow { get; set; }

        public int FollowBorderTop { get; set; }
        public int FollowBorderRight { get; set; }
        public int FollowBorderBottom { get; set; }
        public int FollowBorderLeft { get; set; }
        public void SetFollowBorders(int top, int right, int bottom, int left)
        {
            FollowBorderTop = top;
            FollowBorderRight = right;
            FollowBorderBottom = bottom;
            FollowBorderLeft = left;
        }


        public int ScreenX { get; set; }
        public int ScreenY { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        private List<GameObject> objectsInView = new List<GameObject>();

        /// <summary>
        /// Updates the position of the view and finds the objects that need to be drawn.
        /// </summary>
        public virtual void Update()
        {
            if (ObjectToFollow != null)
            {
                var objectToFollowBounds = new Microsoft.Xna.Framework.Rectangle(
                    FollowBorderLeft,
                    FollowBorderTop,
                    Width - FollowBorderLeft - FollowBorderRight,
                    Height - FollowBorderTop - FollowBorderBottom);
                
                //find the current absolute bounds with in the level based on the view position in the level
                var absBounds = new Microsoft.Xna.Framework.Rectangle(X + objectToFollowBounds.X, Y + objectToFollowBounds.Y,
                objectToFollowBounds.Width, objectToFollowBounds.Height);

                //follow horizontal
                if (ObjectToFollow.X < absBounds.X)
                    X = ObjectToFollow.X - objectToFollowBounds.X;
                else if (ObjectToFollow.Right > absBounds.Right)
                    X = ObjectToFollow.Right - objectToFollowBounds.Right;

                //follow vertical
                if (ObjectToFollow.Y < absBounds.Y)
                    Y = ObjectToFollow.Y - objectToFollowBounds.Y;
                else if (ObjectToFollow.Bottom > absBounds.Bottom)
                    Y = ObjectToFollow.Bottom - objectToFollowBounds.Bottom;

                //keep view within level
                //horizontal
                if (X < 0)
                    X = 0;
                else if (X + Width > Level.Width)
                    X = Level.Width - Width;
                //vertical
                if (Y < 0)
                    Y = 0;
                else if (Y + Height > Level.Height)
                    Y = Level.Height - Height;
            }

            //update objects within views bounds that need drawing
            objectsInView.Clear();
            foreach (GameObject gameObject in Level.GameObjects)
            {
                if (this.Intersects(gameObject))
                {
                    objectsInView.Add(gameObject);
                }
            }
        }

        /// <summary>
        /// Draws the area of the level in the view onto the screen.
        /// </summary>
        public virtual void Draw()
        {
            if (ScreenWidth == 0 && ScreenHeight == 0)
            {
                ScreenWidth = Width;
                ScreenHeight = Height;
            }

            if (viewport.X != ScreenX)
                viewport.X = ScreenX;
            if (viewport.Y != ScreenY)
                viewport.Y = ScreenY;
            if (viewport.Width != ScreenWidth)
                viewport.Width = ScreenWidth;
            if (viewport.Height != ScreenHeight)
                viewport.Height = ScreenHeight;

            graphicsDevice.Viewport = viewport;

            spriteBatch.Begin();
            foreach (GameObject gameObject in objectsInView)
            {
                gameObject.Draw(gameObject.X - X, gameObject.Y - Y);
            }
            spriteBatch.End();
        }
    }
}
