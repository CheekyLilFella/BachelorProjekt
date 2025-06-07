using UnityEngine;
using Unity.Netcode;
using System.Globalization;
using UnityEngine.SceneManagement;

public class FollowPlayer : NetworkBehaviour
{
    public Camera camera;

    public Transform player;

    public Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var cameraGameObject = GameObject.FindGameObjectWithTag("Camera");
        camera = cameraGameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        camera.transform.position = transform.position + offset;
    }
}
