using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Utility;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class SaveScene : Scene
    {
        public override void Show()
        {
            Console.Clear();

            var saveDatas = DataManager.Instance.saveDatas;
            if (saveDatas == null || saveDatas.Count == 0)
            {
                Console.WriteLine("저장된 데이터가 없습니다.\n");
            }
            else
            {
                for (int index = 0; index < saveDatas.Count; index++)
                {
                    var data = saveDatas[index];
                    Console.WriteLine($"- [{data.Item1}] Lv.{data.Item3.Level} {data.Item3.Name} {data.Item3.CurrentJob.Name} | {data.Item2.ToString("g")}");
                }
            }

            Console.WriteLine("\n숫자를 입력하고 엔터를 눌러 저장하세요.");
            Console.WriteLine("세이브 파일과 숫자가 같다면 덮어씌워집니다.");
            Console.WriteLine("\n0. 처음으로\n");

            int playerSelect;
            while (true)
            {
                playerSelect = InputHandler.GetInputToInt();

                if (playerSelect == 0)
                {
                    Console.WriteLine("처음으로 돌아갑니다.");
                    Console.ReadKey();

                    GameManager.Instance.CurrentState = GameState.Home;
                    return;
                }
                else if(playerSelect == int.MaxValue)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    ConsoleUtility.ClearLine(Console.CursorTop, 4);
                }
                else
                {
                    DataManager.Instance.SaveData(playerSelect);
                    Console.WriteLine("\n데이터를 성공적으로 저장했습니다.");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }
}
