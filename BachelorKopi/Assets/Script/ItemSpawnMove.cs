using UnityEngine;

public class ItemSpawnMove : MonoBehaviour
{

    Vector3 spawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPos = new Vector3(0,0,0);

        InvokeRepeating("ChangePosition", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangePosition()
    {
        transform.position = spawnPos;

        spawnPos = new Vector3 (Random.Range(-30,30), 1, Random.Range(-30,30));
    }
}
