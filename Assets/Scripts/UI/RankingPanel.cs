using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RankingPanel : MonoBehaviour
{
    Transform _content;

    private void Init()
    {
        _content = Util.FindChildByName(gameObject, "Content").transform;
    }

    public IEnumerator CoUpdateRanking(Score[] scores)
    {
        Init();

        for(int i = 0; i < scores.Length; i++)
        {
            GameObject rankingUI = Managers.UI.CreateUI("Ranking", _content);
            Ranking ranking = rankingUI.GetComponent<Ranking>();
            ranking.SetRankingUI(i + 1, scores[i]);
        }

        yield break;
    }
}
