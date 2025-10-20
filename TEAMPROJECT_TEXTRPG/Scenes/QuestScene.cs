using TEAMPROJECT_TEXTRPG.Core;
using TEAMPROJECT_TEXTRPG.Managers;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class QuestScene : Scene
    {
        QuestManager questManager;
        public QuestScene()
        {
            questManager = QuestManager.Instance;
        }

        public override void Show()
        {
            SelectCategory();
        }

        /// <summary>
        /// 퀘스트 씬 진입시 첫 화면
        /// </summary>
        public void SelectCategory()
        {
            while (true)
            {
                //InitOnce();

                Console.Clear();
                Console.WriteLine("\n\n");
                Console.WriteLine("1. 현재 진행중인 퀘스트");
                Console.WriteLine("\n\n");
                Console.WriteLine("2. 퀘스트 선택");
                Console.WriteLine("\n\n");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">>");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    CurrentQuestScene();
                    break;
                }
                else if (input == "2")
                {
                    SelectQuestScene();
                    break;
                }
                else if (input == "0")
                {
                    GameManager.Instance.CurrentState = GameState.Home;
                    return;

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();

                    continue;
                }
            }
        }

        /// <summary>
        /// 현재 진행중인 퀘스트 출력
        /// </summary>
        public void CurrentQuestScene()
        {
            var onGoingQuestList = questManager.OnGoingQuestList;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quest!!");
                Console.WriteLine();
                Console.WriteLine("진행중인 퀘스트");
                Console.WriteLine();

                if (onGoingQuestList.Count != 0) //진행중인 퀘스트가 있을때
                {
                    for (int i = 0; i < onGoingQuestList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {onGoingQuestList[i].Value.Name} {onGoingQuestList[i].Value.ClearText}");
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("원하시는 퀘스트를 선택해주세요.(퀘스트 숫자 입력)");
                    Console.WriteLine("\n\n");
                    Console.WriteLine("0. 나가기");
                    Console.Write(">>");

                    //0이 아닌 퀘스트를 눌렀을때
                    if (int.TryParse(Console.ReadLine(), out int input) && input <= onGoingQuestList.Count && input > 0)
                    {
                        Console.Clear();

                        if (input >= 1 && input - 1 <= onGoingQuestList.Count)
                        {
                            var selectedQuest = onGoingQuestList[input - 1].Value;

                            if (onGoingQuestList[input - 1].Value is MonsterKillQuest monQuest)
                            {
                                monQuest.Show();
                            }

                            Console.WriteLine("1. 보상받기");
                            Console.WriteLine("2. 돌아가기");
                            Console.WriteLine("원하시는 행동을 입력해주세요");
                            while (true)
                            {
                                Console.Write(">>");
                                string input2 = Console.ReadLine();
                                if (input2 == "1") //보상받기
                                {
                                    if (selectedQuest.GetRewarded == false)
                                    {
                                        if (selectedQuest.IsComplete == true)
                                        {
                                            int rewardGold = selectedQuest.GoldReward;
                                            CharacterManager.Instance.player.Gold += rewardGold;

                                            selectedQuest.GetRewarded = true;

                                            Console.WriteLine("보상이 수령되었습니다! 아무키를 눌러주세요.");
                                            Console.ReadKey();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("아직 퀘스트조건을 완료하지 못했습니다!");
                                            Console.ReadKey();

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("이미 보상을 수령했습니다!");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                                else if (input2 == "2") //돌아가기
                                {
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("잘못된 입력입니다");
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("해당 번호의 퀘스트는 없습니다!");
                        }
                    }
                    else if (input == 0) //0을 눌렀을때
                    {
                        Console.Clear();
                        SelectCategory();
                        break;
                    }
                    else //퀘스트 리스트에서 퀘스트 번호 선택했을때 퀘스트가 있어서
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
                else if (onGoingQuestList.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine("-- 현재 진행중인 퀘스트가 없습니다 --");
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine();

                    Console.WriteLine("0. 나가기");
                    Console.Write(">>");
                    string input = Console.ReadLine();

                    if (input == "0")
                    {
                        SelectCategory(); //진행중인 퀘스트가 없어서 진행할 퀘스트를 고를수도 있기 때문에 0번으로 선택퀘스트있는 창으로 돌아감
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 퀘스트 수주 화면 출력
        /// </summary>
        public void SelectQuestScene()
        {
            var questList = questManager.QuestList;

            Console.Clear();
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine("퀘스트 선택하기");
            Console.WriteLine();

            foreach (var quest in questList)
            {
                Console.WriteLine($"{quest.Key}. {quest.Value.Name}");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 퀘스트를 선택해주세요.(퀘스트 숫자 입력)");
            Console.WriteLine("\n\n");
            Console.WriteLine("0. 나가기");
            Console.Write(">>");

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (questList.Any(quest => quest.Key == input))
                {
                    Console.Clear();

                    var selectedQuest = questList.Find(quest => quest.Key == input).Value;

                    if (questList.Find(quest => quest.Key == input).Value is MonsterKillQuest monQuest)
                    {
                        monQuest.Show();
                    }

                    Console.WriteLine("1. 수락");
                    Console.WriteLine("2. 거절");
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");

                    while (true)
                    {
                        string input2 = Console.ReadLine();
                        if (input2 == "1")
                        {
                            selectedQuest.IsAccept = true;

                            if(selectedQuest is MonsterKillQuest)
                            {
                                questManager.OnGoingQuestList.Add(new KeyValuePair<int, Quest>(input, new MonsterKillQuest(selectedQuest)));
                            }
                            Console.WriteLine("수락 되었습니다! 나가려면 아무키나 눌러주세요");
                            Console.ReadKey();
                            SelectQuestScene();
                            break;
                        }
                        else if (input2 == "2")
                        {
                            Console.WriteLine("거절 되었습니다! 나가려면 아무키나 눌러주세요");
                            Console.ReadKey();
                            SelectQuestScene();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 키입니다");
                            continue;
                        }
                    }
                }
                else if (input == 0)
                {
                    SelectCategory();
                    return;
                }
                else
                {
                    Console.WriteLine("해당 번호의 퀘스트는 없습니다!");
                }
            }
        }
    }
}