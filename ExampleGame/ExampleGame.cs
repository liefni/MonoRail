using ExampleGame.GameObjects;
using ExampleGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoRail;
using MonoRail.MonoGame;
using MonoRail.TextLevel;

namespace ExampleGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ExampleGame : BaseGame
    {
        protected override void Initialize()
        {
            base.Initialize();

            LevelLoadFormat = new TextLevelFormat("Levels", ".txt",
                new GameObjectNotation<Brick>('O'),
                new GameObjectNotation<Block>('^'),
                new GameObjectNotation<Player>('X')
                );

            ChangeLevel(new Level1());
        }
    }
}
