using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
{
    internal class BattleStart : Scene
    {




        List<Monster> monster = new List<Monster>() {

            new Monster("미니언", 2, 15, 5),
            new Monster("공허충", 3, 10, 9),
            new Monster("대포미니언", 5, 25, 8)



        };


        public void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Random random = new Random();

            int monsterCount = random.Next(0, 2); ;

            Monster spawn = monster[random.Next(monster.Count)];

            List<Monster> spawnMon = new List<Monster>() { spawn };

            for (int i = 0; i < monsterCount; i++)

            {
                Console.WriteLine($"Lv.{spawnMon[i].Level} {spawnMon[i].Name} HP {spawnMon[i].Hp}");

            }



            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.1  Chad (전사)");
            Console.WriteLine("HP 100/100");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();









        }

        internal override void Show()
        {
            StartBattle();
        }
    }


        internal class Monster
        {


            

            public string Name;

            public int Level;

            public int Hp;

            public int Atk { get; set; }
        



            public Monster (string name, int level, int hp, int atk)
            {
                Name = name;
                Level = level;
                Hp = hp;
                Atk = atk;
                
            }



           




        }


















    
}
