using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public class HardDungeonBuilder : IDungeonBuilder
    {
        public DungeonRoom BuildDungeon(IUnitFactory unitFactory)
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom1 = new DungeonRoom("Monster1", unitFactory.CreateGoblinEnemy());
            var monsterRoom2 = new DungeonRoom("Monster2", unitFactory.CreateGoblinEnemy());
            var lootRoom = new DungeonRoom("Loot", new Gold());
            var grindRoom = new DungeonRoom("Grindstone", new Grindstone("Sharpening Stone"));
            var armourRoom = new DungeonRoom("Armour", new Helmet(5, 10, "Weak Helmet"));
            var rangeRoom = new DungeonRoom("Range", new RangeWeapon(12, "Crossbow"));
            var finalRoom = new DungeonRoom("Final");

            // Hard layout: more enemies, branches, mixed loot (some poor quality)
            enter.TrySetDirection(Direction.Left, monsterRoom1);
            enter.TrySetDirection(Direction.Right, armourRoom);
            enter.TrySetDirection(Direction.Forward, grindRoom);

            armourRoom.TrySetDirection(Direction.Forward, rangeRoom);
            rangeRoom.TrySetDirection(Direction.Forward, monsterRoom2);

            monsterRoom1.TrySetDirection(Direction.Forward, lootRoom);
            grindRoom.TrySetDirection(Direction.Forward, monsterRoom1);
            lootRoom.TrySetDirection(Direction.Forward, monsterRoom2);

            monsterRoom2.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }
}
