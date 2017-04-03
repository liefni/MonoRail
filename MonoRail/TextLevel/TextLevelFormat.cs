using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.TextLevel
{
    /// <summary>
    /// Represents the configuration for creating TextLevelLoaders.
    /// </summary>
    public class TextLevelFormat : ILevelFormat
    {

        private List<GameObjectNotation> notations;
        private string levelsFolder;
        private readonly string levelsExtention;
        /// <summary>
        /// Initializes a new TextLevelFormat specifing the extention and directory for level data files and a list GameObjectNotations.
        /// </summary>
        /// <param name="levelsDirectory">The directory level data files are stored in.</param>
        /// <param name="levelsExtention">The file extention used by level data files.</param>
        /// <param name="gameObjectNotations">A list of GameObjectNotations specifying what GameObjects should be created by the specified characters in the data file.</param>
        public TextLevelFormat(string levelsDirectory, string levelsExtention, params GameObjectNotation[] gameObjectNotations)
        {
            this.notations = new List<GameObjectNotation>(gameObjectNotations);
            this.levelsFolder = levelsDirectory;
            this.levelsExtention = "." + levelsExtention.Trim('.');
        }

        /// <summary>
        /// Creates a new TextLevelLoader for the specified level and configures it.
        /// </summary>
        /// <param name="level">The level that the TextlevelLoader is for.</param>
        /// <returns>A new TextLevelLoader for the specified level.</returns>
        public ILevelLoader GetLevelLoader(Level level)
        {
            if (notations == null)
                return null;

            string levelFileName = Path.Combine(levelsFolder, level.GetType().Name + levelsExtention);
            if (File.Exists(levelFileName))
                return new TextLevelLoader(levelFileName, notations);
            else
                return null;
        }
    }
}
