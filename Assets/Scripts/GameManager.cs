using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Animator blackScreen;
    public Animator mainMenu;

    public Text scoreDisplay;
    public Text highScoreDisplay;
    public Text fishDisplay;

    public GameObject Obstacle;
    public GameObject iceSkate;
    public GameObject flyingOb;
    public GameObject fishOb;

    public BackgroundScroller[] scrollers = new BackgroundScroller[4];

    public Player p;

    public float score = 0;
    private float highScore = 0;
    public int fish = 0;

    public float difficulty = 0;

    public float backgroundScrollSpeed;
    public float speedBoost;

    void Awake() {
        if (instance != null && instance != this){
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // public void Start(){
    //     StartGame();
    // }

    public void fadePanel(){
        mainMenu.SetTrigger("MainMenuFade");
    }

    public void StartGame(){
        StartCoroutine(spawnObstacle(5f));
        StartCoroutine(spawnIceSkates(10f));
        StartCoroutine(spawnFish(15f));
        StartCoroutine(spawnFlyingObstacles(20f));
        StartCoroutine(updateScore());
        p.gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateFishDisplay(){
        fishDisplay.text = "" + fish;
    }

    //score display
    public IEnumerator updateScore(){
        score += 0.1f;
        if (p.speedBoosted)
            score += 0.1f;
        score = Mathf.Round(score * 100f) / 100f;
        yield return new WaitForSeconds(0.5f);
        scoreDisplay.text = "Score: " + score;
        if (!p.gameOver)
            StartCoroutine(updateScore());
    }

    public void updateHighScore(){
        if (score >= highScore){
            highScore = score;
            highScoreDisplay.text = "High Score: " + highScore;
        }
    }

    public void changeSpeed(float newSpeed){

        backgroundScrollSpeed = newSpeed;

        foreach (BackgroundScroller scroll in scrollers){
            scroll.scrollSpeed = scroll.scrollSpeed * backgroundScrollSpeed;
            scroll.rb.velocity = new Vector2(-scroll.scrollSpeed, 0);
        }
    }

    public IEnumerator spawnObstacle(float respawn){
        yield return new WaitForSeconds(respawn);
        if (!p.gameOver){
            Instantiate(Obstacle, new Vector3(110f, -16.5f), Quaternion.identity);
            StartCoroutine(spawnObstacle(Random.Range(5f, 1f)));
        }
    }

    public IEnumerator spawnIceSkates(float respawn){
        yield return new WaitForSeconds(respawn);
        if (!p.gameOver){
            Instantiate(iceSkate, new Vector3(110, Random.Range(15f, -15f)), Quaternion.identity);
            StartCoroutine(spawnIceSkates(Random.Range(10f, 5f)));
        }
        
    }

    public IEnumerator spawnFlyingObstacles(float respawn){
        yield return new WaitForSeconds(respawn);
        if (!p.gameOver){
            Instantiate(flyingOb, new Vector3(110, Random.Range(20f, 5f)), Quaternion.identity);
            StartCoroutine(spawnFlyingObstacles(Random.Range(20f, 10f)));
        }
    }

    public IEnumerator spawnFish(float respawn){
        yield return new WaitForSeconds(respawn);
        if (!p.gameOver){
            Instantiate(fishOb, new Vector3(110, Random.Range(15f, -15f)), Quaternion.identity);
            StartCoroutine(spawnFlyingObstacles(Random.Range(15f, 5f)));
        }
    }

    public void LoadNextScene(string sceneToChange){
        StartCoroutine(ChangeScene(sceneToChange));
    }

    IEnumerator ChangeScene(string sceneToChange){
        blackScreen.SetTrigger("FadeOut");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneToChange);
    }

    public void StartFade(){
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut(){
        blackScreen.SetTrigger("FadeOut");

        yield return new WaitForSeconds(0.5f);

        blackScreen.SetTrigger("FadeOut");
    }
}
