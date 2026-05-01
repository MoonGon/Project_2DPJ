using UnityEngine;
using TMPro; 

public class CatapultShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float launchForce = 15f;
    public float launchAngle = 30f;

    [Header("Cooldown Settings")]
    public float cooldownTime = 2f;    
    private float nextFireTime = 0f;   
    public TextMeshProUGUI reloadUI;  

    void Update()
    {
        bool isReady = Time.time >= nextFireTime;

        if (reloadUI != null)
        {
            reloadUI.gameObject.SetActive(!isReady);
            if (!isReady)
            {
                float timeLeft = nextFireTime - Time.time;
                reloadUI.text = "RELOADING: " + timeLeft.ToString("F1") + "s";
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && isReady)
        {
            Shoot();
            nextFireTime = Time.time + cooldownTime;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float angleInRadians = launchAngle * Mathf.Deg2Rad;
            Vector2 forceDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            rb.AddForce(forceDirection * launchForce, ForceMode2D.Impulse);
        }
    }
    public void HideReloadUI()
    {
        if (reloadUI != null)
        {
            reloadUI.gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        if (Time.timeScale == 0f)
        {
            if (reloadUI != null && reloadUI.gameObject.activeSelf)
            {
                reloadUI.gameObject.SetActive(false);
            }
        }
    }
}