using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyFactory : Factory<BulletEnemy>
{
    public BulletEnemyFactory(BulletEnemy p)
    {
        prefab = p;
    }
}
