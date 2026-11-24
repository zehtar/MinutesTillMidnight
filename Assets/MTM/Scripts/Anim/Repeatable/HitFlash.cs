using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Enemy _enemy;
    public Material flashMaterial; // Тот самый белый материал
    
    private Material originalMaterial;
    private Coroutine flashCoroutine;

    void Start()
    {
        if (spriteRenderer == null) 
            spriteRenderer = GetComponent<SpriteRenderer>();
        _enemy = GetComponent<Enemy>();
        // Запоминаем родной материал врага (с текстурой)
        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Вместо сохранения текущей скорости, лучше иметь в скрипте врага 
        // переменную 'baseSpeed', которую мы не меняем.
        float originalSpeed = _enemy.speed; 
    
        // Если враг уже стоит (например, от прошлого хита), не сохраняем 0
        if (originalSpeed <= 0) originalSpeed = _enemy.maxSpeed; 

        spriteRenderer.material = flashMaterial;
        _enemy.speed = 0;

        yield return new WaitForSeconds(0.08f);

        spriteRenderer.material = originalMaterial;
        _enemy.speed = originalSpeed;
        flashCoroutine = null;
    }

}