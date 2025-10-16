using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAMPROJECT_TEXTRPG.Skills
{
    internal class SkillDatabase
    {
        internal static List<Skill> WarriorSkills = new List<Skill>()
        {
            new Skill("알파 스트라이크", 10, (int)CharacterManager.Instance.player.Attack * 2, 1, false, 1, "하나의 적을 공격합니다."),
            new Skill("더블 스트라이크", 15, (int)(CharacterManager.Instance.player.Attack * 1.5f), 2, true, 2 ,"2명의 적을 랜덤으로 공격합니다."),
        };
    }
}
