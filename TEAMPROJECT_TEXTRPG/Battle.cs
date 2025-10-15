using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEAMPROJECT_TEXTRPG;

namespace TeamProject_Attack
{
    internal class battle
    {
        private Player player;
        private Monster[] monsters;
        private Random random = new Random();

        public battle(Player player, Monster[] monsters)
        {
            this.player = player;
            this.monsters = monsters;

        }
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("**Battle!!**\n");
            ShowMonsters();
            Console.WriteLine("\n. 취소\n");
            Console.Write("대상을 선택해주세요.\n>>");

            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine("전투를 취소했습니다.");
                return;
            }
            int targetIndex;
            if (!int.TryParse(input, out targetIndex)) targetIndex < 1 || targetIndex > monsters.Length
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            monster target = monsters[targetIndex];

            if (!target.IsAlive)
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                return;
            }

            PlayerAttack(target);



        }
        private void PlayerAttack(Monster target)
        {
            int attackPower = GetRandomAttack(player.Attack);
            Console.WriteLine($"\n{player.Name}의 공격");
            Console.WriteLine($"{target.Name}을(를) 맞췄습니다. [데미지 : {attackPower}]");

            int oldHP = target.HP;
            target.HP -= attackPower;

            if (target.HP <= 0)
            {
                target.HP = 0;
                target.IsAlive = false;
                Console.WriteLine($"\n{target.Name}");
                Console.WriteLine($"HP{oldHP} -> Dead");
            }
            else
            {
                Console.WriteLine($"\n{target.Name}");
                Console.WriteLine($"HP {oldHP} -> {target.HP}");

            }
            Console.WriteLine("\n0.다음");
            Console.ReadLine();
        }
        private int GetRandomAttack(int baseattack)
        {
            double error = baseattack * 0.1;
            int errorInt = (int)Math.Ceiling(error);
            return random.Next(baseattack - errorInt, baseattack + errorInt + 1);
        }
        private void ShowMonster()
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                monsters m = monsters[i];
                if (m.IsAlive)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"**{i + 1}** Lv.{m.Level} {m.Name} HP {m.HP}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"**{i + 1}** Lv.{m.Level} {m.Name} Dead ");
                }

            }
            Console.ResetColor();

        }


    }

}

