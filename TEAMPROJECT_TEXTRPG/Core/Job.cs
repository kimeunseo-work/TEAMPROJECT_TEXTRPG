namespace TEAMPROJECT_TEXTRPG.Core
{
    internal class Job
    {
        public string Name { get; private set; }    // 직업명
        public int MaxHP { get; private set; }      // 직업 간 기초 최대 Hp
        public int MaxMP { get; private set; }      // 직업 간 기초 최대 Mp
        public double BaseAttack { get; private set; }  // 직업 간 기본 공격력
        public double BaseDefense { get; private set; } // 직업 간 기본 방어력
        public int LvUpAddMaxHP { get; private set; }   // 레벨업 시 직업 간 최대 Hp 증가량
        public int LvUpAddMaxMP { get; private set; }   // 레벨업 시 직업 간 최대 Mp 증가량
        public double LvUpAttack { get; private set; }  // 레벨업 시 직업 간 공격력 증가량
        public double LvUpDefense { get; private set; } // 레벨업 시 직업 간 방어력 증가량

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
    }
}
