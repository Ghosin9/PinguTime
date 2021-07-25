using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPingu : MonoBehaviour
{
    public GameObject onScreen;
    public GameObject offScreen;
    public Player p;

    public float Speed;
    private float step;
    public bool walking;

    public int cycle = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        step = Speed * Time.deltaTime;
        if (walking){
            if (cycle == 1){
                transform.position = Vector3.MoveTowards(transform.position, onScreen.transform.position, step);

                if (Vector3.Distance(transform.position, onScreen.transform.position) < 0.001f){
                    walking = false;

                    StartCoroutine(doneWalking());
                }
            } else if (cycle == 2){
                transform.position = Vector3.MoveTowards(transform.position, offScreen.transform.position, step);

                if (Vector3.Distance(transform.position, offScreen.transform.position) < 0.001f){
                    walking = false;

                    StartCoroutine(doneWalking());
                }
            }
        }
    }

    public IEnumerator doneWalking(){
        
        if(cycle == 1){
            yield return new WaitForSeconds(10f);
            if (!p.gameOver){
                cycle++;
                walking = true;
            }
        } else if (cycle == 2 && !p.gameOver){
            cycle = 1;
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
