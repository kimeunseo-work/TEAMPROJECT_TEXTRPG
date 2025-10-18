#if false
using TEAMPROJECT_TEXTRPG.Core;
using TEAMPROJECT_TEXTRPG.Lagacy.Skills;
using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Scenes;

namespace TEAMPROJECT_TEXTRPG.Lagacy.Scenes
{
    internal class BattleScene : Scene
    {
        private Random random = new Random();

        //전투 시작
        public void Start()
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("**Battle!!**\n");
            var Player = CharacterManager.Instance.player;
            Console.WriteLine($"플레이어 정보");
            Console.WriteLine($"이름: {Player.Name}");
            Console.WriteLine($"레벨: {Player.Level}");
            Console.WriteLine($"직업: {Player.CurrentJob.Name}");
            Console.WriteLine($"HP: {Player.Hp}/{Player.MaxHP}");
            Console.WriteLine($"MP: {Player.Mp}/{Player.MaxMP}");
            Console.WriteLine();

            Console.WriteLine("몬스터 정보");
            ShowMonsters();
            Console.WriteLine("\n0. 취소\n");

            // 몬스터 선택
            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine("전투를 취소했습니다.");
                GameManager.Instance.currentState = GameState.Home;
            }
            int targetIndex;
            // 플레이어가 숫자를 제대로 입력했는지 확인
            if (!int.TryParse(input, out targetIndex) || targetIndex < 1 || targetIndex > GameManager.Instance.monsters.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            Monster target = GameManager.Instance.monsters[targetIndex - 1]; //리스트는 0부터 시작하기 때문에 -1 추가

            if (target.Hp <= 0)
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                return;
            }
            // 몬스터 선택 후 공격 방식 선택
            Console.WriteLine("\n무엇을 하시겠습니까?");
            Console.WriteLine("1. 기본 공격");
            Console.WriteLine("2. 스킬 사용");
            Console.WriteLine("0. 돌아가기");
            Console.Write(">>");
            string action = Console.ReadLine();
            if (action == "1")
            {
                PlayerAttack(target);
            }
            else if (action == "2")
            {
                var player = CharacterManager.Instance.player;
                SkillAction.HaveSkill(player, target);
            }
            else if (action == "0")
            {
                Console.WriteLine("다시 몬스터를 선택합니다.");
                Start();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }
        //플레이어 공격(치명타)
        private void PlayerAttack(Monster target)
        {
            int attackPower = GetRandomAttack((int)CharacterManager.Instance.player.Attack);

            bool isCritical = random.Next(100) < 15;
            if (isCritical)
            {
                attackPower = (int)(attackPower * 1.6);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n 치명타 공격!!");
                Console.ResetColor();
            }
            Console.WriteLine($"\n{CharacterManager.Instance.player.Name}의 공격");
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

        // 랜덤 공격력
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
#endif