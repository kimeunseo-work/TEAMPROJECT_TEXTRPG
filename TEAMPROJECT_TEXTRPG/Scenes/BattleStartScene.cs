namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class BattleStartScene : Scene
    {
        private Monsters monsters;
        private BattleScene battleScene;

        public BattleStartScene()
        {
            monsters = new Monsters();
            battleScene = new BattleScene();
        }

        public void StartBattle()
        {
            int x = CharacterManager.Instance.player.Hp;
            CharacterManager.Instance.player.hped = x;

            GameManager.Instance.monsters = monsters.SpawnRandomMonsters();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();

                for (int i = 0; i < GameManager.Instance.monsters.Count; i++)
                {
                    Console.WriteLine($"Lv.{GameManager.Instance.monsters[i].Level} {GameManager.Instance.monsters[i].Name} HP {GameManager.Instance.monsters[i].Hp}");
                }

                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{CharacterManager.Instance.player.Level}  {CharacterManager.Instance.player.Name} ({CharacterManager.Instance.player.CurrentJob.Name})");
                Console.WriteLine($"HP {CharacterManager.Instance.player.Hp}/100");
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine("0. 나가기");

                int input = InputHandler.GetUserActionInput();

                if (input == 0)
                {
                    GameManager.Instance.currentState = GameState.Home;
                    break;
                }
                else if (input == 1)
                {
                    battleScene.Start();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                }
            }
        }

        internal override void Show()
        {
            StartBattle();
        }
    }
}
