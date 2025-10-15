using System.Numerics;
using TEAMPROJECT_TEXTRPG.Scenes;

namespace TEAMPROJECT_TEXTRPG
{
    /// <summary>
    /// 게임 화면 상태
    /// </summary>
    internal enum GameState
    {
        None,// 게임 종료
        Home,// 메인 화면
        Stat,// 상태창
        Battle,// 전투
        Example1,
        Example2,
    }

    internal class GameManager
    {
        /* 싱글톤 */
        //============================================================//
        private static GameManager instance;
        internal static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        /* 게임 상태 */
        //============================================================//
        internal GameState currentState;
        internal Dictionary<GameState, Scene> scenes;

        /* 생성자 */
        //============================================================//
        internal GameManager()
        {
            // 예제 확인하려면 currentState = GameState.Example1
            currentState = GameState.Home;
            scenes = new Dictionary<GameState, Scene>();
            Player player = new Player();// 플레이어 객채
            // 씬을 매니저에 추가하는 방법 예시
            scenes.Add(GameState.Example1, new ExampleScene());
            scenes.Add(GameState.Example2, new ExampleScene2());
            scenes.Add(GameState.Home, new Home());
            scenes.Add(GameState.Stat, new PlayerInfo(player));
            scenes.Add(GameState.Battle, new BattleStart());
            scenes.Add(GameState.Battle, new BattleResult());
        }

        internal void Run()
        {
            while (currentState != GameState.None)
            {
                scenes[currentState].Show();
            }
        }
    }
}
