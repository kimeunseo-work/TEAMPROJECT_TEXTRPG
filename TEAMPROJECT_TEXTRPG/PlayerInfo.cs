using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
{


    internal class PlayerInfo: Scene
    {
        public int Level { get; set; } = 1;
        public string Name { get; set; } = "Chad";
        public string Job { get; set; } = "전사";
        public int Hp { get; set; } = 100;
        public int Gold { get; set; } = 1500;
        public int MaxHP {  get; set; }
        public double BaseAttack { get; set; } = 10;
        public double BaseDefense { get; set; } = 5;
        public double Attack { get; set; }
        public double Defense { get; set; }

        public PlayerInfo() // 기본 공격력과 추가된 총 공격력 구별
        {
            Attack = BaseAttack;
            Defense = BaseDefense;
        }

        //상태보기
        public void DisplayPlayerinfo()
        {
            bool keep = true;
            while (keep)
            {
                Console.Clear();
                Console.WriteLine($"\nLv.{Level}\n\n{Name}({Job})\n\n공격력: {Attack:F1}\n\n방어력: {Defense}" +
                $"\n\n체력: {Hp}\n\nGold: {Gold}");

                Console.WriteLine($"\n1.\n0. 나가기");

                int input = InputHandler.GetUserActionInput();
                if (input == 0)
                {
                    keep = false;
                    GameManager.Instance.currentState = GameState.Home;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        internal override void Show()
        {
            DisplayPlayerinfo();
        }
    }


}
