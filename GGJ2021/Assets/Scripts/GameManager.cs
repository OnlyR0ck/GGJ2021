using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;


public class GameManager : MonoBehaviour
{
    private ObjectPooling _pool;
    private OreController _oreControllerScript;
    private BalanceObjectController _balanceControllerScript;
    private GameObject _ore;
    private float _currentScore;
    [SerializeField] private int _currentLevel = 0;
    [SerializeField] private int _mustKill;
    [SerializeField] private int _currentKills;
    [SerializeField] private float _newHealth;
    private double _manualDamage {get; set; }
    private readonly Vector3 _startPosition = new Vector3(10, 0);
    
    public TextMeshProUGUI lvlText;
    private double _newScore;


    // Start is called before the first frame update
    void Awake()
    {
        LoadGameProgress();
        _newHealth = 10;
        _manualDamage = 1;
    }
    void Start()
    {
        if (_currentLevel == 0)
        {
            _currentScore = 0;
            ChangeOreParams();
        }
        ChangeOre();
    }
    
    
    void LoadGameProgress()
    {
        PlayerPrefs.GetInt("Level", _currentLevel);
    }

    void OnEnable()
    {
        _pool = GameObject.Find("SpawnManager").GetComponent<ObjectPooling>();
        _balanceControllerScript = GameObject.Find("BalanceObject").GetComponent<BalanceObjectController>();
        OreController.oreDestroyed += OreDestroyed;
        CurrentHealthController.oreDestroyed += OreDestroyed;
        ClickersUpgradesController.Upgrade += ChangeManualDamage;
    }

    void OnDisable()
    {
        OreController.oreDestroyed -= OreDestroyed;
        CurrentHealthController.oreDestroyed -= OreDestroyed;
        ClickersUpgradesController.Upgrade -= ChangeManualDamage;

    }

    private void ChangeOre()
    {
        _ore = _pool.GetOre();
        _ore.transform.position = _startPosition;
        _oreControllerScript = _ore.GetComponent<OreController>();

        _oreControllerScript.health = _newHealth;
        _oreControllerScript.damage = _manualDamage;
        _ore.SetActive(true);
    }
    
    //Меняет здоровье каждое разрушение
    void ChangeOreParams()
    {
        _mustKill = Random.Range(5, 8);
        _currentKills = 0;
        _newHealth = _newHealth + _newHealth * Mathf.Exp(_currentLevel / 10) +
                     Mathf.Pow(-1, Random.Range(0, 2)) * _newHealth / 10;
        _newScore = _newScore + _newScore * Mathf.Exp(_currentLevel / 40) + Mathf.Pow(-1, Random.Range(0, 2)) * _newScore / 10;
    }

    void UpdateLvlText()
    {
        lvlText.text = $"ORE LV {_currentLevel}\n";
    }

    private void ChangeManualDamage(double term)
    {
        _manualDamage += term;
    }

    void CheckLevel()
    {
        if (_currentKills == _mustKill)
        {
            _currentLevel++;
            UpdateLvlText();
        }
    }

    void SetHealthBarParams()
    {
        /*HPBarController.health = _newHealth;*/
        HPBarController.Damage = _manualDamage;
        HPBarController._sliderController.maxValue = _newHealth;
        HPBarController._sliderController.value = _newHealth;
    }

    void OreDestroyed()
    {
        ExtractPotassium();
        _currentKills++;
        CheckLevel();
        ChangeOreParams();
        SetHealthBarParams();
        ChangeOre();
    }

    void ExtractPotassium()
    {
        _balanceControllerScript.ChangeBalance(_newScore);
    }
}