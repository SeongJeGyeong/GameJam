using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerData : MonoBehaviour
{
    public static TestPlayerData Instance // ΩÃ±€≈Ê
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TestPlayerData>();
                if (instance == null)
                {
                    GameObject instanceContainter = new GameObject("TestPlayerData");
                    instance = instanceContainter.AddComponent<TestPlayerData>();
                }
            }
            return instance;
        }
    }
    private static TestPlayerData instance;

    public GameObject Player;

}

