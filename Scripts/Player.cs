using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float _thrustSpeed = 1.0f;
    public float _turnSpeed = 1.0f;
    private bool _thrusting;
    private float _turnDirection;
    public Rigidbody2D _rb;

    // Gets called once when the game starts
    // Used for initialization work
    // Establishing references
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); // Function Unity provides that allows you to get a component
        // Searches for the game object the player script is attached to (Player) and finds the Component specified (RigidBody2D)
    }

    // Gets called for every frame the game is running
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
            _turnDirection = 1.0f;
        } 
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else 
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    // Gets called on a Fixed Time Interval, ensures physics is consistent
    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rb.AddForce(this.transform.up * this._thrustSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rb.AddTorque(_turnDirection * this._turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collisions)
    {
        if (collisions.gameObject.tag == "Asteroid")
        {
            // Return the velocity and angular rotation to zero.
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0.0f;

            // Turn off the game object
            this.gameObject.SetActive(false); // Stops all components 

            // Cannot invoke on game object that is turned off (GameManager)
            FindObjectOfType<GameManager>().PlayerDied(); // Bad - Costly & Slow (Also bad to use inside of Update())

        }
    }

    

}
