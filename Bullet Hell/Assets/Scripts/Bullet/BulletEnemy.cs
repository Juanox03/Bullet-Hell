using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float dmg;
    [SerializeField] float _lifeTime;

    ObjectPool<BulletEnemy> _objectPool;
    public float counter;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        transform.position += transform.forward * _speed * Time.deltaTime;

        if (_lifeTime < 0)
        {
            Destroy(gameObject);
        }

        counter += Time.deltaTime;

        if (counter >= 2)
        {
            _objectPool.StockAdd(this);
        }
    }

    public void AddReference(ObjectPool<BulletEnemy> op)
    {
        _objectPool = op;
    }

    public static void TurnOff(BulletEnemy bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    public static void TurnOn(BulletEnemy bullet)
    {
        bullet.counter = 0;
        bullet.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            entity.GetDamage(dmg);
            Destroy(gameObject);
        }

    }
}
