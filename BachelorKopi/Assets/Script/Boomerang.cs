using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;

public class Boomerang : MonoBehaviour
{

    public float timer;
    public float boomerangSpeed;
    public float damage;
    public float pointLoss;
    public float pointGain;
    public float lifeTime;

    public NetworkObject client;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameObject.HasTag("0Boomerang"))
        {
            client = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
        }

        if (gameObject.HasTag("1Boomerang"))
        {
            client = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
        }

        if (gameObject.HasTag("2Boomerang"))
        {
            client = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
        }

        if (gameObject.HasTag("3Boomerang"))
        {
            client = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 720 * Time.deltaTime, 0, Space.World);

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            StartCoroutine(BoomerangReturn());
        }
    }

    public IEnumerator BoomerangReturn()
    {
        while (Vector3.Distance(client.transform.position, gameObject.transform.position) > 0.3f)
        {

            gameObject.transform.position += (client.transform.position - gameObject.transform.position).normalized * boomerangSpeed * Time.deltaTime;
            yield return null;
        }
        if (Vector3.Distance(client.transform.position, gameObject.transform.position) < 0.3f)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject != client.gameObject)
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            other.gameObject.GetComponent<PointManager>().points -= pointLoss;

            if (gameObject.HasTag("0Boomerang"))
            {
                client.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("1Boomerang"))
            {
                client.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("2Boomerang"))
            {
                client.GetComponent<PointManager>().points += pointGain;
            }

            if (gameObject.HasTag("3Boomerang"))
            {
                client.GetComponent<PointManager>().points += pointGain;
            }
        }

        if (other.gameObject.CompareTag("EarthWall"))
        {
            timer = 0;
        }

        if (other.gameObject.CompareTag("Homing"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("IceAttack"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Meteor"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            timer = 0;
            boomerangSpeed = 1;
        }

        if (other.gameObject.CompareTag("IceWave"))
        {
            timer = 0;
        }

        if (other.gameObject.CompareTag("WaterWave"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Tsunami"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Fireball"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("WindWave"))
        {
            timer = 0;
            boomerangSpeed = 1;
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
