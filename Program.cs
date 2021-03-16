using System;
using System.Collections.Generic;

namespace HeistPartDeux
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IRobber> rolodex = new List<IRobber>()
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
            Console.WriteLine("Available Operatives:");
            foreach (IRobber goon in rolodex)
            {
                Console.WriteLine("------------");
                Console.WriteLine($"Name: {goon.Name}");
                Console.WriteLine($"Specialty: {goon.Specialty}");
                Console.WriteLine($"Skill: {goon.SkillLevel}");
                Console.WriteLine($"Expected cut: {goon.PercentageCut}%");
            }
            bool entering = true;
            while (entering)
            {
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
            Console.Clear();
        }
    }
}
