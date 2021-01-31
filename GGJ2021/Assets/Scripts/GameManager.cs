using UnityEngine;
using System;
using Random = UnityEngine.Random;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private Dictionary<int, string> _dictionary;
    private ObjectPooling _pool;
    public Sprite[] oreSprites;
    private GameObject _ore;
    private MaxHealthController _maxHealth;
    private CurrentHealthController _currentHealth;
    private OreController oreControllerScript;
    private BalanceObjectController _balanceControllerScript;
    [SerializeField] private int _currentLevel = 0;
    private int _mustKill;
    private int _currentKills;
    private double _oldHealth = 10;
    private double _newHealth;
    private double _manualDamage = 1;
    private readonly Vector3 _startPosition = new Vector3(10, 0);
    
    public TextMeshProUGUI lvlText;
    private double _newPotassium = 10;
    private double _oldPotassium = 10;
    [SerializeField] private bool _shopState = true;

    // Start is called before the first frame update
    void Awake()
    {
        _dictionary = new Dictionary<int, string>();
        _dictionary.Add(0, "");
        _dictionary.Add(1, "K"); _dictionary.Add(2, "M"); _dictionary.Add(3, "T"); _dictionary.Add(4, "QD"); _dictionary.Add(5, "QN");
        _dictionary.Add(5, "S");

    }
    void Start()
    {
        
        _pool = GameObject.Find("SpawnManager").GetComponent<ObjectPooling>();
        _balanceControllerScript = GameObject.Find("BalanceObject").GetComponent<BalanceObjectController>();
        _maxHealth = GameObject.Find("MaxHealth").GetComponent<MaxHealthController>();
        _currentHealth = GameObject.Find("CurrentHealth").GetComponent<CurrentHealthController>();

        LoadProgress();
        
        if (_maxHealth == null)
        {
            Debug.LogError("_maxHealth has no founded");
        }
        if (_currentLevel == 0)
        {
            _mustKill = Random.Range(5, 8);
            _oldPotassium = 10;
            _newHealth = 10;
            _manualDamage = 1;
            ChangeNewOreParams();
            SetHealthBarParams();
        }
        ChangeOre();
    }
    public string ConvertBigNumber(double number)
    {
        double temp = 100000;
        if (number <= temp)
            return $"{number: 0}";
        int i = 0;
        while (temp < number)
        {
            temp *= 1000;
            i++;
        }
        double t = number / temp * 100;
        return $"{(int) t}.{(int)((t - (int)t) * 100)}{_dictionary[i + 1]}";
    }
    
    void OnEnable()
    {
        OreController.oreDestroyed += OreDestroyed;
        CurrentHealthController.oreDestroyed += OreDestroyed;
        ClickersUpgradesController.Upgrade += ChangeManualDamage;
        ShopController.shopAction += ChangeShopState;
    }

    void OnDisable()
    {
        OreController.oreDestroyed -= OreDestroyed;
        CurrentHealthController.oreDestroyed -= OreDestroyed;
        ClickersUpgradesController.Upgrade -= ChangeManualDamage;
        ShopController.shopAction -= ChangeShopState;
    }

    private void ChangeOre()
    {
        
        _ore = _pool.GetOre();
        _ore.transform.position = _startPosition;
        oreControllerScript = _ore.GetComponent<OreController>();
        oreControllerScript._oreSpriteRender.sprite = oreSprites[Random.Range(0, oreSprites.Length)];
        _currentHealth.SetCurrentHealth(_newHealth);
        _maxHealth.MaxHealthSet(_newHealth);
        if (!_shopState)
        {
            _ore.SetActive(true);
        }
    }
    void ChangeOldOreParams()
    {
        _mustKill = Random.Range(5, 8);
        _oldHealth += _oldHealth * Mathf.Exp(_currentLevel / 10f);
        _oldPotassium += _oldPotassium * Mathf.Exp(_currentLevel / 40f);
    }

    void UpdateLvlText()
    {
        lvlText.text = $"ORE LV {_currentLevel}\n";
    }

    private void ChangeManualDamage(double term)
    {
        _manualDamage += term;
    }
    public double GetManualDamage()
    {
        return _manualDamage;
    }
    void CheckLevel()
    {
        if (_currentKills == _mustKill)
        {
            _currentLevel++;
            UpdateLvlText();
            ChangeOldOreParams();
            _currentKills = 0;
            _mustKill = Random.Range(5, 8);
        }
    }

    void SetHealthBarParams()
    {
        /*HPBarController.health = _newHealth;*/
        //HPBarController.Damage = _manualDamage;
        HPBarController._sliderController.maxValue = (float) _newHealth;
        HPBarController._sliderController.value = (float) _newHealth;
    }
    private void ChangeNewOreParams()
    {
        _newHealth = _oldHealth + Mathf.Pow(-1, Random.Range(0, 2)) * _oldHealth / 10f;
        _newPotassium = _oldPotassium + Mathf.Pow(-1, Random.Range(0, 2)) * _oldPotassium / 10f;
    }
    void OreDestroyed()
    {
        ExtractPotassium();
        _currentKills++;
        CheckLevel();
        ChangeNewOreParams();
        SetHealthBarParams();
        _ore.SetActive(false);
        ChangeOre();
        SaveProgress();
    }

    void ExtractPotassium()
    {
        _balanceControllerScript.ChangeBalance(_newPotassium);
    }

    void ChangeShopState()
    {
        _shopState = !_shopState;
        _ore?.SetActive(!_shopState);
    }

    void SaveProgress()
    {
        PlayerPrefs.SetInt("_mustKill", _mustKill);
        PlayerPrefs.SetInt("_currentKills", _currentKills);
        PlayerPrefs.SetString("_oldHealth", _oldHealth.ToString());
        PlayerPrefs.SetString("_newHealth", _newHealth.ToString());
        PlayerPrefs.SetString("_manualDamage", _manualDamage.ToString());
        PlayerPrefs.SetString("_oldPot", _oldPotassium.ToString());
        PlayerPrefs.SetString("_newPot", _newPotassium.ToString());
    }

    void LoadProgress()
    {
        _mustKill = PlayerPrefs.GetInt("_mustKill", _mustKill);
        _currentKills = PlayerPrefs.GetInt("_currentKills", _currentKills);
        _oldHealth = Convert.ToDouble(PlayerPrefs.GetString("_oldHealth", _oldHealth.ToString()));
        _newHealth = Convert.ToDouble(PlayerPrefs.GetString("_newHealth", _newHealth.ToString()));
        _manualDamage = Convert.ToDouble(PlayerPrefs.GetString("_manualDamage", _manualDamage.ToString()));
        _oldPotassium = Convert.ToDouble(PlayerPrefs.GetString("_oldPot", _oldPotassium.ToString()));
        _newPotassium = Convert.ToDouble(PlayerPrefs.GetString("_newPot", _newPotassium.ToString()));
    }
}
