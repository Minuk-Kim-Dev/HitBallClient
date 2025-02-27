using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : BaseButton
{
    public override void OnClicked()
    {
        base.OnClicked();
        Managers.Instance.StartCoroutine(Managers.Game.GameStart());
        Destroy(gameObject);
    }
}
