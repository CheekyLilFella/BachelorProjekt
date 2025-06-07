using UnityEngine;
using TMPro;
using Unity.Mathematics;
using Unity.Netcode;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    //[SerializeField] float remainingTime;
    public NetworkVariable<float> remainingTime = new NetworkVariable<float>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(remainingTime.Value / 60);
        int seconds = Mathf.FloorToInt(remainingTime.Value % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime.Value > 0)
        {
            remainingTime.Value -= Time.deltaTime;
        }
        else if (remainingTime.Value < 0)
        {
            remainingTime.Value = 0;

            timerText.color = Color.red;
        }
    }
}
