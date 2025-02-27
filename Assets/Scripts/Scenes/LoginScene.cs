using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Scene.Login;

        Application.runInBackground = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Managers.Sound.Play("bgm", Sound.Bgm);
    }
}
