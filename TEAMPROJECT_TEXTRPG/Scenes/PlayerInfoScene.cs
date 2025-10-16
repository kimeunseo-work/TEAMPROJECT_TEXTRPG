namespace TEAMPROJECT_TEXTRPG.Scenes
{


    internal class PlayerInfoScene : Scene
    {
        private Player player;

        public PlayerInfoScene(Player player)
        {
            this.player = player;
        }
        internal override void Show()
        {
            DisplayPlayerInfo();
        }

        //상태보기
        public void DisplayPlayerInfo()
        {
            bool keep = true;
            while (keep)
            {
                Console.Clear();
                Console.WriteLine($"\nLv.{player.Level}\n\n{player.Name}({player.Job})\n\n공격력: {player.Attack:F1}\n\n방어력: {player.Defense}" +
                $"\n\n체력: {player.Hp}\n\nGold: {player.Gold}");

                Console.WriteLine($"\n1.\n0. 나가기");

                int input = InputHandler.GetUserActionInput();
                if (input == 0)
                {
                    keep = false;
                    GameManager.Instance.currentState = GameState.Home;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }


    }


}
