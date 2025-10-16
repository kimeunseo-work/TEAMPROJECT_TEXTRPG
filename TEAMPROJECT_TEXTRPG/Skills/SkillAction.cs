namespace TEAMPROJECT_TEXTRPG.Skills
{
    internal class SkillAction
    {
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

            //누구에게 지정할지
            if (!skill.IsRandom)
            {
                Console.WriteLine("공격할 적을 선택하세요: ");
                for (int i = 0; i < GameManager.Instance.monsters.Count; i++)
                {
                    var m = GameManager.Instance.monsters[i];
                    Console.WriteLine($"{i + 1}. {m.Name} (HP: {m.Hp})");
                }
                int targetIndex = int.Parse(Console.ReadLine()) - 1;
                var target = GameManager.Instance.monsters[targetIndex];

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
