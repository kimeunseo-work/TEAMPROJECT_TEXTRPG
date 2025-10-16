namespace TEAMPROJECT_TEXTRPG
{
    internal class Job
    {
        public string Name { get; private set; }
        public int MaxHP { get; private set; }
        public int MaxMP { get; private set; }
        public double BaseAttack { get; private set; }
        public double BaseDefense { get; private set; }
        public int LvUpAddMaxHP { get; private set; }
        public int LvUpAddMaxMP { get; private set; }

        public Job(string name, int maxHP, int maxMP, double baseAttack, double baseDefense, int lvUpAddMaxHP, int lvUpAddMaxMP)
        {
            Name = name;
            MaxHP = maxHP;
            MaxMP = maxMP;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            LvUpAddMaxHP = lvUpAddMaxHP;
            LvUpAddMaxMP = lvUpAddMaxMP;
        }

        public int AddMaxHP()
        {
            MaxHP += LvUpAddMaxHP;
            return MaxHP;
        }

        public int AddMaxMP()
        {
            MaxMP += LvUpAddMaxMP;
            return MaxMP;
        }

        // public List<Skill> SkillList { get; private set; } = new List<Skill>();  //직업간의 스킬 목록

        internal List<Job> job = new List<Job>()
        {
            new Job("전사", 150, 50, 5, 10, 20, 5),
            new Job("마법사", 100, 100, 8, 7, 10, 10),
            new Job("도적", 120, 70, 10, 5, 10, 10)
        };

    }
}
