using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolTest
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Poolable poolablePrefab;

        [SerializeField] private int poolSize; // �ε��� �̸� ������ ������
        [SerializeField] private int maxSize; // �ݳ��� �ִ� ������ �񱳿�

        public Stack<Poolable> objectPool = new Stack<Poolable>(); // ����� Ǯ

        private void Awake()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                Poolable poolable = Instantiate(poolablePrefab);

                poolable.Pool = this; // �ݳ��� �� ����

                poolable.gameObject.SetActive(false); // active �� false
                poolable.transform.parent = transform; // ������� Ǯ ������Ʈ�� ��Ƶд�.

                objectPool.Push(poolable); // ���� ����� Ǯ�� Ǫ�� �ص�.
            }
        }

        public Poolable Get()
        {
            if (objectPool.Count > 0)
            {
                Poolable poolable = objectPool.Pop();
                poolable.gameObject.SetActive(true);
                poolable.transform.SetParent(null);// == poolable.transform.parent = null;
                return poolable;
            }
            else
            {
                Poolable poolable = Instantiate(poolablePrefab);
                poolable.Pool = this; // �ݳ��� �� ����
                return poolable;
            }
        }

        //���� �ݳ�
        public void Release(Poolable poolable)
        {
            if (objectPool.Count < maxSize)  
            {
                poolable.gameObject.SetActive(false);
                poolable.transform.SetParent(transform); //== poolable.transform.parent = transform;
                objectPool.Push(poolable);
            }
            else// Ǯ�� ������Ʈ�� �ʹ� ���� ������µ� ��� �ݳ��̾ƴ϶� MaxSize ��ŭ�� �ݳ��ϴ� �ó�����
                Destroy(poolable.gameObject);
        }

    }
}