using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    public float timeElapsed = 0.0f;
    public int buildingsDestroyed = 0;
    public int swings = 0; 

    // Start is called before the first frame update
    void Start()
    {
        SetAllDestructibleObjects();
    }

    private void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetInt("strokes", swings);
        PlayerPrefs.SetFloat("time", timeElapsed);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }


    public void SetLauncherSwingEvent()
    {
        FindObjectOfType<LauncherController>().OnLaunchAction.AddListener(AddSwingCount);
    }

    public void AddBuildingDestroyed()
    {
        buildingsDestroyed += 1;
    }


    public void AddSwingCount()
    {
        swings += 1; 
    }

    public void AddNewDestructibleObject(DestroyIt.Destructible destructible)
    {
        destructible.DestroyedEvent += AddBuildingDestroyed;
    }

    public void SetAllDestructibleObjects()
    {
        DestroyIt.Destructible[] destructibles = FindObjectsOfType<DestroyIt.Destructible>();

        foreach (var destructible in destructibles)
        {
            destructible.DestroyedEvent += AddBuildingDestroyed;
        }

    }

}
