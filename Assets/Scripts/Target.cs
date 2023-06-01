using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour,IHittable
{
    private Rigidbody rgd;
    private void Awake()
    {
        rgd = GetComponent<Rigidbody>();
    }
    public void Hit(RaycastHit hit, int damage)
    {
        //맞았는데 리지드바디가있으면 실행
        //살짝밀리는 느낌

        rgd?.AddForceAtPosition(-10 * hit.normal,hit.point,ForceMode.Impulse);
        // 노멀 벡터의 반대방향으로해야 맞은위치에 반대로 힘을가하게된다.
    }
}
    