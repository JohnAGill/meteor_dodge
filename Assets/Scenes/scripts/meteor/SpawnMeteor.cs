using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject meteor;
    public GameObject meteor2;

    public float spawnTime = 2f;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNewMeteor", spawnTime, spawnTime);
    }
    void SpawnNewMeteor()
    {
        if (int.Parse(score.text) > 30) {
            var x2 = Random.Range(-2.3f, 2.3f);
            var newMeteor2 = Instantiate(meteor2, new Vector3(x2, 6f, 0f), Quaternion.identity);
        }
        var x = Random.Range(-2.3f, 2.3f);
        var newMeteor = Instantiate(meteor, new Vector3(x, 6f, 0f), Quaternion.identity);
    }    
}
