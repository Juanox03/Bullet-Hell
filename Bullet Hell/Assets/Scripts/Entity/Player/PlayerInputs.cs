using UnityEngine;

public class PlayerInputs
{
    Player _player;

    public PlayerInputs(Player player)
    {
        _player = player;
    }

    public void OnUpdate()
    {
        Move();
        Shoot();
    }

    public void Move()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");

        _player.Movement(vertical, horizontal);
    }

    public void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _player.Shoot();
        }
    }
}