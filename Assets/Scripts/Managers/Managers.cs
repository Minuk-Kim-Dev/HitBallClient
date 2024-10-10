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

    #region Contents

    static GameManager _game = new GameManager();

    public static GameManager Game { get { return _game; } }

    #endregion

    #region Core

    #endregion

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
