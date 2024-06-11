using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField] protected BulletEnemy _bulletPrefab;

    [Header("Target Player")]
    [SerializeField] protected Transform _playerTarget;

    [Header("Shield Force")]
    [SerializeField] protected GameObject _shield;
}