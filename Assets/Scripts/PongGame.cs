using UnityEngine;
using TMPro;

public class PongGame : MonoBehaviour
{
    public Transform ball;
    public Transform respawnPosition;
    public TMP_Text playerScoreText;
    public TMP_Text opponentScoreText;
    public AudioClip playerScoredSound;
    public AudioClip opponentScoredSound;

    private int playerScore;
    private int opponentScore;

    internal void OnGoal(Team team)
    {
        // Depending on which team's goal it was
        switch(team)
        {
            // It was the opponent's goal
            case Team.Opponent:
                // Increase the player score
                playerScore++;

                // Update the UI
                UpdateUI();

                // Respawn the ball
                RespawnBall();

                // Play the player scored sound
                GetComponent<AudioSource>().PlayOneShot(playerScoredSound);
                break;
            case Team.Player:
                // Increase the opponent score
                opponentScore++;

                // Update the UI
                UpdateUI();

                // Respawn the ball
                RespawnBall();

                // Play the player scored sound
                GetComponent<AudioSource>().PlayOneShot(opponentScoredSound);
                break;
        }
    }

    private void RespawnBall()
    {
        // Move the ball back to the respawn position
        ball.position = respawnPosition.position;

        // Reset the ball's velocity
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<TrailRenderer>().Clear();
    }

    private void UpdateUI()
    {
        playerScoreText.text = playerScore.ToString();
        opponentScoreText.text = opponentScore.ToString();
    }
}
