using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletEffect
{
    string EffectID { get; } 
    void OnHit(Enemy enemy, Vector2 hitPoint, Bullet orgBullet);
}
