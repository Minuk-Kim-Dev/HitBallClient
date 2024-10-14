using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingButton : BaseButton
{
    public override void OnClicked()
    {
        base.OnClicked();
        Managers.Instance.StartCoroutine(Managers.Network.CoGetScore());
    }
}
