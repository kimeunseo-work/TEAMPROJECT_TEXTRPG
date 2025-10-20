using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TEAMPROJECT_TEXTRPG._Unfinished;

//﻿#if false
using TEAMPROJECT_TEXTRPG.Managers;

namespace TEAMPROJECT_TEXTRPG.Core
{
    internal class Quest
    {
        public string Name;
        public string Description;
        public string QuestInfo;
        public Item ItemReward;
        public int GoldReward;
        public int ExpReward;
        public bool isSelected = false;
        public bool isClear = false;
        public bool getRewarded = false;
        public int _kill = QuestManager.Instance._kills.Count;
        public int CurrentKillCount
        => QuestManager.Instance._kills.TryGetValue(QuestMonster.Id, out var v) ? v : 0;
        public string ClearText => isClear ? "[완료]" : " "; 
        public int KillCount;
        public int itemGivingCount;

        protected virtual int GetCurrentCount()
        {
            return 0; // 기본값
        }

        protected virtual string GetProgressLabel() => $"{GetCurrentCount()}/{KillCount}";
       
        public Monsters monsters = new Monsters(); //몬스터 리스트
       //Monsters에서 monster만 monsterlist로 지정 괜히 복잡해져서 그냥 없앰
        public Monster QuestMonster; //퀘스트 몬스터 지정
        public bool QuestSelected = false;
     
        internal virtual void Show() { }
    }

    internal class MonsterKillQuest : Quest
    {
          public virtual string QuestInfo => $"미니언 5마리 처치 ({GetProgressLabel()})";

        public MonsterKillQuest()
        : base()
        {
            KillCount = 1;
            getRewarded = false;
            // GameManager에서 Monsters에 몬스터 리스트 가져옴 
            QuestMonster = monsters.monster[0]; //미니언 몬스터 할당
            Name = "마을을 위협하는 미니언 처치";
            Description = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n" +
                "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n" +
                "모험가인 자네가 좀 처치해주게!";
            ItemReward = QuestManager.Instance.items.equipItems[1];
            itemGivingCount = 1;
            GoldReward = 5;
            ExpReward = 100;
            // ItemReward = QuestManager.Instance.items.equipItem1;
        }

        protected override int GetCurrentCount()
        {
            return QuestManager.Instance._kills.TryGetValue(QuestMonster.Id, out var v) ? v : 0;
        }

        internal override void Show()
        {
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine($"{Name}{ClearText}"); //그 clear가 되면 여기서 어 퀘스트 클래스안에 그 show라는 메서드가있는거죠 네? //홈에서 3번을 누르면
            Console.WriteLine();
            Console.WriteLine($"{Description}");
            Console.WriteLine();
            Console.WriteLine($"- {QuestInfo}");
            Console.WriteLine($"_kills = {QuestManager.Instance._kills[QuestMonster.Id]}");
            Console.WriteLine("-보상-");
            Console.WriteLine($"{ItemReward.itemName} x 1");
            Console.WriteLine($"{GoldReward}G");
            Console.WriteLine();
        }
    }
}
//#endif