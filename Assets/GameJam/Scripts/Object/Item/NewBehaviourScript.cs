using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    GameObject weapon;
    [SerializeField]
    GameObject equip;
    [SerializeField]
    GameObject comsume;

    Item item;
    Weapon weap;
    private void Start()
    {
        item = weapon.GetComponent<Item>();
        weap = weapon.GetComponent<Weapon>();
    }

    void Update()
    {
        if (item == null)
        {
            Debug.Log("���д�..");
        }
        else
        {
            Debug.Log("�ȴ� �̰�.");
        }

        if (weap == null)
        {
            Debug.Log("���д�..");
        }
        else
        {
            Debug.Log("�ȴ� �̰�.");
        }

    }


}
