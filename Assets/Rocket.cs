using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : PlayerWeapon
{
    public GameObject explosion;

    protected override void Start()
    {
        base.Start();
    }

    public override void HitEnemy(GameObject other)
    {
        Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
        Destroy(gameObject);
    }
}
