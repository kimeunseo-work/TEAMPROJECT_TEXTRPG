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

        public List<(int, DateTime, PlayerData, List<QuestData>)> saveDatas;
        public List<string> saveNames;

        public string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..", "SaveData");

        public DataManager()
        {
            // 폴더 없으면 생성
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            // 이름만 불러오기
            saveNames = Directory.GetFiles(folderPath).ToList();

            // 데이터 파일 로드
            List<(int, DateTime, PlayerData, List<QuestData>)> tempList = new();
            foreach (string fileName in saveNames)
            {
                //var currentfilePath = Path.Combine(folderPath, fileName);
                var jsonData = File.ReadAllText(fileName);
                var loadData = JsonConvert.DeserializeObject<(int, DateTime, PlayerData, List<QuestData>)>(jsonData);
                tempList.Add(loadData);
            }
            saveDatas = tempList;
        }

        /// <summary>
        /// 데이터 세이브
        /// </summary>
        public void SaveData(int input)
        {
            // 퀘스트 자식 클래스 판별
            List<QuestData> questDatas = new();
            foreach (var quest in QuestManager.Instance.OnGoingQuestList)
            {
                if (quest.Value is MonsterKillQuest monQuest)
                {
                    questDatas.Add(new QuestData()
                    {
                        QuestType = "MonsterKillQuest",
                        OnGoingQuest = quest
                    });
                }
            }
            var data = (input, DateTime.Now, CharacterManager.Instance.ExportPlayerData(), questDatas);
            // 직렬화
            var dataJson = JsonConvert.SerializeObject(data);

            // 저장
            var fileName = $"PlayerData{input}.json";
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..", "SaveData", fileName);
            // 실제로 덮어씌우기가 가능함.
            File.WriteAllText(filePath, dataJson);

            // 화면 출력 위해서
            // 덮어씌우기
            if (saveNames.Contains(filePath))
            {
                var index = saveNames.FindIndex(0, saveDatas.Count, x => x == filePath);
                saveDatas[index] = data;
            }
            else
            {
                saveDatas.Add(data);
            }
        }

        /// <summary>
        /// 데이터 로드
        /// </summary>
        public void LoadSave(int index)
        {
            // 파일 이름 불러오기
            var dataName = saveNames[index];
            var jsonData = File.ReadAllText(Path.Combine(folderPath, dataName));
            // 데이터 역직렬화
            var loadData = JsonConvert.DeserializeObject<(DateTime, PlayerData, List<QuestData>)>(jsonData);

            // 캐릭터 정보 로딩
            CharacterManager.Instance.LoadPlayerData(loadData.Item2);

            // 퀘스트 정보 로딩
            var QuestList = new List<KeyValuePair<int, Quest>>();
            foreach (var quest in loadData.Item3)
            {
                if (quest.QuestType == nameof(MonsterKillQuest))
                {
                    KeyValuePair<int, Quest> valuePair = new(quest.OnGoingQuest.Key, new MonsterKillQuest(quest.OnGoingQuest.Value));
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
        public Inventory Inventory;
    }

    public class QuestData
    {
        public string QuestType;
        public KeyValuePair<int, Quest> OnGoingQuest;
    }
}
