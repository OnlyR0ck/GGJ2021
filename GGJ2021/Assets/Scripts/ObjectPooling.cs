using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;
    public List<GameObject> prefabsToSpawn;
    public GameObject objectToPool;
    public int amountPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
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

    public GameObject GetObject()
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (!prefabsToSpawn[i].gameObject.activeInHierarchy)
            {
                return prefabsToSpawn[i];
            }
        }
        return null;
    }
}
