using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn;
    public GameObject objectToPool;
    public int amountPool;


    void Start()
    {
        prefabsToSpawn = new List<GameObject>();
        for (int i = 0; i < amountPool; i++)
        {
            GameObject temp = (GameObject) Instantiate(objectToPool);
            prefabsToSpawn.Add(temp);
            temp.SetActive(false);
            temp.transform.SetParent(this.transform);
        }
    }

    public GameObject GetOre()
    {
        return prefabsToSpawn[UnityEngine.Random.Range(0, amountPool)];
        //for (int i = 0; i < amountPool; i++)
        //{
        //    if (!prefabsToSpawn[i].gameObject.activeInHierarchy)
        //    {
        //        return prefabsToSpawn[i];
        //    }
        //}
        //Debug.Log("Закончил!");
        //return null;
    }
}
