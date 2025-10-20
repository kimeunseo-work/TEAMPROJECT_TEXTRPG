using TEAMPROJECT_TEXTRPG.Core;
using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Utility;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class PlayerInfoScene : Scene
    {
        public override void Show()
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
                Console.WriteLine($"\nLv.{CharacterManager.Instance.player.Level}\n\n{CharacterManager.Instance.player.Name}({CharacterManager.Instance.player.CurrentJob.Name})\n\n공격력: {CharacterManager.Instance.player.Attack:F1}\n\n방어력: {CharacterManager.Instance.player.Defense}" +
                $"\n\n체력: {CharacterManager.Instance.player.Hp}\n\n마나: {CharacterManager.Instance.player.Mp}\n\n경험치: {CharacterManager.Instance.player.Exp}\n\nGold: {CharacterManager.Instance.player.Gold}");

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("[ 보유 아이템 목록 ]");
                Console.WriteLine();
                CharacterManager.Instance.player.ShowInventory();
                Console.WriteLine();

                Console.WriteLine($"\n0. 나가기");

                int input = InputHandler.GetUserActionInput();
                if (input == 0)
                {
                    keep = false;
                    GameManager.Instance.CurrentState = GameState.Home;
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