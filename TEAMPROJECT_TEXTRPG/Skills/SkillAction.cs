namespace TEAMPROJECT_TEXTRPG.Skills
{
    internal class SkillAction
    {
        private static Random random = new Random();

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

            if (skillIndex < 0 || skillIndex > skills.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
                return;
            }

            var selectedSkill = skills[skillIndex -1];

            // 선택한 스킬 실행

            Action(player, target, selectedSkill);
        }
        internal static void Action(Player player, Monster target, Skill skill)
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
                //단일 대상
                ApplyDamage(target, damage);
            }
            else
            {
                //복수 대상
                //살아있는 몬스터만 대상으로
                var aliveMonsters = GameManager.Instance.monsters
                    .Where(m => !m.IsDead)
                    .ToList();

                // 실제 타격 수는 Count와 생존 수 중 작은 값으로
                var count = Math.Min(skill.Count, aliveMonsters.Count);
                
                for (int i = 0; i < count; i++)
                {
                    if (aliveMonsters.Count == 0) break;
                    // aliveMonsters 에서만 대상 선정
                    var idx = random.Next(aliveMonsters.Count);
                    var t = aliveMonsters[idx];
                    
                    //같은 대상 중복 방지
                    aliveMonsters.RemoveAt(idx);

                    ApplyDamage(t, damage);

                }
            }

            EndPlayerTurn();
            
        }

        private static void EndPlayerTurn()
        {
            Console.WriteLine("\n0.다음");
            Console.ReadKey();

            bool allDead = GameManager.Instance.monsters.All(x => x.IsDead);
            if (allDead)
            {
                GameManager.Instance.currentBattleState = BattleState.Victory;
                GameManager.Instance.currentState = GameState.BattleResult;
            }
            else
            {
                GameManager.Instance.currentState = GameState.EnemyTurn;
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
