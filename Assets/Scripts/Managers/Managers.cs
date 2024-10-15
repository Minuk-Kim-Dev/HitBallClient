using System;
using System.Collections;
using System.Resources;
using System.Runtime.Serialization;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

    static GameManager _game = new GameManager();
    static NetworkManager _network = new NetworkManager();
    static DataManager _data = new DataManager();
    static SoundManager _sound = new SoundManager();
    static PoolManager _pool = new PoolManager();
    static SceneManagerEx _scene = new SceneManagerEx();
    static ResourceManager _resource = new ResourceManager();
    static UIManager _ui = new UIManager();
    static SettingManager _setting = new SettingManager();

    public static GameManager Game { get { return _game; } }
    public static NetworkManager Network { get { return _network; } }
    public static DataManager Data { get { return _data; } }
    public static SoundManager Sound { get { return _sound; } }
    public static PoolManager Pool { get { return _pool; } }
    public static SceneManagerEx Scene { get {  return _scene; } }
    public static ResourceManager Resource { get { return _resource; } }
    public static UIManager UI { get { return _ui; } }
    public static SettingManager Setting { get { return _setting; } }

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();

            _sound.Init();
            _data.Init();
            _setting.Init();
        }
    }

    public static void Clear()
    {

    }
}
