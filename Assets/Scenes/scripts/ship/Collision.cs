using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Functions;

public class Collision : MonoBehaviour
{ 
    protected FirebaseFunctions functions;
    public GameObject test;
    public GameObject EndPanel;
    public Text EndPanelScoreText;
    public Text ScoreText;
    private bool arcade = true;

    IEnumerator ExampleCoroutine()
    {
        
        Debug.Log(gameObject.transform.position.x);
        Instantiate(test, gameObject.transform.position, Quaternion.identity);
        EndPanel.SetActive(true);
        EndPanelScoreText.text = "Your score was: " + (int.Parse(ScoreText.text) + 1);
        if(arcade) {
            functions = FirebaseFunctions.DefaultInstance;
            var data = new Dictionary<string, object>();
            data["username"] = "test1";
            data["score"] = (int.Parse(ScoreText.text) + 1);
            data["game_id"] = "test2";
            var func = functions.GetHttpsCallable("sendScoreToStakester");
            var task = func.CallAsync(data).ContinueWithOnMainThread((callTask) => {
                if (callTask.IsFaulted) {
                    Debug.Log("FAILED!");
                }
            });
        }
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 0;
        Destroy(gameObject);
        Debug.Log("OnCollisionEnter2D");
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Meteor") {
            StartCoroutine(ExampleCoroutine());
        }
       
    }
}
