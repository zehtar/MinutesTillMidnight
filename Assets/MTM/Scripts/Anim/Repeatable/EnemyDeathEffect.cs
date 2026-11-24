using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    private Animator animator;
    private EnemyDeathEffectPool pool;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetPool(EnemyDeathEffectPool effectPool)
    {
        pool = effectPool;
    }

    public void Play(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);

        gameObject.SetActive(true);

        // Сбрасываем анимацию в начальное состояние.
        animator.Rebind();
        animator.Update(0f);
    }

    // Вызывается через Animation Event в конце анимации.
    public void StopAndReturn()
    {
        pool.ReturnToPool(this);
    }
}