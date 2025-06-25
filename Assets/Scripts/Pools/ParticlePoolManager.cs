using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolManager : MonoBehaviour
{
    [SerializeField] private GameObject animPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private string animationState = "Play";
    [SerializeField] private float animationDuration = 1f;

    private Queue<GameObject> pool = new Queue<GameObject>();

    public static ParticlePoolManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < poolSize; i++)
        {
            var go = Instantiate(animPrefab, transform);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public void PlayAnimationAt(Transform target)
    {
        if (pool.Count == 0) return;
        var go = pool.Dequeue();
        go.transform.position = target.position;
        go.SetActive(true);
        var animator = go.GetComponent<Animator>();
        animator.Play(animationState, 0, 0f);
        StartCoroutine(Recycle(go));
    }

    private IEnumerator Recycle(GameObject go)
    {
        yield return new WaitForSeconds(animationDuration);
        go.SetActive(false);
        pool.Enqueue(go);
    }
}
