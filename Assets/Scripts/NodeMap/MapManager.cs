using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public MapConfig config;
        public MapFloorDataConfig FloorDataConfig;
        public ItemPoolConfig itemPoolConfig;
        public MapView view;
        public int CurrentFloor = 0;
        public static MapManager Instance;
        public SpriteRenderer MapImage;
        public GameObject[] PostProcessing;
        [SerializeField]
        private GameObject Clone_PostProcessing;

        public Map CurrentMap { get; private set; }

        private void Start()
        {
            Instance = this;
            /*if (PlayerPrefs.HasKey("Map"))
            {
                string mapJson = PlayerPrefs.GetString("Map");
                Map map = JsonConvert.DeserializeObject<Map>(mapJson);
                // using this instead of .Contains()
                if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
                {
                    // payer has already reached the boss, generate a new map
                    GenerateNewMap();
                }
                else
                {
                    CurrentMap = map;
                    // player has not reached the boss yet, load the current map
                    view.ShowMap(map);
                }
            }
            else
            {
                GenerateNewMap();
            }*/
        }
        void Update()
        {
            //print(CurrentMap.nodes.Count);
        }

        public void GenerateNewMap()
        {
            Map map = MapGenerator.GetMap(config,CurrentFloor);
            CurrentMap = map;
            Debug.Log(map.ToJson());
            view.ShowMap(map);
            MapImage.sprite = config.FloorLayers[CurrentFloor].MapImage;
            SoundManager.instance.PlayMusic(config.FloorLayers[CurrentFloor].music);
            if(Clone_PostProcessing != null) Destroy(Clone_PostProcessing);
            if (PostProcessing[CurrentFloor] != null)
            {
                Clone_PostProcessing = Instantiate(PostProcessing[CurrentFloor]);
                Clone_PostProcessing.transform.position = new Vector3(0,0,0);
            }
        }

        public void SaveMap()
        {
            if (CurrentMap == null) return;

            string json = JsonConvert.SerializeObject(CurrentMap, Formatting.Indented,
                new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            SaveMap();
        }
    }
}
