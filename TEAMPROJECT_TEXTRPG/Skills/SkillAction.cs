using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG.Skills
{
    internal class SkillAction
    {
        internal static void Action(Player player, Monster monster, Skill skill)
        {
            if (player.Mp < skill.Mp)
            {
                Console.WriteLine($"{player.Name}의 MP가 부족합니다.");
                return;
            }

            player.Mp -= skill.Mp;


        }
    }
}
