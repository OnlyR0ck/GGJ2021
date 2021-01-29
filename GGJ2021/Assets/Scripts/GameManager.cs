using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private ObjectPooling _pool;
    private OreController _oreControllerScript;
    private GameObject _ore;
    [SerializeField] private int _currentLevel = 0;
    [SerializeField] private int _mustKill;
    [SerializeField] private int _currentKills;
    [SerializeField] private float _newHealth;
    [SerializeField] private float _newScore;
    [SerializeField] private float _newDamage;
    private readonly Vector3 _startPosition = new Vector3(10, 0);

    
    // Start is called before the first frame update
    void Awake()
    {
        LoadGameProgress();
        _newHealth = 10;
        _newDamage = 1;
        _newScore = 10;
    }
    void Start()
    {
        if (_currentLevel == 0)
        {
            SetParams();
        }
        ChangeObject();
    }
    
    
    void LoadGameProgress()
    {
        PlayerPrefs.GetInt("Level", _currentLevel);
    }

    void OnEnable()
    {
        _pool = GameObject.Find("SpawnManager").GetComponent<ObjectPooling>();
        OreController.oreDestroyed += ChangeObject;
    }

    void OnDisable()
    {
        OreController.oreDestroyed -= ChangeObject;
    }

    private void ChangeObject()
    {
        _ore = _pool.GetObject();
        _ore.transform.position = _startPosition;
        _oreControllerScript = _ore.GetComponent<OreController>();
        _currentKills++;
        
        if (_currentKills == _mustKill)
        {
            _currentLevel++;
            SetParams();
        }

        /*HPBarController.health = _newHealth;*/
        HPBarController.damage = _newDamage;
        HPBarController._sliderController.maxValue = _newHealth;
        HPBarController._sliderController.value = _newHealth;

        _oreControllerScript.health = _newHealth;
        _oreControllerScript.damage = _newDamage;
        _oreControllerScript.score = _newScore;
        _ore.SetActive(true);
        
    }

    void SetParams()
    {
        _mustKill = Random.Range(5, 8);
        _currentKills = 0;
        _newHealth = _newHealth + _newHealth * Mathf.Exp(_currentLevel / 10) +
                     Mathf.Pow(-1, Random.Range(0, 2)) * _newHealth / 10;
        _newScore = _newScore + _newScore * Mathf.Exp(_currentLevel / 40) +
                    Mathf.Pow(-1, Random.Range(0, 2)) * _newScore / 10;
        _newDamage = _newDamage * Mathf.Exp(_currentLevel / 20);
    }
}
