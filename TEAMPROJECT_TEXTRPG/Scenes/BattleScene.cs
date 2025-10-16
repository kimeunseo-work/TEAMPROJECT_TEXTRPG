namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class BattleScene : Scene
    {
        private Player Player;

        private Random random = new Random();

        public BattleScene(Player player)
        {
            Player = player;

        }
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("**Battle!!**\n");
            ShowMonsters();
            Console.WriteLine("\n0. 취소\n");

            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine("전투를 취소했습니다.");
                GameManager.Instance.currentState = GameState.Home;
            }
            int targetIndex;
            if (!int.TryParse(input, out targetIndex) || targetIndex < 1 || targetIndex > GameManager.Instance.monsters.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            Monster target = GameManager.Instance.monsters[targetIndex - 1];

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
                target.IsDead = true;
                Console.WriteLine($"\n{target.Name}");
                Console.WriteLine($"HP{oldHP} -> Dead");
            }
            else
            {
                Console.WriteLine($"\n{target.Name}");
                Console.WriteLine($"HP {oldHP} -> {target.Hp}");

            }
            Console.WriteLine("\n0.다음");
            Console.ReadKey();

            bool allDead = GameManager.Instance.monsters.All(x => x.IsDead);
            if (allDead)
            {
                GameManager.Instance.currentState = GameState.BattleResult;
                GameManager.Instance.currentBattleState = BattleState.Victory;

            }
            else
            {
                GameManager.Instance.currentState = GameState.EnemyTurn;
            }
        }

        private int GetRandomAttack(double baseattack)
        {
            double error = baseattack * 0.1;
            int errorInt = (int)Math.Ceiling(error);
            return random.Next((int)baseattack - errorInt, (int)baseattack + errorInt + 1);
        }
        private void ShowMonsters()
        {
            for (int i = 0; i < GameManager.Instance.monsters.Count; i++)
            {
                Monster m = GameManager.Instance.monsters[i];
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

        internal override void Show()
        {
            Start();
        }
    }

}

