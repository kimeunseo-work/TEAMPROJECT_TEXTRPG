namespace TEAMPROJECT_TEXTRPG
{
    public class BattleResult
    {
        string input;
        bool inputTryParse;
        int inputInt;


        public void BattleResultWin()
        {
            Console.Clear();
            Console.Write(@$"
Battle!! - Result

Victory

던전에서 몬스터 {" 랜덤 몬스터 개수 "}마리를 잡았습니다.   // 참조_랜덤 몬스터 개수

Lv.{" Player 레벨 "} {" Player 이름 "}   // 참조_Player 레벨, Player 이름
HP {" 전투 전 체력 "} -> {" 전투 후 체력 "}   // 참조_전투 전 체력, 전투 후 체력

0. 다음

>> ");
                                    
            while (inputInt != 0)
            {
                input = Console.ReadLine();

                CheckInputInt();

                switch (inputInt)
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


        public void CheckInputInt()
        {
            inputTryParse = int.TryParse(input, out inputInt);

            if (inputTryParse != true)
            {
                inputInt = -1;
            }
            else
            {
                inputInt = int.Parse(input);
            }
        }
    }
}
