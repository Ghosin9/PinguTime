using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float speed = -4f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < -4){
            transform.position = new Vector2(8, Random.Range(1.1f, -1.2f));
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            Player p = other.GetComponent<Player>();

            p.lives--;

            if (p.lives <= 0){
                p.endGame();
            }
        }
    }
}
