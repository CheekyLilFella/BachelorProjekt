using UnityEngine;

public class IceWave : MonoBehaviour
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
        if (other.gameObject.CompareTag("IceAttack"))
        {
            Destroy(other.gameObject);

            lifeTime = 5;
        }

        if (other.gameObject.CompareTag("WindWave"))
        {
            Destroy(gameObject);
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
