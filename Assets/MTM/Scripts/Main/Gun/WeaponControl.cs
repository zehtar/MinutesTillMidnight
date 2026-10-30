using UnityEngine;
using YG;

public class WeaponControl : MonoBehaviour
{
    public float offset;
    public bool autoAim = false;
    public float crosshairDistance = 7.5f;
    public GameObject crosshairLine;
    public Transform crosshair;
    public GameObject fireButton;
    public GameObject dashButton;
    public GameObject autoAimButton;
    public Joystick joystick;
    public Joystick joystickPlayer;

    public GameObject dashImage;
    public GameObject skillImage;
    public GameObject aimImage;

    public StatsSaved st;
    private PlayerDash playerDash;

    public string deviceStr;
    
    private string GetDeviceStr()
    {
#if UNITY_EDITOR
        // В редакторе - симуляция
        return YG2.infoYG.Simulation.device.ToString();
#else
        // В БИЛДЕ - Проверка через ОС
        string os = SystemInfo.operatingSystem.ToLower();
        
        // Если в названии ОС есть "android" или "iphone" - это мобилка
        if (os.Contains("android") || os.Contains("iphone") || os.Contains("ipad"))
        {
            return "mobile";
        }
        // Во всех остальных случаях (Windows, Mac, Linux) считаем, что это ПК
        else
        {
            return "desktop";
        }
#endif
    }

    public void Start()
    {
        deviceStr = GetDeviceStr();
        Debug.Log(deviceStr);
        //Desktop
        //Mobile

        st = FindObjectOfType<StatsSaved>();
        playerDash = FindObjectOfType<PlayerDash>();

        if (deviceStr == "desktop")
        {
            st.controlType = ControlType.PC;
        }
        else
        {
            st.controlType = ControlType.Android;
            aimImage.SetActive(false);
            dashImage.SetActive(false);
            skillImage.SetActive(false);
        }

        if (st.controlType == ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
            joystickPlayer.gameObject.SetActive(false);
            fireButton.SetActive(false);
        }
        
        autoAim = true;
    }

    void Update()
    {
        Transform target = null;

        if (st.controlType == ControlType.Android && joystick != null)
        {
            Vector2 joystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
            joystickPlayer.gameObject.SetActive(true);

            if (autoAim)
            {
                crosshair.gameObject.SetActive(false);
                joystick.gameObject.SetActive(false);
                fireButton.SetActive(true);
                target = FindClosestEnemy();
                if (target != null)
                {
                    AimAtTarget(target.position);
                }
            }
            else
            {
                joystick.gameObject.SetActive(true);
                fireButton.SetActive(false);

                if (joystickDirection.magnitude > 0.1f)
                {
                    Vector3 aimPosition = transform.position + (Vector3)joystickDirection.normalized * crosshairDistance;
                    AimAtTarget(aimPosition);
                    crosshair.position = aimPosition;
                    crosshair.gameObject.SetActive(true);
                    crosshairLine.SetActive(true);
                }
                else
                {
                    crosshair.gameObject.SetActive(false);
                }
            }
        }
        else //PC
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePosition - transform.position).normalized;
            joystickPlayer.gameObject.SetActive(false);

            if (autoAim)
            {
                crosshair.gameObject.SetActive(true);
                joystick.gameObject.SetActive(false);
                fireButton.SetActive(false);
                target = FindClosestEnemy();
                if (target != null)
                {
                    AimAtTarget(target.position);
                }
            }
            else
            {
                AimAtTarget(mousePosition);
                joystick.gameObject.SetActive(false);
                crosshair.gameObject.SetActive(false);
                fireButton.SetActive(false);
            }
        }

        Vector3 LocalScale = transform.localScale;
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            LocalScale.y = -1f;
        }
        else
        {
            LocalScale.y = +1f;
        }
        transform.localScale = LocalScale;

        if (crosshair != null && target != null)
        {
            crosshair.position = target.position;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AutoAimButton();
        }
    }

    public void AutoAimButton()
    {
        autoAim = !autoAim;
        autoAimButton.SetActive(autoAim);
    }

    void AimAtTarget(Vector3 targetPosition)
    {
        Vector3 difference = targetPosition - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    public void puAutoAim()
    {
        autoAim = true;
    }
}
