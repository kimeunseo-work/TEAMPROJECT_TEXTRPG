using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG.Scenes
{
    internal class QuestScene : Scene
    {
        internal override void Show()
        {

            QuestManager.Instance.SelectCategory();


        }
    }
}
