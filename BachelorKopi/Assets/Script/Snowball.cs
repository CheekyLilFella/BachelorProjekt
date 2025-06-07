using UnityEngine;

public class Snowball : MonoBehaviour
{

    public float lifeTime;
    public float snowPush;

    public GameObject nearestObject;
    public GameObject[] playerArray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerArray = MultiTags.FindGameObjectsWithMultiTag("Head");

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<Items>().snowActive == false)
        {
            if (other.GetComponent<Items>().shieldActive == false)
            {
                Vector3 dir = other.transform.position - transform.position;
                dir.Normalize();

                other.GetComponent<Rigidbody>().AddForce(dir * snowPush, ForceMode.Impulse);

                //Vector3 direction = other.transform.position - transform.position;
                //other.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized, transform.position * snowPush);
            } 
        }
    }
}
