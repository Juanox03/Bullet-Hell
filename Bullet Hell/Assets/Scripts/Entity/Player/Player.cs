using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] BulletPlayer _bulletPrefab;

    PlayerInputs _inputs;
    PlayerView _view;

    Factory<BulletPlayer> _factory;
    ObjectPool<BulletPlayer> _objectPool;

    private void Start()
    {
        _inputs = new PlayerInputs(this);
        _view = new PlayerView();

        _factory = new BulletPlayerFactory(_bulletPrefab);
        _objectPool = new ObjectPool<BulletPlayer>(_factory.GetObj, BulletPlayer.TurnOff, BulletPlayer.TurnOn, 4);
    }

    private void Update()
    {
        _inputs.OnUpdate();
    }

    public void Movement(float vertical, float horizontal)
    {
        transform.position += transform.forward * vertical * _speed * Time.deltaTime;
        transform.position += transform.right * horizontal * _speed * Time.deltaTime;
    }

    public void Shoot()
    {
        _timer += Time.deltaTime;

        if(_timer >= _fireRate)
        {
            var bullet = _objectPool.Get();
            bullet.AddReference(_objectPool);
            bullet.transform.position = _spawner.position;
            bullet.transform.forward = _spawner.forward;

            _timer = 0;
        }
    }

    public override void GetDamage(float dmg)
    {
        Debug.Log("Daño");
    }
}