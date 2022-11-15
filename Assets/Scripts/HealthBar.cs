using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;

    private float _recoveryRate = 0.1f;
    private float _currentPlayerHealth;
    private float _divider = 100;
    private float _healthChanging = 10;

    private void Start()
    {
        _slider.maxValue = _player.MaxHealth / _divider;
        _slider.value = _player.Health / _divider;
    }

    public void Heal()
    {
        _currentPlayerHealth = _player.Health;
        _player.Health += _healthChanging;
        StartCourutines();
    }

    public void Damage()
    {
        _currentPlayerHealth = _player.Health;
        _player.Health -= _healthChanging;
        StartCourutines();
    }

    private void StartCourutines()
    {
        if (_currentPlayerHealth < _player.Health)
        {
            StopCoroutine(Unfill());
            var fill = StartCoroutine(Fill());
        }
        else
        {
            StopCoroutine(Fill());
            var unFill = StartCoroutine(Unfill());
        }
    }

    private IEnumerator Fill()
    {
        while (_slider.value < (_player.Health / _divider))
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _player.Health / _divider, _recoveryRate * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator Unfill()
    {
        while (_slider.value > (_player.Health / _divider))
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _player.Health / _divider, _recoveryRate * Time.deltaTime);
            yield return null;
        }
    }
}
