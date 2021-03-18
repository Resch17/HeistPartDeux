using System;
using System.Collections.Generic;
using System.Linq;

namespace HeistPartDeux
{
    class Program
    {
        static void Main(string[] args)
        {
            // affordance to add new goons
            List<IRobber> rolodex = StartingLineup();
            GoonEntry(rolodex);

            Console.WriteLine("------------");

            Bank targetBank = new Bank()
            {
                AlarmScore = new Random().Next(1, 101),
                VaultScore = new Random().Next(1, 101),
                SecurityGuardScore = new Random().Next(1, 101),
                CashOnHand = new Random().Next(50000, 1000001)
            };

            // assess bank security
            Dictionary<int, string> bankProps = new Dictionary<int, string>(){
                {targetBank.AlarmScore, "Alarms"},
                {targetBank.VaultScore, "Vault"},
                {targetBank.SecurityGuardScore, "Security Guards"}
            };
            List<int> bankKeys = bankProps.Select(kvp => kvp.Key).ToList();
            bankKeys.Sort();
            Console.WriteLine("Recon Report");
            Console.WriteLine("------------");
            Console.WriteLine($"Most Secure: {bankProps[bankKeys[2]]}");
            Console.WriteLine($"Least Secure: {bankProps[bankKeys[0]]}");
            Console.WriteLine();
            PauseClear();

            Console.WriteLine("Choose your crew!");
            Console.WriteLine("You can choose as many goons as you want, but keep an eye on the cut %");
            PauseClear();
            List<IRobber> crew = new List<IRobber>();
            List<IRobber> eligibleGoons = new List<IRobber>();
            bool choosingCrew = true;
            int availableCut = 100;
            while (choosingCrew)
            {
                // eligible goons won't take our available cut below zero
                eligibleGoons = rolodex.Where(g => availableCut - g.PercentageCut > 0).ToList();
                if (eligibleGoons.Count > 0)
                {
                    PrintGoons(eligibleGoons);
                    Console.WriteLine("==================================");
                    Console.WriteLine($"Unallocated loot: {availableCut}%");
                    Console.WriteLine("==================================");
                    Console.WriteLine("Select a crew member # from the list above.");
                    Console.Write(" > ");
                    int userChoice = 0;
                    IRobber newMember;
                    string userInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        choosingCrew = false;
                    }
                    if (int.TryParse(userInput, out userChoice))
                    {
                        if (userChoice <= eligibleGoons.Count)
                        {
                            if (availableCut - eligibleGoons[userChoice - 1].PercentageCut >= 0)
                            {
                                newMember = eligibleGoons[userChoice - 1];
                                crew.Add(newMember);
                                availableCut -= newMember.PercentageCut;
                                Console.Clear();
                                Console.WriteLine($"{newMember.Name} - {newMember.Specialty} added to the crew!");
                                rolodex.Remove(newMember);
                                PauseClear();
                            }
                        }
                        else
                        {
                            InvalidEntry();
                            PrintGoons(eligibleGoons);
                        }
                    }
                    else
                    {
                        InvalidEntry();
                        PrintGoons(eligibleGoons);
                    }
                }
                else
                {
                    Console.WriteLine("No more eligible goons!");
                    choosingCrew = false;
                }
            }
            Console.Clear();


            Console.WriteLine("----------------------------------");
            Console.WriteLine("Let's hit the bank!!!");
            Console.WriteLine("Here's the crew: ");
            Console.WriteLine("----------------------------------");
            PrintGoons(crew);
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            PauseClear();

            BankHeist(crew, targetBank);            
            Console.WriteLine("-----------------------------");

            if (targetBank.IsSecure)
            {
                Console.WriteLine("You failed to rob the bank!");
                Console.WriteLine("Have fun in jail!");
            }
            else
            {
                Console.WriteLine("You did it! Good robbery. Here's the take:");
                Console.WriteLine("=============================");
                foreach (IRobber goon in crew)
                {
                    int goonTake = targetBank.CashOnHand / goon.PercentageCut;
                    Console.WriteLine($"{goon.Name} brings home ${goonTake}!");

                    Console.WriteLine("-----------------------------");
                    targetBank.CashOnHand -= goonTake;
                }
                Console.WriteLine("And for you, our fearless leader...");
                Console.ReadKey();
                Console.WriteLine(" ");
                Console.WriteLine($"You get ${targetBank.CashOnHand}!");
                Console.ReadKey();
                Console.WriteLine(" ");
                Console.WriteLine("Not bad for a day's work!");
            }
        }

