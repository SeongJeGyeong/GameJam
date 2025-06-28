using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    int totalDurability = 0;
    int totalAttackPower;


    public void ChangeDurability(int damage)
    {
        totalDurability += damage;
        if(totalDurability < 0)
        {
            Dead();
        }
    }

    public void SetDurability(int durability)
    {
        totalDurability = durability;
    }

    public int GetAttackPower()
    {
        return totalAttackPower;
    }

    public void SetAttackPower(int attackPower)
    {
        totalAttackPower = attackPower;
    }    

    void Dead()
    {

    }
}
