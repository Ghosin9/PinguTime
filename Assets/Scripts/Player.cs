using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D collider;

    public bool gameOver = false;
    public float speed = 3f;
    private Vector2 movement = new Vector2(0, 0);

    public int lives = 3;

    //ui stuff
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver){
            if (!paused){

                float v = Input.GetAxis("Vertical");
                movement.y = v;
            }
        }
    }

    void FixedUpdate(){
        rb.velocity = movement * speed;
    }

    public void endGame(){
        gameOver = true;
        Destroy(this.gameObject);
    }
}
