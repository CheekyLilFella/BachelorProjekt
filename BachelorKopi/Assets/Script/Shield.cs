using Unity.Netcode;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public float lifeTime;

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

        if (gameObject.HasTag("0Shield"))
        {
            var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
            gameObject.transform.position = client0.transform.position;
        }

        if (gameObject.HasTag("1Shield"))
        {
            var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
            gameObject.transform.position = client1.transform.position;
        }

        if (gameObject.HasTag("2Shield"))
        {
            var client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
            gameObject.transform.position = client2.transform.position;
        }

        if (gameObject.HasTag("3Shield"))
        {
            var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
            gameObject.transform.position = client3.transform.position;
        }
    }
}
