namespace TEAMPROJECT_TEXTRPG
{
    internal class BattleResult : Scene
    {
        string input;
        int parsedInput;
        bool isParsedSuccess;

        private Player player;


        internal BattleResult(Player player)
        {
            this.player = player;
        }


        internal void BattleResultWin()
        {
            Console.Clear();
            // 참조 요구_{ 랜덤 몬스터 개수 }
            Console.Write(@$"
Battle!! - Result

Victory

던전에서 몬스터 {" 랜덤 몬스터 개수 "}마리를 잡았습니다.

Lv.{player.Level} {player.Name}
HP 100 -> {player.Hp}

0. 다음

>> ");

            WhileInput0();
        }


        internal void BattleResultLose()
        {
            Console.Clear();
            Console.Write($@"
Battle!! - Result

You Lose

Lv.{player.Level} {player.Name}
HP 100 -> 0

0. 다음

>> ");

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
                        Console.Write("잘못된 입력입니다.\n\n>> ");
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
