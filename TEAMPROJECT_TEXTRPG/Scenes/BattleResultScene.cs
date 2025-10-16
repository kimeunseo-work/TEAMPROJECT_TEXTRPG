namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class BattleResultScene : Scene
    {
        string input;
        int parsedInput;
        bool isParsedSuccess;

        internal void BattleResultWin()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("Victory!!");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {GameManager.Instance.monsters.Count}마리를 잡았습니다.");
            Console.WriteLine($"Lv.{CharacterManager.Instance.player.Level} {CharacterManager.Instance.player.Name}");
            Console.WriteLine($"HP { CharacterManager.Instance.player.hped} -> { CharacterManager.Instance.player.Hp}");
            Console.WriteLine();

            for (int i = 0; i < GameManager.Instance.monsters.Count; i++)
            {
                CharacterManager.Instance.player.AddExp(GameManager.Instance.monsters[i].MonExp);
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">> ");

            WhileInput0();
        }


        internal void BattleResultLose()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("You Lose..");
            Console.WriteLine();
            Console.WriteLine($"Lv.{CharacterManager.Instance.player.Level} {CharacterManager.Instance.player.Name}");
            Console.WriteLine($"HP {CharacterManager.Instance.player.hped} -> 0");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">> ");

            WhileInput0();
        }

        // Input 값이 '0' 일 때까지 반복하는 메서드
        private void WhileInput0()
        {
            do
            {
                input = Console.ReadLine();

                CheckInputInt();

                switch (parsedInput)
                {
                    case 0:
                        Console.Clear();
                        GameManager.Instance.currentState = GameState.Home;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                        Console.Write(">> ");
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

        internal override void Show()
        {
            if (GameManager.Instance.currentBattleState == BattleState.Victory)
            {
                BattleResultWin();
            }
            else if (GameManager.Instance.currentBattleState == BattleState.Defeat)
            {
                BattleResultLose();
            }
        }
    }
}
