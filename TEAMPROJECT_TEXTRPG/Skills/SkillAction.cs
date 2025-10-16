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
            //MP 충분한지 확인
            if (player.Mp < skill.Mp)
            {
                Console.WriteLine($"{player.Name}의 MP가 부족합니다.");
                return;
            }

            //MP 차감
            player.Mp -= skill.Mp;

            //스킬 실행 시 문구 출력
            Console.WriteLine($"{player.Name}이(가) {skill.Name}을 사용했습니다!");
            Console.WriteLine($">>{skill.Description}");

            //누구에게 지정할지
            if (!skill.IsRandom)
            {
                var target = GameManager.Instance.monsters[0];
                Console.WriteLine($"{target.Name}을(를) 지정했습니다");
            }
            else
            {
                Console.WriteLine($"{skill.Count}명의 랜덤 적을 대상으로 지정했습니다.");
            }
            
        }
    }
}
