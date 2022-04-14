using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    public void UseKISetActive(GameObject _ki)
    {
        _ki.SetActive(true);
    }

    public void UseKISetNotActive(GameObject _other)
    {
        _other.SetActive(false);
    }

    public void ShowKIText(GameObject _text)
    {
        _text.SetActive(true);
    }

    public void HideOtherText(GameObject _text)
    {
        _text.SetActive(false);
    }

    public void UseOtherSetActive(GameObject _other)
    {
        _other.SetActive(true);
    }

    public void UseOtherSetNotActive(GameObject _ki)
    {
        _ki.SetActive(false);
    }

    public void ShowOtherText(GameObject _text)
    {
        _text.SetActive(true);
    }

    public void HideKIText(GameObject _text)
    {
        _text.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
