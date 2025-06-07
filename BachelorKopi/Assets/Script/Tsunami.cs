using UnityEngine;

public class Tsunami : MonoBehaviour
{

    public float lifeTime;
    public float reverseTsunamiSpeed;

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
        if (other.gameObject.CompareTag("Shield"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.position * reverseTsunamiSpeed, ForceMode.Impulse);
        }
    }
}
