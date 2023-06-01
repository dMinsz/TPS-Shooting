using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Gun gun;

    List<Gun> gunList = new List<Gun>();
    public void Fire()
    {
        gun.Fire();
    }

    public void Swap(Gun gun) 
    {
        if (gunList.Count < 0)
            return;

        //if (gunList.Find(gun)) 
        //{
        //    gunList[gun]
        //}
    }

    public void GetWeapon(Gun gun) 
    {
        gunList.Add(gun);
    }
}
