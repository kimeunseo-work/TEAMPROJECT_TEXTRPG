namespace TEAMPROJECT_TEXTRPG.Core
{
    public class Job
    {
        public string Name { get; private set; }
        public int MaxHP { get; private set; }
        public int MaxMP { get; private set; }
        public double BaseAttack { get; private set; }
        public double BaseDefense { get; private set; }
        public int LvUpAddMaxHP { get; private set; }
        public int LvUpAddMaxMP { get; private set; }
        public double LvUpAttack { get; private set; }
        public double LvUpDefense { get; private set; }

        public Job(string name, int maxHP, int maxMP, double baseAttack, double baseDefense, int lvUpAddMaxHP, int lvUpAddMaxMP, double lvUpAttack, double lvUpDefense)
        {
            Name = name;
            MaxHP = maxHP;
            MaxMP = maxMP;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            LvUpAddMaxHP = lvUpAddMaxHP;
            LvUpAddMaxMP = lvUpAddMaxMP;
            LvUpAttack = lvUpAttack;
            LvUpDefense = lvUpDefense;
        }

        // public List<Skill> SkillList { get; private set; } = new List<Skill>();  //직업간의 스킬 목록
    }
}
