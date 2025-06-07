using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Collections;

public class PlayerHealth : NetworkBehaviour
{

    public float health;
    public float maxHealth;
    public float killLoss;

    private Transform respawnPoint;

    private Image healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var healthBarGameObject = GameObject.FindGameObjectWithTag("Health");
        healthBar = healthBarGameObject.GetComponent<Image>();
        
        maxHealth = health;

        var respawnGameObject = GameObject.FindGameObjectWithTag("Respawn");
        respawnPoint = respawnGameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health <= 0)
        {
            transform.position = respawnPoint.position;
            health = maxHealth;

            if (OwnerClientId == 0)
            {
                NetworkManager.Singleton.ConnectedClients[0].PlayerObject.GetComponent<PointManager>().points -= killLoss;
            }

            if (OwnerClientId == 1)
            {
                NetworkManager.Singleton.ConnectedClients[1].PlayerObject.GetComponent<PointManager>().points -= killLoss;
            }

            if (OwnerClientId == 2)
            {
                NetworkManager.Singleton.ConnectedClients[2].PlayerObject.GetComponent<PointManager>().points -= killLoss;
            }

            if (OwnerClientId == 3)
            {
                NetworkManager.Singleton.ConnectedClients[3].PlayerObject.GetComponent<PointManager>().points -= killLoss;
            }
            //StartCoroutine(waitKill());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Outside"))
        {
            health = maxHealth;
        }
    }

    public IEnumerator waitKill()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<PointManager>().points -= killLoss;
    }
}
