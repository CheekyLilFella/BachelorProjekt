using Unity.Netcode;
using UnityEngine;
using UnityEngine.VFX;

public class Homing : NetworkBehaviour
{

    public float damage;
    public float pointLoss;
    public float pointGain;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;

            other.gameObject.GetComponent<PointManager>().points -= pointLoss;

            if (gameObject.HasTag("0Homing"))
            {
                var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
                client0.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("1Homing"))
            {
                var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
                client1.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("2Homing"))
            {
                var client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
                client2.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("3Homing"))
            {
                var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
                client3.GetComponent<PointManager>().points += pointGain;
            }

            //Destroy(gameObject);
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("Homing"))
        {
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("EarthWall"))
        {
            Destroy(other.gameObject);
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("WindWave"))
        {
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("WaterWave"))
        {
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("IceAttack"))
        {
            Destroy(other.gameObject);
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("IceWave"))
        {
            Destroy(other.gameObject);
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("Tsunami"))
        {
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("Meteor"))
        {
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("Sword"))
        {
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (other.gameObject.CompareTag("Snowball"))
        {
            gameObject.GetComponent<VisualEffect>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
