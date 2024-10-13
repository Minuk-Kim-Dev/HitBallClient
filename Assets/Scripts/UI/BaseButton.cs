using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    public virtual void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    public virtual void OnClicked()
    {
        Managers.Sound.Play("Sounds/ButtonClick");
    }
}
