using System;
using GamePrototype.Utils;

namespace GamePrototype.Game
{
    public sealed class Map
    {
        private readonly Difficulty _difficulty;

        public Map(Difficulty difficulty)
        {
            _difficulty = difficulty;
        }

        public void Show(string currentRoom)
        {
            Console.WriteLine(currentRoom);

            if (_difficulty == Difficulty.Easy)
            {
                Console.WriteLine(@"
    +-------------+
    |    Final    |
    +-------------+
           ^       
           |               
    +-------------+
    |   Monster   |
    +-------------+
           ^       
           |       
    +-------------+
    |   Weapon    |
    +-------------+
           ^       
           |       
    +-------------+
    |   Loot.     |
    +-------------+
           ^       
           |       
    +-------------+
    |   Enter     |
    +-------------+");
            }
            if (_difficulty == Difficulty.Hard)
            {
Console.WriteLine(@"
            +---------------+
            |     Final     |
            +---------------+
                    ^
                    |
            +---------------+
            |   Monster2    |
            +---------------+
                ^         ^
                /           \
    +-------------+   +------------+
    |     Loot    |   |    Range   |
    +-------------+   +------------+
            ^                   ^
            |                   |
    +-------------+   +------------+
    |   Monster1  |   |   Armour   |
    +-------------+   +------------+
        ^         ^            ^
        |         |            |
        |   +-----------+      |
        |   |   Grind   |      |
        |   +-----------+      |
        |         ^            |
        |         |            |
    +-------------------------------+
    |             Enter             |
    +-------------------------------+
");            }

            else
            {
                Console.WriteLine(@"
                  +-------------+
                  |    Final    |
                  +-------------+
                      ^     ^
                      /      \
        -------------+   +-----------+
        |    Loot    |   | LootStone |
        +------------+   +-----------+
              ^               ^
              |               |
      +-------------+   +-------------+
----> |   Monster   |   |     Empty   |
|     +-------------+   +-------------+
|           ^                ^
|           |                |
|    +-------------+         |
|    |   Weapon    |         |
|    +-------------+         |
|           ^                |
|           |                |
|    +-------------+         |
|    |   Armour    |         |
|    +-------------+         |
|            ^               |
|            |               |
|       +-------------+      |
|  — - -|    Enter    |— - - |
        +-------------+");
            }
        }
    }

}
