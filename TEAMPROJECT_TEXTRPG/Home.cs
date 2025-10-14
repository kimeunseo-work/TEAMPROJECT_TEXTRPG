using System;
using System.Collections.Generic;
using System.Threading;

namespace TEAMPROJECT_TEXTRPG
{
    internal class Home : Scene
    {
        internal override void Show()
        {
            DisplayHome();
        }

        public void DisplayHome()
        {
            int playerSelect;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Clear();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("============================================================================================================");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("||                               스파르타 마을에 오신 여러분 환영합니다.                                  ||");
                Console.WriteLine("||                          이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.                          ||");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("|| 1. 상태 보기                                                                                           ||");
                Console.WriteLine("|| 2. 전투 시작                                                                                           ||");
                Console.WriteLine("|| 0. 게임 종료                                                                                           ||");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("============================================================================================================");

                playerSelect = InputHandler.GetUserActionInput();

                switch (playerSelect)
                {
                    case 1: // 1. 상태 보기
                        isValidInput = true;
                        Console.WriteLine("상태보기를 선택하셨습니다.");
                        GameManager.Instance.currentState = GameState.Stat;
                        break;

                    case 2: // 2. 전투 시작
                        isValidInput = true;
                        Console.WriteLine("전투를 선택하셨습니다.");
                        GameManager.Instance.currentState = GameState.Battle;
                        break;

                    case 0: // 0. 게임 종료
                        isValidInput = true;
                        Console.WriteLine("게임을 종료합니다.");
                        GameManager.Instance.currentState = GameState.None;
                        break;

                    default: // 유효하지 않은 입력
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }

                if (isValidInput && GameManager.Instance.currentState != GameState.None)
                {
                    Console.WriteLine("\n아무 키나 눌러 다음으로 진행해주세요.");
                    Console.ReadKey();
                }
            }
        }
    }
}