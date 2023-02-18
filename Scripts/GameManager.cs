using UnityEngine;

// Keeps track of the overall state of the game

public class GameManager : MonoBehaviour
{
    public Player player;
    public int lives = 3;
    public ParticleSystem explosion;
    public float respawnerTime = 3.0f;
    public float respawnTime = 3.0f;
    public int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        // TODO: Increase Score based on different size of the asteroid 
        if (asteroid.size < 0.75f)
        {
            this.score += 100;
        } else if (asteroid.size < 1.2f)
        {
            this.score += 50;
        } else{
            this.score += 25;
        }
        //Debug.Log(score);
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        // Change position of explosion to be position of player
        this.lives--;

        if (this.lives <= 0)
        {
            // Game Over
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnerTime);
        // Layer Collion - Can also do void OnEnable()/OnDisable() in main player class
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        this.lives = 3;
        this.score = 0;
        Invoke(nameof(Respawn), this.respawnTime);

        // Call a UI Menu for the Game Over to Return to the Start Screen
    }

}
