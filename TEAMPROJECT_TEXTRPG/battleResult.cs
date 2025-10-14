namespace TEAMPROJECT_TEXTRPG
{
    public class BattleResult
    {
        string input;
        int parsedInput;
        bool isParsedSuccess;


        public void BattleResultWin()
        {
            Console.Clear();
            // 참조 요구_{ 랜덤 몬스터 생성 값 }, { 현재 Player 레벨 }, { Player 이름 }, { 전투 전 체력 }, { 전투 후 체력 }
            Console.Write(@$"
Battle!! - Result

Victory

던전에서 몬스터 {" 랜덤 몬스터 개수 "}마리를 잡았습니다.

Lv.{" Player 레벨 "} {" Player 이름 "}
HP {" 전투 전 체력 "} -> {" 전투 후 체력 "}

0. 다음

>> ");

            WhileInput0();
        }


        public void BattleResultLose()
        {
            Console.Clear();
            // 참조 요구_{ 현재 Player 레벨 }, { Player 이름 }, { 전투 전 체력 }
            Console.Write($@"
Battle!! - Result

You Lose

Lv.{" Player 레벨 "} {" Player 이름 "}
HP {" 전투 전 체력 "} -> 0

0. 다음

>> ");

            WhileInput0();
        }


        // Input 값이 '0' 일 때까지 반복하는 메서드
        public void WhileInput0()
        {
            while (parsedInput != 0)
            {
                input = Console.ReadLine();

                CheckInputInt();

                switch (parsedInput)
                {
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        Console.Write("잘못된 입력입니다.\n\n>> ");
                        break;
                }
            }
        }


        // Input 값이 'int 자료형' 으로 변환 가능한지 체크하는 메서드 (변환 불가 시 '-1' 값으로 고정)
        public void CheckInputInt()
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
