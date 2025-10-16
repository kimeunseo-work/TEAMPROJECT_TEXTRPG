namespace TEAMPROJECT_TEXTRPG.Skills
{
    internal struct Skill
    {
        public string Name {  get; set; }
        public int Mp {  get; set; }
        public double Multiple {  get; set; }
        public bool IsRandom {  get; set; }
        public int Count {  get; set; }
        public string Description {  get; set; }
    }   
        //스킬 데이터베이스 (싱글톤)
        internal class SkillDatabase
    {
        //유일한 저장 공간
        private static SkillDatabase instance;

        //외부에서 겁근 가능

        internal static SkillDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SkillDatabase(); // 처음에만 생성
                }
                return instance;
            }
        }

        //전사 스킬 종류
        internal List<Skill> WarriorSkills = new();

        public SkillDatabase()
        {
            WarriorSkills.Add(new Skill
            {
                Name = "알파 스트라이크",
                Mp = 10,
                Multiple = 2,
                IsRandom = false,
                Count = 1,
                Description = "하나의 적을 공격합니다."
            });

            WarriorSkills.Add(new Skill()
            {
                Name = "더블 스트라이크",
                Mp = 10,
                Multiple = 1.5,
                IsRandom = true,
                Count = 2,
                Description = "2명의 적을 랜덤으로 공격합니다."
            });
        }

    }

    
    


}
