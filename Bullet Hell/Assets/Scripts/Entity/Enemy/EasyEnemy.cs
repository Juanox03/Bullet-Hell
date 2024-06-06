using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyEnemy : Enemy
{
    enum ShootType
    {
        Lineal
    }

    [Header("Shoot Type")]
    [SerializeField] private ShootType _shootType;

    [Header("Health Config")]
    [SerializeField] Image _healthBar;
    [SerializeField] float _currentHealth;
    [SerializeField] float _maxHealth;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {

        }
    }

    public override void GetDamage(float dmg)
    {
        _currentHealth -= dmg;

        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }
}