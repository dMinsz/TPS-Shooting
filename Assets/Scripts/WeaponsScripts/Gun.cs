using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TrailRenderer bulletTrail;
    

    public void Awake()
    {
        //bulletEffect = GameManager.Resource.Load<ParticleSystem>("Prefabs/BulletHitEffect");
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
            
            //맞은거 이펙트
            var effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)); // 히트 된 위치의 직교하게 만들어준다.
            effect.transform.parent = hit.transform;//맞은오브젝트 따라가기

            //총알 움직이는 궤도 이펙트
            //var trail = Instantiate(bulletTrail,muzzleEffect.transform.position,Quaternion.identity);
            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, hit.point));

            target?.Hit(hit, damage);

        }
        else
        {
            //히트가안됬을때도 발사 궤적 이펙트는 나와야함
            //TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, hit.point));
          
        }
    }

    IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    {
        TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
        float totalTime = Vector2.Distance(startPoint, endPoint) / maxDistance;

        float time = 0;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, endPoint, time);
            time += Time.deltaTime / totalTime;
            

            yield return null;
        }

        Destroy(trail.transform.gameObject);
    }
}
