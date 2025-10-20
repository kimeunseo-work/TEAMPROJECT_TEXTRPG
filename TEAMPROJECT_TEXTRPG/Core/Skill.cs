namespace TEAMPROJECT_TEXTRPG.Core
{
    internal struct Skill
    {
        public string Name { get; set; }
        public int Mp { get; set; }
        public double Multiple { get; set; }
        public bool IsRandom { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }
}
