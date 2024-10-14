using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    TMP_Text _rank;
    TMP_Text _id;
    TMP_Text _major;
    TMP_Text _name;
    TMP_Text _score;
    TMP_Text _hitCount;

    private void Awake()
    {
        _rank = Util.FindChildByName(gameObject, "Rank").GetComponent<TMP_Text>();
        _id = Util.FindChildByName(gameObject, "Id").GetComponent<TMP_Text>();
        _major = Util.FindChildByName(gameObject, "Major").GetComponent<TMP_Text>();
        _name = Util.FindChildByName(gameObject, "Name").GetComponent<TMP_Text>();
        _score = Util.FindChildByName(gameObject, "Score").GetComponent<TMP_Text>();
        _hitCount = Util.FindChildByName(gameObject, "HitCount").GetComponent<TMP_Text>();
    }

    public void SetRankingUI(int rank, Score score)
    {
        _rank.text = rank.ToString();
        _id.text = score.Id;
        _major.text = score.Major;
        _name.text = score.Name;
        _score.text = score.TotalScore.ToString();
        _hitCount.text = score.HitCount.ToString();
    }
}
