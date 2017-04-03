# MonoRail
MonoRails is an open source game framework/engine for creating 2D games with MonoGame. It takes care of a number of common issues required for many games such as Collision Detection, Object Solidity and Gravity. MonoRails is still under development so currently has a limited number of features, however more will be coming soon.

## Introduction
In MonoRail games are made up of Levels that contain GameObjects. A GameObject represents a visable object on the screen such as the player, a brick, a point/powerup or a 'bad guy'. A game object has an X and Y coordinate and a sprite (image) that it draws to the screen. It can have speed, gravity and be solid against other objects (preventing from being able to move through that object). These objects can then be put into levels.

## Getting Started
To get started download the MonoRails project. Create a new MonoGame Windows DirectX project and reference the MonoRails project. 

### Setting up the Game class
Update the game class created in the new project to inherit from the MonoRails.MonoGame.BaseGame class. Replace the code inside the class with the Initialize method shown below.

game1.cs
```
public class Game1 : BaseGame
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
```

This will make the game load Level1 when it starts. It also sets a a LevelLoadFormat so that we can load levels from a text file. Now we need to make the level.
### Creating a Level
Create a new class that inherits from MonoGame.Level as shown below:

Level1.cs
```
public class Level1 : Level
{

}
```
This is all that is needed for a basic level.

### Creating Game Objects
Now let's create three game objects. A player that we can move around, a brick that is solid from all sides and a block that the player can jump up through but cannot fall through

Block.cs
```
public class Block : GameObject
{
   protected override void Initialize()
    {
        base.Initialize();
        Sprite = ResourceManager.GetSprite<IStaticSprite>("Graphics\\Block");
    }
}
```

Brick.cs
```
public class Brick : Block
{
    protected override void Initialize()
    {
        base.Initialize();
        Sprite = ResourceManager.GetSprite<IStaticSprite>("Graphics\\Brick");
    }
}
```

Player.cs
```
[SolidAgainst(typeof(Brick), Side.All, true)]
[SolidAgainst(typeof(Block), Side.Top, true)]
public class SimplePlayer : GameObject
{
    protected override void Initialize()
    {
        base.Initialize();
        
        Sprite = ResourceManager.GetSprite<IStaticSprite>("Graphics\\Player");
        Gravity = 1;
        TerminalVelosity = 10;
    }

    protected override void Update()
    {
        var keyboardState = Keyboard.GetState();

        //move left/right
        if (keyboardState.IsKeyDown(Keys.Left))
            HSpeed = -4;
        else if (keyboardState.IsKeyDown(Keys.Right))
            HSpeed = 4;
        else
            HSpeed = 0;

        //jump
        if (keyboardState.IsKeyDown(Keys.Space))
        {
            if (VSpeed == 0 && CollidesWithType<Block>(false, X, Y + 1).Any())
                VSpeed = -15;
        }

        base.Update();
    }
}
```

### Creating Sprites

Open the Content.mcgb file. Create a Graphics folder and put three files in it. These will be the sprites for our GameObjects. They should be 32 pixels by 32 pixels. These are the three files to create

 - Player.png
 - Block.png
 - Brick.png

### Designing Levels
In the future MonoRails will support better formats for designing levels. At the moment though the inbuilt LevelLoader uses a text file where characters represent different objects. When we setup the game class at the begining we setup some characters to represent different objects. X for the player, ^ for a block and O for a brick. In the text file is used as a grid. Every character accross is placed 16 pixels further right in the level and every new line is placed 32 pixels down. 

Create a new folder and put a new text file, called Level1.txt in it. In the file properties set 'Copy to output directory' to 'Copy if newer'. Now put the following into the file:

```
O O O O O O O O O O O O O O O O O O O O O O O O O
O                                               O
O                                               O
O         O O O O O O O O O O O O O O O O O     O
O                                               O
O     ^ ^ ^                                     O
O                                               O
O                                               O
O     ^ ^ ^                                     O
O                                               O
O                                               O
O   O O O O                                     O
O                                               O
O X                                             O
O O O O O O O O O O O O O O O O O O O O O O O O O
```

Run the game and it should work now. 
