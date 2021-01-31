using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRebootButton : MonoBehaviour
{
    public GameObject _rebootShop;
    private RectTransform _rectTransform;

    private float speed;

    void Start()
    {
        _rectTransform = _rebootShop.GetComponent<RectTransform>();
    }
    public void OpenRebootShop()
    {
        _rebootShop.SetActive(true);
    }

    public void CloseRebootShop()
    {
        _rebootShop.SetActive(false);
    }

    //private IEnumerator MoveForward()
    //{
    //    while (_rebootShop.transform.position.x < 6.5f)
    //    {
    //        yield return new WaitForFixedUpdate();
    //        _rectTransform.position = new Vector2(_rectTransform.position.x + 0.02f,
    //            _rectTransform.position.y);
    //    }
    //}
}
