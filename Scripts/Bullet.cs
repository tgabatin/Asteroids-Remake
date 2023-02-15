using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D _rb;
    public float speed = 500.0f;
    public float maxLife = 10.0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rb.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLife);
    }

    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

}
