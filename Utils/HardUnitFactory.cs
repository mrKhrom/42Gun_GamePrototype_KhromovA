using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public class HardUnitFactory : IUnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 25, 25, 5);
            player.AddItemToInventory(new Weapon(8, 12, "Rusty Sword"));
            player.AddItemToInventory(new Armour(5, 12, "Worn Armour"));
            player.AddItemToInventory(new HealthPotion("Small Potion"));
            return player;
        }

        public Unit CreateGoblinEnemy() => new Goblin("Fierce Goblin", 28, 28, 6);
    }
}
