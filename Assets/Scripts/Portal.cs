using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public WinScreen winScreen;

    private void Awake()
    {
        winScreen = GameObject.FindWithTag("WinScreen").GetComponent<WinScreen>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            collision.gameObject.transform.GetComponent<PlayerController>().StopMove();
            collision.gameObject.transform.GetComponent<PlayerController>().enabled = false;
            collision.gameObject.transform.GetComponent<Animator>().SetTrigger("death");
            yield return new WaitForSeconds(1.5f);
            if (SceneManager.GetActiveScene().buildIndex + 1 == sceneCount)
            {
                winScreen.Setup();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
