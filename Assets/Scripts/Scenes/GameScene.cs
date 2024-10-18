using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Managers.Game.Init(); 
        SceneType = Scene.Game;
        Application.runInBackground = true;
        Managers.Sound.StopBGM();
    }
}
