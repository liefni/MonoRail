using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    internal sealed class SolidHandler
    {
        private abstract class SolidState
        {
            protected SolidState(Side solidSides, bool checkPixels)
            {
                SolidSides = solidSides;
                CheckPixels = checkPixels;
            }

            public Side SolidSides { get; }
            public bool CheckPixels { get; }

            public abstract void PreventOverlap(GameObject primary);

            public abstract Type OtherType { get; }
        }

        private class SolidState<TOther> : SolidState where TOther : GameObject
        {
            public SolidState(Side solidSides, bool checkPixels)
                : base(solidSides, checkPixels) { }

            public override void PreventOverlap(GameObject primary)
            {
                //do not check if object has not moved
                if (primary.X == primary.PreviousX && primary.Y == primary.PreviousY)
                    return;

                //check if there are any collisions to prevent overlap with
                if (primary.CollidesWithType<TOther>(CheckPixels, primary.X, primary.Y).Any(o => o.EnableSolid))
                {
                    //find if it was the change in the X or Y coordinate that caused overlap
                    bool xMadeCollision = primary.CollidesWithType<TOther>(CheckPixels, primary.X, primary.PreviousY).Any(o => o.EnableSolid);
                    bool yMadeCollision = primary.CollidesWithType<TOther>(CheckPixels, primary.PreviousX, primary.Y).Any(o => o.EnableSolid);

                    //we know that there is currently a collision so 
                    //if neither x or y made collision then then the combined effect of both made the collision
                    if (!xMadeCollision && !yMadeCollision)
                        xMadeCollision = yMadeCollision = true;

                    //ignore collision comming from side that is not solid
                    if ((SolidSides & Side.Top) == 0 && primary.PreviousY < primary.Y)
                        yMadeCollision = false;
                    if ((SolidSides & Side.Left) == 0 && primary.PreviousX < primary.X)
                        xMadeCollision = false;
                    if ((SolidSides & Side.Bottom) == 0 && primary.PreviousY > primary.Y)
                        yMadeCollision = false;
                    if ((SolidSides & Side.Right) == 0 && primary.PreviousX > primary.X)
                        xMadeCollision = false;

                    //get the difference between current and previous x and y 
                    //and the direction (+/-) the difference is in
                    int xCountTo = xMadeCollision ? Math.Abs(primary.PreviousX - primary.X) : 0;
                    int xCountDir = Math.Sign(primary.PreviousX - primary.X);
                    int yCountTo = yMadeCollision ? Math.Abs(primary.PreviousY - primary.Y) : 0;
                    int yCountDir = Math.Sign(primary.PreviousY - primary.Y);

                    //max of x or why that we need to count to
                    int countTo = Math.Max(xCountTo, yCountTo);
                    for (int i = 1; i <= countTo; i++)
                    {
                        //find the x and y that we are going to check for collision at.
                        int testX = primary.X + Math.Min(i, xCountTo) * xCountDir;
                        int testY = primary.Y + Math.Min(i, yCountTo) * yCountDir;
                        //check for colision at test coordinates
                        if (!primary.CollidesWithType<TOther>(CheckPixels, testX, testY).Any())
                        {
                            //change x and y so that objects no longer overlap
                            primary.X = testX;
                            primary.Y = testY;
                            //stop object from moving so that it is no longer moving into object
                            if (xMadeCollision)
                                primary.HSpeed = 0;
                            if (yMadeCollision)
                                primary.VSpeed = 0;
                            break;
                        }
                    }
                }
            }

            public override Type OtherType
            {
                get { return typeof(TOther); }
            }
        }

        private SolidHandler() { }

        public static SolidHandler Current { get; } = new SolidHandler();

        private Dictionary<Type, List<SolidState>> solidStates = new Dictionary<Type, List<SolidState>>();

        /// <summary>
        /// Registers a solid state so that any game object of the TPrimary type cannot pass through
        /// an object of the TOther type if it is coming from a side of the TOther that is solid
        /// </summary>
        /// <typeparam name="TPrimary">The type of object that will not be able to pass though any TOther game objects</typeparam>
        /// <typeparam name="TOther">The type of object that the TPrimary game object type will not be able to pass through</typeparam>
        /// <param name="solidSides">The sides of the TOther game object that the TPrimary game object will not be able to pass through</param>
        /// <param name="checkPixesl">Indicates if pixel collision checking should be used</param>
        public void RegisterSolidState<TPrimary, TOther>(
            Side solidSides,
            bool checkPixels) where TPrimary : GameObject where TOther : GameObject
        {
            if (!solidStates.ContainsKey(typeof(TPrimary)))
                solidStates.Add(typeof(TPrimary), new List<SolidState>());

            var typeSolidStates = solidStates[typeof(TPrimary)];

            var existing = typeSolidStates.FindIndex(s => s.OtherType == typeof(TOther));
            if (existing >= 0)
                typeSolidStates[existing] = new SolidState<TOther>(solidSides, checkPixels);
            else
                typeSolidStates.Add(new SolidState<TOther>(solidSides, checkPixels));
        }

        public void PreventOverlap(GameObject gameObject)
        {
            List<SolidState> typeSolidStates = null;
            solidStates.TryGetValue(gameObject.GetType(), out typeSolidStates);

            if (typeSolidStates != null)
            {
                foreach (SolidState solidState in typeSolidStates)
                {
                    if (solidState.SolidSides != Side.None)
                        solidState.PreventOverlap(gameObject);
                }
            }
        }

    }
}
