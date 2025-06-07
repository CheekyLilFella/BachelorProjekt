using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class TidalWave : MonoBehaviour
{

    public float lifeTime;
    public float reverseWaterSpeed;
    public float pushPower;

    public GameObject iceWavePrefab;

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
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.position * reverseWaterSpeed, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("WaterWave"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("IceAttack"))
        {
            Destroy(other.gameObject);
            GameObject iceWave = Instantiate(iceWavePrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.position * reverseWaterSpeed, ForceMode.Impulse);
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
