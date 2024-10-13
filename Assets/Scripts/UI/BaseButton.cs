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
        //TODO : 버튼 클릭 사운드 발생
    }
}
