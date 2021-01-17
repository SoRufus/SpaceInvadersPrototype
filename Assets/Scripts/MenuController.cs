using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [HideInInspector] [SerializeField] private Text[] menuOptions;
    private int selectedText;
    void Start()
    {
        selectedText = 1;
        ColorSelectedOption();
    }

    public void Choose(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() !=0)
        {
            selectedText = 1 - selectedText;
            ColorSelectedOption();
        }
    }

    public void Select(InputAction.CallbackContext value)
    {
        float selectValue = value.ReadValue<float>();

        if (selectValue == 1)
        {
            if(selectedText == 1)
            {
                SceneManager.LoadScene("Main");
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void ColorSelectedOption()
    {
        menuOptions[selectedText].color = Color.green;
        menuOptions[1 - selectedText].color = Color.white;
    }
}



