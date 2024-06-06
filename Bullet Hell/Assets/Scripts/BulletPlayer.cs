using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float dmg;
    [SerializeField] float _lifeTime;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        transform.position += transform.forward * _speed * Time.deltaTime;

        if(_lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Entity>();
        if(enemy != null)
        {
            enemy.GetDamage(dmg);
            Destroy(gameObject);
        }

    }
}