using TEAMPROJECT_TEXTRPG.Skills;

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

        /* 생명 주기 */
        //============================================================//

        private void OnEnter()
        {
            BattleManager.Instance.OnMonsterSpawned += ShowBattleStart;
            BattleManager.Instance.OnMonsterActioned += ShowMonsterTurn;
        }

        private void OnExit()
        {
            BattleManager.Instance.OnMonsterSpawned -= ShowBattleStart;
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
            Console.WriteLine($"Lv.{CharacterManager.Instance.player.Level}  {CharacterManager.Instance.player.Name} ({CharacterManager.Instance.player.CurrentJob.Name})");
            Console.WriteLine($"HP {CharacterManager.Instance.player.Hp}/{CharacterManager.Instance.player.MaxHP}");
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
                }
                else break;
            }

            if (isBattleStart.HasFlag(BattleInput.IsQuit))
            {
                Console.WriteLine("메인 화면으로 돌아갑니다.");
                Console.ReadKey();
            }
        }
        private void ShowPlayerTurn()
        {
            Console.Clear();
            Console.WriteLine("**Battle!!**\n");

            // ShowMonsters()
            for (int i = 0; i < BattleManager.Instance.CurrentMonsters.Count; i++)
            {
                var monster = BattleManager.Instance.CurrentMonsters[i];
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
                Console.WriteLine($"{CharacterManager.Instance.player.Name}이 피했습니다!");
            }
            else
            {
                Console.WriteLine($"{CharacterManager.Instance.player.Name} 을(를) 맞췄습니다. [데미지 : {monster.Atk}]");
                Console.WriteLine($"Lv.{CharacterManager.Instance.player.Level} {CharacterManager.Instance.player.Name} ({CharacterManager.Instance.player.CurrentJob.Name})");
                Console.WriteLine($"HP {playerOldHp} → {CharacterManager.Instance.player.Hp}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");

            Console.ReadKey();
        }
    }
}
