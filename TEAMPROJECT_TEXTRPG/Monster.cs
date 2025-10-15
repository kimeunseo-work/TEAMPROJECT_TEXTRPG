using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
{
    internal class Monster
    {
        public string Name;

        public int Level;

        public int Hp;

        public int Atk;




        public Monster(string name, int level, int hp, int atk)
        {
            Name = name;
            Level = level;
            Hp = hp;
            Atk = atk;

        }

    }


    internal class Monsters
    {


       internal List<Monster> monster = new List<Monster>() {

            new Monster("미니언", 2, 15, 5),
            new Monster("공허충", 3, 10, 9),
            new Monster("대포미니언", 5, 25, 8)



        };






    }


}
