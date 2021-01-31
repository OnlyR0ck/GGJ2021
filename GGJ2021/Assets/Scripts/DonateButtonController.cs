using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DonateButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _textGameObject;
    private TextMeshProUGUI _textMesh;
    private BalanceObjectController _balanceObject;
    private DPSObjectController _dpsObject;
    private GameManager game;
    private TextMeshProUGUI _titleText;
    private double _cost = 100000f;
    private double _procent = 100;
    void Start()
    {
        _titleText = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        _dpsObject = GameObject.Find("DPSObject").GetComponent<DPSObjectController>();
        _balanceObject = GameObject.Find("BalanceObject").GetComponent<BalanceObjectController>();
        _textMesh = _textGameObject.GetComponent<TextMeshProUGUI>();
        _textMesh.text = $"BONUS {game.ConvertBigNumber(_procent)}% Damage";
        _titleText.text = $"NEED {game.ConvertBigNumber(_cost)}";
    }
    public void OnDonateButtonClick()
    {
         Debug.Log("Clicked!");
         _balanceObject.ChangeBalance(-_balanceObject.GetBalance());
            _cost -= _balanceObject.GetBalance();
         _dpsObject.ChangeDPS(_dpsObject.GetDps() * (1 + _procent / 100f));
         _procent *= 100;
         _textMesh.text = $"BONUS {game.ConvertBigNumber(_procent)}% Damage";
         _titleText.text = $"NEED {game.ConvertBigNumber(_cost)}";
    }
}
