using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.TextLevel
{
    /// <summary>
    /// Configures and loads GameObjects into a level from a text file
    /// </summary>
    public class TextLevelLoader : ILevelLoader
    {
        private List<GameObjectNotation> notations;
        private string levelFileName;
        private TextReader levelReader;
        /// <summary>
        /// Initializes a new TextLevelLoader that opens a file to read data.
        /// </summary>
        /// <param name="levelFileName">The name of the file to load the level data from.</param>
        /// <param name="notations">List of GameObjectNotations that define a character representation of a GameObject.</param>
        public TextLevelLoader(string levelFileName, List<GameObjectNotation> notations)
        {
            this.notations = notations;
            this.levelFileName = levelFileName;
        }

        /// <summary>
        /// Initializes a new TextLevelLoader that uses a TextReader to read data.
        /// </summary>
        /// <param name="levelReader">The TextReader containing the level data to load</param>
        /// <param name="notations">List of GameObjectNotations that define a character representation of a GameObject.</param>
        public TextLevelLoader(TextReader levelReader, List<GameObjectNotation> notations)
        {
            this.notations = notations;
            this.levelReader = levelReader;
        }

        /// <summary>
        /// Configures and loads GameObjects into the specified level.
        /// </summary>
        /// <param name="level">The level to configure and load GameObjects into.</param>
        public void LoadLevel(Level level)
        {
            if (levelFileName != null)
                levelReader = new StreamReader(levelFileName);

            try
            {
                int y = 0;
                string line;
                while ((line = levelReader.ReadLine()) != null)
                {
                    int x = 0;
                    foreach (Char notationCharacter in line)
                    {
                        if (!char.IsWhiteSpace(notationCharacter))
                        {
                            GameObject gameObject = null;
                            foreach (GameObjectNotation notation in notations)
                            {
                                if (notation.NotationCharacter == notationCharacter)
                                    gameObject = notation.MakeGameObject();
                            }
                            if (gameObject == null)
                            {
                                throw new InvalidDataException(string.Format("The character {0} has no know GameObject associated", notationCharacter));
                            }
                            else
                            {
                                gameObject.X = x;
                                gameObject.Y = y;
                                level.GameObjects.Add(gameObject);
                            }
                        }

                        x += 16;
                    }
                    x += 32 - (x % 32);
                    if (level.Width < x)
                        level.Width = x;

                    y += 32;
                }
                if (level.Height < y)
                    level.Height = y;
            }
            finally
            {
                if (levelFileName != null)
                {
                    levelReader.Dispose();
                    levelReader = null;
                }
            }
                
        }
    }
}
