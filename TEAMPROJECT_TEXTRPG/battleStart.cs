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

        List<Monster> monster = monsters.monster; //리스트 몬스터




        public void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Random random = new Random();

            int monsterCount = random.Next(1, 5); ;

             
            List<Monster> spawnMon = new List<Monster>(); // 스폰된 몬스터의 리스트가 있음


            for (int i = 0; i < monsterCount; i++)
            {

                Monster spawn = monster[random.Next(monster.Count)]; // for가 실행될때마다 spawn에 들어있는 몬스터의 종류가 랜덤으로 바뀜

                spawnMon.Add(spawn); // spawn에 들어있는 몬스터가 추가됨



            }





            for (int i = 0; i < spawnMon.Count; i++)

            {
                Console.WriteLine($"Lv.{spawnMon[i].Level} {spawnMon[i].Name} HP {spawnMon[i].Hp}");

            }


            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.1  Chad (전사)");
            Console.WriteLine("HP 100/100");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 나가기");
            
            int input = InputHandler.GetUserActionInput();

            if (input == 0)
            {

                GameManager.Instance.currentState = GameState.Home;


            }
            else if (input == 1)
            {






            }
        }

        internal override void Show()
        {
            StartBattle();
        }
    }


     


















    
}