        static List<IRobber> StartingLineup()
        {
            return new List<IRobber>()
            {
                new Muscle(){
                    Name = "Big Jim",
                    SkillLevel = 60,
                    PercentageCut = 10
                },
                new Hacker(){
                    Name = "Thomas Anderson",
                    SkillLevel = 60,
                    PercentageCut = 20
                },
                new LockSpecialist(){
                    Name = "Basher",
                    SkillLevel = 60,
                    PercentageCut = 15
                },
                new Muscle(){
                    Name = "Jean-Claude",
                    SkillLevel = 35,
                    PercentageCut = 5
                },
                new Hacker(){
                    Name = "Angelina",
                    SkillLevel = 35,
                    PercentageCut = 10
                },
                new LockSpecialist(){
                    Name = "Sarah Walker",
                    SkillLevel = 35,
                    PercentageCut = 8
                }
            };
        }

        static void GoonEntry(List<IRobber> rolodex)
        {
            bool entering = true;
            while (entering)
            {
                Console.WriteLine($"Available Operatives: {rolodex.Count}");
                Console.WriteLine();
                Console.WriteLine("Add another operative? (y/n)");
                Console.Write(" > ");
                string userAnswer = Console.ReadLine();
                Console.Clear();
                if (userAnswer.ToLower() == "n")
                {
                    entering = false;
                    break;
                }
                Console.WriteLine(" ");
                Console.WriteLine("Enter new operative's name: ");
                Console.Write(" > ");
                string newName = Console.ReadLine();
                bool choosingSpecialty = true;
                string newSpecialty = "";
                while (choosingSpecialty)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine($"Select {newName}'s specialty:");
                    Console.WriteLine("1. Muscle (Subdues guards)");
                    Console.WriteLine("2. Hacker (Disables alarms)");
                    Console.WriteLine("3. Lock Specialist (Cracks vault)");
                    Console.Write(" > ");
                    string userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            newSpecialty = "Muscle";
                            choosingSpecialty = false;
                            break;
                        case "2":
                            newSpecialty = "Hacker";
                            choosingSpecialty = false;
                            break;
                        case "3":
                            newSpecialty = "Lock Specialist";
                            choosingSpecialty = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Selection");
                            break;
                    }
                }
                bool enteringSkill = true;
                int newSkill = 0;
                while (enteringSkill)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine($"Enter {newName}'s skill level (1-100): ");
                    Console.Write(" > ");
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out newSkill))
                    {
                        enteringSkill = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid entry.");
                    }
                }
                bool enteringCut = true;
                int newCut = 0;
                while (enteringCut)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine($"Enter {newName}'s cut (1-100%): ");
                    Console.Write(" > ");
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out newCut))
                    {
                        enteringCut = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid entry.");
                    }
                }
                if (newSpecialty == "Muscle")
                {
                    rolodex.Add(new Muscle()
                    {
                        Name = newName,
                        SkillLevel = newSkill,
                        PercentageCut = newCut
                    });
                }
                if (newSpecialty == "Hacker")
                {
                    rolodex.Add(new Hacker()
                    {
                        Name = newName,
                        SkillLevel = newSkill,
                        PercentageCut = newCut
                    });
                }
                if (newSpecialty == "Lock Specialist")
                {
                    rolodex.Add(new LockSpecialist()
                    {
                        Name = newName,
                        SkillLevel = newSkill,
                        PercentageCut = newCut
                    });
                }
            }
            Console.WriteLine();
        }

        static void PrintGoons(List<IRobber> goons)
        {
            for (int i = 0; i < goons.Count; i++)
            {
                Console.WriteLine($"Operative #{i + 1}");
                PrintGoon(goons[i]);
            }
        }

        static void PrintGoon(IRobber goon)
        {
            Console.WriteLine("==============");
            Console.WriteLine($"Name: {goon.Name}");
            Console.WriteLine($"Specialty: {goon.Specialty}");
            Console.WriteLine($"Skill: {goon.SkillLevel}");
            Console.WriteLine($"Expected cut: {goon.PercentageCut}%");
            Console.WriteLine(" ");
        }

        static void BankHeist(List<IRobber> crewInput, Bank bankInput)
        {
            foreach (IRobber goon in crewInput)
            {
                Console.WriteLine(" ");
                goon.PerformSkill(bankInput);
                Console.WriteLine(" ");
                PauseClear();
            }
        }

        static void InvalidEntry()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid entry.");
            Console.ResetColor();
        }

        static void PauseClear()
        {
            Console.WriteLine("Press any key to continue...");            
            Console.ReadKey();
            Console.Clear();
        }
    }
}
