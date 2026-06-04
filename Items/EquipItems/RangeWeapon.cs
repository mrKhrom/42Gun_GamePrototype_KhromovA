using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class RangeWeapon : EquipItem
    {
        public RangeWeapon(uint damage, uint durability, string name)
            : base(durability, name) => Damage = damage;

        public RangeWeapon(uint damage, string name)
            : this(damage, 15, name)
        {
        }

        public uint Damage { get; }

        public override EquipSlot Slot => EquipSlot.RangeWeapon;
    }
}