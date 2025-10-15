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
        private Player Player;
        private Monster[] monsters;
        private Random random = new Random();

        public battle(Player player, Monster[] monsters)
        {
            this.Player = player;
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
            if (!int.TryParse(input, out targetIndex) || targetIndex < 1 || targetIndex > monsters.Length)
                {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            Monster target = monsters[targetIndex];

            if (target.Hp <= 0)
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                return;
            }

            PlayerAttack(target);



        }
        private void PlayerAttack(Monster target)
        {
            int attackPower = GetRandomAttack((int)Player.Attack);
            Console.WriteLine($"\n{Player.Name}의 공격");
            Console.WriteLine($"{target.Name}을(를) 맞췄습니다. [데미지 : {attackPower}]");

            int oldHP = target.Hp;
            target.Hp -= attackPower;

            if (target.Hp <= 0)
            {
                target.Hp = 0;
                
                Console.WriteLine($"\n{target.Name}");
                Console.WriteLine($"HP{oldHP} -> Dead");
            }
            else
            {
                Console.WriteLine($"\n{target.Name}");
                Console.WriteLine($"HP {oldHP} -> {target.Hp}");

            }
            Console.WriteLine("\n0.다음");
            Console.ReadLine();
        }
        private int GetRandomAttack(double baseattack)
        {
            double error = baseattack * 0.1;
            int errorInt = (int)Math.Ceiling(error);
            return (int)random.Next((int)baseattack - errorInt, (int)baseattack + errorInt + 1);
        }
        private void ShowMonsters()
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                Monster m = monsters[i];
                if (m.Hp > 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"**{i + 1}** Lv.{m.Level} {m.Name} HP {m.Hp}");
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

