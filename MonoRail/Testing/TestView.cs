using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Testing
{
    public class TestView : IView
    {
        public int FollowBorderBottom { get; set; }
        public int FollowBorderLeft { get; set; }
        public int FollowBorderRight { get; set; }
        public int FollowBorderTop { get; set; }

        public int Height { get; set; }

        public GameObject ObjectToFollow { get; set; }

        public Level Level { get; set; }

        public int ScreenHeight { get; set; }

        public int ScreenWidth { get; set; }

        public int ScreenX { get; set; }

        public int ScreenY { get; set; }

        public int Width { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        
        public void Draw()
        {
            if (ScreenWidth == 0 && ScreenHeight == 0)
            {
                ScreenWidth = Width;
                ScreenHeight = Height;
            }

            foreach (GameObject gameObject in objectsInView)
            {
                gameObject.Draw(gameObject.X - X, gameObject.Y - Y);
            }
        }

        public void SetFollowBorders(int top, int right, int bottom, int left)
        {
            FollowBorderTop = top;
            FollowBorderRight = right;
            FollowBorderBottom = bottom;
            FollowBorderLeft = left;
        }

        private List<GameObject> objectsInView = new List<GameObject>();
        public void Update()
        {
            if (ObjectToFollow != null)
            {
                Rectangle objectToFollowBounds = new Rectangle(
                    FollowBorderLeft,
                    FollowBorderTop,
                    Width - FollowBorderLeft - FollowBorderRight,
                    Height - FollowBorderTop - FollowBorderBottom);

                //find the current absolute bounds with in the level based on the view position in the level
                Rectangle absBounds = new Rectangle(X + objectToFollowBounds.X, Y + objectToFollowBounds.Y,
                objectToFollowBounds.Width, objectToFollowBounds.Height);

                //follow horizontal
                if (ObjectToFollow.X < absBounds.X)
                    X = ObjectToFollow.X - objectToFollowBounds.X;
                else if (ObjectToFollow.Right > absBounds.GetRight())
                    X = ObjectToFollow.Right - objectToFollowBounds.GetRight();

                //follow vertical
                if (ObjectToFollow.Y < absBounds.Y)
                    Y = ObjectToFollow.Y - objectToFollowBounds.Y;
                else if (ObjectToFollow.Bottom > absBounds.GetBottom())
                    Y = ObjectToFollow.Bottom - objectToFollowBounds.GetBottom();

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
    }
}
