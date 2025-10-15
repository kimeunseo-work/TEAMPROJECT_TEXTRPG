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
                MonsterAttack(monster);
                WriteResult(monster);

                while (true)
                {
                    input = InputHandler.GetUserActionInput();
                    if (input == 0) break;
                }
            }

            input = InputHandler.GetUserActionInput();
            if (input == 0)
            {
                GameManager.Instance.currentState = GameState.Battle;
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
        private void WriteResult(Monster monster)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Console.WriteLine("Lv.1  Chad (전사)");
            Console.WriteLine("HP 100/100");
        }
    }
}
