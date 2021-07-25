using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public CapsuleCollider2D collider;
    public SpriteRenderer renderer;
    public LayerMask platformsLayerMask;

    public GameObject pauseMenu;
    public Animator GameOverMenu;

    public Animator camShake;
    public GameObject evil;

    public SoundScript sound;

    public bool gameOver = false;

    public float jumpVelocity;
    public float jumpControlVelocity;
    public float speed;
    private Vector2 movement = new Vector2(0, 0);

    public bool speedBoosted = false;
    public float invicibilityTimer;
    public int damage = 0;

    //ui stuff
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver){

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)){
                pauseGame();
            }

            if (!paused){

                float h = Input.GetAxis("Horizontal");
                if (h != 0){

                    // if (IsGrounded()){
                        rb.velocity = new Vector2(h * speed, rb.velocity.y);
                    // } else {
                    //     rb.velocity += new Vector2(h * jumpControlVelocity * Time.deltaTime, 0);
                    //     rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -jumpControlVelocity, jumpControlVelocity), rb.velocity.y);
                    // }

                    if (h > 0){
                        renderer.flipX = false;
                    } else {
                        renderer.flipX = true;
                    }
                } else {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }

                if (IsGrounded() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))){
                    rb.velocity = Vector2.up * jumpVelocity;
                }

                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -jumpVelocity, jumpVelocity));
            }
        } else {
            //slow down velocity
            rb.velocity = new Vector2(rb.velocity.x * 0.95f * Time.deltaTime, rb.velocity.y * 0.95f * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Escape)){
                //reset game
                resetGame(true);
            }
        }
    }

    public void resetGame(bool restart){
        //reset all variables and restart game
        //player position
        transform.position = new Vector3(-28, -12, 0);
        damage = 0;
        gameObject.tag = "Player";

        //reset speed
        speed = 40;
        GameObject.Find("Platform").GetComponent<BackgroundScroller>().scrollSpeed = 25;
        GameObject.Find("Platform2").GetComponent<BackgroundScroller>().scrollSpeed = 25;
        GameObject.Find("Background").GetComponent<BackgroundScroller>().scrollSpeed = 10;
        GameObject.Find("Background2").GetComponent<BackgroundScroller>().scrollSpeed = 10;

        GameManager.instance.changeSpeed(1f);

        //evil
        EvilPingu temp = evil.GetComponent<EvilPingu>();
        evil.transform.position = temp.offScreen.transform.position;
        temp.cycle = 1;
        temp.walking = false;

        if (restart){
            //game over menu
            // Debug.Log("fucker");
            GameOverMenu.SetTrigger("GameOver");
            GameManager.instance.StartGame();
            //set back to false
            gameOver = false;
        }

        //score
        GameManager.instance.score = 0;
        GameManager.instance.scoreDisplay.text = "Score: ";

        //sound
        sound.playMainMenu();
    }

    public void pauseGame(){
        if (!paused){
            Time.timeScale = 0;
            rb.velocity = new Vector2(0, 0);
        } else {
            Time.timeScale = 1;
        }

        paused = !paused;
        pauseMenu.SetActive(paused);
    }

    private bool IsGrounded(){
        RaycastHit2D raycast = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        // Debug.Log(raycast.collider);
        return raycast.collider != null;
    }

    public void endGame(bool restart){
        gameOver = true;
        gameObject.tag = "Untagged";

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Obstacle");

        for(int x = 0; x < objs.Length; ++x){
            Destroy(objs[x]);
        }

        if (restart){
            //insert animation to end game
            // Debug.Log("penis");
            StartCoroutine(displayGameEnd());

            GameManager.instance.updateHighScore();
        }
    }

    public void removeSelection(){
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
    }

    public IEnumerator displayGameEnd(){
        yield return new WaitForSeconds(2f);

        GameOverMenu.SetTrigger("GameOver");
    }

    public void getHit(){
        camShake.SetTrigger("Shake");
        
        switch (damage){
            case 0:
                StartCoroutine(tempHit(invicibilityTimer));
                 
                break;
            case 1:
                endGame(true);
                break;
        }

        ++damage;
    }

    public IEnumerator tempHit(float timer){

        Color c = renderer.color;
        gameObject.tag = "Untagged";

        evil.GetComponent<EvilPingu>().walking = true;

        renderer.color = new Color(c.r, c.g, c.b, 0.25f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.5f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.25f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.5f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.25f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.5f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.25f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.5f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.25f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.5f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = new Color(c.r, c.g, c.b, 0.25f);
        yield return new WaitForSeconds(timer/10);

        renderer.color = c;

        gameObject.tag = "Player";

        yield return new WaitForSeconds(7.0f);

        damage = 0;
    }
}
