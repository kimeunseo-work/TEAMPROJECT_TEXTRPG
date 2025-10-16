using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG
{
    internal class Quest
    {
        string Name;

        string Description;

        Item ItemReward;

        int GoldReward;

        int ExpReward;


        public Monsters monsters;
        public List<Monster> monsterlist;


        public Quest(Monsters monsters)
        {
            this.monsters = monsters;
            this.monsterlist = monsters.monster;
         



        }

        //이렇게 생성자에서 Monsters를 받아올 수 있게 하면 Monsters의 클래스의 변수를 가져올수있는건가요 이 quest에서
        //
        
        


        public Monster QuestMonster;





        









    }

    internal class MonsterKillQuest : Quest
    {
        public MonsterKillQuest(Monsters monsters)
        : base(monsters) 
        
        
        {

            QuestMonster = monsterlist[1];

            

















        
        
        }


        //이 클래스의 퀘스트 몬스터는  = 이거













    }

    internal class MonsterKillQuest2 : Quest
    {









    }

}
