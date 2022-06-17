using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.transform.GetComponent<PlayerController>().StopMove();
            collision.gameObject.transform.GetComponent<PlayerController>().enabled = false;
            collision.gameObject.transform.GetComponent<Animator>().SetTrigger("death");
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
