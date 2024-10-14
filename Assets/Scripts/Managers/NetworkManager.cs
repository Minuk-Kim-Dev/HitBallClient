using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager
{
    private string apiUrl = "http://localhost:5054/api/scores";

    public IEnumerator CoPostScore(Score newScore)
    {
        GameObject waitingPanel = Managers.UI.CreateUI("WaitingPanel");

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
                Managers.Instance.StartCoroutine(Managers.Network.CoGetScore());
            }
        }
        UnityEngine.Object.Destroy(waitingPanel);
    }

    public IEnumerator CoGetScore()
    {
        GameObject waitingPanel = Managers.UI.CreateUI("WaitingPanel");

        using (UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "GET"))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                Score[] scores = JsonConvert.DeserializeObject<Score[]>(jsonResponse);

                yield return Managers.Instance.StartCoroutine(CoDisplayRanking(scores));
            }
        }
        UnityEngine.Object.Destroy(waitingPanel);
    }

    public IEnumerator CoDisplayRanking(Score[] scores)
    {
        GameObject panel = Managers.UI.CreateUI("RankingPanel");

        if (panel == null)
            Debug.LogError("RankingPanel 프리팹이 없습니다.");

        RankingPanel rankingPanel = panel.GetComponent<RankingPanel>();

        if (rankingPanel == null)
            Debug.LogError("No RankingPanel Component");

        Score[] sortedScores = scores
            .OrderByDescending(s => s.TotalScore) // 1순위: TotalScore 높은 순
            .ThenByDescending(s => s.HitCount)     // 2순위: HitCount 높은 순
            .ThenBy(s => s.Date)                    // 3순위: Date가 빠른 순
            .ToArray();

        yield return Managers.Instance.StartCoroutine(rankingPanel.CoUpdateRanking(sortedScores));
    }
}

[System.Serializable]
public class Score
{
    public int TotalScore { get; set; }
    public int HitCount { get; set; }
    public DateTime Date { get; set; }

    //player info
    public string Id { get; set; }
    public string Major { get; set; }
    public string Name { get; set; }
}
