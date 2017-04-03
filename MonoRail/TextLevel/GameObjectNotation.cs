using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoRail.TextLevel
{
    /// <summary>
    /// Represents a specification of what type of GameObject should be created for an occurance of the specified characted in TextLevelLoader data file.
    /// </summary>
    public abstract class GameObjectNotation
    {
        public GameObjectNotation(char notationCharacter)
        {
            this.NotationCharacter = notationCharacter;
        }

        /// <summary>
        /// Creates an instance of the GameObject this notation is for.
        /// </summary>
        /// <returns>New GameObject of the type this notation is for.</returns>
        public abstract GameObject MakeGameObject();

        /// <summary>
        /// Get the specified character for this notation.
        /// </summary>
        public char NotationCharacter { get; }
    }

    /// <summary>
    /// Represents a specification of what type of GameObject should be created for an occurance of the specified character in TextLevelLoader data file.
    /// </summary>
    /// <typeparam name="TGameObject">The type of GameObject to create for the specified character.</typeparam>
    public class GameObjectNotation<TGameObject> : GameObjectNotation where TGameObject : GameObject, new()
    {
        /// <summary>
        /// Initializes a GameObjectNotation defining the type of GameObject to create for a specified character.
        /// </summary>
        /// <param name="notationCharacter">The specified character to create a GameObject of type TGameObject for.</param>
        public GameObjectNotation(char notationCharacter)
            : base(notationCharacter) { }

        /// <summary>
        /// Creates an instance of the GameObject this notation is for.
        /// </summary>
        /// <returns>New GameObject </returns>
        public override GameObject MakeGameObject()
        {
            return new TGameObject();
        }
    }
}
