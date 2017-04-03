using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoRail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGame.GameObjects
{
    public class Block : GameObject
    {
        protected override void Initialize()
        {
            base.Initialize();

            Sprite = ResourceManager.GetSprite<IStaticSprite>("Graphics\\Block");
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
