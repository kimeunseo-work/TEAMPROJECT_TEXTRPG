using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEAMPROJECT_TEXTRPG.Managers;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class QuestScene : Scene
    {
        public override void Show()
        {
                       
            QuestManager.Instance.SelectCategory(); 
           
        }
    }
}
