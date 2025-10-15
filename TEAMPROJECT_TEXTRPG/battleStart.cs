using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TEAMPROJECT_TEXTRPG
{
    internal class BattleStart : Scene
    {
        private Monsters monsters;
        private Player player;
        

        public BattleStart(Player player)
        {
            monsters = new Monsters();
            this.player = player;
            
        }

        public void StartBattle()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            GameManager.Instance.monsters = monsters.SpawnRandomMonsters();

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
