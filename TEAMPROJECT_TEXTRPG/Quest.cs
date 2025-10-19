using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
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


        public int KillCount;

        public int itemGivingCount;


        public Monsters monsters = new Monsters(); //몬스터 리스트
       //Monsters에서 monster만 monsterlist로 지정 괜히 복잡해져서 그냥 없앰
        public Monster QuestMonster; //퀘스트 몬스터 지정

        public bool QuestSelected = false;

        public Quest()
        {

            

        }
     

        //이렇게 생성자에서 Monsters를 받아올 수 있게 하면 Monsters의 클래스의 변수를 가져올수있는건가요 이 quest에서
        //

        
        
        internal void GetRewarded()
        {



            //플레이어의 인벤토리에 아이템이 들어감.



        }


       



        internal virtual void SelectShow() 
        {
            
        }

        internal virtual void Show() { }





    }

    internal class MonsterKillQuest : Quest
    {



        // 실제 저장되는 공간 (필드)
        /*public int Count
        {
            get => KillCount;                  // 읽을 때 실행되는 코드
            set => KillCount = Math.Clamp(value, 0, 5); // 쓸 때 실행되는 코드
        }*/

        



        public MonsterKillQuest()
        : base()
        {
            KillCount = 5;

            // GameManager에서 Monsters에 몬스터 리스트 가져옴 
            QuestMonster = monsters.monster[0]; //미니언 몬스터 할당

            Name = "마을을 위협하는 미니언 처치";

            Description = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n" +
                "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n" +
                "모험가인 자네가 좀 처치해주게!";

            QuestInfo = $"미니언 5마리 처치 ({KillCount}/5)";

            


            itemGivingCount = 1;

            GoldReward = 5;

            ExpReward = 100;

            ItemReward = QuestManager.Instance.items.equipItem1;

            


        }
        public void UpdateQuest()
        {
            
            if (isClear)
                return;

            // 살아있는 몬스터 중 목표 이름 가진 애 세기
            int deadCount = GameManager.Instance.monsters
                .Count(m => m.Name == QuestMonster.Name && m.IsDead);

            QuestManager.Instance._kills[QuestMonster] += deadCount;

            if (QuestManager.Instance._kills[QuestMonster] >= 5)
            {
                isClear = true;
               
            }
        }

       

     
   
       
       
       



     

        internal override void Show()
        {

            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine($"{Name}");
            Console.WriteLine();
            Console.WriteLine($"{Description}");
            Console.WriteLine();
            Console.WriteLine($"- {QuestInfo}");
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine($"{ItemReward.itemName} x 1");
            Console.WriteLine($"{GoldReward}G");
            Console.WriteLine();
            



        }
        


    }
    internal class MonsterKillQuest2 : Quest
    {
        public MonsterKillQuest2(Monsters monsters)
        : base() { }

        internal override void Show()
        {
            throw new NotImplementedException();
        }
    }
}
