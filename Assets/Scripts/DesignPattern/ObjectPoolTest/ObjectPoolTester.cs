using PoolTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPoolTester: MonoBehaviour
{
    [SerializeField] private int count;
    private ObjectPool pool;

    private Stack<Poolable> releasePool = new Stack<Poolable>(); // 반납용

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            for (int i = 0; i < count; i++)
            {
                Poolable poolable = pool.Get();
                poolable.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

                releasePool.Push(poolable);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            if (releasePool.Count <= 0)
            {
                Debug.Log("반납할 풀오브젝트가 없습니다.");
                return;
            }

            //if (pool.objectPool.Count <= 0)
            //{
            //    Debug.Log("시간이 지나서 자동으로 릴리즈되었습니다.");
            //    Debug.Log("반납할 풀오브젝트가 없습니다.");
            //    return;
            //}

            for (int i = 0; i < count; i++)
            {
                Poolable poolable = releasePool?.Pop();
                if (poolable == null)
                {
                    Debug.Log("반납할 풀오브젝트가 없습니다.");
                    break;
                }
                pool.Release(poolable);
                
            }
        }
    }
}
