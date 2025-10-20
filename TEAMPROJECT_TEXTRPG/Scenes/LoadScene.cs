using System.Xml;
using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Utility;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class LoadScene : Scene
    {
        public override void Show()
        {
            Console.Clear();

            var saveDatas = DataManager.Instance.saveDatas;
            if ( saveDatas == null || saveDatas.Count == 0)
            {
                Console.WriteLine("저장된 데이터가 없습니다.");
                Console.WriteLine("처음으로 돌아갑니다.");
                Console.ReadKey();

                GameManager.Instance.CurrentState = GameState.Intro;
                return;
            }


            for (int index = 0; index < saveDatas.Count; index++)
            {
                var data = saveDatas[index];
                Console.WriteLine($"- [{index + 1}] Lv.{data.Item2.Level} {data.Item2.Name} {data.Item2.CurrentJob.Name} | {data.Item1.ToString("g")}");
            }
            Console.WriteLine("\n0. 처음으로");


            int playerSelect;
            while (true)
            {
                playerSelect = InputHandler.GetUserActionInput();

                if (playerSelect >= 1 && playerSelect <= saveDatas.Count)
                {
                    DataManager.Instance.LoadSave(playerSelect - 1);
                    Console.WriteLine("\n데이터를 로드했습니다.");
                    Console.ReadKey();

                    GameManager.Instance.CurrentState = GameState.Home;
                    return;
                }
                else if (playerSelect == 0)
                {
                    Console.WriteLine("처음으로 돌아갑니다.");
                    Console.ReadKey();

                    GameManager.Instance.CurrentState = GameState.Intro;
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                }
            }
        }
    }
}
