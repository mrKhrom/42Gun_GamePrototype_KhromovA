using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
            {
                return BaseDamage + weapon.Damage;
            }
            if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out item) && item is RangeWeapon rangeWeapon)
            {
                return BaseDamage + rangeWeapon.Damage;
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i] is EconomicItem economicItem)
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                if (_equipment.TryGetValue(equipItem.Slot, out var current))
                {
                    if (Inventory.TryAdd(current))
                    {
                        _equipment[equipItem.Slot] = equipItem;
                        Console.WriteLine($"{Name} changed {current.Name} for {equipItem.Name}.");
                        return;
                    }

                    Console.WriteLine($"Inventory is full, {Name} cannot replace {current.Name} with {equipItem.Name}.");
                    return;
                }

                _equipment[equipItem.Slot] = equipItem;
                Console.WriteLine($"{Name} equipped {equipItem.Name}.");
                return;
            }

            base.AddItemToInventory(item);
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion && Health < MaxHealth)
            {
                var restoreAmount = healthPotion.HealthRestore;
                var effectiveRestore = MaxHealth - Health <= restoreAmount ? MaxHealth - Health : restoreAmount;
                Health += effectiveRestore;
                Console.WriteLine($"{Name} uses {economicItem.Name} and restores {effectiveRestore} health. Current health {Health}/{MaxHealth}.");
                return;
            }

            if (economicItem is Grindstone)
            {
                if (_equipment.TryGetValue(EquipSlot.Weapon, out var weaponItem) && weaponItem is Weapon weapon)
                {
                    weapon.Repair(4);
                    Console.WriteLine($"{Name} uses {economicItem.Name} on weapon and repairs it by 4 points.");
                    return;
                }

                if (_equipment.TryGetValue(EquipSlot.Armour, out var armourItem) && armourItem is Armour armour)
                {
                    armour.Repair(4);
                    Console.WriteLine($"{Name} uses {economicItem.Name} on armour and repairs it by 4 points.");
                }
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Helmet, out var helmetItem) && helmetItem is Helmet helmet)
            {
                damage -= (uint)(damage * (helmet.Defence / 100f));
            }

            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour && armour.Durability > 0)
            {
                damage -= (uint)(damage * (armour.Defence / 100f));
            }
            return damage;
        }

        protected override void DamageReceiveHandler()
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour && armour.Durability > 0)
            {
                armour.ReduceDurability(1);
                Console.WriteLine($"{Name}'s armour loses 1 durability and now has {armour.Durability}.");

                if (armour.Durability == 0)
                {
                    _equipment.Remove(EquipSlot.Armour);
                    Console.WriteLine($"{Name}'s armour is broken.");
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}
