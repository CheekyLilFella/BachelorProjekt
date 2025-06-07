using Unity.Netcode;
using UnityEngine;

public class Sword : NetworkBehaviour
{

    public float lifeTime;
    public float damage;
    public float pointLoss;
    public float pointGain;

    public NetworkObject client;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MultiTagsHelperMethods.AddTag(gameObject, OwnerClientId + "Slash");

        if (gameObject.HasTag("0Slash"))
        {
            client = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
        }

        if (gameObject.HasTag("1Slash"))
        {
            client = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
        }

        if (gameObject.HasTag("2Slash"))
        {
            client = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
        }

        if (gameObject.HasTag("3Slash"))
        {
            client = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject != client.gameObject)
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            other.gameObject.GetComponent<PointManager>().points -= pointLoss;

            if (gameObject.HasTag("0Slash"))
            {
                client.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("1Slash"))
            {
                client.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("2Slash"))
            {
                client.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("3Slash"))
            {
                client.GetComponent<PointManager>().points += pointGain;
            }
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Sword"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Snowball"))
        {
            Destroy(gameObject);
        }
    }
}
