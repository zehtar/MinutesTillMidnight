using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Material flashMaterial;

    [SerializeField] private float stunDuration = 0.12f;

    private Enemy _enemy;
    private Material originalMaterial;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        _enemy = GetComponent<Enemy>();

        if (spriteRenderer != null)
            originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        if (_enemy != null)
            _enemy.isHitStunned = true;

        if (spriteRenderer != null && flashMaterial != null)
            spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(stunDuration);

        if (spriteRenderer != null && originalMaterial != null)
            spriteRenderer.material = originalMaterial;

        if (_enemy != null)
            _enemy.isHitStunned = false;

        flashCoroutine = null;
    }
}