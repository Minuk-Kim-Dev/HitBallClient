using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : BaseButton
{
    public override void OnClicked()
    {
        base.OnClicked();
        Destroy(transform.parent.gameObject);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
