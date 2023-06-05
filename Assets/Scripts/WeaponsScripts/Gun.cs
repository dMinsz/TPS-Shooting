using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;
    [SerializeField] private ParticleSystem muzzleEffect;

    private ParticleSystem bulletEffect;
    public void Awake()
    {
        bulletEffect = GameManager.Resource.Load<ParticleSystem>("PreFabs/HitEffect");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // 그라운드 체크 디버깅용
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 1000);

    }

    public void Fire()
    {
        //Debug.Log("발사");
        muzzleEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
           
            IHittable target = hit.transform.GetComponent<IHittable>();
            target?.Hit(hit, damage);

            //맞은거 이펙트
            ParticleSystem effect = GameManager.Resource.Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal), true);
            effect.transform.parent = hit.transform.transform;
            GameManager.Resource.Destroy(effect.gameObject, 3f);

            //총알 움직이는 궤도 이펙트
            TrailRenderer trail = GameManager.Resource.Instantiate<TrailRenderer>("PreFabs/BulletTrail", muzzleEffect.transform.position, Quaternion.identity, true);
            trail.transform.position = transform.position;
            StartCoroutine(TrailRoutine(trail, trail.transform.position, hit.point));
            GameManager.Resource.Destroy(trail.gameObject, 3f);

           

        }
        else
        {
            //히트가안됬을때도 발사 궤적 이펙트는 나와야함
            TrailRenderer trail = GameManager.Resource.Instantiate<TrailRenderer>("PreFabs/BulletTrail", muzzleEffect.transform.position, Quaternion.identity, true);
            trail.transform.position = transform.position;
            StartCoroutine(TrailRoutine(trail, trail.transform.position, Camera.main.transform.forward * maxDistance));
            GameManager.Resource.Destroy(trail.gameObject, 3f);
        }
    }

    IEnumerator TrailRoutine(TrailRenderer trail, Vector3 startPoint, Vector3 endPoint)
    {
        float totalTime = Vector2.Distance(startPoint, endPoint) / maxDistance;

        float time = 0;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, endPoint, time);
            time += Time.deltaTime / totalTime;

            yield return null;
        }
    }

    //IEnumerator ReleaseRoutine(ParticleSystem effect) 
    //{
    //    yield return new WaitForSeconds(3f);
    //    GameManager.Pool.Release(effect);
    //}

}
