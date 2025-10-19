using TEAMPROJECT_TEXTRPG.Scenes;

namespace TEAMPROJECT_TEXTRPG.Managers
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
        TotalBattle,
        NewBattleResult,
        Quest,
    }

    internal class GameManager
    {
        /* 싱글톤 */
        //============================================================//
        private static GameManager instance;
        public static GameManager Instance
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

        /* 이벤트 */
        //============================================================//

        // BattleManager
        public Func<Task> OnBattleEntered;

        /* 게임 상태 */
        //============================================================//
        private GameState currentState;
        public GameState CurrentState
        {
            get => currentState;
            set
            {
                if (value == GameState.TotalBattle)
                {
                    OnBattleEntered.Invoke();
                }
                currentState = value;
            }
        }
        public Dictionary<GameState, Scene> scenes;
        public Scene currentScene;

        /* 생성자 */
        //============================================================//
        public GameManager()
        {
            // 첫 시작 게임 상태
            CurrentState = GameState.CharacterCreate;

            // 화면 모음
            scenes = new Dictionary<GameState, Scene>
            {
                { GameState.CharacterCreate, new InitCharacterScene() },
                { GameState.SelectJob, new SelectJobScene() },
                { GameState.Home, new HomeScene() },
                { GameState.Stat, new PlayerInfoScene() },
                { GameState.TotalBattle, new TotalBattleScene() },
                { GameState.NewBattleResult, new NewBattleResultScene() },
                { GameState.Quest, new QuestScene() }
            };
        }

        /// <summary>
        /// 씬을 로드하는 메서드
        /// </summary>
        public void Run()
        {
            // 이벤트 구독
            BattleManager.Instance.OnBattleEnd += () => CurrentState = GameState.NewBattleResult;
            BattleManager.Instance.OnBattleQuit += () => CurrentState = GameState.Home;

            while (CurrentState != GameState.None)
            {
                currentScene = scenes[CurrentState];
                currentScene.Show();
            }

            // 이벤트 해지
            BattleManager.Instance.OnBattleEnd -= () => CurrentState = GameState.NewBattleResult;
            BattleManager.Instance.OnBattleQuit -= () => CurrentState = GameState.Home;
        }
    }
}