namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class CharacterCreateScene : Scene
    {
        private Player player;

        public CharacterCreateScene(Player player)
        {
            this.player = player;
        }

        internal override void Show()
        {
            DisplayCreate();
        }

        public void DisplayCreate()
        {
            while (true)
            {
                string playerInputName;
                int minNameLength = 2;
                int MaxNameLength = 8;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("============================================================================================================");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("||                                 스파르타 던전에 오신 여러분 환영합니다.                                ||");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("============================================================================================================");

                playerInputName = InputHandler.GetUserCharacterName();

                if (string.IsNullOrWhiteSpace(playerInputName))
                {
                    Console.WriteLine("이름은 공백으로 설정할 수 없습니다. 다시 입력해주세요.");
                    Console.WriteLine("아무 키나 눌러 다음으로 진행해주세요.");
                    Console.ReadKey();
                }
                else if (playerInputName.Length < minNameLength || playerInputName.Length > MaxNameLength)
                {
                    Console.WriteLine("이름은 최소 2글자에서 최대 8글자까지 가능합니다.");
                    Console.WriteLine("아무 키나 눌러 다음으로 진행해주세요.");
                    Console.ReadKey();
                }
                else
                {
                    player.Name = playerInputName;
                    Console.WriteLine($"'{playerInputName}'(으)로 캐릭터 이름이 성공적으로 설정되었습니다.");
                    Console.WriteLine("아무 키나 눌러 다음으로 진행해주세요.");
                    Console.ReadKey();
                    GameManager.Instance.currentState = GameState.Home;
                    // GameManager.Instance.currentState = GameState.SelectJob;
                    break;
                }

            }
        }
    }
}
