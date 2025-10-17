using TEAMPROJECT_TEXTRPG.Scenes;

namespace TEAMPROJECT_TEXTRPG
{
    /// <summary>
    /// 배틀 결과 상태
    /// </summary>
    internal enum NewBattleState
    {
        None,// 전투 X
        Start,// 전투 시작. 몬스터 소환
        PlayerTurn,
        MonsterTurn,
        Victory,
        Lose
    }

    internal class BattleManager
    {
        /* 싱글톤 */
        //============================================================//
        private static BattleManager instance;
        internal static BattleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BattleManager();
                }
                return instance;
            }
        }

        /* 이벤트 */
        //============================================================//

        internal Action<List<Monster>> OnMonsterSpawned;
        // 몬스터 공격 후 출력할 화면
        internal Action<Monster, int?, bool> OnMonsterActioned;

        /* 필드 */
        //============================================================//
        private NewBattleState currentBattleState = NewBattleState.None;
        internal NewBattleState CurrentBattleState
        {
            get => currentBattleState;
            set
            {
                currentBattleState = value;
                StateHandler();
            }
        }

        /* 상태 관련 메서드 */
        //============================================================//

        /// <summary>
        /// 현재 배틀 상태를 바꾸는 메서드.
        /// Battle 씬의 첫 번째로 작동하는 외부 호출용.
        /// </summary>
        internal void ChangeBattleState()
        {
            // 나중에 이걸로 배틀 상태 저장할 수 있음.
            if (GameManager.Instance.currentState != GameState.Battle)
            {
                // 이어서 하기
                //ChangeBattleState(currentBattleState);
                ChangeBattleState(NewBattleState.None);
                return;
            }

            switch (CurrentBattleState)
            {
                case NewBattleState.None:
                    // 현재 상태가 배틀이 아니면 배틀 스타트
                    ChangeBattleState(NewBattleState.Start);
                    break;
                case NewBattleState.Start:
                    // 현재 상태가 배틀 시작이면 플레이어 턴으로
                    ChangeBattleState(NewBattleState.PlayerTurn);
                    break;
                case NewBattleState.PlayerTurn:
                    if (IsMonstersAllDead())
                        // 플레이어 턴에서 몬스터가 다 죽었으면 승리
                        ChangeBattleState(NewBattleState.Victory);
                    else
                        // 아니면 몬스터 턴으로
                        ChangeBattleState(NewBattleState.MonsterTurn);
                    break;
                case NewBattleState.MonsterTurn:
                    if (IsPlayerDead())
                        // 몬스터 턴에서 플레이어가 죽었으면 패배
                        ChangeBattleState(NewBattleState.Lose);
                    else
                        // 아니면 몬스터 턴으로
                        ChangeBattleState(NewBattleState.PlayerTurn);
                    break;
                case NewBattleState.Victory:
                case NewBattleState.Lose:
                    // 현재 상태가 승리 또는 패배라면
                    // 배틀 상태가 아니므로 None
                    ChangeBattleState(NewBattleState.None);
                    GameManager.Instance.ChangeGameState(GameState.Home);
                    break;
            }
        }

        /// <summary>
        /// 현재 배틀 상태에 따라
        /// 적절한 로직을 호출하는 메서드
        /// </summary>
        private void StateHandler()
        {
            if (CurrentBattleState == NewBattleState.None)
            {
                // 이전 배틀 내용을 초기화?
            }
            else if (CurrentBattleState == NewBattleState.Start)
            {
                // 배틀 시작 위한 준비 코드

            }
            else if (CurrentBattleState == NewBattleState.PlayerTurn)
            {
                // 플레이어 공격 입력 받고 처리해야 함.
            }
            else if (CurrentBattleState == NewBattleState.MonsterTurn)
            {
                // 몬스터가 공격하는 로직
            }
            else if (CurrentBattleState == NewBattleState.Victory || CurrentBattleState == NewBattleState.Lose)
            {
                // 화면 호출
            }
        }

        /* 유틸리티 */
        //============================================================//
        private void ChangeBattleState(NewBattleState state) => CurrentBattleState = state;
        private bool IsMonstersAllDead() => CurrentMonsters.All(monster => monster.IsDead == true);
        private bool IsPlayerDead() => CharacterManager.Instance.player.Hp <= 0;
    }
}
