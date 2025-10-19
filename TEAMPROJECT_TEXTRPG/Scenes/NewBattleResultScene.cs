using TEAMPROJECT_TEXTRPG.Managers;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class NewBattleResultScene : Scene
    {
        string input;
        int parsedInput;
        bool isParsedSuccess;
        
        public override void Show()
        {
            BattleManager.Instance.OnBattleResultReady += HandleResult;

            while (GameManager.Instance.CurrentState == GameState.NewBattleResult)
            {
                Thread.Sleep(100);
            }

            BattleManager.Instance.OnBattleResultReady -= HandleResult;
        }

        private void HandleResult(NewBattleState result, int[] monsterExps)
        {
            if (result == NewBattleState.Victory)
            {
                BattleResultWin(monsterExps);
            }
            else if (result == NewBattleState.Lose)
            {
                BattleResultLose(monsterExps.Length);
            }
        }

        public void BattleResultWin(int[] monsterExps)
        {
            Console.Clear();

            BattleResultWinText(monsterExps.Length);

            for (int i = 0; i < monsterExps.Length; i++)
            {
                CharacterManager.Instance.player.AddExp(monsterExps[i]);
            }
            DaumText();

            WhileInput0(monsterExps.Length);
        }

        public void BattleResultLose(int count)
        {
            Console.Clear();

            BattleResultLoseText();

            DaumText();

            WhileInput0(count);
        }

        private void BattleResultWinText(int count)
        {
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("Victory!!");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {count}마리를 잡았습니다.");
            Console.WriteLine($"Lv.{CharacterManager.Instance.player.Level} {CharacterManager.Instance.player.Name}");
            Console.WriteLine($"HP {CharacterManager.Instance.player.hped} -> {CharacterManager.Instance.player.Hp}");
            Console.WriteLine();
        }

        private void BattleResultLoseText()
        {
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("You Lose..");
            Console.WriteLine();
            Console.WriteLine($"Lv.{CharacterManager.Instance.player.Level} {CharacterManager.Instance.player.Name}");
            Console.WriteLine($"HP {CharacterManager.Instance.player.hped} -> 0");
            Console.WriteLine();
        }

        private void DaumText()
        {
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">> ");
        }

        // Input 값이 '0' 일 때까지 반복하는 메서드
        private void WhileInput0(int count)
        {
            do
            {
                input = Console.ReadLine();

                CheckInputInt();

                switch (parsedInput)
                {
                    case 0:
                        Console.Clear();
                        GameManager.Instance.CurrentState = GameState.Home;
                        break;
                    default:
                        if (CharacterManager.Instance.player.Hp > 0)
                        {
                            Console.Clear();

                            BattleResultWinText(count);
                            /* 
                             - 레벨업 시 출력되던 내역 누락됨(수정 필요)
                               경험치 관련 코드를 그대로 사용 시 경험치가 중복 획득되는 오류가 있음
                             */
                            DaumText();
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.WriteLine();
                            Console.Write(">> ");
                        }
                        else if (CharacterManager.Instance.player.Hp <= 0)
                        {
                            Console.Clear();

                            BattleResultLoseText();
                            DaumText();
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.WriteLine();
                            Console.Write(">> ");
                        }
                        break;
                }
            }
            while (parsedInput != 0);
        }

        // Input 값이 'int 자료형' 으로 변환 가능한지 체크하는 메서드 (변환 불가 시 '-1' 값으로 고정)
        private void CheckInputInt()
        {
            isParsedSuccess = int.TryParse(input, out parsedInput);

            if (isParsedSuccess)
            {
                parsedInput = int.Parse(input);
            }
            else
            {
                parsedInput = -1;
            }
        }
    }
}