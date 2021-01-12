using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public BoxCollider spawnArea;
    public Target targetPrefab;
    public TMP_Text scoreText;
    public TMP_Text countdownText;
    public TMP_Text gameOverText;
    public int gameLength;

    private int score;
    private float timePastSinceLastCountdown;
    private int timeLeft;

    void Start()
    {
        // Spawn the first target
        SpawnTarget();

        // Reset the time left in the game
        timeLeft = gameLength;

        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        countdownText.text = $"Time Left: {timeLeft}";
    }

    public void SpawnTarget()
    {
        // Get a random position inside the spawn area
        Vector3 position = GetRandomSpawnPosition();

        // Instantiate the new target
        Target target = Instantiate(targetPrefab, position, Quaternion.Euler(90, 0, 0));
        target.game = this;
    }

    public void OnTargetHit(Target target)
    {
        SpawnTarget();

        ++score;
        UpdateUI();
    }

    private void Update()
    {
        // Move the whole world forward
        transform.Translate(transform.forward * Time.deltaTime);

        // Keep track of how much time has past since the last countdown
        timePastSinceLastCountdown += Time.deltaTime;

        // If a second has past since the last time we counted down
        if(timePastSinceLastCountdown >= 1f)
        {
            timePastSinceLastCountdown = 0f;
            timeLeft -= 1;

            UpdateUI();

            // Has the time run out
            if (timeLeft <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0f;

        // Hide the countdown text
        countdownText.gameObject.SetActive(false);

        // Show the game over text
        gameOverText.gameObject.SetActive(true);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return spawnArea.transform.position + new Vector3(
            Random.Range(-spawnArea.size.x / 2f, spawnArea.size.x / 2f),
            Random.Range(-spawnArea.size.y / 2f, spawnArea.size.y / 2f),
            Random.Range(-spawnArea.size.z / 2f, spawnArea.size.z / 2f));
    }
}
