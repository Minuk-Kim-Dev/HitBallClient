using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : BaseButton
{
    public override void OnClicked()
    {
        base.OnClicked();
        Managers.UI.CreateUI("OptionPanel");
    }
}

