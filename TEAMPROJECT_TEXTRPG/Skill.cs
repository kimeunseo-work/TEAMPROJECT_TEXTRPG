namespace TEAMPROJECT_TEXTRPG
{
    internal struct Skill
    {
        public string Name { get; }
        public int Mp { get; }

        public int Damage { get; }
        public int Multiple { get; }
        public bool IsRandom { get; }
        public int Count { get; }
        public string Description { get; }

        public Skill(string name, int mp, int damage, int multiple, bool israndom, int count, string description)
        {
            Name = name;
            Mp = mp;
            Damage = damage;
            Multiple = multiple;
            IsRandom = israndom;
            Count = count;
            Description = description;

        }
    }

}
