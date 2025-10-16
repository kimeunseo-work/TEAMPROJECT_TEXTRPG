using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
{
    internal class QuestManager
    {
        /// <싱글톤>
        /// 
        /// </summary>
        private static QuestManager instance;
        internal static QuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance;
            }
        }



        Dictionary<int, Quest> Quests;
        List<Quest> AbleQuests;
        Quest CurrentQuest;
        int TotalMonsterKillCount;
        int BestEquipmentStat;
        int BestLevel;


        public QuestManager() { }

        public void AddMonsterCount() { }
        public void CheckBestStat() { }
        public void CheckBestLevel() { }












    }
}
