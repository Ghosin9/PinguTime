using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{

    public Animator blackScreen;

    public void LoadNextScene(string sceneToChange){
        StartCoroutine(ChangeScene(sceneToChange));
    }

    IEnumerator ChangeScene(string sceneToChange){
        blackScreen.SetTrigger("FadeOut");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneToChange);
    }
}
