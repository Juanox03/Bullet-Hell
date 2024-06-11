using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EasyEnemy : Enemy
{
    enum ShootType
    {
        Lineal,
        Espiral,
        MultiEspiral
    }

    [Header("Shoot Type")]
    [SerializeField] private ShootType _shootType;

    [Header("Health Config")]
    [SerializeField] Image _healthBar;
    [SerializeField] float _currentHealth;
    [SerializeField] float _maxHealth;

    [Header("Phase Attack")]
    int _totalPhases = 3;
    [SerializeField] int _currentPhase = 1;

    [Header("Wachines")]
    [SerializeField] GameObject _wachinContainer;
    [SerializeField] List<GameObject> _wachines = new();

    Factory<BulletEnemy> _factory;
    ObjectPool<BulletEnemy> _objectPool;

    private void Start()
    {
        _factory = new BulletEnemyFactory(_bulletPrefab);
        _objectPool = new ObjectPool<BulletEnemy>(_factory.GetObj, BulletEnemy.TurnOff, BulletEnemy.TurnOn, 4);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            switch (_shootType)
            {
                case ShootType.Lineal:
                    ShootLineal();
                    break;
                case ShootType.Espiral:
                    ShootEspiral();
                    break;
                case ShootType.MultiEspiral:
                    ShootMultiEspiral();
                    break;
                default:
                    break;
            }
        }

        if (_currentPhase == 1)
        {
            _shootType = ShootType.Lineal;
        }
        if (_currentPhase == 2)
        {
            _shootType = ShootType.Espiral;
        }
        if (_currentPhase == 3)
        {
            _shootType = ShootType.MultiEspiral;
            _wachinContainer.SetActive(true);
        }

        if (_currentHealth <= 0)
        {
            _currentPhase += 1;
            if (_currentPhase <= _totalPhases)
            {
                StartCoroutine(ChargeLife());
            }
            else
            {
                Destroy(this);
            }
        }
}
    #region Shoot Lineal
    private void ShootLineal()
    {
        _fireRate = 0.2f;

        _timer += Time.deltaTime;

        if(_timer > _fireRate)
        {
            var bullet = _objectPool.Get();
            bullet.AddReference(_objectPool);
            bullet.transform.position = _spawner.position;
            bullet.transform.forward = _spawner.forward;
            var direction = (_playerTarget.position - _spawner.position).normalized;
            bullet.transform.forward = direction;

            _timer = 0;
        }
    }
    #endregion

    #region Shoot Espiral
    private void ShootEspiral()
    {
        _fireRate = 0.1f;

        _timer += Time.deltaTime;

        if (_timer > _fireRate)
        {
            var bullet = _objectPool.Get();
            bullet.AddReference(_objectPool);
            bullet.transform.position = _spawner.position;
            bullet.transform.forward = _spawner.forward;
            _spawner.eulerAngles = new Vector3(0, _spawner.eulerAngles.y + 10, 0);
            _timer = 0;
        }
    }
    #endregion

    #region Shoot MultiEspiral
    private void ShootMultiEspiral()
    {
        _fireRate = 0.2f;

        _timer += Time.deltaTime;

        if (_timer > _fireRate)
        {
            foreach (var item in _wachines)
            {
                var bullet = _objectPool.Get();
                bullet.AddReference(_objectPool);
                bullet.transform.position = _spawner.position;
                bullet.transform.forward = _spawner.forward;
            }
            _spawner.eulerAngles = new Vector3(0, _spawner.eulerAngles.y + 10, 0);

            _timer = 0;
        }
    }
    #endregion

    public override void GetDamage(float dmg)
    {
        _currentHealth -= dmg;

        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    IEnumerator ChargeLife()
    {
        _shield.SetActive(true);
        while (_currentHealth < _maxHealth)
        {
            //_currentHealth = 0;
            _currentHealth += Time.deltaTime * 200;
            if (_currentHealth >= _maxHealth) _currentHealth = _maxHealth;

            _healthBar.fillAmount = _currentHealth / _maxHealth;

            yield return null;
        }
        _shield.SetActive(false);
    }
}