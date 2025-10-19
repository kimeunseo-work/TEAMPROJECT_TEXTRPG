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


        public int _count;


        public Monsters monsters;
        public List<Monster> monsterlist;

        public bool QuestSelected = false;

        public Quest()
        {

        }


        //이렇게 생성자에서 Monsters를 받아올 수 있게 하면 Monsters의 클래스의 변수를 가져올수있는건가요 이 quest에서
        //


        public void GetMonster(Monsters monsters)
        {
            this.monsters = monsters;
            monsterlist = monsters.monster;




        }


        public Monster QuestMonster;



        internal virtual void SelectShow()
        {

        }

        internal virtual void Show() { }





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

            QuestMonster = monsterlist[0]; //미니언 몬스터 할당

            Name = "마을을 위협하는 미니언 처치";

            Description = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n" +
                "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n" +
                "모험가인 자네가 좀 처치해주게!";

            QuestInfo = "미니언 5마리 처치 (0/5)";




            GoldReward = 5;

            ExpReward = 100;

            // ItemReward = QuestManager.Instance.items.equipItem1;

            GetMonster(monsters);


        }
        public void UpdateQuest()
        {

            if (isClear)
                return;

            // 살아있는 몬스터 중 목표 이름 가진 애 세기
            int deadCount = GameManager.Instance.monsters
                .Count(m => m.Name == QuestMonster.Name && m.IsDead);

            Count += deadCount;

            if (Count >= 5)
            {
                isClear = true;

            }
        }






        void Except()
        {


            while (true)
            {

                string input = Console.ReadLine();


                if (input == "1")
                {





                    break;
                }
                else if (input == "2")
                {



                    break;

                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다");
                    Console.Write(">>");
                    continue;

                }

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
