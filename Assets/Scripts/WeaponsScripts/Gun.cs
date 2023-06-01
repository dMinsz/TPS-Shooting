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
        Gizmos.color = Color.green; // �׶��� üũ ������
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 1000);

    }

    public void Fire()
    {
        //Debug.Log("�߻�");
        muzzleEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
           
            IHittable target = hit.transform.GetComponent<IHittable>();
            
            //������ ����Ʈ
            var effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)); // ��Ʈ �� ��ġ�� �����ϰ� ������ش�.
            effect.transform.parent = hit.transform;//����������Ʈ ���󰡱�

            //�Ѿ� �����̴� �˵� ����Ʈ
            //var trail = Instantiate(bulletTrail,muzzleEffect.transform.position,Quaternion.identity);
            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, hit.point));

            target?.Hit(hit, damage);

        }
        else
        {
            //��Ʈ���ȉ������� �߻� ���� ����Ʈ�� ���;���
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
