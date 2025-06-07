using Unity.Netcode;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Unity.Netcode;

public class Lightning : NetworkBehaviour
{

    public float lifeTime;
    public float damage;
    public float pointLoss;
    public float pointGain;

    public GameObject lightningPrefab;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            other.gameObject.GetComponent<PointManager>().points -= pointLoss;

            if (gameObject.HasTag("0Lightning"))
            {
                var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
                client0.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("1Lightning"))
            {
                var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
                client1.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("2Lightning"))
            {
                var client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
                client2.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("3Lightning"))
            {
                var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
                client3.GetComponent<PointManager>().points += pointGain;
            }

        }

        if (other.gameObject.CompareTag("EarthWall"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Homing"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("IceAttack"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            if (gameObject.HasTag("0Lightning"))
            {
                var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;

                GameObject lightning = Instantiate(lightningPrefab, client0.transform.position, Quaternion.identity);
                MultiTagsHelperMethods.AddTag(lightning, OwnerClientId + "Lightning");
            }

            if (gameObject.HasTag("1Lightning"))
            {
                var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;

                GameObject lightning = Instantiate(lightningPrefab, client1.transform.position, Quaternion.identity);
                MultiTagsHelperMethods.AddTag(lightning, OwnerClientId + "Lightning");
            }

            if (gameObject.HasTag("2Lightning"))
            {
                var client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;

                GameObject lightning = Instantiate(lightningPrefab, client2.transform.position, Quaternion.identity);
                MultiTagsHelperMethods.AddTag(lightning, OwnerClientId + "Lightning");
            }

            if (gameObject.HasTag("3Lightning"))
            {
                var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;

                GameObject lightning = Instantiate(lightningPrefab, client3.transform.position, Quaternion.identity);
                MultiTagsHelperMethods.AddTag(lightning, OwnerClientId + "Lightning");
            }
        }

        if (other.gameObject.CompareTag("IceWave"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("WaterWave"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Tsunami"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Fireball"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("WindWave"))
        {
            Destroy(gameObject);
        }
    }
}
