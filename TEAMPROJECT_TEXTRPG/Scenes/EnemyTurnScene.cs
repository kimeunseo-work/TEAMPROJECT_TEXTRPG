namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class EnemyTurnScene : Scene
    {
        private Player player;

        internal EnemyTurnScene(Player player)
        {
            this.player = player;
        }

        internal override void Show()
        {
            var input = -1;
            foreach (var monster in GameManager.Instance.monsters)
            {
                var oldHp = player.Hp;
                MonsterAttack(monster);
                WriteResult(monster, oldHp);

                while (true)
                {
                    input = InputHandler.GetUserActionInput();
                    if (input == 0) break;
                }
            }

            // 입력
            while (true)
            {
                input = InputHandler.GetUserActionInput();
                
                if (input == 0)
                {
                    if(player.Hp <= 0)
                    {
                        GameManager.Instance.currentBattleState = BattleState.Defeat;
                        GameManager.Instance.currentState = GameState.BattleResult;
                    }
                    else
                    {
                        GameManager.Instance.currentState = GameState.Battle;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 몬스터 공격
        /// </summary>
        private void MonsterAttack(Monster monster)
        {
            if (monster.IsDead) return;
            monster.Attack(player);
        }

        /// <summary>
        /// 화면 출력
        /// </summary>
        private void WriteResult(Monster monster, int oldHp)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Console.WriteLine($"Lv.{monster.Level} {monster.Name}의 공격!");
            Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {monster.Atk}]");

            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {oldHp} → {player.Hp}");
            Console.WriteLine();

            Console.WriteLine("0. 다음");
        }
    }
}
