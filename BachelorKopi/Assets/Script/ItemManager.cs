using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.BossRoom.Infrastructure;

public class ItemManager : NetworkBehaviour
{

    public GameObject prefab;

    public Transform itemBoxPos;

    private Vector3 newPosition;

    public bool itemSpawnAvailable = false;

    public float waitToSpawn;

    private NetworkVariable<int> spawnPointX = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private NetworkVariable<int> spawnPointY = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private NetworkVariable<int> spawnPointZ = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPointX.Value = Random.Range(-30, 30);
        spawnPointY.Value = 1;
        spawnPointZ.Value = Random.Range(-30, 30);

        newPosition = new Vector3(spawnPointX.Value, spawnPointY.Value, spawnPointZ.Value);

        gameObject.transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(WaitOnMove());
        }
    }

    IEnumerator WaitOnMove ()
    {
        yield return new WaitForSeconds(waitToSpawn);

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
