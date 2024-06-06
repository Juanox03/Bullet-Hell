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

    Vector3 _pointToMove;
    Vector3 _point1;
    Vector3 _point2;

    private void Start()
    {
        _point1 = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z);
        _point2 = new Vector3(transform.position.x - 8, transform.position.y, transform.position.z);

        _pointToMove = _point1;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            switch (_shootType)
            {
                case ShootType.Lineal:
                    ShootLineal();
                    MoveLineal();
                    break;
                default:
                    break;
            }
        }
    }

    #region Shoot Lineal
    private void ShootLineal()
    {
        _timer += Time.deltaTime;

        if(_timer > _fireRate)
        {
            Instantiate(_bulletPrefab, _spawner.position, _spawner.rotation);

            _timer = 0;
        }

    }

    private void MoveLineal()
    {
        transform.position = Vector3.Lerp(transform.position, _pointToMove, _speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _pointToMove) < 0.3f)
        {
            _pointToMove = _point2;
        }
        
        if(Vector3.Distance(transform.position, _pointToMove) < 0.3f)
        {
            _pointToMove = _point1;
        }
    }
    #endregion

    public override void GetDamage(float dmg)
    {
        _currentHealth -= dmg;

        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }
}