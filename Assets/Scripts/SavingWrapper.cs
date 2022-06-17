using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class SavingWrapper : MonoBehaviour
{
    const string defaultSaveFile = "save";

    private void Awake()
    {
        StartCoroutine(LoadLastScene());
    }

    IEnumerator LoadLastScene()
    {
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Delete();
        }
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }

    public void Delete()
    {
        GetComponent<SavingSystem>().Delete(defaultSaveFile);
    }
}
