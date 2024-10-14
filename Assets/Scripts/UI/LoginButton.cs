using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginButton : BaseButton
{
    public override void OnClicked()
    {
        base.OnClicked();
        Managers.UI.CreateUI("LoginPanel");
    }
}
