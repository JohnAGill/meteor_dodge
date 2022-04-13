using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Functions;
using Firebase.Firestore;
[FirestoreData]
public class SavedScore
{
        [FirestoreProperty]
        public int score { get; set; }

        [FirestoreProperty]
        public string userid { get; set; }

        [FirestoreProperty]
        public string entryid { get; set; }
}

public class Collision : MonoBehaviour
{ 
    protected FirebaseFirestore db;
    protected FirebaseFunctions functions;
    public GameObject test;
    public GameObject EndPanel;
    public Text EndPanelScoreText;
    public Text ScoreText;
    private bool arcade = true;
    private float spawnTime = 5f;

    private void Start() {
        db = FirebaseFirestore.DefaultInstance;
        Time.timeScale = 0;
        DocumentReference docRef = db.Collection("savedScores").Document("user id here"); // this creates a reference to the 'document' in the firestore
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            //we get the document if it exsists & send that score to stakester
            DocumentSnapshot snapshot = task.Result;
            if(snapshot.Exists) {
                Debug.Log("Document data for {0} document: " + snapshot.Id);
                SavedScore savedScore = snapshot.ConvertTo<SavedScore>();
                 functions = FirebaseFunctions.DefaultInstance;
            var data = new Dictionary<string, object>();
            data["username"] = savedScore.userid;
            data["score"] = savedScore.score;
            data["game_id"] = savedScore.entryid;
            var func = functions.GetHttpsCallable("sendScoreToStakester"); // replace this with your current call to the stakester partner api
            func.CallAsync(data).ContinueWithOnMainThread((callTask) => {
                if (callTask.IsFaulted) {
                    Debug.Log("FAILED!");
                }
            });

            }
            docRef.DeleteAsync(); // this removes the record after a successful score submission
            Time.timeScale = 1;
        }
        );

        InvokeRepeating("SaveScore", spawnTime, spawnTime);
    }

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
            DocumentReference docRef = db.Collection("savedScores").Document("test2");
            docRef.DeleteAsync();
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

    void SaveScore() {
        Debug.Log("Save Score");
        DocumentReference docRef = db.Collection("savedScores").Document("user id here");
      
        SavedScore savedScore = new SavedScore
        {
            score = (int.Parse(ScoreText.text) + 1),
            userid = "user id here",
            // entryid = "test2",
        };
    // here we update the document in the db with the users most recent score
        docRef.SetAsync(savedScore).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the LA document in the cities collection.");
        });
    }

}
