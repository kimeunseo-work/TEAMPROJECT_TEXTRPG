using TEAMPROJECT_TEXTRPG.Core;
using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Utility;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class TotalBattleScene : Scene
    {
        //private Dictionary<BattleState, Action> battleDisplays;
        //internal TotalBattleScene()
        //{
        //    battleDisplays = new Dictionary<BattleState, Action>();
        //    battleDisplays.Add(BattleState.Start, ShowBattleStart);
        //    battleDisplays.Add(BattleState.PlayerTurn, ShowPlayerTurn);
        //    battleDisplays.Add(BattleState.MonsterTurn, ShowMonsterTurn);
        //}

        private Player player;
        internal TotalBattleScene()
        {
            player = CharacterManager.Instance.player;
        }

        /* 생명 주기 */
        //============================================================//

        private void OnEnter()
        {
            BattleManager.Instance.OnMonsterSpawned += ShowBattleStart;
            BattleManager.Instance.OnPlayerTurn += ShowPlayerTurn;
            BattleManager.Instance.OnMonsterActioned += ShowMonsterTurn;
        }

        private void OnExit()
        {
            BattleManager.Instance.OnMonsterSpawned -= ShowBattleStart;
            BattleManager.Instance.OnPlayerTurn -= ShowPlayerTurn;
            BattleManager.Instance.OnMonsterActioned -= ShowMonsterTurn;
        }

        /* Show */
        //============================================================//

        internal override void Show()
        {
            OnEnter();

            // 현재 씬이 배틀일 때 반복
            while (GameManager.Instance.currentState == GameState.TotalBattle)
            {
                HandleBattleScene();

                //// 현재 배틀 상태가 None = 배틀 상태 아님 이면 화면 출력 안함
                //// 몬스터만 출력한 상태는 Start
                //if (BattleManager.Instance.CurrentBattleState != BattleState.None)
                //{
                //    var action = BattleSceneHandler();
                //    action();
                //}

                //// 현재 배틀 상태 및 조건들(몬스터 IsDead, 플레이어 Hp) 검사해서
                //// 적절한 배틀 상태 할당
                //BattleManager.Instance.ChangeBattleState();
            }

            OnExit();
        }

        /* Handler */
        //============================================================//

        private void HandleBattleScene() => BattleManager.Instance.ChangeBattleState();

        /* Displays */
        //============================================================//

        private void ShowBattleStart(
            List<Monster> CurrentMonsters,
            Func<int, BattleInput> GetBattleStart
            )
        {
            var isBattleStart = new BattleInput();

            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            for (int i = 0; i < CurrentMonsters.Count; i++)
            {
                Console.WriteLine($"Lv.{CurrentMonsters[i].Level} {CurrentMonsters[i].Name} HP {CurrentMonsters[i].Hp}");
            }
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.CurrentJob.Name})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHP}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 나가기");

            while (true)
            {
                var input = InputHandler.GetUserActionInput();
                isBattleStart = GetBattleStart(input);

                if (!isBattleStart.HasFlag(BattleInput.IsValid))
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    ConsoleUtility.ClearLine(Console.CursorTop, 4);
                }
                else break;
            }

            if (isBattleStart.HasFlag(BattleInput.IsQuit))
            {
                Console.WriteLine("메인 화면으로 돌아갑니다.");
                Console.ReadKey();
            }
        }

        private void ShowPlayerTurn(
            List<Monster> currentMonsters,
            Func<int, BattleInput> GetVaildMonsterSelection,
            Func<int, (BattleInput, List<Skill>?)> GetAttackType,
            Func<int, (string, SelectAttackBasicResult)> GetBasicAttackResult,
            Func<int, int, (BattleInput, SkillAttackResult[]?)> GetSkillAttackResult
            )
        {
            var input = 0;
            var monsterIndex = 0;

            BattleInput battleStartResult = default;
            (BattleInput inputResult, List<Skill>? skills) attackType = default;
            (string targetName, SelectAttackBasicResult result) attackResult = default;
            (BattleInput inputResult, SkillAttackResult[]? results) skillResult = default;

            while (true)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("**Battle!!**\n");
                Console.WriteLine($"플레이어 정보");
                Console.WriteLine($"이름: {player.Name}");
                Console.WriteLine($"레벨: {player.Level}");
                Console.WriteLine($"직업: {player.CurrentJob.Name}");
                Console.WriteLine($"HP: {player.Hp}/{player.MaxHP}");
                Console.WriteLine($"MP: {player.Mp}/{player.MaxMP}");
                Console.WriteLine();

                /* 몬스터 선택창 */
                //============================================================//

                // ShowMonsters()
                Console.WriteLine("몬스터 정보");
                for (int i = 0; i < currentMonsters.Count; i++)
                {
                    var monster = currentMonsters[i];
                    var text = $"**{i + 1}** Lv.{monster.Level} {monster.Name} HP {monster.Hp}";
                    if (monster.IsDead)
                    {
                        UIColorUtility.WriteColoredLine(text, ConsoleColor.DarkGray);
                        continue;
                    }
                    Console.WriteLine(text);
                }

                // 몬스터 고를지 나갈지
                while (true)
                {
                    monsterIndex = InputHandler.GetUserActionInputInBattle();
                    battleStartResult = GetVaildMonsterSelection(monsterIndex);

                    if (!battleStartResult.HasFlag(BattleInput.IsValid))
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    else if (battleStartResult.HasFlag(BattleInput.IsDead))
                    {
                        Console.WriteLine("이미 죽은 몬스터입니다.");
                    }
                    else break;
                }

                // 배틀 취소
                if (battleStartResult.HasFlag(BattleInput.IsQuit)) break;

                /* 공격 방식 선택창 */
                //============================================================//

                while (true)
                {
                    Console.WriteLine("\n무엇을 하시겠습니까?");
                    Console.WriteLine("1. 기본 공격");
                    Console.WriteLine("2. 스킬 사용");
                    Console.WriteLine("0. 나가기");
                    Console.Write(">>");

                    // 스킬 선택할지 뒤로 갈지
                    while (true)
                    {
                        input = InputHandler.GetInputToInt();
                        attackType = GetAttackType(input);

                        if (!attackType.inputResult.HasFlag(BattleInput.IsValid))
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                        else
                            break;
                    }

                    // 몬스터 다시 선택. 나가서 처리.
                    if (attackType.inputResult.HasFlag(BattleInput.IsQuit)) break;

                    /* 일반 공격 처리 */
                    //============================================================//

                    if (attackType.inputResult.HasFlag(BattleInput.IsBasicAttack))
                    {
                        attackResult = GetBasicAttackResult(monsterIndex);

                        if (attackResult.result.IsCritical)
                        {
                            UIColorUtility.WriteColoredLine("\n 치명타 공격!!", ConsoleColor.Red);
                        }

                        Console.WriteLine($"\n{player.Name}의 공격");
                        Console.WriteLine($"{attackResult.targetName}을(를) 맞췄습니다. [데미지 : {attackResult.result.AttackPower}]");
                        Console.WriteLine($"\n{attackResult.targetName}");

                        if (attackResult.result.IsDead)
                        {
                            Console.WriteLine($"HP{attackResult.result.OldHp} -> Dead");
                        }
                        else
                        {
                            Console.WriteLine($"HP {attackResult.result.OldHp} -> {attackResult.result.NewHp}");
                        }

                        Console.WriteLine("\n0.다음");
                        Console.ReadKey();

                        return; // 일반 공격하면 여기서 플레이어 턴 종료?
                    }

                    /* 스킬 공격 처리 */
                    //============================================================//

                    // 현재 가지고 있는 스킬들
                    var currentSkills = attackType.skills ?? throw new ArgumentNullException(nameof(attackType.skills));

                    //스킬 목록 출력
                    Console.WriteLine($"\n[사용 가능 스킬 목록][현재 마나: {player.Mp}]");
                    for (int i = 0; i < currentSkills.Count; i++)
                    {
                        var skill = currentSkills[i];
                        Console.WriteLine($"{i + 1}. {skill.Name} (Mp: {skill.Mp}) - {skill.Description})");
                    }

                    // 스킬 선택 입력할지 공격 방식 선택하는 창으로 돌아갈지.
                    while (true)
                    {
                        //사용할 스킬 번호 입력
                        input = InputHandler.GetUserActionInput();
                        skillResult = GetSkillAttackResult(input, monsterIndex);

                        if (!skillResult.inputResult.HasFlag(BattleInput.IsValid))
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ReadKey();
                        }
                        else if (!skillResult.inputResult.HasFlag(BattleInput.IsSkillAttack))
                        {
                            Console.WriteLine($"{player.Name}의 MP가 부족합니다.");
                            Console.ReadKey();
                        }
                        else break;
                    }

                    // 공격 방식 재선택
                    if (skillResult.inputResult.HasFlag(BattleInput.IsQuit))
                    {
                        Console.WriteLine("다시 공격 방식을 선택합니다.");
                        Console.ReadKey();
                        continue;
                    }

                    // 스킬 실행 출력
                    var skillResultData = skillResult.results ?? throw new ArgumentNullException(nameof(skillResult.results));

                    Console.WriteLine($"{player.Name}이(가) {skillResultData[0].SkillName}을 사용했습니다!");
                    Console.WriteLine($">>{skillResultData[0].SkillDesc}");

                    foreach (var result in skillResultData)
                    {
                        Console.WriteLine($"{result.TargetName}에게 {result.Damage}의 피해를 입혔습니다.");
                        if (result.IsDead)
                        {
                            Console.WriteLine($"{result.TargetName}을(를) 처치했습니다.");
                        }
                    }
                    Console.WriteLine("\n0.다음");
                    Console.ReadLine();
                    break;
                }

                // 몬스터 다시 선택
                if (attackType.inputResult.HasFlag(BattleInput.IsQuit))
                {
                    Console.WriteLine("다시 몬스터를 선택합니다.");
                    Console.ReadKey();
                    continue;
                }

                break;
            }
        }

        private void ShowMonsterTurn(Monster monster, int? playerOldHp, bool isDodge)
        {
            Console.Clear();
            Console.WriteLine("Battle");
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster.Level} {monster.Name}의 공격!");

            // 회피 출력
            if (isDodge)
            {
                Console.WriteLine($"{player.Name}이 피했습니다!");
            }
            else
            {
                Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {monster.Atk}]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.CurrentJob.Name})");
                Console.WriteLine($"HP {playerOldHp} → {player.Hp}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            Console.ReadKey();
        }
    }
}
