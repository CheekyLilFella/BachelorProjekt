using UnityEngine;

public class Cloud : MonoBehaviour
{

    //public GameObject target; //the cloud object
    public GameObject nearestObject;
    public GameObject[] playerArray;

    public float lifeTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerArray = GameObject.FindGameObjectsWithTag("Feet");
        playerArray = MultiTags.FindGameObjectsWithMultiTag("Feet");

        nearestObject = playerArray[0];
        float distanceToNearest = Vector3.Distance(transform.position, nearestObject.transform.position);

        for (int i = 1; i < playerArray.Length; i++)
        {
            float distanceToCurrent = Vector3.Distance(transform.position, playerArray[i].transform.position);

            if (distanceToCurrent < distanceToNearest)
            {
                nearestObject = playerArray[i];
                distanceToNearest = distanceToCurrent;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = nearestObject.transform.position;

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
