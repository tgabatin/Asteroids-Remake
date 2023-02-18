using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float maxLife = 30.0f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)]; // Picks a random sprite
        this.transform.eulerAngles = new Vector3(0, 0, Random.value * 360.0f); // Creates a random rotation 
        this.transform.localScale = Vector3.one * size; // == new Vector3(this.size, this.size, this.size)
        _rb.mass = this.size * 2.0f;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rb.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLife);
    }

    private void OnCollisionEnter2D(Collision2D collisions)
    {
        // Specifically collide with a bullet and destroy it
        if (collisions.gameObject.tag == "Bullet")
        {
            // Destroy the objects
            // Split into multiple pieces if the asteroid is large enough
            if ((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid halfSplit = Instantiate(this, position, this.transform.rotation);
        halfSplit.size = this.size * 0.5f;

        halfSplit.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
