using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public float MaxPower = 1;

    public float CurrentPower
    {
        get { return _currentPower; }
        set { _currentPower = value; }
    }


    private float _currentPower = 0.5f;
    private Image _powerImage;

    // Use this for initialization
    void Start()
    {
        _powerImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        //float percent = (_currentPower / MaxPower) *100;
        //percent = 100 - percent;

        //_powerImage.rectTransform.localPosition = new Vector2(-percent, 0);
        float percent = (_currentPower / MaxPower);
        _powerImage.fillAmount = percent;
    }

    public void Add(float _power)
    {
        _currentPower += _power;
        if (_currentPower >= MaxPower)
            _currentPower = MaxPower;
    }

    public void Sub(float _power)
    {
        _currentPower -= _power;
        if (_currentPower <= 0)
            _currentPower = 0;
    }
}
