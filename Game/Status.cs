using System;
using GamePrototype.Units;

namespace GamePrototype.Game
{
    public sealed class Status
    {
        public void Show(Player player)
        {
            if (player == null)
            {
                return;
            }

            Console.WriteLine("=== Player state ===");
            Console.WriteLine(player.ToString());
            Console.WriteLine($"Health state: {GetHealthState(player)}");
            Console.WriteLine("=====================");
        }

        private static string GetHealthState(Player player)
        {
            if (player.Health == 0)
            {
                return "Dead";
            }

            var healthRatio = player.Health / (double)player.MaxHealth;
            if (healthRatio <= 0.25)
            {
                return "Critical";
            }

            if (healthRatio <= 0.5)
            {
                return "Injured";
            }

            return "Normal";
        }
    }
}
