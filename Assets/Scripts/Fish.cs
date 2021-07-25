using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;
    public BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        rb.velocity = new Vector2(-speed, 0);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            ++GameManager.instance.fish;
            GameManager.instance.updateFishDisplay();
            Destroy(this.gameObject);
        }
    }

    void Update(){
        if (transform.position.x < -50){
            Destroy(this.gameObject);
        }
    }
}
