using Unity.Netcode;
using UnityEngine;

public class Fireball : NetworkBehaviour
{

    public float damage;
    public float pointLoss;
    public float pointGain;
    public float lifeTime;
    public float stopForce;
    public float reverseFireballSpeed;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0 )
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

            if (gameObject.HasTag("0Fireball"))
            {
                var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
                client0.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("1Fireball"))
            {
                var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
                client1.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("2Fireball"))
            {
                var client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
                client2.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("3Fireball"))
            {
                var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
                client3.GetComponent<PointManager>().points += pointGain;
            }

            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Fireball"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("EarthWall"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Homing"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("WindWave"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.position * reverseFireballSpeed, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("WaterWave"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("IceAttack"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("IceWave"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Tsunami"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.position * reverseFireballSpeed, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("Meteor"))
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
