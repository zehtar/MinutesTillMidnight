using UnityEngine;

[System.Serializable] // Позволяет редактировать в инспекторе
public class WeaponStats 
{
    public bool isSuperWeapon = false;
    public string weaponName = "DefaultGun";  // Название оружия
    public float fireRate = 1f;  // Скорострельность
    public int shotsPerShot = 1; // Количество выстрелов за раз
    public float bulletSpeed = 10f; // Скорость пули
    public float bulletDamage = 1f; // Урон
    public int passivesLevel = 0; //лвл пасивки 
    public int penetration = 0;
    public int bulletsInMagazine = 3;
    public float spread = 10f;
    public float reloadTime = 1f;
    public float critChance = 0f;
    public float critDamage = 0.5f;
}
