using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolTest
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Poolable poolablePrefab;

        [SerializeField] private int poolSize; // 로딩시 미리 만들어둘 사이즈
        [SerializeField] private int maxSize; // 반납시 최대 사이즈 비교용

        public Stack<Poolable> objectPool = new Stack<Poolable>(); // 사용할 풀

        private void Awake()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                Poolable poolable = Instantiate(poolablePrefab);

                poolable.Pool = this; // 반납할 곳 지정

                poolable.gameObject.SetActive(false); // active 는 false
                poolable.transform.parent = transform; // 만들어진 풀 오브젝트를 모아둔다.

                objectPool.Push(poolable); // 실제 사용할 풀에 푸쉬 해둠.
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
                poolable.Pool = this; // 반납할 곳 지정
                return poolable;
            }
        }

        //강제 반납
        public void Release(Poolable poolable)
        {
            if (objectPool.Count < maxSize)  
            {
                poolable.gameObject.SetActive(false);
                poolable.transform.SetParent(transform); //== poolable.transform.parent = transform;
                objectPool.Push(poolable);
            }
            else// 풀링 오브젝트를 너무 많이 만들었는데 모두 반납이아니라 MaxSize 만큼만 반납하는 시나리오
                Destroy(poolable.gameObject);
        }

    }
}