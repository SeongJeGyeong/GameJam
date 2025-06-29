using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    int totalDurability = 0;
    int totalAttackPower;


    public event Action OnDead;

    public void ChangeDurability(int value)
    {
        totalDurability += value;
        if(totalDurability < 0)
        {
            OnDead?.Invoke();
        }
    }

    public int GetDurability()
    {
        return totalDurability;
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
}
