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
    private List<int> meleeTypeNum;
    [SerializeField]
    private List<int> armorTypeNum;

    ItemSpawner itemSpawner;
    int randomNum;
    void Start()
    {
        randomItem = (RandomItem)Random.Range(0, 2);
        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        randomNum = 0;
    }

    void SpawnRandomItem()
    {
        Debug.Log(randomItem);
        switch(randomItem)
        {
            case RandomItem.Melee:
                randomNum = Random.Range(0, 7);
                Debug.Log(randomNum);

                if (randomNum > 3)
                {
                    itemSpawner.SpawnItem(spawnPoint.position, GlobalEnums.ItemType.STAFF, meleeTypeNum[randomNum], 10);
                }
                else
                {
                    itemSpawner.SpawnItem(spawnPoint.position, GlobalEnums.ItemType.MELEE, meleeTypeNum[randomNum], 10);
                }
                break;

            case RandomItem.Armor:
                randomNum = Random.Range(0, 4);
                Debug.Log(randomNum);

                itemSpawner.SpawnItem(spawnPoint.position, GlobalEnums.ItemType.ARMOR, armorTypeNum[randomNum], 0);
                break;

        }
    }

    IEnumerator OpenChest()
    {
        boxCollider.enabled = false;
        GetComponent<SpriteRenderer>().sprite = openSprite;
        SpawnRandomItem();
        yield return new WaitForSeconds(1.0f);
        Debug.Log("확인2");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("확인1");
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