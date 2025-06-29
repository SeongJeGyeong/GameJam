using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [SerializeField]
    Timer timer;
    [SerializeField]
    GameObject clearUI;
    [SerializeField]
    GameObject failUI;
    [SerializeField]
    Text text;
    float time;
    float left;
    bool clear;
    bool IsFirst;
    void Start()
    {
        time = 0;
        clear = false;
        IsFirst = true;
        left = timer.GetLeftTime();
        clearUI.SetActive(false);
        failUI.SetActive(false);
    }

    void Update()
    {
        left = timer.GetLeftTime();
        if (left <= 0 && !clear && IsFirst)
        {
            IsFirst = false;
            ActiveGameFailUI();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(ActiveGameClearUI());
        }
    }

    IEnumerator ActiveGameClearUI()
    {
        clear = true;
        //Time.timeScale = 0f;
        time = timer.GetCurTime();
        text.text = "걸린 시간 : " + time.ToString() + " 초";
        clearUI.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        //Time.timeScale = 1.0f;
        GameManager.Instance.LoadLobbyScene();
    }

    void ActiveGameFailUI()
    {
        failUI.SetActive(true);
    }
}
