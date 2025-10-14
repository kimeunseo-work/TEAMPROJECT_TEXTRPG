using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

                if (playerSelect == 1)
                {
                    isValidInput = true;
                    Console.WriteLine("상태보기를 선택하셨습니다.");
                    //GameManager.Instance.currentState = GameState.Stat;
                }
                else if (playerSelect == 2)
                {
                    isValidInput = true;
                    Console.WriteLine("전투를 선택하셨습니다.");
                    //GameManager.Instance.currentState = GameState.Battle;
                }
                else if (playerSelect == 0)
                {
                    isValidInput = true;
                    Console.WriteLine("게임종료를 선택하셨습니다.");
                    GameManager.Instance.currentState = GameState.None;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
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