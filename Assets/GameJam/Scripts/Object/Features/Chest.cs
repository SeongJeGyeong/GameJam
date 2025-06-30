using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    RandomItem randomItem;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    Sprite openSprite;
    [SerializeField]
    BoxCollider2D boxCollider;
    [SerializeField]
    bool IsRandom;
    [SerializeField]
    private List<int> meleeTypeNum;
    [SerializeField]
    private List<int> armorTypeNum;

    ItemSpawner itemSpawner;
    int randomNum;
    void Start()
    {
        if (IsRandom)
        {
            randomItem = (RandomItem)Random.Range(0, 2);
        }
        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        randomNum = 0;
    }

    void SpawnRandomItem()
    {
        switch(randomItem)
        {
            case RandomItem.Melee:
                randomNum = Random.Range(0, 7);

                if (randomNum > 3)
                {
                    itemSpawner.SpawnItem(spawnPoint.position, GlobalEnums.ItemType.STAFF, meleeTypeNum[randomNum], itemSpawner.StaffPrefabs[randomNum-4].GetComponent<Weapon>().GetAttackStat());
                }
                else
                {
                    itemSpawner.SpawnItem(spawnPoint.position, GlobalEnums.ItemType.MELEE, meleeTypeNum[randomNum], itemSpawner.MeleePrefabs[randomNum].GetComponent<Weapon>().GetAttackStat());
                }
                break;

            case RandomItem.Armor:
                randomNum = Random.Range(0, 4);
                
                itemSpawner.SpawnItem(spawnPoint.position, GlobalEnums.ItemType.ARMOR, armorTypeNum[randomNum], itemSpawner.EquipmentPrefabs[randomNum + 1].GetComponent<Equipment>().durability);
                break;

        }
    }

    IEnumerator OpenChest()
    {

        boxCollider.enabled = false;
        GetComponent<SpriteRenderer>().sprite = openSprite;
        SpawnRandomItem();
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.Instance.PlaySound(SoundEnum.ChestOpen);
            StartCoroutine("OpenChest");
        }
    }
}

public enum RandomItem
{
    Melee,
    Armor,
    Consumption
}