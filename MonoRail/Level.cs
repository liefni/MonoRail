using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail
{
    /// <summary>
    /// Represents an area with it's own collection of GameObjects
    /// </summary>
    public class Level
    {
        private class GameObjectCollection : CustomCollection<GameObject>
        {
            readonly Level level;

            public GameObjectCollection(Level level)
                :base(new HashSet<GameObject>())
            {
                this.level = level;
            }

            public override void Add(GameObject item)
            {
                if (item.ResourceManager == null)
                    item.ResourceManager = level.ResourceManager;

                AttributeHandler.Current.RegisterAttributesForType(item.GetType());
                item.ContainingLevel = level;
                base.Add(item);
            }
        }

        private class ViewCollection : CustomCollection<IView>
        {
            readonly Level level;

            public ViewCollection(Level level)
                : base(new HashSet<IView>())
            {
                this.level = level;
            }

            public override void Add(IView item)
            {
                AttributeHandler.Current.RegisterAttributesForType(item.GetType());
                item.Level = level;
                base.Add(item);
            }
        }

        /// <summary>
        /// Creates a new level
        /// </summary>
        public Level()
        {
            GameObjects = new GameObjectCollection(this);
            Views = new ViewCollection(this);
        }

        /// <summary>
        /// Gets the collection of GameObjects for this level.
        /// </summary>
        public ICollection<GameObject> GameObjects { get; }

        /// <summary>
        /// Gets or sets the width of the level.
        /// </summary>
        public int Width { get; set; } = 800;
        /// <summary>
        /// Gets or sets the height of the level.
        /// </summary>
        public int Height { get; set; } = 480;

        /// <summary>
        /// Gets or sets the implementation of IResourceLoader used to load resource for this GameObject.
        /// </summary>
        public IResourceManager ResourceManager { get; set; }

        /// <summary>
        /// Gets the loader used to load GameObjects into this level.
        /// </summary>
        public ILevelLoader LevelLoader { get; internal set; }

        /// <summary>
        /// Gets the an IList containing the views for this level.
        /// </summary>
        public ICollection<IView> Views { get; }

        private bool initialized = false;
        internal virtual void DoInit()
        {
            if (!initialized)
            {
                LoadGameObjects();

                Initialize();

                foreach (GameObject gameObject in GameObjects)
                {
                    gameObject.Initialize();
                    gameObject.ContainingLevel = this;
                }

                if (Views.Count == 0)
                {
                    var view = ResourceManager.GetView();
                    view.Width = Width;
                    view.Height = Height;
                    Views.Add(view);
                }
            }
            initialized = true;
        }

        /// <summary>
        /// Initialize the level.
        /// </summary>
        protected virtual void Initialize()
        {
            
        }

        /// <summary>
        /// Loads the GameObjects into the level that will be initially used.
        /// </summary>
        protected virtual void LoadGameObjects()
        {
            if (LevelLoader != null)
                LevelLoader.LoadLevel(this);
        }

        /// <summary>
        /// Updates the level and all it's components every update cycle.
        /// </summary>
        internal protected virtual void Update()
        {
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.DoBeforeUpdate();
            }
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.DoUpdate();
            }
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.DoAfterUpdate();
            }
            foreach (IView view in Views)
            {
                view.Update();
            }
        }

        /// <summary>
        /// Draws the level onto the screen.
        /// </summary>
        internal protected virtual void Draw()
        {
            foreach (IView view in Views)
            {
                view.Draw();
            }
        }
    }
}
