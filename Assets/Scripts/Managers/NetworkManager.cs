using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager
{
    private string apiUrl = "http://CGlabHospital.iptime.org:40000/api/scores";

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

    public IEnumerator CoGetGamePlayCount(string id, string major, string name, Action<int> callback)
    {
        string url = $"{apiUrl}/playerinfo?id={id}&major={major}&name={name}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // 요청 전송
            yield return webRequest.SendWebRequest();

            // 요청에 실패한 경우
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
                callback.Invoke(-1);
            }
            else
            {
                // 요청 성공한 경우 서버에서 반환된 데이터 출력
                Debug.Log($"Player info count: {webRequest.downloadHandler.text}");
                callback.Invoke(int.Parse(webRequest.downloadHandler.text));
            }
        }
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
