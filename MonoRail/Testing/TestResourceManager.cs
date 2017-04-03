using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.Testing
{
    public class TestResourceManager : IResourceManager
    {
        public List<TestSprite> TestSprites { get; } = new List<TestSprite>();

        public int DefaultWidth { get; set; } = 32;
        public int DefaultHeight { get; set; } = 32;

        public ISprite LoadResources(ISprite sprite)
        {
            if (sprite is TestSprite)
                return sprite;

            foreach (var testSprite in TestSprites)
            {
                if (testSprite.ResourceName == sprite.ResourceName)
                    return testSprite;
            }
            return new TestSprite(sprite.ResourceName, DefaultWidth, DefaultHeight);
        }

        public virtual TInterface GetSprite<TInterface>(string resourceName) 
            where TInterface : ISprite
        {
            return (TInterface)(object)new TestSprite(resourceName, DefaultWidth, DefaultHeight);
        }

        public virtual IView GetView()
        {
            return new TestView();
        }
    }
}
