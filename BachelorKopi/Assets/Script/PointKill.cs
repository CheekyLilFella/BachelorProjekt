using Unity.Netcode;
using UnityEngine;

public class PointKill : NetworkBehaviour
{

    public float pointLoss;
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
            other.gameObject.GetComponent<PointManager>().points -= pointLoss;

            Destroy(gameObject);
        }
    }
}
