using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Utility;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class InitCharacterScene : Scene
    {
        public override void Show()
        {
            DisplayCreate();
        }

        public void DisplayCreate()
        {
            while (true)
            {
                string playerInputName;

                // 설정 가능한 최소 닉네임
                int minNameLength = 2;
                // 설정 가능한 최대 닉네임
                int MaxNameLength = 8;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("============================================================================================================");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("||                                 스파르타 던전에 오신 여러분 환영합니다.                                ||");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("============================================================================================================");

                playerInputName = InputHandler.GetUserCharacterName();

                // 사용자가 빈 공간으로 입력 시
                if (string.IsNullOrWhiteSpace(playerInputName))
                {
                    Console.WriteLine("이름은 공백으로 설정할 수 없습니다. 다시 입력해주세요.");
                    Console.WriteLine("\n아무 키나 눌러 다음으로 진행해주세요.");
                    Console.ReadKey();
                }
                // 사용자의 입력이 최소 최대 글자 수 범위에 벗어 났을 때
                else if (playerInputName.Length < minNameLength || playerInputName.Length > MaxNameLength)
                {
                    Console.WriteLine("이름은 최소 2글자에서 최대 8글자까지 가능합니다.");
                    Console.WriteLine("\n아무 키나 눌러 다음으로 진행해주세요.");
                    Console.ReadKey();
                }
                else
                {
                    // 사용자가 입력한 이름 출력 및 사용자에게 확인 여부 제공
                    Console.WriteLine($"작성하신 이름은 {playerInputName} 입니다.");
                    Console.WriteLine("정말 이 이름으로 하시겠습니까? ( Y / N ) ");
                    string confirmInput = Console.ReadLine();

                    // Y or y 입력 시 다음으로 진행
                    if (confirmInput == "Y" || confirmInput == "y")
                    {
                        CharacterManager.Instance.player.Name = playerInputName;
                        Console.WriteLine($"'{playerInputName}'(으)로 캐릭터 이름이 성공적으로 설정되었습니다.");
                        Console.WriteLine("\n아무 키나 눌러 다음으로 진행해주세요.");
                        Console.ReadKey();
                        GameManager.Instance.CurrentState = GameState.SelectJob;
                        break;
                    }

                }
            }
        }
    }
}