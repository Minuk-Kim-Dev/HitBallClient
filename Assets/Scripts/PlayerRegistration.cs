using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRegistration : MonoBehaviour
{
    private string apiUrl = "http://localhost:5054/api/scores";

    void Start()
    {
        Score newScore = new Score
        {
            TotalScore = 100,
            HitCount = 20,
            RemainTime = 15,
            Date = DateTime.Now,
            Id = "1",
            Major = "Computer",
            Name = "gildong"
        };

        StartCoroutine(PostPlayer(newScore));
    }

    IEnumerator PostPlayer(Score newScore)
    {
        string jsonData = JsonConvert.SerializeObject(newScore);
        Debug.Log("JSON Data: " + jsonData);

        using (UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST"))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                Debug.Log("Player registered successfully!");
            }
        }
    }
}

[System.Serializable]
public class Score
{
    public int TotalScore { get; set; }
    public int HitCount { get; set; }
    public int RemainTime { get; set; }
    public DateTime Date { get; set; }

    //player info
    public string Id { get; set; }
    public string Major { get; set; }
    public string Name { get; set; }
}

