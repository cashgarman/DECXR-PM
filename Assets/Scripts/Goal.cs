using UnityEngine;
public enum Team
{
    Player,
    Opponent,
    Audience
}

public class Goal : MonoBehaviour
{
    public PongGame game;
    public Team team;

    private void OnTriggerEnter(Collider other)
    {
        // If the object is the ball
        if (other.gameObject.name == "Ball")
        {
            game.OnGoal(team);
        }
    }
}