using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pickup : MonoBehaviour
{
    public int goldValue = 20;
    public bool pickedUp = false;
    public GameObject floatingTextPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !pickedUp)
        {
            pickedUp = true;
            ShowGold(goldValue.ToString());
            collision.transform.GetComponent<PlayerGold>().IncreaseGold(goldValue);
            GetComponent<Animator>().SetTrigger("pickup");
        }
    }

    public void ShowGold(string text)
    {
        if (floatingTextPrefab != null)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = "+ " + text;
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
