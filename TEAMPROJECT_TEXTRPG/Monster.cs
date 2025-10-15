using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;

namespace TEAMPROJECT_TEXTRPG
{
    internal class Monster
    {

        {
            Name = name;
            Level = level;
            Hp = hp;
            Atk = atk;
        }

    }

    internal class Monsters
    {
            new Monster("미니언", 2, 15, 5),
            new Monster("공허충", 3, 10, 9),
            new Monster("대포미니언", 5, 25, 8)
        };

        /// <summary>
        /// 몬스터 소환 메서드
        /// </summary>
        internal List<Monster> SpawnRandomMonsters()
        {
            var monsterCount = new Random().Next(1, 5);
            var monsters = GetRandomMonsters(monsterCount);
            
            return monsters;
        }
        
        private List<Monster> GetRandomMonsters(int count)
        {
            var monsters = new List<Monster>();
            var monsterCount = monster.Count;
            for (int i = 0; i < count; i++)
            {
                var index = new Random().Next(monsterCount);
                monsters.Add(monster[index]);
            }
            return monsters;
        }
    }
}
