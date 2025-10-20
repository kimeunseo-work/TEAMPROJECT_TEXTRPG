#if false
using TEAMPROJECT_TEXTRPG.Core;

namespace TEAMPROJECT_TEXTRPG.Managers
{
    internal enum QuestType
    {
        Monster,
        Gear,
        enhance
    }

    internal class QuestManager
    {
        /// <summary>
        /// 싱글톤
        /// </summary>
        private static QuestManager instance;
        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance;
            }
        }

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
            monsters = new Monsters();

            Quests = new Dictionary<QuestType, Quest>();
            items = new Items();

            Quests.Add(QuestType.Monster, new MonsterKillQuest(monsters));
            QuestList = Quests.ToList();
        }

        //quest 생성자가 함수인데 이제 필드로 선언만했을때는 this가 완성되지 않아서 프로그램이 알 방법이없다. 그래서 함수 안에서 선언을 해서 
        //함수가 실행될때 선언되도록
        /// <summary>
        /// 생성자 매개변수때 왜 매개변수를 왜 변수필드때 못넣는지
        /// </summary>
        public void AddMonsterCount()
        {

            if (GameManager.Instance.monsters.All(x => x.IsDead))  //퀘스트 몬스터가 있을시 )
            {
                // 여기서 이제 몬스터 카운트가 올라감
                //그니까 이제 퀘스트에 몬스터 카운트가 올라가는건데 

                //그니까 이제 그 죽은 몬스터가 퀘스트 몬스터였던거임;;;
                //그러면 이제 퀘스트에 있는 몬스터였다 그러면
                //이제 그 퀘스트 완료조건의 그 몬스터 카운트를

                //이제 이 함수를 실행해서
                //그 퀘스트 완료조건을 채울수있게 하는거임
            }
        }
        public void SelectShow()
        {
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
        }
        public void OngoingShow()
        {
            Console.WriteLine("1. 보상받기");
            Console.WriteLine("2. 돌아가기");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
        }
        public void CheckBestStat() { }
        public void CheckBestLevel() { }
        public void CurrentQuestScene()
        {

            Console.WriteLine("Quest!!");
            Console.WriteLine();

            for (int i = 0; i < QuestList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {QuestList[i].Value.Name}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();

            if (input == "1")
            {

            }
        }
        public void SelectQuestScene()
        {

        }
        public void SelectCategory()
        {
            Console.Clear();

            Console.WriteLine("1. 현재 진행중인 퀘스트");
            Console.WriteLine();
            Console.WriteLine("2. 퀘스트 선택");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");

            while (true)
            {
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
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.Write(">>");

                    continue;
                }
            }
        }
    }
}
#endif