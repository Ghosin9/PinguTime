using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float speed;

    public Rigidbody2D rb;
    public BoxCollider2D collider;
    private bool destroyObj = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        rb.velocity = new Vector2(-speed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < -50 && destroyObj){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            Player p = other.GetComponent<Player>();

            p.getHit();
            SoundScript.instance.playHit();
        }
    }
}
