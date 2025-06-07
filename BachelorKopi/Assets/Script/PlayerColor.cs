using UnityEngine;
using Unity.Netcode;

public class PlayerColor : NetworkBehaviour
{

     private Color color;
   //private NetworkVariable<Color> color = new NetworkVariable<Color>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerID();

        PlayerColorMethod();

        /*if (IsOwner && IsHost)
        {
            HostColorRpc();
        }

        else if (IsOwner && IsClient)
        {
            ClientColorRpc();
        }*/

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Rpc(SendTo.ClientsAndHost)]
    void HostColorRpc()
    {
        color = GetComponent<Renderer>().material.color = Color.red;
    }

    [Rpc(SendTo.ClientsAndHost)]
    void ClientColorRpc()
    {
        color = GetComponent<Renderer>().material.color = Color.blue;
        //color = GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    public void PlayerID()
    {
        if (NetworkObject.OwnerClientId == 0)
        {
            MultiTagsHelperMethods.AddTag(gameObject, "0Client");
        }

        if (NetworkObject.OwnerClientId == 1)
        {
            MultiTagsHelperMethods.AddTag(gameObject, "1Client");
        }

        if (NetworkObject.OwnerClientId == 2)
        {
            MultiTagsHelperMethods.AddTag(gameObject, "2Client");
        }

        if (NetworkObject.OwnerClientId == 3)
        {
            MultiTagsHelperMethods.AddTag(gameObject, "3Client");
        }
    }

    public void PlayerColorMethod()
    {
        if (NetworkObject.OwnerClientId == 0)
        {
            color = GetComponent<Renderer>().material.color = Color.red;
        }

        if (NetworkObject.OwnerClientId == 1)
        {
            color = GetComponent<Renderer>().material.color = Color.blue;
        }

        if (NetworkObject.OwnerClientId == 2)
        {
            color = GetComponent<Renderer>().material.color = Color.green;
        }

        if (NetworkObject.OwnerClientId == 3)
        {
            color = GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
}
