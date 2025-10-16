using TEAMPROJECT_TEXTRPG.Scenes;

namespace TEAMPROJECT_TEXTRPG
{
    /// <summary>
    /// 게임 화면 상태
    /// </summary>
    internal enum GameState
    {
        None,// 게임 종료
        CharacterCreate,//캐릭터 생성 화면
        SelectJob,//캐릭터 직업 선택 화면
        Home,// 메인 화면
        Stat,// 상태창
        Battle,// 전투
        BattleResult,//전투 결과
        BattleStart,// 전투
        EnemyTurn,
    }

    /// <summary>
    /// 배틀 결과 상태
    /// </summary>
    internal enum BattleState
    {
        None,
        Victory,
        Defeat
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
        internal BattleState currentBattleState;
        internal Dictionary<GameState, Scene> scenes;
        internal List<Monster> monsters = new List<Monster>();

        /* 생성자 */
        //============================================================//
        internal GameManager()
        {
            // 예제 확인하려면 currentState = GameState.Example1
            monsters = new List<Monster>();
            var characterMgr = CharacterManager.Instance;
            currentState = GameState.CharacterCreate;
            currentBattleState = BattleState.None;
            scenes = new Dictionary<GameState, Scene>();
            //Player player = new Player();// 플레이어 객채

            // 씬을 매니저에 추가하는 방법 예시
            scenes.Add(GameState.CharacterCreate, new CharacterCreateScene());
            //scenes.Add(GameState.SelectJob, new SelectJobScene());
            scenes.Add(GameState.Home, new HomeScene());
            scenes.Add(GameState.Stat, new PlayerInfoScene());
            scenes.Add(GameState.BattleResult, new BattleResultScene());
            scenes.Add(GameState.BattleStart, new BattleStartScene());
            scenes.Add(GameState.Battle, new BattleScene());
            scenes.Add(GameState.EnemyTurn, new EnemyTurnScene());
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
