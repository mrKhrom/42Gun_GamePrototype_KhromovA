using System;
using GamePrototype.Units;

namespace GamePrototype.Game
{
    public sealed class Map
    {
        public void Show(string currentRoom)
        {
            Console.WriteLine(currentRoom);
            Console.WriteLine(@"
                  +-------------+
                  |    Final    |
                  +-------------+
                      ^     ^
                      /      \
        -------------+   +-----------+
        |    Loot     |  | LootStone |
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
