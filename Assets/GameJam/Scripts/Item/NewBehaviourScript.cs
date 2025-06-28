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
            Debug.Log("실패다..");
        }
        else
        {
            Debug.Log("된다 이거.");
        }

        if (weap == null)
        {
            Debug.Log("실패다..");
        }
        else
        {
            Debug.Log("된다 이거.");
        }

    }


}
