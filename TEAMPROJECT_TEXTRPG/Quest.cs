using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
{
    internal abstract class Quest
    {
        public string Name;

        public string Description;

        public string QuestInfo;

        public Item ItemReward;

        public int GoldReward;

        public int ExpReward;

        public bool isclear = false;


        public int _count;


        public Monsters monsters;
        public List<Monster> monsterlist;

        public Quest()
        {
            
        }
     

        //이렇게 생성자에서 Monsters를 받아올 수 있게 하면 Monsters의 클래스의 변수를 가져올수있는건가요 이 quest에서
        //


        public void GetMonster(Monsters monsters)
        {
            this.monsters = monsters;
            this.monsterlist = monsters.monster;




        }


        public Monster QuestMonster;



        internal abstract void Show();





    }

    internal class MonsterKillQuest : Quest
    {

        

         // 실제 저장되는 공간 (필드)
        public int Count
        {
            get => _count;                  // 읽을 때 실행되는 코드
            set => _count = Math.Clamp(value, 0, 5); // 쓸 때 실행되는 코드
        }


        public MonsterKillQuest(Monsters monsters)
        : base()
        {

            QuestMonster = monsterlist[0];

            Name = "마을을 위협하는 미니언 처치";

            Description = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n" +
                "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n" +
                "모험가인 자네가 좀 처치해주게!";

            QuestInfo = "미니언 5마리 처치 (0/5)";




            GoldReward = 5;

            ExpReward = 100;

            ItemReward = QuestManager.Instance.items.items[1];

            GetMonster(monsters);


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
            Console.WriteLine($"{ItemReward.itemName}");





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
