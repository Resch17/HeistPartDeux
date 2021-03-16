namespace HeistPartDeux
{
    public interface IRobber
    {
        string Name { get; set; }
        int SkillLevel { get; set; }
        int PercentageCut { get; set; }
        string Specialty { get; set; }
        void PerformSkill(Bank bank);
    }
}