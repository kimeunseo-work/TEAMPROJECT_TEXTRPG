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
        private bool initialized;
        public Dictionary<QuestType, Quest> Quests;
        public List<Quest> AbleQuests;
        public Quest CurrentQuest;
        public int TotalMonsterKillCount;
        public int BestEquipmentStat;
        public int BestLevel;
        public Items items;
        public Monster monster;
        public Monsters monsters;
        public Quest Quest;
        public List<KeyValuePair<QuestType, Quest>> QuestList; //그냥 퀘스트 리스트
        public List<KeyValuePair<QuestType, Quest>> OngoingQuestList;


        private QuestManager()
        {


            
        }


        internal void InitOnce()
        {
            if (initialized)
            {
                return;
            }
            initialized = true;

            monsters = new Monsters();
            items = new Items();
            Quests = new Dictionary<QuestType, Quest>(); 

            // ⚠ MonsterKillQuest 안에서 QuestManager.Instance 사용하지 않도록 주의!
            Quests.Add(QuestType.Monster, new MonsterKillQuest(monsters)); 

            QuestList = Quests.ToList(); 
        }



     
        /// <summary>
        /// 생성자 매개변수때 왜 매개변수를 왜 변수필드때 못넣는지
        /// </summary>










        public void AddMonsterCount() 
        {

            if(GameManager.Instance.monsters.All(x => x.IsDead))
            {



            } 

           






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
                        Console.WriteLine();
                        Console.WriteLine($"{OngoingQuestList[input].Value.Name}");
                        Console.WriteLine();
                        Console.WriteLine($"{OngoingQuestList[input].Value.Description}");
                        Console.WriteLine();
                        Console.WriteLine($"- {OngoingQuestList[input].Value.QuestInfo}");
                        Console.WriteLine();
                        Console.WriteLine("-보상-");
                        Console.WriteLine($"{OngoingQuestList[input].Value.ItemReward.itemName} x {OngoingQuestList[input].Value.itemGivingCount}");
                        Console.WriteLine($"{OngoingQuestList[input].Value.GoldReward}G");
                        Console.WriteLine();
                        Console.WriteLine("1. 보상받기");
                        Console.WriteLine("2. 돌아가기");
                        Console.WriteLine("원하시는 행동을 입력해주세요");
                        Console.Write(">>");

                        string input2 = Console.ReadLine();
                        if (input2 == "1")
                        {
                            //플레이어 인벤토리에 아이템이 들어갈 자리
                            OngoingQuestList[input].Value.getRewarded = true;

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



            

            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("원하시는 행동을 입력해주세요");




        }
        public void SelectCategory() //퀘스트선택창 1번
        {
            while (true) 
            {

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
