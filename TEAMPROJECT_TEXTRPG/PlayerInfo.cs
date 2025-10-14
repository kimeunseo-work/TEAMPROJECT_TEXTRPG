using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
{


    internal class PlayerInfo
    {
        public int Level { get; set; } = 1;
        public string Name { get; set; } = "Chad";
        public string Job { get; set; } = "전사";
        public int Hp { get; set; } = 100;
        public int Gold { get; set; } = 1500;

        public double BaseAttack { get; set; } = 10;
        public double BaseDefense { get; set; } = 5;
        public double Attack { get; set; }
        public double Defense { get; set; }

        public PlayerInfo() // 기본 공격력과 추가된 총 공격력 구별
        {
            Attack = BaseAttack;
            Defense = BaseDefense;
        }

        public void DisplayPlayerinfo()
        {
            Console.WriteLine($"\nLv.{Level}\n{Name}({Job})\n공격력: {Attack:F1}\n방어력: {Defense}" +
                $"\n체력: {Hp}\nGold: {Gold}");

            Console.WriteLine($"1.\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
        }
    }


}
