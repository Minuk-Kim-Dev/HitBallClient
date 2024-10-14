using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : BaseButton
{
    public override void OnClicked()
    {
        base.OnClicked();
        Destroy(transform.parent.gameObject);
    }
}
