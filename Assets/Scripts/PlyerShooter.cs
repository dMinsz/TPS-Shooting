using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
public class PlyerShooter : MonoBehaviour
{
    [SerializeField] private Rig aimRig;
    [SerializeField] private float reloadTime;
    [SerializeField] private WeaponHolder weaponHolder;

    private Animator animator;
    private bool isReloading;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(1);
        //info.length = reloadTime;

        //weaponHolder = GetComponent<WeaponHolder>();
    }

    public void Fire()
    {
        weaponHolder.Fire();
        animator.SetTrigger("Fire");
    }

    private void OnFire(InputValue value)
    {
        if (isReloading)
            return;

        Fire();
    }

    private void OnReload(InputValue value)
    {
        if (isReloading)
            return;

        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        animator.SetTrigger("Reloading");
        aimRig.weight = 0f;
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        aimRig.weight = 0.6f;
        isReloading = false;
    }
}
