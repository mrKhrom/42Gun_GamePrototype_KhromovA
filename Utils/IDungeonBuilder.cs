using GamePrototype.Dungeon;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public interface IDungeonBuilder
    {
        DungeonRoom BuildDungeon(IUnitFactory unitFactory);
    }
}
