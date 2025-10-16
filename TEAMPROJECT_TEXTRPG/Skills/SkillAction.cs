namespace TEAMPROJECT_TEXTRPG.Skills
{
    internal class SkillAction
    {

        // 직업에 따른 스킬 목록 불러오기
        internal static void HaveSkill(Player player, Monster target)
        {
            List<Skill> skills = new();

            switch (player.CurrentJob.Name)
            {
                case "전사":
                    skills = SkillDatabase.Instance.WarriorSkills;
                    break;
                case "마법사":
                    skills = SkillDatabase.Instance.MageSkills;
                    break;
                case "도적":
                    skills = SkillDatabase.Instance.ThiefSkills;
                    break;
                default:
                    Console.WriteLine("이 직업은 아직 스킬이 없습니다.");
                    return;
            }

            //스킬 목록 출력

            Console.WriteLine($"\n[사용 가능 스킬 목록][현재 마나: {player.Mp}]");
            for (int i = 0; i < skills.Count; i++)
            {
                var s= skills[i];
                Console.WriteLine($"{i + 1}. {s.Name} (Mp: {s.Mp}) - {s.Description})");
            }

            //사용할 스킬 번호 입력
            int skillIndex = InputHandler.GetUserActionInput();

            if (skillIndex < 0 || skillIndex >= skills.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            var selectedSkill = skills[skillIndex -1];

            // 선택한 스킬 실행

            Action(player, target, selectedSkill);
        }
        internal static void Action(Player player, Monster monster, Skill skill)
        {
            //MP 충분한지 확인
            if (player.Mp < skill.Mp)
            {
                Console.WriteLine($"{player.Name}의 MP가 부족합니다.");
                return;
            }

            //MP 차감
            player.Mp -= skill.Mp;

            //스킬 실행 시 문구 출력
            Console.WriteLine($"{player.Name}이(가) {skill.Name}을 사용했습니다!");
            Console.WriteLine($">>{skill.Description}");

            var atk = (int)player.Attack;
            int damage = (int)(atk * skill.Multiple); // 실행시 데미지 계산

            //스킬이 랜덤형태인지 아닌지 확인
            if (!skill.IsRandom)
            {
                for (int i = 0; i < GameManager.Instance.monsters.Count; i++)
                {
                    var m = GameManager.Instance.monsters[i];
                    Console.WriteLine($"{i + 1}. {m.Name} (HP: {m.Hp})");
                }
                int targetIndex = InputHandler.GetUserActionInput();
                var target = GameManager.Instance.monsters[targetIndex - 1];

                ApplyDamage(target, damage);

            }
            else
            {
                var random = new Random();
                var count = Math.Min(skill.Count, GameManager.Instance.monsters.Count);

                for (int i = 0; i < count; i++)
                {
                    var t = GameManager.Instance.monsters[random.Next(GameManager.Instance.monsters.Count)];
                    Console.WriteLine($"{skill.Count}명의 랜덤 적을 대상으로 지정했습니다.");

                    ApplyDamage(t, damage);

                }
            }

        }

        private static void ApplyDamage(Monster target, int damage)
        {
            target.Hp -= damage;
            Console.WriteLine($"{target.Name}에게 {damage}의 피해를 입혔습니다.");

            if (target.Hp <= 0)
            {
                target.Hp = 0;
                target.IsDead = true;
                Console.WriteLine($"{target.Name}을(를) 처치했습니다.");
            }
        }
    }
}
