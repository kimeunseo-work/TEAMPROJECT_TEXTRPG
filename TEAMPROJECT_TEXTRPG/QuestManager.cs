using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TEAMPROJECT_TEXTRPG
{

    internal enum QuestType
    {

        Monster,
        Gear,
        enhance

    }



    internal class QuestManager
    {
        /// <싱글톤>
        /// 
        /// </summary>
        private static QuestManager instance;
        internal static QuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance;
            }
            set { }
        }//싱글톤 용 프로퍼티

        public Dictionary<Monster, int> _kills = new();
        private bool initialized;
        public Dictionary<int, Quest> Quests;
        public List<Quest> AbleQuests;
        public Quest CurrentQuest;
        public int TotalMonsterKillCount;
        public int BestEquipmentStat;
        public int BestLevel;
        public Items items = new(); //Quest클래스에서 보상아이템 빠르게 찾아놓으려고 싱글톤에 인스턴스를 만들어놓음
        public Monster monster;
        public Monsters monsters = new();
        public Quest Quest;
         //그냥 퀘스트 리스트
        public List<KeyValuePair<int, Quest>> OngoingQuestList;
        public List<KeyValuePair<int, Quest>> QuestList;




        internal void InitOnce()
        {
            if (initialized)
            {
                return;
            }
            initialized = true;

            
            
            Quests = new Dictionary<int, Quest>(); 

            // ⚠ MonsterKillQuest 안에서 QuestManager.Instance 사용하지 않도록 주의!
            Quests.Add(1, new MonsterKillQuest());
            QuestList = new List<KeyValuePair<int, Quest>>();
            OngoingQuestList = new List<KeyValuePair<int, Quest>>();

            QuestList = Quests.ToList();


        }




        /// <summary>
        /// 생성자 매개변수때 왜 매개변수를 왜 변수필드때 못넣는지
        /// </summary>










        public void OnMonsterKilled(Monster monster)
        {
            // 진행 중인 퀘스트 중에 '이 몬스터를 잡는 퀘스트'가 있는지 확인
            foreach (var quest in OngoingQuestList)
            {
                if (quest.Value.QuestMonster == monster)
                {
                    // Dictionary<Monster, int> _kills에 카운트 증가
                    if (!_kills.ContainsKey(monster))
                        _kills[monster] = 0;
                    _kills[monster]++;

                    // 퀘스트 목표 달성 체크
                    if (_kills[monster] >= quest.Value.KillCount)
                    {
                        quest.Value.isClear = true;
                        Console.WriteLine($"퀘스트 완료: {quest.Value.Name}");
                    }
                }
            }
        }


        public void QuestAccept()
        {






        }





        public void CheckBestStat() { }
        public void CheckBestLevel() { }


        public void CurrentQuestScene() // 현재 진행중인 퀘스트
        {
            while (true)
            {


                Console.Clear();
                Console.WriteLine("Quest!!");
                Console.WriteLine();
                if (OngoingQuestList != null) //진행중인 퀘스트가 있을때
                {


                    for (int i = 0; i < OngoingQuestList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {OngoingQuestList[i].Value.Name}");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("원하시는 퀘스트를 선택해주세요.(퀘스트 숫자 입력");
                    Console.WriteLine("\n\n");
                    Console.WriteLine("0. 나가기");
                    Console.Write(">>");


                    if (int.TryParse(Console.ReadLine(), out int input) && input <= OngoingQuestList.Count && input > 0)
                    {

                        Console.Clear();
                        Console.WriteLine("Quest!!");
                        if (OngoingQuestList.Any(kvp => kvp.Key == input))
                        {
                            var selectedQuest = OngoingQuestList.Find(kvp => kvp.Key == input).Value;

                            selectedQuest.Show();

                        }
                        else
                        {
                            Console.WriteLine("해당 번호의 퀘스트는 없습니다!");
                        }



                        Console.WriteLine("1. 보상받기");
                        Console.WriteLine("2. 돌아가기");
                        Console.WriteLine("원하시는 행동을 입력해주세요");
                        Console.Write(">>");

                        string input2 = Console.ReadLine();
                        if (input2 == "1")
                        {
                            //플레이어 인벤토리에 아이템이 들어갈 자리
                            var selectedQuest = OngoingQuestList.Find(kvp => kvp.Key == input).Value;
                            selectedQuest.getRewarded = true;

                        }
                        else if (input2 == "2")
                        {
                            Console.Clear();

                            continue;


                        }

                    }
                    else if (input == 0)
                    {

                        Console.Clear();
                        SelectCategory();


                    }
                    else //퀘스트 리스트에서 퀘스트 번호 선택했을때 퀘스트가 있어서
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        continue;


                    }


                }
                else if (OngoingQuestList == null)
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


        public void SelectQuestScene()  // 퀘스트 선택
        {
            Console.Clear();
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine();
            foreach (var kvp in QuestList)
            {
                Console.WriteLine($"{kvp.Key}. {kvp.Value.Name}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 퀘스트를 선택해주세요.(퀘스트 숫자 입력");
            Console.WriteLine("\n\n");
            Console.WriteLine("0. 나가기");
            Console.Write(">>");

            if(int.TryParse(Console.ReadLine(), out int input))
            {

                if (QuestList.Any(kvp => kvp.Key == input))
                {
                    var selectedQuest = QuestList.Find(kvp => kvp.Key == input).Value;

                    selectedQuest.Show();

                    Console.WriteLine("1. 수락");
                    Console.WriteLine("2. 거절");
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
                    

                    while (true)
                    {

                        string input2 = Console.ReadLine();
                        if (input2 == "1")
                        {

                            selectedQuest.isSelected = true;

                            OngoingQuestList.Add(QuestList.Find(kvp => kvp.Key == input));
                            Console.WriteLine("수락 되었습니다! 나가려면 아무키나 눌러주세요");
                            Console.ReadKey();
                            SelectCategory();




                        }
                        else if (input2 == "2")
                        {

                            Console.WriteLine("거절 되었습니다! 나가려면 아무키나 눌러주세요");
                            Console.ReadKey();
                            SelectCategory();

                        }
                        else
                        {

                            Console.WriteLine("잘못된 키입니다");
                            /*Thread.Sleep(1000);
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.Write(">>");*/

                            continue;


                        }


                    }
                    








                }
                else if (input == 0)
                {

                    SelectCategory();
                    

                }
                else
                {
                    Console.WriteLine("해당 번호의 퀘스트는 없습니다!");
                }
            }
         
                //퀘스트가 정해지면 그다음에 
            
        }




        
        public void SelectCategory() //퀘스트선택창 1번
        {
            while (true) 
            {
                InitOnce();
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
                    GameManager.Instance.currentState = GameState.Home;
                    break;

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();

                    continue;

                }



            }
            



         
           

        }










    }
}
