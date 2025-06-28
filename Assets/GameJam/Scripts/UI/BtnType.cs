using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnType : MonoBehaviour
{
    public BTNType currentType;

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Start:
                GameManager.Instance.LoadStageScene();
                break;

            case BTNType.Resume:
                GameObject.Find("StageMenuUI").GetComponent<StageMenuUI>().SetStageMenu(false);
                Time.timeScale = 1.0f;
                break;

            case BTNType.Restart:
                Time.timeScale = 1.0f;
                GameManager.Instance.LoadStageScene();
                break;

            case BTNType.Back:
                Time.timeScale = 1.0f;
                GameManager.Instance.LoadLobbyScene();
                break;

            case BTNType.Exit:
                Debug.Log("게임 종료");
                Application.Quit();
                break;
        }
    }
}

public enum BTNType
{
    Start,
    Resume,
    Restart,
    Back,
    Exit
}