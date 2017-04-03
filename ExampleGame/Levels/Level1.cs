using ExampleGame.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoRail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGame.Levels
{
    public class Level1 : Level
    {
        protected override void Initialize()
        {
            var view1 = ResourceManager.GetView();
            view1.ObjectToFollow = GameObjects.OfType<Player>().First();
            view1.Width = 800;
            view1.Height = 480;
            view1.SetFollowBorders(150, 200, 150, 200);
            Views.Add(view1);

            base.Initialize();
        }
    }
}
