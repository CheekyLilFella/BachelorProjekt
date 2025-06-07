using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUII : MonoBehaviour
{

    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    //public GameObject itemBoxPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        serverButton.onClick.AddListener(() => 
        {
            NetworkManager.Singleton.StartServer();
            //SpawnItemBox();
        });

        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            //SpawnItemBox();
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            //SpawnItemBox();
        });
    }

    /*private void SpawnItemBox()
    {
        GameObject itemBoxGameObject = Instantiate(itemBoxPrefab);
        itemBoxGameObject.GetComponent<NetworkObject>().Spawn(true);

        //Instantiate(itemBoxPrefab, transform.position, Quaternion.identity);
    }*/

}
