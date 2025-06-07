using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RankManager : NetworkBehaviour
{
    public NetworkVariable<bool> client0Active = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> client1Active = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> client2Active = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> client3Active = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> timerStart = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public List<float> rankPointList = new List<float>();

    public NetworkVariable<float> client0Points = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> client1Points = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> client2Points = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> client3Points = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    //public NetworkList<float> rankPointList = new NetworkList<float>(new List<float>(), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //rankPointList = new NetworkList<float>(new List<float>(), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    }

    void Start()
    {
        client0Active.Value = false;
        client1Active.Value = false;
        client2Active.Value = false;
        client3Active.Value = false;
        timerStart.Value = false;
    }

    // Update is called once per frame
    void Update()
    {
        rankPointList.Sort();
        rankPointList.Reverse();
        //ListSortRPC();
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void RankListRPC()
    {
        rankPointList.Add(NetworkManager.Singleton.ConnectedClients[0].PlayerObject.GetComponent<PointManager>().points);
        rankPointList.Add(NetworkManager.Singleton.ConnectedClients[1].PlayerObject.GetComponent<PointManager>().points);

        rankPointList.RemoveRange(2, 2);
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void ListSortRPC()
    {
        rankPointList.Sort();
        rankPointList.Reverse();
    }

}
