using TEAMPROJECT_TEXTRPG.Core;

namespace TEAMPROJECT_TEXTRPG.Managers
{
    internal class CharacterManager
    {
        /* 싱글톤 */
        //============================================================//
        private static CharacterManager instance;
        public static CharacterManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CharacterManager();
                }
                return instance;
            }
        }

        public Player player = new();

        /// <summary>
        /// 플레이어 데이터 불러오기
        /// </summary>
        public void LoadPlayerData(PlayerData data)
        {
            player.Level = data.Level;
            player.Name = data.Name;
            player.CurrentJob = data.CurrentJob;
            player.Hp = data.Hp;
            player.Mp = data.Mp;
            player.MaxHP = data.MaxHP;
            player.MaxMP = data.MaxMP;
            player.BaseAttack = data.BaseAttack;
            player.BaseDefense = data.BaseDefense;
            player.Attack = data.Attack;
            player.Defense = data.Defense;
            player.LvUpAttack = data.LvUpAttack;
            player.LvUpDefense = data.LvUpDefense;
            player.Exp = data.Exp;
            player.Gold = data.Gold;
            player.Inventory = data.Inventory;
        }

        /// <summary>
        /// 플레이어 데이터 내보내기
        /// </summary>
        public PlayerData ExportPlayerData()
        {
            return new PlayerData()
            {
                Level = player.Level,
                Name = player.Name,
                CurrentJob = player.CurrentJob,
                Hp = player.Hp,
                Mp = player.Mp,
                MaxHP = player.MaxHP,
                MaxMP = player.MaxMP,
                BaseAttack = player.BaseAttack,
                BaseDefense = player.BaseDefense,
                Attack = player.Attack,
                Defense = player.Defense,
                LvUpAttack = player.LvUpAttack,
                LvUpDefense = player.LvUpDefense,
                Exp = player.Exp,
                Gold = player.Gold,
                Inventory = player.Inventory,
            };
        }
    }
}
