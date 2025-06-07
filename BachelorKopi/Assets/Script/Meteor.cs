using Unity.Netcode;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public float lifeTime;
    public float damage;
    public float pointLoss;
    public float pointGain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<Items>().shieldActive == false)
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            other.gameObject.GetComponent<PointManager>().points -= pointLoss;

            if (gameObject.HasTag("0Meteor"))
            {
                var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
                client0.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("1Meteor"))
            {
                var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
                client1.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("2Metetor"))
            {
                var client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
                client2.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("3Meteor"))
            {
                var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
                client3.GetComponent<PointManager>().points += pointGain;
            }
        }
    }

   /* private void OnTriggerEnter(Collider other)
    {
        
    }*/
}
