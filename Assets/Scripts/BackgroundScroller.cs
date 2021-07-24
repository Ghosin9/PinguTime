using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    public float scrollSpeed = -2f;

    public Rigidbody2D rb;
    public BoxCollider2D collider;

    public float width;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        width = collider.size.x;
        collider.enabled = false;

        rb.velocity = new Vector2(scrollSpeed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x <= -width){
            Vector2 resetPosition = new Vector2(width * 1.95f, 0);
            transform.position = resetPosition;
        }
    }
}
