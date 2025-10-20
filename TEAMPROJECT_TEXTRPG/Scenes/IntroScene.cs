using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Utility;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class IntroScene : Scene
    {
        public override void Show()
        {
            var isValidInput = false;

            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============================================================================================================");
            Console.WriteLine("||                                                                                                        ||");
            Console.WriteLine("||                                             스파르타 TextRPG                                           ||");
            Console.WriteLine("||                                                                                                        ||");
            Console.WriteLine("||                                                                                                        ||");
            Console.WriteLine("|| 1. 처음 부터                                                                                           ||");
            Console.WriteLine("|| 2. 이어서 진행                                                                                         ||");
            Console.WriteLine("||                                                                                                        ||");
            Console.WriteLine("|| 0. 게임 종료                                                                                           ||");
            Console.WriteLine("||                                                                                                        ||");
            Console.WriteLine("============================================================================================================");

            var playerSelect = InputHandler.GetUserActionInput();

            switch (playerSelect)
            {
                case 1: // 1. 처음 부터
                    isValidInput = true;
                    Console.WriteLine("캐릭터를 생성합니다.");
                    GameManager.Instance.CurrentState = GameState.CharacterCreate;
                    break;
                case 2: // 2. 이어서 진행
                    isValidInput = true;
                    Console.WriteLine("로딩창을 불러옵니다.");
                    GameManager.Instance.CurrentState = GameState.LoadData;
                    break;
                case 0: // 0. 게임 종료
                    isValidInput = true;
                    Console.WriteLine("게임을 종료합니다.");
                    GameManager.Instance.CurrentState = GameState.None;
                    break;

                default: // 유효하지 않은 입력
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    break;
            }

            if (isValidInput && GameManager.Instance.CurrentState != GameState.None)
            {
                Console.WriteLine("\n아무 키나 눌러 다음으로 진행해주세요.");
                Console.ReadKey();
            }
        }
    }
}