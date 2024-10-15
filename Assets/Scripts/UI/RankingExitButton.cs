using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingExitButton : BaseButton
{
    public override void OnClicked()
    {
        base.OnClicked();

        if(Managers.Scene.CurrentScene as GameScene)
        {
            Managers.Game.Clear();
            Managers.Scene.LoadScene(Scene.Login);
        }
        else if(Managers.Scene.CurrentScene as LoginScene)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
