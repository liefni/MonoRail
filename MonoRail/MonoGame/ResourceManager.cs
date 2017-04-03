using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.MonoGame
{
    public class ResourceManager : IResourceManager
    {
        abstract class SpriteRegistration
        {
            public abstract ISprite CreateSprite(string resourceName);
            public abstract bool IsForInterface<TInterface>();
        }

        class SpriteRegistration<TInterface, TImplementation> : SpriteRegistration
            where TInterface : ISprite
            where TImplementation : TInterface, new()
        {
            ResourceManager resourceManager;
            Action<ResourceManager, TImplementation> configureAction;

            public SpriteRegistration(ResourceManager resourceManager, 
                Action<ResourceManager, TImplementation> configureAction)
            {
                this.resourceManager = resourceManager;
                this.configureAction = configureAction;
            }

            public override ISprite CreateSprite(string resourceName)
            {
                var sprite = new TImplementation();
                sprite.ResourceName = resourceName;
                configureAction(resourceManager, sprite);
                return sprite;
            }

            public override bool IsForInterface<TCheckInterface>()
            {
                return typeof(TInterface) == typeof(TCheckInterface);
            }
        }

        public ContentManager Content { get; }
        public SpriteBatch SpriteBatch { get; }
        public GraphicsDevice GraphicsDevice { get; }

        public ResourceManager(ContentManager content, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            Content = content;
            SpriteBatch = spriteBatch;
            GraphicsDevice = graphicsDevice;

            RegisterSprite<IStaticSprite, StaticSprite>((resourceManage, sprite) => {
                sprite.SpriteBatch = resourceManage.SpriteBatch;
                sprite.Image = resourceManage.Content.Load<Texture2D>(sprite.ResourceName);
            });
        }

        private List<SpriteRegistration> spriteRegistrations = new List<SpriteRegistration>();
        public void RegisterSprite<TInterface, TImplementation>(Action<ResourceManager, TImplementation> configureAction)
            where TInterface : ISprite
            where TImplementation : TInterface, new()
        {
            spriteRegistrations.Add(new SpriteRegistration<TInterface, TImplementation>(this, configureAction));
        }

        public virtual TInterface GetSprite<TInterface>(string resourceName)
            where TInterface : ISprite
        {
            foreach (var spriteRegistration in spriteRegistrations)
            {
                if (spriteRegistration.IsForInterface<TInterface>())
                {
                    TInterface sprite = (TInterface)spriteRegistration.CreateSprite(resourceName);
                    sprite.SetResources(Content.Load<Texture2D>(resourceName), SpriteBatch);
                    return sprite;
                }
            }
            throw new ArgumentException("Unsupported interface.", "TInterface");
        }

        public virtual IView GetView()
        {
            return new View(GraphicsDevice, SpriteBatch);
        }
    }
}
