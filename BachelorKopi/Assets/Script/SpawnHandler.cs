using Coherence.Connection;
using Coherence.Toolkit;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;

    private CoherenceBridge _coherenceBridge;
    private GameObject _playerReference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _coherenceBridge = FindFirstObjectByType<CoherenceBridge>();
        _coherenceBridge.onConnected.AddListener(OnConnected);
        _coherenceBridge.onDisconnected.AddListener(OnDisconnected);
    }

    private void OnConnected(CoherenceBridge arg0)
    {
        _playerReference = Instantiate(playerPrefab, transform.position, transform.rotation);
    }

    private void OnDisconnected(CoherenceBridge arg0, ConnectionCloseReason arg1)
    {
        Destroy(_playerReference);
    }
}
