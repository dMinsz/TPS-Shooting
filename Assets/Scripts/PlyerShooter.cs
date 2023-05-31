using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlyerShooter : MonoBehaviour
{
    //[SerializeField] private Rig aimRig;
    [SerializeField] private float reloadTime;
    //[SerializeField] private WeaponHolder weaponHolder;

    private Animator animator;
    private bool reloading;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Fire()
    {
        //weaponHolder.Fire();
        animator.SetTrigger("Fire");
    }

    private void OnFire(InputValue value)
    {
        if (reloading)
            return;

        Fire();
    }

    private void OnReload(InputValue value)
    {
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        animator.SetTrigger("Reloading");
        //aimRig.weight = 0f;
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        //aimRig.weight = 0.6f;
        reloading = false;
    }
}
