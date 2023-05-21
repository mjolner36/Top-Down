using UnityEngine;

[CreateAssetMenu(fileName = "WeponSO")]
public class WeaponSO : ArtifactSO
{
    public int damage;
    public int gunShop;
    public Caliber caliber;
    public float reloadTime;
}

public enum Caliber
{
    Pistol,Shotgun,Rifle
}
