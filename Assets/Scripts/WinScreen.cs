using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject theGameObject;

    public void Setup()
    {
        theGameObject.SetActive(true);
    }

    public void HideGameObject()
    {
        theGameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        theGameObject.SetActive(false);
        SceneManager.LoadScene(0);
        theGameObject.SetActive(false);
    }

    public void BackToFirstLevel()
    {
        theGameObject.SetActive(false);
        SceneManager.LoadScene(1);
        theGameObject.SetActive(false);
    }
}
