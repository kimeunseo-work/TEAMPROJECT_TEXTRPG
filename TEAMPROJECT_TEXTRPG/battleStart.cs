using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
{
    internal class BattleStart : Scene
    {


        static Monsters monsters = new Monsters();

        List<Monster> monster = monsters.monster;

        


        public void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Random random = new Random();

            int monsterCount = random.Next(0, 2); ;

            Monster spawn = monster [random.Next(monster.Count)];

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

            if( input == "0")
            {

                


            }







        }

        internal override void Show()
        {
            StartBattle();
        }
    }


     


















    
}
