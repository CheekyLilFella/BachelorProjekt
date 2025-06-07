using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class PlayerMovement : NetworkBehaviour
{

    public Joystick joystick;
    //private Camera camera;

    public float runSpeed = 0f;
    public float pushPower;

    float horizontalMove = 0f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        var joystickGameObject = GameObject.FindGameObjectWithTag("Joystick");
        joystick = joystickGameObject.GetComponent<Joystick>();

       /* var cameraGameObject = GameObject.FindGameObjectWithTag("MainCamera");
        camera = cameraGameObject.GetComponent<Camera>();*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        rb.linearVelocity = new Vector3(joystick.Horizontal * runSpeed, rb.linearVelocity.y, joystick.Vertical * runSpeed);

        horizontalMove = joystick.Horizontal * runSpeed;

        Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("IceAttack"))
        {
            StartCoroutine(PlayerFreeze());
        }

       /* if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<Items>().snowActive == true)
        {
            Vector3 dir = other.transform.position - transform.position;
            dir.Normalize();

            gameObject.GetComponent<Rigidbody>().AddForce(dir * pushPower, ForceMode.Impulse);
            //gameObject.GetComponent<Rigidbody>().AddForce(-transform.position * reverseFireballSpeed, ForceMode.Impulse);
        }*/
    }

    public IEnumerator PlayerFreeze()
    {
        joystick.enabled = false;
        yield return new WaitForSeconds(1.5f);
        joystick.enabled = true;
    }
}
