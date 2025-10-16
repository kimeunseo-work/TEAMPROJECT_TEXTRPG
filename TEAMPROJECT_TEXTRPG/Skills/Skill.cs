namespace TEAMPROJECT_TEXTRPG.Skills
{
    internal struct Skill
    {
        public string Name {  get; set; }
        public int Mp {  get; set; }
        public int Damage {  get; set; }
        public int Multiple {  get; set; }
        public bool IsRandom {  get; set; }
        public int Count {  get; set; }
        public string Description {  get; set; }
    }
        internal class SkillDatabase
    {
        internal List<Skill> WarriorSkills = new();

        public SkillDatabase()
        {
            var atk = (int)CharacterManager.Instance.player.Attack;

            WarriorSkills.Add(new Skill
            {
                Name = "알파 스트라이크",
                Mp = 10,
                Damage = atk * 2,
                Multiple = 1,
                IsRandom = false,
                Count = 1,
                Description = "하나의 적을 공격합니다."
            });

            WarriorSkills.Add(new Skill()
            {
                Name = "더블 스트라이크",
                Mp = 10,
                Damage = (int)(atk * 1.5f),
                Multiple = 2,
                IsRandom = true,
                Count = 2,
                Description = "2명의 적을 랜덤으로 공격합니다."
            });
        }

    }

    
    


}
