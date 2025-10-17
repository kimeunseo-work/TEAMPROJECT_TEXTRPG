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

    }
}
