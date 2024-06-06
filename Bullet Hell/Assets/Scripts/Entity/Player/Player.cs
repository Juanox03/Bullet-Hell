using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    PlayerInputs _inputs;
    PlayerView _view;

    private void Start()
    {
        _inputs = new PlayerInputs(this);
        _view = new PlayerView();
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
            Instantiate(_bulletPrefab, _spawner.position, _spawner.rotation);

            _timer = 0;
        }
    }

    public override void GetDamage(float dmg)
    {
        Debug.Log("Daño");
    }
}