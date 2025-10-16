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

        private QuestManager()
        {

            Monsters monsters = new Monsters();
            Quest Quest = new Quest(monsters);


        }
           

        public void settingMonster()
        {


           
            
        }
         //quest 생성자가 함수인데 이제 필드로 선언만했을때는 this가 완성되지 않아서 프로그램이 알 방법이없다. 그래서 함수 안에서 선언을 해서 
        //함수가 실행될때 선언되도록
        /// <summary>
        /// 생성자 매개변수때 왜 매개변수를 왜 변수필드때 못넣는지
        /// </summary>





        Dictionary<int, Quest> Quests;
        List<Quest> AbleQuests;
        Quest CurrentQuest;
        int TotalMonsterKillCount;
        int BestEquipmentStat;
        int BestLevel;


       

        public void AddMonsterCount() 
        {

            if(GameManager.Instance.monsters.All(x => x.IsDead) )//&&) //퀘스트 몬스터가 있을시 )
            {







            }
            






        }




        public void CheckBestStat() { }
        public void CheckBestLevel() { }












    }
}
