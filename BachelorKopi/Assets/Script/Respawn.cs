using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Respawn : NetworkBehaviour
{

    public Transform respawnPoint;

    //public NetworkVariable<float> killLoss = new NetworkVariable<float>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public float KillLoss;

    public GameObject emptyPointPrefab;

    public Collider player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;

            other.gameObject.GetComponent<PointManager>().points -= KillLoss;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other;

            GameObject pointKill = Instantiate(emptyPointPrefab, other.gameObject.transform.position, emptyPointPrefab.transform.rotation);

            StartCoroutine(JustWaitAMoment());
            //other.transform.position = respawnPoint.position;
            //other.gameObject.GetComponent<PointManager>().points -= KillLoss;

        }
    }

    public IEnumerator JustWaitAMoment()
    {
        yield return new WaitForSeconds(1f);
        player.transform.position = respawnPoint.position;
    }
}
