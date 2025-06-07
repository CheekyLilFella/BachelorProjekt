using Unity.Netcode;
using UnityEngine;

public class WindWave : MonoBehaviour
{

    public float lifeTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameObject.HasTag("0WindWave"))
        {
            var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
            Physics.IgnoreCollision(client0.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (gameObject.HasTag("1WindWave"))
        {
            var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
            Physics.IgnoreCollision(client1.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (gameObject.HasTag("2WindWave"))
        {
            var client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
            Physics.IgnoreCollision(client2.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (gameObject.HasTag("3WindWave"))
        {
            var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
            Physics.IgnoreCollision(client3.GetComponent<Collider>(), GetComponent<Collider>());
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
        if (other.gameObject.CompareTag("Meteor"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Sword"))
        {
            Destroy(other.gameObject);
        }
    }
}
