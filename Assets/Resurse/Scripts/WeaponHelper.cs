using UnityEngine;
using System.Collections;
using CompleteProject;

public class WeaponHelper : MonoBehaviour {

   public PlayerShooting playerShooting;

    internal void Shoot()
    {
        playerShooting.Shoot();
    }
}
