using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject stageMenu;

    private bool IsPaused;

    void Start()
    {
        IsPaused = false;
        stageMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                stageMenu.SetActive(false);
                Time.timeScale = 1.0f;
                IsPaused = false;
            }
            else
            {
                stageMenu.SetActive(true);
                Time.timeScale = 0;
                IsPaused = true;
            }
        }
    }

    public void SetStageMenu(bool paused)
    {
        stageMenu.SetActive(paused);
        IsPaused = paused;
    }
}
