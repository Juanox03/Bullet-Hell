using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float dmg;
    [SerializeField] float _lifeTime;

    ObjectPool<BulletPlayer> _objectPool;
    public float counter;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        transform.position += transform.forward * _speed * Time.deltaTime;

        if(_lifeTime < 0)
        {
            Destroy(gameObject);
        }

        counter += Time.deltaTime;

        if (counter >= 2)
        {
            _objectPool.StockAdd(this);
        }
    }

    public void AddReference(ObjectPool<BulletPlayer> op)
    {
        _objectPool = op;
    }

    public static void TurnOff(BulletPlayer bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    public static void TurnOn(BulletPlayer bullet)
    {
        bullet.counter = 0;
        bullet.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<Entity>();
        if(entity != null)
        {
            entity.GetDamage(dmg);
            Destroy(gameObject);
        }

        if(other.CompareTag("Shield Enemy"))
        {
            Destroy(gameObject);
        }
    }
}