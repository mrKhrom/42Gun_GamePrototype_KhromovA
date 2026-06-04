using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;

namespace GamePrototype.Utils
{
    public class EasyDungeonBuilder : IDungeonBuilder
    {
        public DungeonRoom BuildDungeon(IUnitFactory unitFactory)
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", unitFactory.CreateGoblinEnemy());
            var lootRoom = new DungeonRoom("Loot", new HealthPotion("Healing Potion"));
            var weaponRoom = new DungeonRoom("Weapon", new Weapon(20, 30, "Legendary Sword"));
            var finalRoom = new DungeonRoom("Final");

            // Easy path: fewer threats, better loot early
            enter.TrySetDirection(Direction.Forward, lootRoom);
            lootRoom.TrySetDirection(Direction.Forward, weaponRoom);
            weaponRoom.TrySetDirection(Direction.Forward, monsterRoom);
            monsterRoom.TrySetDirection(Direction.Forward, finalRoom);

            // Bonus easy path
            enter.TrySetDirection(Direction.Right, weaponRoom);

            return enter;
        }
    }
}
