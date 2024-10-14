using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    EventSystem system;
    Selectable firstInput;
    Button startButton;
    Button exitButton;

    private void Start()
    {
        system = EventSystem.current;
        firstInput = Util.FindChildByName(gameObject, "IdInput").GetComponent<Selectable>();
        firstInput.Select();
        startButton = Util.FindChildByName(gameObject, "GameStartButton").GetComponent<Button>();
        exitButton = Util.FindChildByName(gameObject, "ExitButton").GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            startButton.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitButton.onClick.Invoke();
        }
    }
}
