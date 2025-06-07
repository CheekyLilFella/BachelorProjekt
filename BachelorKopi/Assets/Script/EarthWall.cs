using Unity.Netcode;
using UnityEngine;

public class EarthWall : NetworkBehaviour
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WindWave"))
        {
            Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
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
