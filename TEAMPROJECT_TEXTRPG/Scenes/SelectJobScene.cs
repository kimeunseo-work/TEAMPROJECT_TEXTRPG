using TEAMPROJECT_TEXTRPG.Core;
using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Utility;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class SelectJobScene : Scene
    {
        public override void Show()
        {
            DisplayJob();
        }

        public void DisplayJob()
        {
            int playerSelect;
            List<Job> selectJobs = JobManager.AllJobs;
            const int jobNameWidth = 106;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("============================================================================================================");
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("||                                       원하시는 직업을 선택해주세요.                                    ||");
                Console.WriteLine("||                                                                                                        ||");
                for (int i = 0; i < selectJobs.Count; i++)
                {
                    string jobStr = $"|| {i + 1}. {selectJobs[i].Name}";
                    Console.Write(jobStr);
                    Console.Write(new string(' ', jobNameWidth - InputHandler.GetPrintableLength(jobStr)));
                    Console.WriteLine("||");
                }
                Console.WriteLine("||                                                                                                        ||");
                Console.WriteLine("============================================================================================================");

                playerSelect = InputHandler.GetUserActionInput();

                if (playerSelect <= 0 || playerSelect > selectJobs.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                }
                else
                {

                    Console.WriteLine($"선택하신 직업은 {selectJobs[playerSelect - 1].Name}입니다.");
                    Console.WriteLine("정말 이 직업으로 선택하시겠습니까? ( Y / N ) ");
                    string confirmInput = Console.ReadLine();

                    if (confirmInput == "Y" || confirmInput == "y")
                    {
                        CharacterManager.Instance.player.SetJobsStat(selectJobs[playerSelect - 1]);
                        Console.WriteLine("성공적으로 직업을 선택하셨습니다.");
                        Console.WriteLine($"{CharacterManager.Instance.player.Name} 님 환영합니다.");
                        Console.WriteLine("\n아무 키나 눌러 다음으로 진행해주세요.");
                        Console.ReadKey();
                        GameManager.Instance.CurrentState = GameState.Home;
                        break;
                    }
                }
            }
        }
    }
}