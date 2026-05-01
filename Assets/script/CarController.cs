using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public Transform laneBottom;
    public Transform laneTop;
    public float moveSpeed = 15f;
    private bool isTopLane = false;

    [Header("Jump Settings")]
    public float normalJumpHeight = 2.5f;
    public float highJumpHeight = 6.0f;
    public float jumpDuration = 0.8f;
    private bool isJumping = false;
    private float jumpTimer = 0f;
    private float currentPeakHeight;

    [Header("Game Stats")]
    public int health = 3;
    public int coinCount = 0;
    public int winCondition = 200;
    public int pointsPerCoin = 5;

    [Header("UI Elements")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI healthText;
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI winFinalScoreText;
    public TextMeshProUGUI loseFinalScoreText;

    void Start()
    {
        Time.timeScale = 1f;
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
        UpdateStatsUI();
    }

    void Update()
    {
        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) isTopLane = true;
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) isTopLane = false;

            if (isTopLane && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                jumpTimer = 0f;
                currentPeakHeight = normalJumpHeight;
            }
        }

        float baseY = isTopLane ? laneTop.position.y : laneBottom.position.y;
        float targetY = baseY;

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float progress = jumpTimer / jumpDuration;
            if (Input.GetKey(KeyCode.Space) && progress < 0.5f)
            {
                currentPeakHeight = Mathf.Lerp(normalJumpHeight, highJumpHeight, progress / 0.5f);
            }
            if (progress >= 1f) isJumping = false;
            else
            {
                float jumpOffset = Mathf.Sin(progress * Mathf.PI) * currentPeakHeight;
                targetY = baseY + jumpOffset;
            }
        }

        Vector3 targetPos = new Vector3(transform.position.x, targetY, 0);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    private void OnDrawGizmos()
    {
        if (laneBottom != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(-50, laneBottom.position.y, 0), new Vector3(50, laneBottom.position.y, 0));
        }

        if (laneTop != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector3(-50, laneTop.position.y, 0), new Vector3(50, laneTop.position.y, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            health--;
            Destroy(collision.gameObject);
            UpdateStatsUI();

            if (health <= 0) ShowGameOver(false);
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount += pointsPerCoin;
            Destroy(collision.gameObject);
            UpdateStatsUI();

            if (coinCount >= winCondition) ShowGameOver(true);
        }
    }

    void ShowGameOver(bool isWin)
    {
        if (coinText != null) coinText.gameObject.SetActive(false);
        if (healthText != null) healthText.gameObject.SetActive(false);

        if (isWin)
        {
            if (winPanel != null) winPanel.SetActive(true);
            if (winFinalScoreText != null) winFinalScoreText.text = "Final Score: " + coinCount;
        }
        else
        {
            if (losePanel != null) losePanel.SetActive(true);
            if (loseFinalScoreText != null) loseFinalScoreText.text = "Final Score: " + coinCount;
        }
        Time.timeScale = 0f;
    }

    void UpdateStatsUI()
    {
        if (coinText != null) coinText.text = "Coins: " + coinCount + " / " + winCondition;
        if (healthText != null) healthText.text = "Health: " + health;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("กลับหน้าหลัก");
    }
}