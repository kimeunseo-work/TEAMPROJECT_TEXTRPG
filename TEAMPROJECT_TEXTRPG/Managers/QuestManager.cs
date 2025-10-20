using System.Xml.Linq;
using TEAMPROJECT_TEXTRPG._Unfinished;
using TEAMPROJECT_TEXTRPG.Core;

namespace TEAMPROJECT_TEXTRPG.Managers
{
    internal enum QuestType
    {
        Monster,
        Gear,
        enhance
    }

    public class QuestManager
    {
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

        // 실행시 최초 1번 이니셜라이징
        //private bool initialized;
        // 업적, 플레이어가 어떤 몬스터를 몇 번 킬 했는지 기록
        // int = 몬스터 ID, int = killCount
        public Dictionary<int, int> killCountRecord;
        // 퀘스트 기본 데이터 저장 딕셔너리
        public Dictionary<int, Quest> Quests;
        // Quest클래스에서 보상아이템 빠르게 찾아놓으려고 싱글톤에 인스턴스를 만들어놓음?
        public Items items = new();
        // public Monster monster; 안쓰네요?
        public Monsters monsters = new();
        // public Quest Quest; 안쓰네요?

        // 현재 진행 중인 퀘스트 리스트... 왜 KeyValuePair? 퀘스트 중복으로 받기 가능한가?
        public List<KeyValuePair<int, Quest>> OnGoingQuestList { get; set; }
        // 전체 퀘스트 리스트?
        public List<KeyValuePair<int, Quest>> QuestList;

        /// <summary>
        /// 초기화 하라고 생성자가 있는거에용
        /// </summary>
        public QuestManager()
        {
            killCountRecord = new();
            for (int i = 0; i < monsters.monster.Count; i++)
            {
                var m = monsters.monster[i];
                killCountRecord.Add(m.Id, 0);
            }

            // 매니저에 가지고 있을 퀘스트 데이터 리스트 할당
            Quests = new Dictionary<int, Quest>();
            Quests.Add(1, new MonsterKillQuest(
                name: "마을을 위협하는 미니언 처치",
                desc: "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n" +
                    "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n" +
                    "모험가인 자네가 좀 처치해주게!",
                needkillCount: 1,
                getRewarded: false,
                questMon: new Monsters().monster[0], //미니언 몬스터 할당
                itemReward: new Items().equipItems[1],// QuestManager.Instance.items.equipItems[1];
                itemCount: 1,
                gold: 5,
                exp: 100));
            // ⚠ MonsterKillQuest 안에서 QuestManager.Instance 사용하지 않도록 주의!
            // 라고 하셨는데 최종본 Quest 53번째 줄에서 사용 중인 것 확인.

            // 출력할 퀘스트 리스트 할당
            QuestList = new(Quests.ToList());
            //QuestList = new List<KeyValuePair<int, Quest>>();
            //QuestList = Quests.ToList();

            // 현재 가지고 있는 퀘스트 리스트 초기화
            OnGoingQuestList = new List<KeyValuePair<int, Quest>>();
        }

        ///// <summary>
        ///// 초기화 메서드
        ///// </summary>
        //internal void InitOnce()
        //{
        //    if (initialized)
        //    {
        //        return;
        //    }
        //    initialized = true;

        //    // 킬 카운트 초기화
        //    killCountRecord = new();
        //    for (int i = 0;i < monsters.monster.Count; i++)
        //    {
        //        var m = monsters.monster[i];
        //        killCountRecord.Add(m.Id, 0);
        //    }

        //    Quests = new Dictionary<int, Quest>(); 

        //    // ⚠ MonsterKillQuest 안에서 QuestManager.Instance 사용하지 않도록 주의!
        //    Quests.Add(1, new MonsterKillQuest());
        //    QuestList = new List<KeyValuePair<int, Quest>>();
        //    OnGoingQuestList = new List<KeyValuePair<int, Quest>>();
        //    QuestList = Quests.ToList();

        //}

        /// <summary>
        /// 승리했을 경우 몬스터가 다 사망 상태라는 전제가 깔려있으므로
        /// result 화면에서 메서드 참조
        /// </summary>
        public void OnMonsterKilled()
        {
            // 현재 게임 필드에 존재하는 몬스터들
            var fieldMonsters = BattleManager.Instance.CurrentMonsters;
            // 이게 뭔 역할?
            //var countedMonsterIds = new HashSet<int>();

            // 진행 중인 모든 퀘스트 확인
            foreach (var quest in OnGoingQuestList)
            {
                // 진행중인 퀘스트에서 요구하는 몬스터
                var questMonster = quest.Value.QuestMonster;

                // GameManager.monsters 중 퀘스트 몬스터가 포함되어 있는지 확인
                // 근데 이거 빈 객체입니다... 당연히 false가 나오죵 의미 없는 코드
                //if (countedMonsterIds.Contains(QuestMonster.Id))
                //    continue;

                // 필드에 존재하는 몬스터들 중에서 퀘스트 몬스터와 이름이 같은 몬스터가 있으면 true
                bool existsInField = fieldMonsters.Any(m => m.Name == questMonster.Name);
                
                // 위에 주석처리 된 부분이 현재 퀘스트 몬스터가 없으면 다음 퀘스트 검사인 것 같은데
                // 덕희님이 작성한 existsInField로 검사 가능합니다.
                if (!existsInField) continue;

                // Dictionary<Monster, int> _kills에 카운트 증가??
                // 몬스터 아이디가 포함되어 있지 않으면 현재까지 죽인 몬스터의 킬 카운트를 초기화?
                // 하는 코드네요? 현재는 퀘스트가 하나지만 다른 퀘스트가 생기면 문제가 발생하겠네요.
                //if (!killCountRecord.ContainsKey(QuestMonster.Id))
                //{
                //    killCountRecord[QuestMonster.Id] = 0;
                //}

                // 퀘스트 지정 몬스터가 필드에 몇 마리 있는지 반환
                int sameMonsterCount = fieldMonsters.Count(m => m.Name == questMonster.Name);
                killCountRecord[questMonster.Id] += sameMonsterCount;

                // 퀘스트 목표 달성 체크
                if (killCountRecord[questMonster.Id] >= quest.Value.NeedKillCount)
                {
                    quest.Value.IsComplete = true;
                    Console.WriteLine($"[√] 퀘스트 완료: {quest.Value.Name}");
                }

                //countedMonsterIds.Add(QuestMonster.Id); // 여기에 추가해서 반환하지 않으니 의미가 없네용
            }
        }
    }
}