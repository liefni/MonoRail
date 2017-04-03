using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    /// <summary>
    /// Represents an object used in the game that has a position in a level and can draw itself onscreen.
    /// </summary>
    public class GameObject : IRectangle
    {
        /// <summary>
        /// Gets or sets the offset from the left of the level.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the offset from the bottom of the level.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the offset of the right side of the GameObject from the left of the level.
        /// </summary>
        public int Right
        {
            get { return X + Width; }
            set { X = value - Width; }
        }
        /// <summary>
        /// Gets or sets the offset of the bottem side of the GameObject from the top of the level.
        /// </summary>
        public int Bottom
        {
            get { return Y + Height; }
            set { Y = value - Height; }
        }

        /// <summary>
        /// Gets the previous value X had before the Update method runs.
        /// </summary>
        public int PreviousX { get; private set; }
        /// <summary>
        /// Gets the previous value Y had before the Update method runs.
        /// </summary>
        public int PreviousY { get; private set; }

        /// <summary>
        /// Gets or sets the width of the GameObject. This is automatically set when the Sprite is set.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Gets or sets the height of the GameObject. This is automatically set when the Sprite is set.
        /// </summary>
        public int Height { get; set; }

        private double hSpeed;
        /// <summary>
        /// Gets or sets the horizontal speed, the amount x will increase each update cycle.
        /// </summary>
        public double HSpeed
        {
            get { return hSpeed; }
            set
            {
                hSpeed = value;
                speed = Math.Sqrt(hSpeed * hSpeed + vSpeed * vSpeed);
                direction = Math.Atan2(vSpeed, hSpeed) * 180 / Math.PI;
            }
        }
        private double vSpeed;
        /// <summary>
        /// Gets or sets the vertical speed, the amount y will increate each update cycle.
        /// </summary>
        public double VSpeed
        {
            get { return vSpeed; }
            set
            {
                vSpeed = value;
                speed = Math.Sqrt(hSpeed * hSpeed + vSpeed * vSpeed);
                direction = Math.Atan2(vSpeed, hSpeed) * 180 / Math.PI;
            }
        }

        private double speed;
        /// <summary>
        /// Gets or sets the total speed in the objects current direction. This will effect both HSpeed and VSpeed.
        /// </summary>
        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                hSpeed = speed * Math.Cos(direction / 180 * Math.PI);
                vSpeed = speed * Math.Sin(direction / 180 * Math.PI);
            }
        }

        /// <summary>
        /// Gets or sets the direction of movement, measure in degrees clockwise from right.
        /// </summary>
        private double direction;
        public double Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                hSpeed = speed * Math.Cos(direction / 180 * Math.PI);
                vSpeed = speed * Math.Sin(direction / 180 * Math.PI);
            }
        }

        /// <summary>
        /// Gets or sets the the amount that the VSpeed will increase each update cycle.
        /// </summary>
        public double Gravity { get; set; }
        /// <summary>
        /// Gets or sets the maximum vertical speed.
        /// </summary>
        public double TerminalVelosity { get; set; }

        /// <summary>
        /// Indicates if this object will prevent other registered solid objects from passing through it
        /// </summary>
        public bool EnableSolid { get; set; } = true;


        /// <summary>
        /// Gets the level which this GameObject is placed in.
        /// </summary>
        public Level ContainingLevel { get; internal set; }

        /// <summary>
        /// Gets or sets the implementation of IResourceLoader used to load resource for this GameObject.
        /// </summary>
        public IResourceManager ResourceManager { get; set; }

        public bool Visible { get; set; } = true;

        private ISprite _sprite;
        /// <summary>
        /// Gets or sets the sprite that this object will draw.
        /// </summary>
        public virtual ISprite Sprite
        {
            get { return _sprite; }
            set
            {
                _sprite = value;
                Width = _sprite.Width;
                Height = _sprite.Height;
            }
        }

        /// <summary>
        /// Performs initialisation for GameObject
        /// </summary>
        internal protected virtual void Initialize()
        {

        }

        internal void DoBeforeUpdate()
        {
            BeforeUpdate();

            PreviousX = X;
            PreviousY = Y;
        }

        /// <summary>
        /// Runs before the GameObject updates itself during the update cycle.
        /// </summary>
        protected virtual void BeforeUpdate()
        {

        }

        internal void DoUpdate()
        {
            Update();

            move();
            SolidHandler.Current.PreventOverlap(this);
        }

        /// <summary>
        /// Runs code to update the GameObject each update cycle.
        /// </summary>
        protected virtual void Update()
        {
            
        }

        internal void DoAfterUpdate()
        {
            AfterUpdate();
        }
        
        /// <summary>
        /// Runs after the GameObject updates itself duing the update cycle.
        /// </summary>
        protected virtual void AfterUpdate()
        {

        }

        private void move()
        {
            //accelerate with gravity
            VSpeed += Gravity;
            //limit speed to terminal velosity
            if (TerminalVelosity > 0 && VSpeed > TerminalVelosity)
                VSpeed = TerminalVelosity;
            else if (TerminalVelosity < 0 && VSpeed < TerminalVelosity)
                VSpeed = TerminalVelosity;

            //move object based on speed
            X += (int)HSpeed;
            Y += (int)VSpeed;
        }

        /// <summary>
        /// Draws the object onto the screen.
        /// </summary>
        /// <param name="screenX">The X coordinate on the screen that the object should appear.</param>
        /// <param name="screenY">The Y coordinate on the screen that the object should appear.</param>
        protected internal virtual void Draw(int screenX, int screenY)
        {
            if (Visible)
                Sprite.Draw(screenX, screenY);
        }


        /// <summary>
        /// Checks to see if the GameObject colides with any other object of the specified type.
        /// </summary>
        /// <typeparam name="TOther">The other type to check for collisions with.</typeparam>
        /// <param name="checkPixels">Value indicating if pixel collision checking should be used.</param>
        /// <returns>An IEnumerable of objects that this object colides with.</returns>
        public IEnumerable<TOther> CollidesWithType<TOther>(bool checkPixels) where TOther : GameObject
        {
            return CollidesWithType<TOther>(checkPixels, X, Y);
        }

        /// <summary>
        /// Checks to see if the GameObject colides with any other object of the specified type when moved to the specified coordinates.
        /// </summary>
        /// <typeparam name="TOther">The other type to check for collisions with.</typeparam>
        /// <param name="checkPixels">Value indicating if pixel collision checking should be used.</param>
        /// <param name="atX">X coordinate to check for collision at.</param>
        /// <param name="atY">Y coordinate to check for collision at.</param>
        /// <returns>An IEnumerable of objects that this object colides with.</returns>
        public IEnumerable<TOther> CollidesWithType<TOther>(bool checkPixels, int atX, int atY) where TOther : GameObject
        {
            foreach (GameObject gameObject in ContainingLevel.GameObjects)
            {
                if (gameObject is TOther)
                {
                    if (CollidesWith(gameObject, checkPixels, atX, atY))
                        yield return (TOther)gameObject;
                }
            }
        }

        /// <summary>
        /// Checks to see if the GameObject collides with another GameObject.
        /// </summary>
        /// <param name="other">The other GameObject to check for collision with</param>
        /// <param name="checkPixels">Value indicating if pixel collision checking should be used.</param>
        /// <returns>Value indicating if there is a collision with the specified object.</returns>
        public bool CollidesWith(GameObject other, bool checkPixels)
        {
            return CollidesWith(other, checkPixels, X, Y);
        }

        /// <summary>
        /// Checks to see if the GameObject collides with another GameObject when moved to the specified coordinates.
        /// </summary>
        /// <param name="other">The other GameObject to check for collision with</param>
        /// <param name="checkPixels">Value indicating if pixel collision checking should be used.</param>
        /// <param name="atX">X coordinate to check for collision at.</param>
        /// <param name="atY">Y coordinate to check for collision at.</param>
        /// <returns>Value indicating if there is a collision with the specified object.</returns>
        public bool CollidesWith(GameObject other, bool checkPixels, int atX, int atY)
        {
            //get current coordinate
            int currentX = X;
            int currentY = Y;
            
            try
            {
                //set coordinate to coordinates to check for collision
                X = atX;
                Y = atY;

                return this.Intersects(other)// If simple intersection fails, don't even bother with per-pixel
                    && (!checkPixels || PerPixelCollision(this, other));
            }
            finally
            {
                //set coordinate back to initial value
                X = currentX;
                Y = currentY;
            }
        }

        static bool PerPixelCollision(GameObject a, GameObject b)
        {
            //Get collision data of each sprite
            bool[] bitsA = a.Sprite.GetCollisionData();
            bool[] bitsB = b.Sprite.GetCollisionData();

            int x1 = Math.Max(a.X, b.X);
            int x2 = Math.Min(a.X + a.Sprite.Width, b.X + b.Sprite.Width);

            int y1 = Math.Max(a.Y, b.Y);
            int y2 = Math.Min(a.Y + a.Sprite.Height, b.Y + b.Sprite.Height);

            //For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; ++y)
            {
                for (int x = x1; x < x2; ++x)
                {
                    //check if value is true (indicating that there is a non transparent pixel)
                    //at that position is the collision data
                    bool cA = bitsA[(x - a.X) + (y - a.Y) * a.Width];
                    bool cB = bitsB[(x - b.X) + (y - b.Y) * b.Width];

                    // If both values are true (they both have a non transparent pixel in that position), then there is a collision
                    if (cA && cB) 
                    {
                        return true;
                    }
                }
            }
            // If no collision occurred by now, we're clear.
            return false;
        }
    }
}
