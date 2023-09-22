using System;
using System.Collections.Generic;
using System.IO;

namespace Blokstart_e1
{


    internal class Program
        {
        static void Main()
        {
            Game game = new Game();

            Console.WriteLine("Dungeon Dude");
            Console.WriteLine("In the village near your home, there is a dark and mysterious cave.");
            Console.WriteLine("Legends say it hides a powerful artifact that can bestow incredible power,");
            Console.WriteLine("but it is guarded by a fearsome dragon and its minions.");
            Console.WriteLine("Do you want to continue a saved game? (yes/no)");
            string continueSavedGame = Console.ReadLine().ToLower();
            if (continueSavedGame == "yes")
            {
                game.LoadSavedGame();
            }
            else if (continueSavedGame == "no")
            {
                game.StartNewGame();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
            }

            game.Start();
        }


    }

    class Game
        {
            private int playerHp;
            private int playerGold;
            private bool hasBetterWeapon;
            private string currentLocation;
        private string lastLocation;
        private bool hasCompletedQuest;


        private string saveFileName = "saved_game.txt"; 

        public void StartNewGame()
        {
            playerHp = 100;
            playerGold = 0;
            hasBetterWeapon = false;
            currentLocation = "start";
        }
        public void LoadSavedGame()
        {
            if (File.Exists(saveFileName))
            {
                using (StreamReader reader = new StreamReader(saveFileName))
                {
                    playerHp = int.Parse(reader.ReadLine());
                    playerGold = int.Parse(reader.ReadLine());
                    hasBetterWeapon = bool.Parse(reader.ReadLine());
                    currentLocation = reader.ReadLine();
                }
                Console.WriteLine("Loaded saved game.");
            }
            else
            {
                Console.WriteLine("No saved game found. Starting a new game.");
                StartNewGame();
            }
        }
        private Dictionary<string, string> instructions = new Dictionary<string, string>
    {
        { "start", "Welcome to Dungeon Dude! You are at your home." },
        { "sleep", "You have recovered your HP." },
        { "shop", "You are at the weapon shop. You can upgrade your gear here." },
        { "forest", "You are on a forest path. Be prepared to encounter enemies." },
        { "cave", "You are in a dark cave. It's a dangerous place with unknown challenges ahead." },
        {"Instructions", "How to Play Dungeon dude" },
        { "save", "Save the game so you can return later." },
        {"quit", "Exit Dungeon Dude thank you for playing." }

    };

            public Game()
            {
                playerHp = 100;
                playerGold = 0;
                hasBetterWeapon = false;
                currentLocation = "start";
            }

            public void Start()
            {
                Console.WriteLine("Dungeon Dude");
                ShowInstructions();

                while (true)
                {
                    Console.WriteLine($"Current HP: {playerHp} | Gold: {playerGold}");
                    Console.WriteLine($"Current Location: {currentLocation}");

                    Console.Write("Enter a command: ");
                    string command = Console.ReadLine().ToLower();

                    switch (command)
                    {
                        case "instructions":
                            ShowInstructions();
                        break;
                        case "sleep":
                            playerHp = 100;
                            Console.WriteLine("Recovered your health.");
                            break;
                        case "shop":
                            VisitShop();
                            break;
                        case "forest":
                            ExploreForest();
                            break;
                    case "cave":
                        ExploreCave();
                        break;
                    case "save": 
                        SaveGame();
                        break;
                    case "quit":
                            Console.WriteLine("Thanks for playing!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid command. Type 'instructions' for help.");
                            break;
                    }
                }
            }

        private void ShowInstructions()
        {
            Console.WriteLine("Instructions:");
            foreach (var instruction in instructions)
            {
                Console.WriteLine($"{instruction.Key} - {instruction.Value}");
            }
        }
        public void SaveGame()
        {
            using (StreamWriter writer = new StreamWriter(saveFileName))
            {
                writer.WriteLine(playerHp);
                writer.WriteLine(playerGold);
                writer.WriteLine(hasBetterWeapon);
                writer.WriteLine(currentLocation);
            }
            Console.WriteLine("Game saved.");
        }
        private void VisitShop()
        {
            Console.WriteLine("Welcome to the Shop!");
            Console.WriteLine("You are standing in a well-lit, cluttered shop filled with various weapons and armor on display. Shelves line the walls, showcasing gleaming swords, sturdy shields, and mysterious potions. The air is filled with the scent of polished metal and leather. A grizzled shopkeeper stands behind the counter, ready to assist you.");
            if (!hasBetterWeapon && playerGold >= 100)
            {
                Console.WriteLine("You bought a better weapon!");
                hasBetterWeapon = true;
            }
            else if (hasBetterWeapon)
            {
                Console.WriteLine("You already have the better weapon.");
            }
            else
            {
                Console.WriteLine("You don't have enough gold to make a purchase. Please come back later. Price: 100 Gold");
            }
        }
        private void SingleEnemyCombat(ref int playerHp, ref int playerGold, bool hasBetterWeapon, int enemyHp)
        {
            while (playerHp > 0 && enemyHp > 0)
            {
                Console.WriteLine($"Your HP: {playerHp} | Enemy HP: {enemyHp}");
                Console.Write("Choose an action (attack/run): ");
                string action = Console.ReadLine().ToLower();

                int playerAttackDamage = hasBetterWeapon ? 30 : 20; 

                if (action == "attack")
                {
                    playerHp -= 20;
                    enemyHp -= playerAttackDamage;

                    if (enemyHp <= 0)
                    {
                        Console.WriteLine("You defeated the enemy!");
                        playerGold += 50;
                        break;
                    }
                }
                else if (action == "run")
                {
                    Console.WriteLine("You managed to escape from the enemy!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid action. Choose 'attack' or 'run'.");
                }
            }

            if (playerHp <= 0)
            {
                Console.WriteLine("Game over! You were defeated.");
                Environment.Exit(0);
            }
        }

