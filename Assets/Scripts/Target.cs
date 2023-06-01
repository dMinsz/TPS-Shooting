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
        //�¾Ҵµ� ������ٵ������� ����
        //��¦�и��� ����

        rgd?.AddForceAtPosition(-10 * hit.normal,hit.point,ForceMode.Impulse);
        // ��� ������ �ݴ���������ؾ� ������ġ�� �ݴ�� �������ϰԵȴ�.
    }
}
    