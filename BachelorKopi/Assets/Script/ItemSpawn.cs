using UnityEngine;
using Unity.Netcode;
using Unity.BossRoom.Infrastructure;
using System.Collections;

public class ItemSpawn : NetworkBehaviour
{

    [SerializeField] private GameObject prefab;

    private const int MaxPrefabCount = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += SpawnItemBoxStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnItemBoxStart()
    {
        NetworkManager.Singleton.OnServerStarted -= SpawnItemBoxStart;
        NetworkObjectPool.Singleton.InitializePool();

        for (int i = 0; i < 5; i++)
        {
            SpawnItemBox();
        }

        StartCoroutine(SpawnOverTime());
    }

    private void SpawnItemBox()
    {
  
        NetworkObject obj = NetworkObjectPool.Singleton.GetNetworkObject(prefab, GetRandomPosOnMap(), Quaternion.identity);
        obj.GetComponent<ItemManager>().prefab = prefab;
        if (!obj.IsSpawned) obj.Spawn(destroyWithScene: true);
    }

    private Vector3 GetRandomPosOnMap()
    {
        return new Vector3(x: Random.Range(-30, 30), y: 1, z: Random.Range(-30, 30));
    }

    private IEnumerator SpawnOverTime()
    {
        while (NetworkManager.Singleton.ConnectedClients.Count > 0)
        {
            yield return new WaitForSeconds(5f);
            if (NetworkObjectPool.Singleton.GetCurrentPrefabCount(prefab) < MaxPrefabCount)
                SpawnItemBox();
        }
    }
}
