using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerFactory : Factory<BulletPlayer>
{
    public BulletPlayerFactory(BulletPlayer p)
    {
        prefab = p;
    }
}