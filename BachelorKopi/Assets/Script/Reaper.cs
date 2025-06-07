using Unity.Netcode;
using UnityEngine;

public class Reaper : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.HasTag("0Client"))
        {
            if (gameObject.HasTag("0ReaperWin"))
            {
                var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
                client0.GetComponent<PointManager>().winScreen.enabled = true;
                client0.GetComponent<PlayerMovement>().joystick.enabled = false;
                client0.GetComponent<PlayerMovement>().runSpeed = 0f;
                client0.GetComponent<Items>().button.enabled = false;
            }

            if (gameObject.HasTag("0Reaper"))
            {
                var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
                client0.GetComponent<PointManager>().loseScreen.enabled = true;
                client0.GetComponent<PlayerMovement>().joystick.enabled = false;
                client0.GetComponent<PlayerMovement>().runSpeed = 0f;
                client0.GetComponent<Items>().button.enabled = false;
            }
        }

        if (other.gameObject.HasTag("1Client"))
        {
            if (gameObject.HasTag("1ReaperWin"))
            {
                var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
                client1.GetComponent<PointManager>().winScreen.enabled = true;
                client1.GetComponent<PlayerMovement>().joystick.enabled = false;
                client1.GetComponent<PlayerMovement>().runSpeed = 0f;
                client1.GetComponent<Items>().button.enabled = false;
            }

            if (gameObject.HasTag("1Reaper"))
            {
                var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
                client1.GetComponent<PointManager>().loseScreen.enabled = true;
                client1.GetComponent<PlayerMovement>().joystick.enabled = false;
                client1.GetComponent<PlayerMovement>().runSpeed = 0f;
                client1.GetComponent<Items>().button.enabled = false;
            }
        }

        if (other.gameObject.HasTag("2Client"))
        {
            if (gameObject.HasTag("2ReaperWin"))
            {
                var client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
                client2.GetComponent<PointManager>().winScreen.enabled = true;
                client2.GetComponent<PlayerMovement>().joystick.enabled = false;
                client2.GetComponent<PlayerMovement>().runSpeed = 0f;
                client2.GetComponent<Items>().button.enabled = false;
            }

            if (gameObject.HasTag("2Reaper"))
            {
                var client1 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
                client1.GetComponent<PointManager>().loseScreen.enabled = true;
                client1.GetComponent<PlayerMovement>().joystick.enabled = false;
                client1.GetComponent<PlayerMovement>().runSpeed = 0f;
                client1.GetComponent<Items>().button.enabled = false;
            }
        }

        if (other.gameObject.HasTag("3Client"))
        {
            if (gameObject.HasTag("3ReaperWin"))
            {
                var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
                client3.GetComponent<PointManager>().winScreen.enabled = true;
                client3.GetComponent<PlayerMovement>().joystick.enabled = false;
                client3.GetComponent<PlayerMovement>().runSpeed = 0f;
                client3.GetComponent<Items>().button.enabled = false;
            }

            if (gameObject.HasTag("3Reaper"))
            {
                var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
                client3.GetComponent<PointManager>().loseScreen.enabled = true;
                client3.GetComponent<PlayerMovement>().joystick.enabled = false;
                client3.GetComponent<PlayerMovement>().runSpeed = 0f;
                client3.GetComponent<Items>().button.enabled = false;
            }
        }
    }
}
