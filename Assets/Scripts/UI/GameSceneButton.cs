using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneButton : BaseButton
{
    public TMP_InputField idInput;
    public TMP_InputField majorInput;
    public TMP_InputField nameInput;

    CheckPlayerState state = CheckPlayerState.None;

    public override void OnClicked()
    {
        base.OnClicked();

        if (string.IsNullOrEmpty(idInput.text) || string.IsNullOrEmpty(majorInput.text) || string.IsNullOrEmpty(nameInput.text))
        {
            Managers.UI.CreateUI("WarningPanel").GetComponentInChildren<TMP_Text>().text = "입력되지 않은 정보가 있습니다.";
            return;
        }

        Managers.Instance.StartCoroutine(Managers.Network.CoGetGamePlayCount(idInput.text, majorInput.text, nameInput.text, CheckPlayCount));
        Managers.Instance.StartCoroutine(CoStartGame());
    }

    IEnumerator CoStartGame()
    {
        yield return new WaitUntil(() => state != CheckPlayerState.None);

        if(state == CheckPlayerState.Can)
        {
            Managers.Data.SetPlayerInfo(idInput.text, majorInput.text, nameInput.text);
            Managers.Scene.LoadScene(Scene.Game);
        }
        else if(state == CheckPlayerState.Cant)
        {
            Managers.UI.CreateUI("WarningPanel");
        }
    }

    void CheckPlayCount(int count)
    {
        if(count == -1)
        {
            Debug.LogError("DB error : count is -1");
            state = CheckPlayerState.Cant;
        }
        else
        {
            if (count >= 3)
            {
                state = CheckPlayerState.Cant;
            }
            else
            {
                state = CheckPlayerState.Can;
            }
        }
    }
}

public enum CheckPlayerState
{
    None,
    Can,
    Cant
}
