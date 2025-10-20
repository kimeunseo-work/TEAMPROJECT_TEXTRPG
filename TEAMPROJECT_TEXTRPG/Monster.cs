using System;

namespace TEAMPROJECT_TEXTRPG
{
    internal class Monster
    {

        public int Id { get; set; }
        public string Name { get;  set; }
        public int Level { get; set; }
        public int Hp { get;  set; }
        public int Atk { get; set; }
        public int MonExp {  get; set; }
        public bool IsDead { get;  set; }
        public bool IsAttackComplete { get; private set; }

        public Monster(int id, string name, int level, int hp, int atk, int monExp, bool isDead = false)
        {
            Id = id;
            Name = name;
            Level = level;
            Hp = hp;
            Atk = atk;
            MonExp = monExp;
            IsDead = isDead;
        }

        public Monster(Monster clone)
        {

            Name = clone.Name;
            Level = clone.Level;
            Hp = clone.Hp;
            Atk = clone.Atk;
            MonExp = clone.MonExp;
            IsDead = clone.IsDead;

        }

        /// <summary>
        /// 몬스터 일반 공격
        /// </summary>
        internal void Attack(Player player) => player.TakeDamage(Atk);
        internal void TakeDamage(int amount)
        {
            Hp -= amount;
            if(Hp <= 0) IsDead = false;
        }
    }

    internal class Monsters
    {
        internal List<Monster> monster = new List<Monster>()
        {
            new Monster(0,"미니언", 2, 15, 5, 5),
            new Monster(1,"공허충", 3, 10, 9, 3),
            new Monster(2,"대포미니언", 5, 25, 8, 10),
            new Monster(3,"돌거북", 3, 10, 3, 2),
            new Monster(4,"고대 돌거북", 8, 25, 7, 9),
            new Monster(5,"칼날부리", 5, 15, 6, 6)

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
                monsters.Add(new Monster(monster[index]));
            }
            return monsters;
        }

    }

}
