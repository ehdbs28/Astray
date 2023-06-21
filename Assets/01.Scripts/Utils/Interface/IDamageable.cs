using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public bool OnDamage(float damage, Vector3 point, Vector3 normal);
}
