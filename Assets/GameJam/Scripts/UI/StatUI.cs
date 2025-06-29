using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    List<GameObject> DurabilityUI;
    [SerializeField]
    int curDurability;
    PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject image in DurabilityUI)
        {
            image.SetActive(false);
        }
        status = player.GetComponent<PlayerStatus>();
        curDurability = status.GetToTalDurability();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + Vector3.up * 4f;
        SetDurabilityUI();
    }

    void SetDurabilityUI()
    {
        curDurability = status.GetToTalDurability();
        for (int i = 0; i < 5; i++)
        {
            if (i < curDurability)
            {
                DurabilityUI[i].SetActive(true);
            }
            else
            {
                DurabilityUI[i].SetActive(false);
            }
        }
    }
}
