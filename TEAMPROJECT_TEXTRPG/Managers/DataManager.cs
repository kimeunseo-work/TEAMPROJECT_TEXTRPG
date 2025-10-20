using Newtonsoft.Json;
using TEAMPROJECT_TEXTRPG._Unfinished;
using TEAMPROJECT_TEXTRPG.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TEAMPROJECT_TEXTRPG.Managers
{
    internal class DataManager
    {
        private static DataManager instance;
        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataManager();
                }
                return instance;
            }
        }

        public List<(DateTime, PlayerData, List<QuestData>)> saveDatas;

        public DataManager()
        {
            // 폴더 경로 지정해서 불러오는 작업
            saveDatas = new();

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..", "SaveData");

            var index = 0;
            while (true)
            {
                var currentfilePath = Path.Combine(filePath, $"PlayerData{index}.json");

                if (File.Exists(currentfilePath))
                {
                    var data = File.ReadAllText(currentfilePath);
                    var loadData = JsonConvert.DeserializeObject<(DateTime, PlayerData, List<QuestData>)>(data);

                    saveDatas.Add(loadData);
                    index++;
                    continue;
                }
                break;
            }
        }

        /// <summary>
        /// 데이터 세이브
        /// </summary>
        public void SaveData()
        {
            var count = saveDatas.Count;

            List<QuestData> questDatas = new();
            foreach (var quest in QuestManager.Instance.OnGoingQuestList)
            {
                if(quest.Value is MonsterKillQuest monQuest)
                {
                    questDatas.Add(new QuestData()
                    {
                        QuestType = "MonsterKillQuest",
                        OnGoingQuest = quest
                    });
                }
            }

            var data = (DateTime.Now, CharacterManager.Instance.ExportPlayerData(), questDatas);
            var dataJson = JsonConvert.SerializeObject(data);

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..", "SaveData", $"PlayerData{count}.json");
            File.WriteAllText(filePath, dataJson);

            saveDatas.Add(data);
        }

        /// <summary>
        /// 데이터 로드
        /// </summary>
        public void LoadSave(int index)
        {
            var data = saveDatas[index];

            CharacterManager.Instance.LoadPlayerData(data.Item2);

            var QuestList = new List<KeyValuePair<int, Quest>>();
            foreach (var quest in data.Item3)
            {
                if (quest.QuestType == nameof(MonsterKillQuest))
                {
                    KeyValuePair<int, Quest> valuePair = new (quest.OnGoingQuest.Key, new MonsterKillQuest(quest.OnGoingQuest.Value));
                    QuestList.Add(valuePair);
                }
            }
            QuestManager.Instance.OnGoingQuestList = QuestList;
        }
    }

    /// <summary>
    /// 세이브 로드용 플레이어 데이터 전송 객체
    /// </summary>
    public class PlayerData
    {
        public int Level;
        public string Name;
        public Job CurrentJob;
        public int Hp;
        public int Mp;
        public int MaxHP;
        public int MaxMP;
        public double BaseAttack;
        public double BaseDefense;
        public double Attack;
        public double Defense;
        public double LvUpAttack;
        public double LvUpDefense;
        public int Exp;
        public int Gold;
        public Inven Inventory;
    }

    public class QuestData
    {
        public string QuestType;
        public KeyValuePair<int, Quest> OnGoingQuest;
    }
}
