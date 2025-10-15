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
        private Player player;
        



        public BattleStart(Player player)
        {
            this.player = player;
            
        }

        public void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Random random = new Random();

            int monsterCount = random.Next(1, 5); ;

            Monster Spawn()
            {
                Monster clonmon = monster[random.Next(monster.Count)]; // 몬스터리스트에서 몬스터가 가져와짐. 이제 몬스터리스트에서 가져온 몬스터를 새 개체로 생성해서 (생성될때마다)그 새로생긴 몬스터들이
                // GameManager에 있는 몬스터 리스트에 추가해야됨
                return t.Clone(); // ← 새로 만든 개체라 HP 안 공유
            }



            for (int i = 0; i < monsterCount; i++)
            {

                Monster spawn = monster[random.Next(monster.Count)]; // for가 실행될때마다 spawn에 들어있는 몬스터의 종류가 랜덤으로 바뀜

                GameManager.Instance.monsters.Add(spawn); // spawn에 들어있는 몬스터가 추가됨



            }





            for (int i = 0; i < GameManager.Instance.monsters.Count; i++)

            {
                Console.WriteLine($"Lv.{GameManager.Instance.monsters[i].Level} {GameManager.Instance.monsters[i].Name} HP {GameManager.Instance.monsters[i].Hp}");

            }


            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/100");
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

                GameManager.Instance.currentState = GameState.Battle;





            }
        }

        internal override void Show()
        {
            StartBattle();
        }
    }


     


















    
}
