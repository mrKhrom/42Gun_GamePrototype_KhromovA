using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public class EasyUnitFactory : IUnitFactory
    {
        public Unit CreatePlayer(string name)
        {
            var player = new Player(name, 40, 40, 8);
            player.AddItemToInventory(new Weapon(15, 25, "Strong Sword"));
            player.AddItemToInventory(new Armour(15, 25, "Strong Armour"));
            player.AddItemToInventory(new HealthPotion("Big Potion"));
            return player;
        }

        public Unit CreateGoblinEnemy() => new Goblin("Weak Goblin", 15, 15, 3);
    }
}
