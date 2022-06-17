using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Canvas canvas;
    public float maxHitpoint = 15f;
    private float hitpoint = 0f;
    private bool isDead = false;
    public GameObject floatingTextPrefab;
    public GameObject experienceText;

    private void Start()
    {
        hitpoint = maxHitpoint;
    }

    public void TakeDamage(float damage, Animator animator)
    {
        hitpoint = Mathf.Max(hitpoint - damage, 0);
        if (hitpoint <= 0 && !isDead)
        {
            isDead = true;
            canvas.enabled = false;
            animator.SetTrigger("death");
            ShowExperienceReward("+ 15 XP");
        } 
        else
        {
            ShowDamage(damage.ToString());
        }
    }

    public float GetFraction()
    {
        return hitpoint / maxHitpoint;
    }

    public float GetPercentage()
    {
        return 100 * GetFraction();
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void DestroyTextPrefab()
    {
        Destroy(floatingTextPrefab);
    }

    public float GetCurrentHitpoint()
    {
        return hitpoint;
    }

    public void ShowDamage(string text)
    {
        if (floatingTextPrefab != null)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, new Vector2(transform.position.x, transform.position.y + 1.25f), Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }

    public void ShowExperienceReward(string text)
    {
        if (experienceText != null)
        {
            GameObject prefab = Instantiate(experienceText, new Vector2(transform.position.x, transform.position.y + 1.25f), Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
}
