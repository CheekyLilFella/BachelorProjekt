//using System.Drawing;
using Unity.Netcode;
using UnityEngine;

public class WinLose : NetworkBehaviour
{

    private Color color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*int spawnPointX = Random.Range(-30, 30);
        int spawnPointY = 1;
        int spawnPointZ = Random.Range(-30, 30);

        Vector3 newPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        gameObject.transform.position = newPosition;*/

        /*if (gameObject.HasTag("0Trophy"))
        {
            color = GetComponent<Renderer>().material.color = Color.red;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.HasTag("0Trophy"))
        {
            color = GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.HasTag("0Client"))
            {
                var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
                //client0.GetComponent<PointManager>().winScreen.enabled = true;
                client0.GetComponent<PointManager>().joystick.enabled = false;
                client0.GetComponent<PlayerMovement>().runSpeed = 0f;
                client0.GetComponent<PointManager>().itemButton.enabled = false;

                /*var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
                client1.GetComponent<PointManager>().loseScreen.enabled = true;
                client1.GetComponent<PointManager>().joystick.enabled = false;
                client1.GetComponent<PointManager>().itemButton.enabled = false;*/
                
                Destroy(gameObject);
            }
        }
        
    }
}
