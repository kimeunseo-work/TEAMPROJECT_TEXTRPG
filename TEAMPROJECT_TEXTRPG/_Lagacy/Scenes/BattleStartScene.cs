using TEAMPROJECT_TEXTRPG.Core;
using TEAMPROJECT_TEXTRPG.Managers;
using TEAMPROJECT_TEXTRPG.Scenes;
using TEAMPROJECT_TEXTRPG.Utility;

namespace TEAMPROJECT_TEXTRPG.Lagacy.Scenes
{
    internal class BattleStartScene : Scene
    {
        private Monsters monsters;

        public BattleStartScene()
        {
            monsters = new Monsters();
        }

        public void StartBattle()
        {
            int x = CharacterManager.Instance.player.Hp;
            CharacterManager.Instance.player.hped = x;

            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            GameManager.Instance.monsters = monsters.SpawnRandomMonsters();

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
            }
            else if (input == 1)
            {
                GameManager.Instance.currentState = GameState.Battle;
            }
        }

        internal override void Show()
        {
            StartBattle();
        }
    }
}
