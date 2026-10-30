using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 12f;  // Сила рывка
    public float dashDuration = 0.2f;  // Длительность рывка
    public float dashCooldown = 1.5f;  // Время перезарядки
    public bool canDash;
    
    public player player;
    public bool isDashing = false;
    public float cooldownTimer = 0f;

    public float plBaseSpeed;
    
    public GameObject dashEffect;
    public Image dashLockImage;
    
    void Start()
    {
        player = FindObjectOfType<player>();
    }

    void Update()
    {
        dashLockImage.fillAmount = cooldownTimer / dashCooldown;
        // Обновляем таймер перезарядки
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0f)
        {
            canDash = true;
            dashLockImage.gameObject.SetActive(false);
        }
        else
        {
            canDash = false;
            dashLockImage.gameObject.SetActive(true);
        }

        // ПК: рывок по Left Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Dashing (Left Shift Pressed)");
            if (canDash)
            {
                PerformDash();
            }
        }
    }

    // Метод для UI-кнопки на Android
    public void DoDash()
    {
        if (canDash)
        {
            PerformDash();
        }
    }
    
    // Реализация рывка
    private void PerformDash()
    {
        isDashing = true;
        
        plBaseSpeed = player.speed;
      
        player.speed += dashForce;
        GameObject dashEf = Instantiate(dashEffect, player.transform.position, Quaternion.identity);
        dashEf.transform.localScale = new Vector3(1.5f , 1.5f , 1.5f);
        dashEf.transform.parent = player.transform;
            
        Invoke(nameof(ResetVelocity), dashDuration);
    }
    
    private void ResetVelocity()
    {
        player.speed = plBaseSpeed;
        cooldownTimer = dashCooldown;
        isDashing = false;
    }
}
