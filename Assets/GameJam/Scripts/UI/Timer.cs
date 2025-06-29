using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Image timerBar;
    [SerializeField]
    float maxTime;
    [SerializeField]
    GameObject timeUpText;

    float timeLeft;
    float curTime;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        timeLeft = maxTime;
        curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            curTime += Time.deltaTime;
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public float GetCurTime()
    {
        return curTime;
    }
    public float GetLeftTime()
    {
        return timeLeft;
    }
}
