using TEAMPROJECT_TEXTRPG._Unfinished;
using TEAMPROJECT_TEXTRPG.Managers;

namespace TEAMPROJECT_TEXTRPG.Core
{
    public class Quest
    {
        public string Name; // 이름
        public string Description; // 설명
        public int NeedKillCount; // 목표 킬 수
        public bool GetRewarded = false; // 보상을 받은 퀘스트인지 체크
        public Monster QuestMonster; // 퀘스트 몬스터 지정
        public Item ItemReward; // 지급할 아이템
        public int ItemGivingCount; // 아이템 몇 개 지급할 건지
        public int GoldReward; // 보상 골드
        public int ExpReward; // 보상 경험치
        public bool IsAccept = false; // 퀘스트를 수락 했는지
        public bool IsComplete = false; // 퀘스트를 완료 했는지
        public int CurrentKillCount = 0; // 현재 킬 카운트를 퀘스트 안에서 체크
        //public string QuestInfo;
        //public int _kill = QuestManager.Instance.killCountRecord.Count; // 이 것도 문제가 됩니다. 근데 참조를 안하는 군요.
        //public int CurrentKillCount
        //=> QuestManager.Instance.killCountRecord.TryGetValue(QuestMonster.Id, out var v) ? v : 0;
        //public bool QuestSelected = false; 참조를 안하네요
        //public Monsters monsters = new Monsters(); //몬스터 리스트? //Monsters에서 monster만 monsterlist로 지정 괜히 복잡해져서 그냥 없앰

        public string ClearText => IsComplete ? "[완료]" : " ";

        public Quest() { }
        public Quest(string name, string desc, int needkillCount, bool getRewarded, Monster questMon, Item itemReward, int itemCount, int gold, int exp, bool isAccept = false, bool isComplete = false, int currentKillCount = 0)
        {
            Name = name;
            Description = desc;
            NeedKillCount = needkillCount;
            GetRewarded = getRewarded;
            QuestMonster = questMon;
            ItemReward = itemReward;
            ItemGivingCount = itemCount;
            GoldReward = gold;
            ExpReward = exp;
            IsAccept = isAccept;
            IsComplete = isComplete;
            CurrentKillCount = currentKillCount;
        }

        public Quest(Quest clone)
        {
            Name = clone.Name;
            Description = clone.Description;
            NeedKillCount = clone.NeedKillCount;
            GetRewarded = clone.GetRewarded;
            QuestMonster = clone.QuestMonster;
            ItemReward = clone.ItemReward;
            ItemGivingCount = clone.ItemGivingCount;
            GoldReward = clone.GoldReward;
            ExpReward = clone.ExpReward;
            IsAccept = clone.IsAccept;
            IsComplete = clone.IsComplete;
            CurrentKillCount = clone.CurrentKillCount;
        }

        public string GetProgressLabel() => $"{CurrentKillCount}/{NeedKillCount}";
        public virtual void Show()
        {

        }

        //protected virtual int GetCurrentCount()
        //{
        //    return 0; // 기본값
        //}

        //protected virtual string GetProgressLabel() => $"{GetCurrentCount()}/{NeedKillCount}";

    }

    internal class MonsterKillQuest : Quest
    {
        public string Quest1Info => $"미니언 5마리 처치 ({GetProgressLabel()})";

        public MonsterKillQuest(string name, string desc, int needkillCount, bool getRewarded, Monster questMon, Item itemReward, int itemCount, int gold, int exp, bool isAccept = false, bool isComplete = false, int currentKillCount = 0) : base(name, desc, needkillCount, getRewarded, questMon, itemReward, itemCount, gold, exp, isAccept, isComplete, currentKillCount)
        {
            Name = name;
            Description = desc;
            NeedKillCount = needkillCount;
            GetRewarded = getRewarded;
            QuestMonster = questMon; //미니언 몬스터 할당
            ItemReward = itemReward; // QuestManager.Instance.items.equipItems[1];
            ItemGivingCount = itemCount;
            GoldReward = gold;
            ExpReward = exp;
            IsAccept = isAccept;
            IsComplete = isComplete;
            CurrentKillCount = currentKillCount;
            // ItemReward = QuestManager.Instance.items.equipItem1;
        }

        public MonsterKillQuest(Quest clone) : base(clone)
        {
            Name = clone.Name;
            Description = clone.Description;
            NeedKillCount = clone.NeedKillCount;
            GetRewarded = clone.GetRewarded;
            QuestMonster = clone.QuestMonster;
            ItemReward = clone.ItemReward;
            ItemGivingCount = clone.ItemGivingCount;
            GoldReward = clone.GoldReward;
            ExpReward = clone.ExpReward;
            IsAccept = clone.IsAccept;
            IsComplete = clone.IsComplete;
            CurrentKillCount = clone.CurrentKillCount;
        }

        public int GetCurrentCount()
        {
            return QuestManager.Instance.killCountRecord.TryGetValue(QuestMonster.Id, out var v) ? v : 0;
        }

        public override void Show()
        {
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine($"{Name}{ClearText}");
            Console.WriteLine();
            Console.WriteLine($"{Description}");
            Console.WriteLine();
            Console.WriteLine($"- {Quest1Info}");
            //Console.WriteLine($"_kills = {QuestManager.Instance.killCountRecord[Quest1.QuestMonster.Id]}");
            //Console.WriteLine($"_kills = {QuestMonster.Name} {CurrentKillCount}/{NeedKillCount}");
            Console.WriteLine("-보상-");
            Console.WriteLine($"{ItemReward.itemName} x 1");
            Console.WriteLine($"{GoldReward}G");
            Console.WriteLine();
        }
    }
}