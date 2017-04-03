using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoRail.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MonoRail.MonoGame
{
    public class BaseGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Viewport defaultViewPort;

        public Level CurrentLevel { get; private set; } = new Level();

        public BaseGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        public void ChangeLevel(Level newLevel)
        {
            newLevel.ResourceManager = ResourceManager;

            if (newLevel.LevelLoader == null && LevelLoadFormat != null)
                newLevel.LevelLoader = LevelLoadFormat.GetLevelLoader(newLevel);

            CurrentLevel = newLevel;
            newLevel.DoInit();
        }

        public ILevelFormat LevelLoadFormat { get; set; }

        public IResourceManager ResourceManager { get; set; }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            defaultViewPort = GraphicsDevice.Viewport;

            ResourceManager = new ResourceManager(Content, spriteBatch, GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CurrentLevel.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Viewport = defaultViewPort;
            GraphicsDevice.Clear(Color.Black);

            CurrentLevel.Draw();

            base.Draw(gameTime);
        }
    }
}
