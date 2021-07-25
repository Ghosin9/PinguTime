using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    public float scrollSpeed;

    public Rigidbody2D rb;
    public BoxCollider2D collider;

    public float width;

    public float resetX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        if (GameManager.instance != null){
            scrollSpeed = scrollSpeed * GameManager.instance.backgroundScrollSpeed;
        }

        // width = collider.size.x;
        // width = width * this.transform.localScale.x;

        rb.velocity = new Vector2(-scrollSpeed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x <= -width){
            Vector2 resetPosition = new Vector2(resetX, transform.position.y);
            transform.position = resetPosition;
        }
    }
}
