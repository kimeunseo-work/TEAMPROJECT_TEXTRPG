using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
{
    internal class Player
    {
        public int Level { get; set; } = 1;
        public string Name { get; set; } = "Chad";
        public string Job { get; set; } = "전사";
        public int Hp { get; set; } = 100;
        public int Gold { get; set; } = 1500;
        public int MaxHP { get; set; }
        public double BaseAttack { get; set; } = 10;
        public double BaseDefense { get; set; } = 5;
        public double Attack { get; set; }
        public double Defense { get; set; }

        public Player() // 기본 공격력과 추가된 총 공격력 구별
        {
            Attack = BaseAttack;
            Defense = BaseDefense;
        }
    }
}
