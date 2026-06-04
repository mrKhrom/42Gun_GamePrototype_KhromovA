using System;
using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Units;
using GamePrototype.Utils;

namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private Unit _player;
        private DungeonRoom _dungeon;
        private readonly CombatManager _combatManager = new CombatManager();
        private readonly Status _status = new Status();
        private Map _map;
        
        public void StartGame() 
        {
            Initialize();
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }

        #region Game Loop

        private void Initialize()
        {
            Console.WriteLine("Welcome, player!");
            Console.WriteLine("Choose difficulty (easy/hard):");
            var diffInput = Console.ReadLine()?.ToLowerInvariant().Trim() ?? "easy";
            var difficulty = diffInput.Contains("hard") ? Difficulty.Hard : Difficulty.Easy;
            Console.WriteLine($"Playing on {difficulty} difficulty.");

            IUnitFactory unitFactory = difficulty == Difficulty.Easy
                ? new EasyUnitFactory()
                : new HardUnitFactory();

            IDungeonBuilder dungeonBuilder = difficulty == Difficulty.Easy
                ? new EasyDungeonBuilder()
                : new HardDungeonBuilder();

            _dungeon = dungeonBuilder.BuildDungeon(unitFactory);
            _map = new Map(difficulty);

            Console.WriteLine("Enter your name:");
            var name = Console.ReadLine()?.Trim() ?? "Hero";
            _player = unitFactory.CreatePlayer(name);
            Console.WriteLine($"Hello, {_player.Name}!");
        }

        private void StartGameLoop()
        {
            var currentRoom = _dungeon;
            
            while (currentRoom.IsFinal == false) 
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success) 
                {
                    Console.WriteLine("Game over!");
                    return;
                }
                DisplayRouteOptions(currentRoom);
                while (true) 
                {
                    var input = Console.ReadLine();
                    if (input?.Trim().Equals("status", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        _status.Show((Player)_player);
                        continue;
                    }
                    if (input?.Trim().Equals("map", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        _map.Show(currentRoom.Name);
                        continue;
                    }

                    if (Enum.TryParse<Direction>(input, out var direction) ) 
                    {
                        currentRoom = currentRoom.Rooms[direction];
                        break;
                    }
                    else 
                    {
                        Console.WriteLine("Wrong direction!");
                    }
                }
            }
            Console.WriteLine($"Congratulations, {_player.Name}");
            Console.WriteLine("Result: ");
            Console.WriteLine(_player.ToString());
        }

        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            if (currentRoom.Loot != null) 
            {
                _player.AddItemToInventory(currentRoom.Loot);
            }
            if (currentRoom.Enemy != null) 
            {
                if (_combatManager.StartCombat(_player, currentRoom.Enemy) == _player)
                {
                    _player.HandleCombatComplete();
                    LootEnemy(currentRoom.Enemy);
                }
                else 
                {
                    success = false;
                }
            }

            void LootEnemy(Unit enemy)
            {
                _player.AddItemsFromUnitToInventory(enemy);
            }
        }

        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("Where to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.Write($"{room.Key} : {(int) room.Key}\t");
            }
            Console.WriteLine("Options: status, map");
        }

        
        #endregion
    }
}
