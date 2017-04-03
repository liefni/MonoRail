using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoRail;

namespace ExampleGame.GameObjects
{
    public class Brick : Block
    {
        protected override void Initialize()
        {
            base.Initialize();

            Sprite = ResourceManager.GetSprite<IStaticSprite>("Graphics\\Brick");
        }
    }
}