        private void CaveEnemiesCombat(ref int playerHp, ref int playerGold, bool hasBetterWeapon)
        {
            Console.WriteLine("You encounter two menacing figures in the dark cave!");
            // Adjust Enemy Hp here
            int enemy1Hp = 10;
            int enemy2Hp = 10;

            while (playerHp > 0 && (enemy1Hp > 0 || enemy2Hp > 0))
            {
                Console.WriteLine($"Your HP: {playerHp} | Enemy 1 HP: {enemy1Hp} | Enemy 2 HP: {enemy2Hp}");
                Console.Write("Choose an action (attack/run): ");
                string action = Console.ReadLine().ToLower();

                int playerAttackDamage = hasBetterWeapon ? 30 : 20;

                if (action == "attack")
                {
                    if (enemy1Hp > 0)
                        enemy1Hp -= playerAttackDamage;

                    if (enemy2Hp > 0)
                        enemy2Hp -= playerAttackDamage;

                    playerHp -= 15; 

                    if (enemy1Hp <= 0 && enemy2Hp <= 0)
                    {
                        Console.WriteLine("You defeated both enemies!");
                        playerGold += 50;
                        break;
                    }
                }
                else if (action == "run")
                {
                    Console.WriteLine("You managed to escape from the enemies!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid action. Choose 'attack' or 'run'.");
                }
            }

            if (playerHp <= 0)
            {
                Console.WriteLine("Game over! You were defeated.");
                Environment.Exit(0);
            }
        }

        // Adjust the boss health here
        int bossDragonHp = 70;
        private void BossEnemyCombat(ref int playerHp, ref int playerGold, bool hasBetterWeapon, int bossEnemyHp)
        {

            while (playerHp > 0 && bossEnemyHp > 0)
            {
                Console.WriteLine($"Your HP: {playerHp} | Boss Enemy HP: {bossEnemyHp}");
                Console.Write("Choose an action (attack/run): ");
                string action = Console.ReadLine().ToLower();

                int playerAttackDamage = hasBetterWeapon ? 50 : 30;

                if (action == "attack")
                {
                    playerHp -= 25;
                    bossEnemyHp -= playerAttackDamage;

                    if (bossEnemyHp <= 0)
                    {
                        bossEnemyHp = 0;
                        Console.WriteLine("You defeated the boss enemy!");
                        playerGold += 100;
                        break;
                    }
                }
                else if (action == "run")
                {
                    Console.WriteLine("You decided to run from the fearsome dragon, leaving the cave behind.");
                    Console.WriteLine("Your adventure ends in disgrace as tales of your cowardice spread throughout the village.");
                    Console.WriteLine("Game over! You have not completed the quest.");
                    Environment.Exit(0);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid action. Choose 'attack' or 'run'.");
                }
            }

            if (playerHp <= 0)
            {
                Console.WriteLine("Game over! You were defeated.");
                Environment.Exit(0);
            }
        }
        private void ExploreForest()
        {
            Console.WriteLine("You are on a forest path. As you step forward, you find yourself surrounded by tall, ancient trees. Shafts of dappled sunlight filter through the dense foliage overhead, creating a tranquil and slightly eerie atmosphere. The forest floor is covered in a soft carpet of fallen leaves and moss, muffling your footsteps. The distant sounds of birds and rustling leaves add to the sense of adventure and mystery. You are well aware that danger could lurk around any corner.");
            Console.WriteLine("You encounter an enemy in the forest!");
            int enemyHp = 50;
            int playerAttackDamage = hasBetterWeapon ? 50 : 40; 

            SingleEnemyCombat(ref playerHp, ref playerGold, hasBetterWeapon, enemyHp);

            if (playerHp <= 0)
            {
                Console.WriteLine("Game over! You were defeated.");
                Environment.Exit(0);
            }
        }

        private void ExploreCave()
        {
            if (hasCompletedQuest)
            {
                Console.WriteLine("You have already completed the quest.");
                return;
            }
            CaveEnemiesCombat(ref playerHp, ref playerGold, hasBetterWeapon);

            Console.WriteLine("As you get to the final area of the cave, you encounter a massive dragon!");


            BossEnemyCombat(ref playerHp, ref playerGold, hasBetterWeapon, bossDragonHp);

            Console.WriteLine("As you continue deeper into the cave, you discover a hidden chamber with a glowing ancient artifact.");

            Console.Write("Do you want to interact with the ancient artifact? (yes/no): ");
            string artifactInteraction = Console.ReadLine().ToLower();

            if (artifactInteraction == "yes")
            {
                Console.WriteLine("You touch the artifact, and a surge of power flows through you.");
                Console.WriteLine("Quest completed! You have retrieved the ancient artifact.");
                hasCompletedQuest = true;

                playerGold += 200; 

                Console.WriteLine("Taking the artifact, you feel its immense power and return to the surface as a hero, known far and wide for your bravery.");
                Console.WriteLine("Congratulations! You have won the game!");
            }
            else if (artifactInteraction == "no")
            {
                Console.WriteLine("You decide not to interact with the artifact, leaving it undisturbed in the cave.");
                Console.WriteLine("You leave the cave, and your adventure continues elsewhere.");
                Console.WriteLine("Congratulations! You have won the game!");
            }
            else
            {
                Console.WriteLine("You decide not to interact with the artifact.");
            }
        }
    }
}

