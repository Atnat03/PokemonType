using System;
using UnityEngine;

public enum ScenesState
{
    Default, Fight, Center
}

public class StateGameManager : Observer
{
    public GameObject[] sceneList;
    public ScenesState currentScene = 0;

    private void Awake()
    {
        ListenToEvent<OnSwapScene>(SwapScene);
    }

    private void Start()
    {
        SwapScene(new OnSwapScene { newSceneIndex = currentScene });
    }

    private void SwapScene(OnSwapScene data)
    {
        for (int i = 0; i < sceneList.Length; i++)
        {
            if (i == (int)data.newSceneIndex)
            {
                sceneList[i].SetActive(true);
            }
            else
            {
                sceneList[i].SetActive(false);
            }
        }
        
        currentScene = data.newSceneIndex;
    }
}
