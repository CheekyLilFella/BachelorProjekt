using UnityEngine;

public class Head : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MultiTagsHelperMethods.AddTag(gameObject, "Head");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
