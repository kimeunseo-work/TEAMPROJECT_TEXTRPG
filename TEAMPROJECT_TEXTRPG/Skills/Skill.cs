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

        //외부에서 접근 가능

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

        //법사 스킬 종류

        internal List<Skill> MageSkills = new();

        //도적 스킬 종류

        internal List<Skill> ThiefSkills = new();

        public SkillDatabase()
        {
            //(전사 스킬)단일 몬스터에게 데미지 
            WarriorSkills.Add(new Skill
            {
                Name = "알파 스트라이크",
                Mp = 10,
                Multiple = 2,
                IsRandom = false,
                Count = 1,
                Description = "하나의 적을 공격합니다."
            });

            //(전사 스킬)랜덤 몬스터 2마리에게 데미지 
            WarriorSkills.Add(new Skill
            {
                Name = "더블 스트라이크",
                Mp = 10,
                Multiple = 1.5,
                IsRandom = true,
                Count = 2,
                Description = "2명의 적을 랜덤으로 공격합니다."
            });

            //(법사 스킬) 단일 몬스터에게 데미지 
            MageSkills.Add(new Skill
            {

                Name = "파이어 볼",
                Mp = 15,
                Multiple = 2.5,
                IsRandom = false,
                Count = 1,
                Description = "강력한 화염 구체를 하나의 적에게 날립니다."
            });
            //(법사 스킬) 랜덤 몬스터 3마리에게 데미지 
            MageSkills.Add(new Skill
            {

                Name = "윈드 커터",
                Mp = 20,
                Multiple = 1.5,
                IsRandom = true,
                Count = 3,
                Description = "3명의 적을 바람으로 이루어진 칼날로 랜덤하게 공격합니다."
            });
            //(도적 스킬) 단일 몬스터에게 데미지
            ThiefSkills.Add(new Skill
            {
                Name = "기습",
                Mp = 10,
                Multiple = 2.5,
                IsRandom = false,
                Count = 1,
                Description = "1명의 적에게 기습으로 공격합니다."
            });
            //(도적 스킬) 랜덤 몬스터 2마리에게 데미지
            ThiefSkills.Add(new Skill
            {
                Name = "단검 던지기",
                Mp = 10,
                Multiple = 1,
                IsRandom = true,
                Count = 2,
                Description = "2명의 적에게 랜덤으로 단검을 던져 공격합니다."
            });


        }
    }
}
