using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{

    public float speed;
    public float playerSpeed;

    public Rigidbody2D rb;
    public BoxCollider2D collider;

    private bool destroyObj = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        if (GameManager.instance != null){
            playerSpeed = GameManager.instance.speedBoost;
        }

        rb.velocity = new Vector2(-speed, 0);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            Player p = other.GetComponent<Player>();

            StartCoroutine(increaseSpeed(p));
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0);
            collider.enabled = false;
        }
    }

    void Update(){
        if (transform.position.x < -50 && destroyObj){
            Destroy(this.gameObject);
        }
    }

    IEnumerator increaseSpeed(Player p){
        p.speedBoosted = true;
        float temp = p.speed;
        p.speed = playerSpeed;
        destroyObj = false;

        GameManager.instance.changeSpeed(playerSpeed/temp);

        yield return new WaitForSeconds(5f);

        p.speed = temp;
        GameManager.instance.changeSpeed(temp/playerSpeed);
        p.speedBoosted = false;
        Destroy(this.gameObject);
    }
}
