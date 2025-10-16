using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class SelectJobScene : Scene
    {
        internal override void Show()
        {
            //DisplayJob();
        }

        public void DisplayJob()
        {
            int playerSelect;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("============================================================================================================");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("||                                       원하시는 직업을 선택해주세요.                                    ||");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine($"|| 1. xxx.job.[index]                                                                                     ||");
                Console.WriteLine($"|| 2. xxx.job.[index]                                                                                     ||");
                Console.WriteLine($"|| 3. xxx.job.[index]                                                                                     ||");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("============================================================================================================");

                playerSelect = InputHandler.GetUserActionInput();

                /*
                if(playerSelect <= 0 || playerSelect > xxx.job.Length또는Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                }
                else
                {
                    
                    Console.WriteLine($"선택하신 직업은 xxx.job.[playerSelect-1]입니다.");
                    Console.WriteLine("정말 이 직업으로 선택하시겠습니까? ( Y / N ) ");
                    string confirmInput = Console.ReadLine();

                    if(confirmInput == "Y" || confirmInput == "y")
                    {
                        Console.WriteLine("성공적으로 직업을 선택하셨습니다.");
                        Console.WriteLine($"{CharacterManager.Instance.player.Name} 님 환영합니다.");
                        Console.WriteLine("\n아무 키나 눌러 다음으로 진행해주세요.");
                        Console.ReadKey();
                    }
                }
                */
            }
        }
    }
}
