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

    public static GameManager Game { get { return _game; } }
    public static NetworkManager Network { get { return _network; } }
    public static DataManager Data { get { return _data; } }

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
        }
    }

    public static void Clear()
    {

    }
}
