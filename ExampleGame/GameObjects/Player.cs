using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoRail;
using MonoRail.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGame.GameObjects
{
    [SolidAgainst(typeof(Brick), Side.All, true)]
    [SolidAgainst(typeof(Block), Side.Top, true)]
    public class Player : GameObject
    {
        float maxJumpSpeed = 15;
        int jumpDelay = 10;
        float maxMoveSpeed = 4;
        float moveAcc = 0.25f;
        float doJumpSpeed = 0;

        protected override void Initialize()
        {
            base.Initialize();

            Sprite = ResourceManager.GetSprite<IStaticSprite>("Graphics\\Player");

            Gravity = 1;
            TerminalVelosity = 10;
        }

        KeyboardState keyboardState;
        protected override void Update()
        {
            keyboardState = Keyboard.GetState();

            MoveHorizontal();
            Jump();

            base.Update();
        }

        private void MoveHorizontal()
        {
            bool keyLeft = keyboardState.IsKeyDown(Keys.Left);
            bool keyRight = keyboardState.IsKeyDown(Keys.Right);

            //accelerate left/right if keypressed
            int moveDir = (keyLeft ? -1 : 0) + (keyRight ? 1 : 0);
            if (moveDir == 0)
                HSpeed -= moveAcc * Math.Sign(HSpeed);
            //otherwise deaccelerate
            else if (Math.Abs(HSpeed) < maxMoveSpeed)
                HSpeed += moveAcc * moveDir;
        }

        private void Jump()
        {
            bool keyJump = keyboardState.IsKeyDown(Keys.Space);

            //build up jump speed while jump key is down
            if (VSpeed == 0 && CollidesWithType<Block>(false, X, Y + 1).Any())
            {
                if (keyJump && doJumpSpeed < maxJumpSpeed)
                    doJumpSpeed += maxJumpSpeed / jumpDelay;
            }
            else
                doJumpSpeed = 0;

            //do jump when jump key released or maximum speed built up
            if ((!keyJump && doJumpSpeed > 0) || (doJumpSpeed >= maxJumpSpeed))
            {
                VSpeed += -doJumpSpeed - Gravity;
                doJumpSpeed = 0;
            }
        }
    }
}
