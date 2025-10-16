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
            foreach (var monster in GameManager.Instance.monsters)
            {
                // 몬스터 공격
                var oldHp = MonsterAttack(monster);

                // 몬스터가 사망하여 공격하지 않았음.
                if (oldHp == null) continue;

                // 화면 출력
                MonsterAttackResultUI(monster, oldHp);

                // 선택지 입력
                while (true)
                {
                    var input = InputHandler.GetUserActionInput();
                    // 유효한 키 아니면 반복
                    if (input == 0) break;
                }

                var list = new List<int>();
                CheckDefeat();
                if (CheckBattleEnd()) return;
            }
            // 몬스터 공격 순회 후 플레이어가 사망하지 않았으면 Battle로
            GameManager.Instance.currentState = GameState.Battle;
        }

        /* 키 입력 */
        //============================================================//

        private void CheckDefeat()
        {
            if (player.Hp <= 0)
            {
                GameManager.Instance.currentBattleState = BattleState.Defeat;
                GameManager.Instance.currentState = GameState.BattleResult;
            }
        }

        private bool CheckBattleEnd()
        {
            return GameManager.Instance.currentBattleState == BattleState.Defeat;
        }

        /* 화면 출력 */
        //============================================================//

        /// <summary>
        /// 공격 후 화면 출력
        /// </summary>
        private void MonsterAttackResultUI(Monster monster, int? oldHp)
        {
            if (oldHp == null) return;

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

        /* 몬스터 공격 처리 */
        //============================================================//

        /// <summary>
        /// 몬스터 공격 처리
        /// </summary>
        /// <returns>공격 전후 플레이어 체력</returns>
        private int? MonsterAttack(Monster monster)
        {
            if (monster.IsDead) return null;

            var oldHp = player.Hp;
            monster.Attack(player);

            return oldHp;
        }
    }
}
