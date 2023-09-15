namespace Blokstart_e1
{

 using System; 
 using System.Collections.Generic;

internal class Program
        {
            static void Main(string[] args)
            {
                Game game = new Game();

                game.Start();
            }
        }

        class Game
        {
            private int playerHp;
            private int playerGold;
            private bool hasBetterWeapon;
            private string currentLocation;

            private Dictionary<string, string> instructions = new Dictionary<string, string>
    {
        { "start", "Welcome to Dungeon Dude! You are at your home." },
        { "sleep", "You have recovered your HP." },
        { "shop", "You are at the weapon shop. You can upgrade your gear here." },
        { "forest", "You are on a forest path. Be prepared to encounter enemies." }
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

        private void ExploreForest()
        {
            Console.WriteLine("You are on a forest path. As you step forward, you find yourself surrounded by tall, ancient trees. Shafts of dappled sunlight filter through the dense foliage overhead, creating a tranquil and slightly eerie atmosphere. The forest floor is covered in a soft carpet of fallen leaves and moss, muffling your footsteps. The distant sounds of birds and rustling leaves add to the sense of adventure and mystery. You are well aware that danger could lurk around any corner.");
            Console.WriteLine("You encounter an enemy in the forest!");
            int enemyHp = 50;
            while (playerHp > 0 && enemyHp > 0)
            {
                Console.WriteLine($"Your HP: {playerHp} | Enemy HP: {enemyHp}");
                Console.Write("Choose an action (attack/run): ");
                string action = Console.ReadLine().ToLower();

                if (action == "attack")
                {
                    if (!hasBetterWeapon)
                    {
                        playerHp -= 20;
                        enemyHp -= 40;
                    }
                    else
                    {
                        playerHp -= 20;
                        enemyHp -= 30;
                    }

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

    }
}