using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Entity Values")]
    [SerializeField] protected float _speed;

    [Header("Bullet Config")]
    [SerializeField] protected Transform _spawner;
    protected float _timer;
    [SerializeField] protected float _fireRate;

    public abstract void GetDamage(float dmg);
}