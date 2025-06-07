using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Collections;
using Unity.Netcode;

public class GrowVine : MonoBehaviour
{

    public List<MeshRenderer> growVinesMeshes;

    public float timeToGrow = 5;
    public float refreshRate = 0.05f;
    [UnityEngine.Range(0, 1)]
    public float minGrow = 0.2f;
    [UnityEngine.Range(0, 1)]
    public float maxGrow = 0.97f;
    public float lifeTime;

    private List<Material> growVinesMaterials = new List<Material>();

    private bool fullyGrown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GrowPlayerPos());

        for (int i = 0; i < growVinesMeshes.Count; i++)
        {
            for (int j = 0; j < growVinesMeshes[i].materials.Length; j++)
            {
                if (growVinesMeshes[i].materials[j].HasProperty("Grow_"))
                {
                    growVinesMeshes[i].materials[j].SetFloat("Grow_", minGrow);
                    growVinesMaterials.Add(growVinesMeshes[i].materials[j]);
                }
            }
        }

        StartCoroutine(GrowStartStop());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            for ( int i = 0;i < growVinesMaterials.Count;i++)
            {
                StartCoroutine(GrowVines(growVinesMaterials[i]));
            }
        }*/

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GrowVines(Material mat)
    {
        float growValue = mat.GetFloat("Grow_");

        if (!fullyGrown)
        {
            while (growValue < maxGrow)
            {
                growValue += 1 / (timeToGrow / refreshRate);
                mat.SetFloat("Grow_", growValue);

                yield return new WaitForSeconds (refreshRate);
            }
        }
        else
        {
            while (growValue > minGrow)
            {
                growValue -= 1 / (timeToGrow / refreshRate);
                mat.SetFloat("Grow_", growValue);

                yield return new WaitForSeconds(refreshRate);
            }
        }

        if (growValue >= maxGrow)
        {
            fullyGrown = true;
        }
        else
        {
            fullyGrown = false;
        }
    }

    IEnumerator GrowStartStop()
    {
        for (int i = 0; i < growVinesMaterials.Count; i++)
        {
            StartCoroutine(GrowVines(growVinesMaterials[i]));
        }

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < growVinesMaterials.Count; i++)
        {
            StartCoroutine(GrowVines(growVinesMaterials[i]));
        }
    }

    IEnumerator GrowPlayerPos()
    {
        if (gameObject.HasTag("0Plant"))
        {
            var client = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
            transform.position = client.transform.position;
            client.GetComponent<PlayerMovement>().runSpeed = 0f;

            yield return new WaitForSeconds(4f);
            client.GetComponent<PlayerMovement>().runSpeed = 7f;
        }

        if (gameObject.HasTag("1Plant"))
        {
            var client = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
            transform.position = client.transform.position;
            client.GetComponent<PlayerMovement>().runSpeed = 0f;

            yield return new WaitForSeconds(4f);
            client.GetComponent<PlayerMovement>().runSpeed = 7f;
        }

        if (gameObject.HasTag("2Plant"))
        {
            var client = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
            transform.position = client.transform.position;
            client.GetComponent<PlayerMovement>().runSpeed = 0f;

            yield return new WaitForSeconds(4f);
            client.GetComponent<PlayerMovement>().runSpeed = 7f;
        }

        if (gameObject.HasTag("3Plant"))
        {
            var client = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
            transform.position = client.transform.position;
            client.GetComponent<PlayerMovement>().runSpeed = 0f;

            yield return new WaitForSeconds(4f);
            client.GetComponent<PlayerMovement>().runSpeed = 7f;
        }
    }
}
