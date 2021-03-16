using System;
using System.Collections.Generic;
using System.Linq;

namespace HeistPartDeux
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IRobber> rolodex = StartingLineup();
            GoonEntry(rolodex);
            Console.WriteLine("------------");
            Console.WriteLine("------------");
            Console.WriteLine("------------");
            Bank targetBank = new Bank()
            {
                AlarmScore = new Random().Next(0, 100) + 1,
                VaultScore = new Random().Next(0, 100) + 1,
                SecurityGuardScore = new Random().Next(0, 100) + 1,
                CashOnHand = new Random().Next(50000, 1000000) + 1
            };
            Dictionary<int, string> bankProps = new Dictionary<int, string>(){
                {targetBank.AlarmScore, "Alarms"}, {targetBank.VaultScore, "Vault"}, {targetBank.SecurityGuardScore, "Security Guards"}
            };
            List<int> bankKeys = bankProps.Select(kvp => kvp.Key).ToList();
            bankKeys.Sort();
            Console.WriteLine("Recon Report");
            Console.WriteLine("------------");
            Console.WriteLine($"Most Secure: {bankProps[bankKeys[2]]}");
            Console.WriteLine($"Least Secure: {bankProps[bankKeys[0]]}");
            Console.WriteLine();
            Console.WriteLine("Choose your crew!");
            PrintGoons(rolodex);
        }

        static List<IRobber> StartingLineup()
        {
            return new List<IRobber>()
            {
                new Muscle(){
                    Name = "Big Jim",
                    SkillLevel = 30,
                    PercentageCut = 10
                },
                new Hacker(){
                    Name = "Thomas Anderson",
                    SkillLevel = 30,
                    PercentageCut = 20
                },
                new LockSpecialist(){
                    Name = "Basher",
                    SkillLevel = 30,
                    PercentageCut = 15
                },
                new Muscle(){
                    Name = "Jean-Claude",
                    SkillLevel = 15,
                    PercentageCut = 5
                },
                new Hacker(){
                    Name = "Angelina",
                    SkillLevel = 15,
                    PercentageCut = 10
                },
                new LockSpecialist(){
                    Name = "Sarah Walker",
                    SkillLevel = 15,
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
                if (userAnswer.ToLower() == "n")
                {
                    entering = false;
                    break;
                }
                Console.WriteLine("Enter new operative's name: ");
                Console.Write(" > ");
                string newName = Console.ReadLine();
                bool choosingSpecialty = true;
                string newSpecialty = "";
                while (choosingSpecialty)
                {
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
                Console.WriteLine("------------");
                Console.WriteLine($"Operative #{i + 1}");
                Console.WriteLine($"Name: {goons[i].Name}");
                Console.WriteLine($"Specialty: {goons[i].Specialty}");
                Console.WriteLine($"Skill: {goons[i].SkillLevel}");
                Console.WriteLine($"Expected cut: {goons[i].PercentageCut}%");
            }
        }
    }
}
