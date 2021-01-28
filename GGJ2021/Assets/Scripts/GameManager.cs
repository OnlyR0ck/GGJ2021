using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool requireOreToSpawn;

    private ObjectPooling _pool;
    private GameObject _ore;
    private readonly Vector3 _startPosition = new Vector3(10, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        _pool = GameObject.Find("SpawnManager").GetComponent<ObjectPooling>();
        requireOreToSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (requireOreToSpawn == true)
        {
            _ore = _pool.GetObject();
            _ore.transform.position = _startPosition;
            _ore.SetActive(true);
            requireOreToSpawn = false;
        }
    }
}
