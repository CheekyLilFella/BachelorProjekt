using System.Globalization;
using TMPro;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter.Xml;

public class PointManager : NetworkBehaviour
{
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI winScreen;
    public TextMeshProUGUI loseScreen;
    public TextMeshProUGUI timer;
    public Timer gameTime;

    public Joystick joystick;

    public Button itemButton;

    public float points;
    public float rankFloat;
    public NetworkVariable<float> client0Points = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> client1Points = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public List<float> playerPoints;
    public float maxPoints;
    public float reaperSpeed;

    public GameObject reaper;
    public GameObject player;
    public GameObject target;
    public GameObject rankManager;

    public bool client0Active = false;
    public bool client1Active = false;
    public bool client2Active = false;
    public bool client3Active = false;
    public bool points0 = false;
    public bool points1 = false;
    public bool timerStart = false;

    public NetworkObject client0;
    public NetworkObject client1;
    public NetworkObject client2;
    public NetworkObject client3;

    private string FirstPlaceText = "Player 0 ERROR";
    private string SecondPlaceText = "Player 1 ERROR";
    private string ThirdPlaceText = "Player 2 ERROR";
    private string FourthPlaceText = "Player 3 ERROR";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!IsOwner) return;

        var pointGameObject = GameObject.FindGameObjectWithTag("PointText");
        pointText = pointGameObject.GetComponent<TextMeshProUGUI>();

        var winGameObject = GameObject.FindGameObjectWithTag("WinScreen");
        winScreen = winGameObject.GetComponent<TextMeshProUGUI>();

        var loseGameObject = GameObject.FindGameObjectWithTag("LoseScreen");
        loseScreen = loseGameObject.GetComponent<TextMeshProUGUI>();

        var joystickGameOb = GameObject.FindGameObjectWithTag("Joystick");
        joystick = joystickGameOb.GetComponent<Joystick>();

        var itemButtonGameOb = GameObject.FindGameObjectWithTag("ItemBtn");
        itemButton = itemButtonGameOb.GetComponent<Button>();

        var timerGameOb = GameObject.FindGameObjectWithTag("Timer");
        timer = timerGameOb.GetComponent<TextMeshProUGUI>();
        gameTime = timerGameOb.GetComponent<Timer>();

        rankManager = GameObject.FindGameObjectWithTag("Rank");

        pointText.text = points.ToString() + " POINTS";


        SetClientAtiveRPC();


        //client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
        //client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
        //client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
        //client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!IsOwner) return;

        if (points < 0)
        {
            points = 0;
        }

        if (OwnerClientId == 0)
        {
            rankManager.GetComponent<RankManager>().client0Points.Value = points;
        }
        if (OwnerClientId == 1)
        {
            rankManager.GetComponent<RankManager>().client1Points.Value = points;
        }
        if (OwnerClientId == 2)
        {
            rankManager.GetComponent<RankManager>().client2Points.Value = points;
        }
        if (OwnerClientId == 3)
        {
            rankManager.GetComponent<RankManager>().client3Points.Value = points;
        }

        if (gameTime.remainingTime.Value == 0)
        {
            WinLoseStateRpc();
        }
        /*if (points >= maxPoints)
        {
            WinLoseStateRpc();
        }*/

        if (rankManager.GetComponent<RankManager>().client0Active.Value == true)
        {
            client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
            //client0Text = "Player " + client0.OwnerClientId + " POINTS " + client0.GetComponent<PointManager>().points;
            /*if (points0 == true)
            {
                playerPoints.Add(client0.GetComponent<PointManager>().points);
                points0 = false;
            }*/
        }
        if (rankManager.GetComponent<RankManager>().client1Active.Value == true)
        {
            client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
            //client1Text = "Player " + client1.OwnerClientId + " POINTS " + client1.GetComponent<PointManager>().points;
            /*if (points1 == true)
            {
                playerPoints.Add(client1.GetComponent<PointManager>().points);
                points1 = false;
            }*/
        }

        if (rankManager.GetComponent<RankManager>().client2Active.Value == true)
        {
            client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
            //client2Text = "Player " + client2.OwnerClientId + " POINTS " + client2.GetComponent<PointManager>().points;
        }
        if (rankManager.GetComponent<RankManager>().client3Active.Value == true)
        {
            client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;
            //client3Text = "Player " + client3.OwnerClientId + " POINTS " + client3.GetComponent<PointManager>().points;
        }

        //pointText.text = points.ToString() + " POINTS \n"
        /*if (rankManager.GetComponent<RankManager>().client1Active.Value == false)
        {
            pointText.text = points.ToString() + " POINTS \n"
            + FirstPlaceText + "\n"
            + SecondPlaceText + "\n"
            + ThirdPlaceText + "\n"
            + FourthPlaceText + "\n";
        }*/

        // RANK POINTS FOR 1 PLAYER GAMES
        /*if (rankManager.GetComponent<RankManager>().client0Active.Value == true)
        {
            RankSystem1();

            pointText.text = points.ToString() + " POINTS \n"
            + "\n"
            + "1st: " + FirstPlaceText + "\n"
            //+ "1st place: " + rankManager.GetComponent<RankManager>().rankPointList[0] + "\n"
            + "2nd: " + SecondPlaceText + "\n"
            //+ "2nd place: " + rankManager.GetComponent<RankManager>().rankPointList[1] + "\n"
            + "3rd: " + ThirdPlaceText + "\n"
            + "4th: " + FourthPlaceText + "\n";

            timer.enabled = true;
            gameTime.enabled = true;
        }*/

        // RANK POINTS FOR 2 PLAYER GAMES
        /*if (rankManager.GetComponent<RankManager>().client1Active.Value == true)
        {
        //client0Points.Value = client0.GetComponent<PointManager>().points;
        //client1Points.Value = client1.GetComponent<PointManager>().points;

        RankSystem2RPC();

        pointText.text = points.ToString() + " POINTS \n"
        + "\n"
        + "1st: " + FirstPlaceText + "\n"
        //+ "1st place: " + rankManager.GetComponent<RankManager>().rankPointList[0] + "\n"
        + "2nd: " + SecondPlaceText + "\n"
        //+ "2nd place: " + rankManager.GetComponent<RankManager>().rankPointList[1] + "\n"
        + "3rd: " + ThirdPlaceText + "\n"
        + "4th: " + FourthPlaceText + "\n";
        //PointUpdateRPC();

        timer.enabled = true;
        gameTime.enabled = true;
        }*/



        // RANK POINTS FOR 4 PLAYER GAMES
        if (rankManager.GetComponent<RankManager>().client3Active.Value == true)
        {
            RankSystem4RPC();

            pointText.text = points.ToString() + " POINTS \n"
            + "\n"
            + "1st: " + FirstPlaceText + "\n"
            //+ "1st place: " + rankManager.GetComponent<RankManager>().rankPointList[0] + "\n"
            + "2nd: " + SecondPlaceText + "\n"
            //+ "2nd place: " + rankManager.GetComponent<RankManager>().rankPointList[1] + "\n"
            + "3rd: " + ThirdPlaceText + "\n"
            + "4th: " + FourthPlaceText + "\n";

            timer.enabled = true;
            gameTime.enabled = true;

        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void SetClientAtiveRPC()
    {
        rankManager = GameObject.FindGameObjectWithTag("Rank");

        if (NetworkObject.OwnerClientId == 0)
        {
            rankManager.GetComponent<RankManager>().client0Active.Value = true;
            //rankManager.GetComponent<RankManager>().timerStart.Value = true;
            //rankManager.GetComponent<RankManager>().RankList();
            //points0 = true;
        }

        if (NetworkObject.OwnerClientId == 1)
        {
            rankManager.GetComponent<RankManager>().client1Active.Value = true;
            //rankManager.GetComponent<RankManager>().timerStart.Value = true;
            //rankManager.GetComponent<RankManager>().RankListRPC();
            //points1 = true;
        }

        if (NetworkObject.OwnerClientId == 2)
        {
            rankManager.GetComponent<RankManager>().client2Active.Value = true;
        }

        if (NetworkObject.OwnerClientId == 3)
        {
            rankManager.GetComponent<RankManager>().client3Active.Value = true;
           // rankManager.GetComponent<RankManager>().timerStart.Value = true;
            //rankManager.GetComponent<RankManager>().RankListRPC();
            //points1 = true;
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void PointUpdateRPC()
    {
        client0Points.Value = client0.GetComponent<PointManager>().points;
        client1Points.Value = client1.GetComponent<PointManager>().points;

        pointText.text = points.ToString() + " POINTS \n"
        + "\n"
        + "1st: " + FirstPlaceText + "\n"
        //+ "1st place: " + rankManager.GetComponent<RankManager>().rankPointList[0] + "\n"
        + "2nd: " + SecondPlaceText + "\n"
        //+ "2nd place: " + rankManager.GetComponent<RankManager>().rankPointList[1] + "\n"
        + "3rd: " + ThirdPlaceText + "\n"
        + "4th: " + FourthPlaceText + "\n";

    }

    [Rpc(SendTo.ClientsAndHost)]
    public void WinLoseStateRpc()
    {
        gameTime.remainingTime.Value = 600;
        //maxPoints = 100;

        if (client0.GetComponent<Items>().firstPlace == true)
        {
            GameObject reaper0 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper0, "0ReaperWin");
            StartCoroutine(Chase0(reaper0));

            GameObject reaper1 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper1, "1Reaper");
            StartCoroutine(Chase1(reaper1));

            GameObject reaper2 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper2, "2Reaper");
            StartCoroutine(Chase2(reaper2));

            GameObject reaper3 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper3, "3Reaper");
            StartCoroutine(Chase3(reaper3));

        }

        if (client1.GetComponent<Items>().firstPlace == true)
        {
            GameObject reaper0 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper0, "0Reaper");
            StartCoroutine(Chase0(reaper0));

            GameObject reaper1 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper1, "1ReaperWin");
            StartCoroutine(Chase1(reaper1));

            GameObject reaper2 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper2, "2Reaper");
            StartCoroutine(Chase2(reaper2));

            GameObject reaper3 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper3, "3Reaper");
            StartCoroutine(Chase3(reaper3));

        }

        if (client2.GetComponent<Items>().firstPlace == true)
        {
            GameObject reaper0 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper0, "0Reaper");
            StartCoroutine(Chase0(reaper0));

            GameObject reaper1 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper1, "1Reaper");
            StartCoroutine(Chase1(reaper1));

            GameObject reaper2 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper2, "2ReaperWin");
            StartCoroutine(Chase2(reaper2));

            GameObject reaper3 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper3, "3Reaper");
            StartCoroutine(Chase3(reaper3));

        }

        if (client3.GetComponent<Items>().firstPlace == true)
        {
            GameObject reaper0 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper0, "0Reaper");
            StartCoroutine(Chase0(reaper0));

            GameObject reaper1 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper1, "1Reaper");
            StartCoroutine(Chase1(reaper1));

            GameObject reaper2 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper2, "2Reaper");
            StartCoroutine(Chase2(reaper2));

            GameObject reaper3 = Instantiate(reaper);
            MultiTagsHelperMethods.AddTag(reaper3, "3ReaperWin");
            StartCoroutine(Chase3(reaper3));

        }
    }

    public IEnumerator Chase0(GameObject reaper0)
    {
        var target = MultiTags.FindWithMultiTag("0Client");

        while (Vector3.Distance(target.transform.position, reaper0.transform.position) > 0.3f)
        {

            reaper0.transform.position += (target.transform.position - reaper0.transform.position).normalized * reaperSpeed * Time.deltaTime;
            yield return null;
        }

    }

    public IEnumerator Chase1(GameObject reaper1)
    {
        var target = MultiTags.FindWithMultiTag("1Client");

        while (Vector3.Distance(target.transform.position, reaper1.transform.position) > 0.3f)
        {

            reaper1.transform.position += (target.transform.position - reaper1.transform.position).normalized * reaperSpeed * Time.deltaTime;
            yield return null;
        }

    }

    public IEnumerator Chase2(GameObject reaper2)
    {
        var target = MultiTags.FindWithMultiTag("2Client");

        while (Vector3.Distance(target.transform.position, reaper2.transform.position) > 0.3f)
        {

            reaper2.transform.position += (target.transform.position - reaper2.transform.position).normalized * reaperSpeed * Time.deltaTime;
            yield return null;
        }

    }

    public IEnumerator Chase3(GameObject reaper2)
    {
        var target = MultiTags.FindWithMultiTag("3Client");

        while (Vector3.Distance(target.transform.position, reaper2.transform.position) > 0.3f)
        {

            reaper2.transform.position += (target.transform.position - reaper2.transform.position).normalized * reaperSpeed * Time.deltaTime;
            yield return null;
        }

    }

    // RANK SYSTEM 1 PLAYER
    public void RankSystem1()
    {
        FirstPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
        client0.GetComponent<Items>().firstPlace = true;
        client0.GetComponent<Items>().secondPlace = false;
        client0.GetComponent<Items>().thirdPlace = false;
        client0.GetComponent<Items>().fourthPlace = false;
    }

    // RANK SYSTEM: 2 PLAYERS
    [Rpc(SendTo.ClientsAndHost)]
    public void RankSystem2RPC()
    {
        // PLAYER 0, FirstPlace
        if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client1Points.Value)
        {
            FirstPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
            client0.GetComponent<Items>().firstPlace = true;
            client0.GetComponent<Items>().secondPlace = false;
            client0.GetComponent<Items>().thirdPlace = false;
            client0.GetComponent<Items>().fourthPlace = false;

            // PLAYER 1, SecondPlace
            SecondPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
            client1.GetComponent<Items>().firstPlace = false;
            client1.GetComponent<Items>().secondPlace = true;
            client1.GetComponent<Items>().thirdPlace = false;
            client1.GetComponent<Items>().fourthPlace = false;

        }

        // PLAYER 1, FirstPlace
        if (rankManager.GetComponent<RankManager>().client1Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value)
        {
            FirstPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
            client1.GetComponent<Items>().firstPlace = true;
            client1.GetComponent<Items>().secondPlace = false;
            client1.GetComponent<Items>().thirdPlace = false;
            client1.GetComponent<Items>().fourthPlace = false;

            // Player 0, SecondPlace
            SecondPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
            client0.GetComponent<Items>().firstPlace = false;
            client0.GetComponent<Items>().secondPlace = true;
            client0.GetComponent<Items>().thirdPlace = false;
            client0.GetComponent<Items>().fourthPlace = false;

        }
    }


    // RANK SYSTEM: 4 PLAYERS
    [Rpc(SendTo.ClientsAndHost)]
    public void RankSystem4RPC()
    {
        // PLAYER 0, FirstPlace
        if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client1Points.Value
            && rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value
            && rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
        {
            FirstPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
            client0.GetComponent<Items>().firstPlace = true;
            client0.GetComponent<Items>().secondPlace = false;
            client0.GetComponent<Items>().thirdPlace = false;
            client0.GetComponent<Items>().fourthPlace = false;

            // PLAYER 1, SecondPlace
            if (rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value
                && rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
            {
                SecondPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                client1.GetComponent<Items>().firstPlace = false;
                client1.GetComponent<Items>().secondPlace = true;
                client1.GetComponent<Items>().thirdPlace = false;
                client1.GetComponent<Items>().fourthPlace = false;

                // PLAYER 2, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client2Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
                {
                    ThirdPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> Points";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = true;
                    client2.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 3, FourthPlace
                    FourthPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = false;
                    client3.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 3, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client2Points.Value)
                {
                    ThirdPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = true;
                    client3.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 2, FourthPlace
                    FourthPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = false;
                    client2.GetComponent<Items>().fourthPlace = true;
                }
            }
            // PLAYER 2, SecondPlace
            if (rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value
                && rankManager.GetComponent<RankManager>().client2Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
            {
                SecondPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                client2.GetComponent<Items>().firstPlace = false;
                client2.GetComponent<Items>().secondPlace = true;
                client2.GetComponent<Items>().thirdPlace = false;
                client2.GetComponent<Items>().fourthPlace = false;

                // PLAYER 1, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
                {
                    ThirdPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = true;
                    client1.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 3, FourthPlace
                    FourthPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = false;
                    client3.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 3, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value)
                {
                    ThirdPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = true;
                    client3.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 1, FourthPlace
                    FourthPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = false;
                    client1.GetComponent<Items>().fourthPlace = true;
                }
            }

            // PLAYER 3, SecondPlace
            if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value
                && rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client2Points.Value)
            {
                SecondPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                client3.GetComponent<Items>().firstPlace = false;
                client3.GetComponent<Items>().secondPlace = true;
                client3.GetComponent<Items>().thirdPlace = false;
                client3.GetComponent<Items>().fourthPlace = false;

                // PLAYER 1, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value)
                {
                    ThirdPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = true;
                    client1.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 2, FourthPlace
                    FourthPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = false;
                    client2.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 2, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value)
                {
                    ThirdPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = true;
                    client2.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 1, FourthPlace
                    FourthPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = false;
                    client1.GetComponent<Items>().fourthPlace = true;
                }
            } 
        }

        // PLAYER 1, FirstPlace
        if (rankManager.GetComponent<RankManager>().client1Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value
            && rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value
            && rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
        {
            FirstPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
            client1.GetComponent<Items>().firstPlace = true;
            client1.GetComponent<Items>().secondPlace = false;
            client1.GetComponent<Items>().thirdPlace = false;
            client1.GetComponent<Items>().fourthPlace = false;

            // Player 0, SecondPlace
            if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value
                && rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
            {
                SecondPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                client0.GetComponent<Items>().firstPlace = false;
                client0.GetComponent<Items>().secondPlace = true;
                client0.GetComponent<Items>().thirdPlace = false;
                client0.GetComponent<Items>().fourthPlace = false;

                // PLAYER 2, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client2Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
                {
                    ThirdPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = true;
                    client2.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 3, FourthPlace
                    FourthPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = false;
                    client3.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 3, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client2Points.Value)
                {
                    ThirdPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = true;
                    client3.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 2, FourthPlace
                    FourthPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = false;
                    client2.GetComponent<Items>().fourthPlace = true;
                }
            }

            // PLAYER 2, SecondPlace
            if (rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value
                && rankManager.GetComponent<RankManager>().client2Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
            {
                SecondPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                client2.GetComponent<Items>().firstPlace = false;
                client2.GetComponent<Items>().secondPlace = true;
                client2.GetComponent<Items>().thirdPlace = false;
                client2.GetComponent<Items>().fourthPlace = false;

                // PLAYER 0, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
                {
                    ThirdPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = true;
                    client0.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 3, FourthPlace
                    FourthPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = false;
                    client3.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 3, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value)
                {
                    ThirdPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = true;
                    client3.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 0, FourthPlace
                    FourthPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = false;
                    client0.GetComponent<Items>().fourthPlace = true;
                }
            }

            // PLAYER 3, SecondPlace
            if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value
                && rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client2Points.Value)
            {
                SecondPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                client3.GetComponent<Items>().firstPlace = false;
                client3.GetComponent<Items>().secondPlace = true;
                client3.GetComponent<Items>().thirdPlace = false;
                client3.GetComponent<Items>().fourthPlace = false;

                // PLAYER 0, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value)
                {
                    ThirdPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = true;
                    client0.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 2, FourthPlace
                    FourthPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = false;
                    client2.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 2, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value)
                {
                    ThirdPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = true;
                    client2.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 0, FourthPlace
                    FourthPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = false;
                    client0.GetComponent<Items>().fourthPlace = true;
                }
            }
        }

        // PLAYER 2, FirstPlace
        if (rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value
            && rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value
            && rankManager.GetComponent<RankManager>().client2Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
        {
            FirstPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
            client2.GetComponent<Items>().firstPlace = true;
            client2.GetComponent<Items>().secondPlace = false;
            client2.GetComponent<Items>().thirdPlace = false;
            client2.GetComponent<Items>().fourthPlace = false;

            // PLAYER 0, SecondPlace
            if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client1Points.Value
                && rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
            {
                SecondPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                client0.GetComponent<Items>().firstPlace = false;
                client0.GetComponent<Items>().secondPlace = true;
                client0.GetComponent<Items>().thirdPlace = false;
                client0.GetComponent<Items>().fourthPlace = false;

                // PLAYER 1, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
                {
                    ThirdPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = true;
                    client1.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 3, FourthPlace
                    FourthPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = false;
                    client3.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 3, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value)
                {
                    ThirdPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = true;
                    client3.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 1, FourthPlace
                    FourthPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = false;
                    client1.GetComponent<Items>().fourthPlace = true;
                }
            }

            // PLAYER 1, SecondPlace
            if (rankManager.GetComponent<RankManager>().client1Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value
                && rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
            {
                SecondPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                client1.GetComponent<Items>().firstPlace = false;
                client1.GetComponent<Items>().secondPlace = true;
                client1.GetComponent<Items>().thirdPlace = false;
                client1.GetComponent<Items>().fourthPlace = false;

                // PLAYER 0, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client3Points.Value)
                {
                    ThirdPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = true;
                    client0.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 3, FourthPlace
                    FourthPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = false;
                    client3.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 3, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value)
                {
                    ThirdPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                    client3.GetComponent<Items>().firstPlace = false;
                    client3.GetComponent<Items>().secondPlace = false;
                    client3.GetComponent<Items>().thirdPlace = true;
                    client3.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 0, FourthPlace
                    FourthPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = false;
                    client0.GetComponent<Items>().fourthPlace = true;
                }
            }

            // PLAYER 3, SecondPlace
            if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value
                && rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value)
            {
                SecondPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
                client3.GetComponent<Items>().firstPlace = false;
                client3.GetComponent<Items>().secondPlace = true;
                client3.GetComponent<Items>().thirdPlace = false;
                client3.GetComponent<Items>().fourthPlace = false;

                // PLAYER 0, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client1Points.Value)
                {
                    ThirdPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = true;
                    client0.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 1, FourthPlace
                    FourthPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = false;
                    client1.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 1, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client1Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value)
                {
                    ThirdPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = true;
                    client1.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 0, FourthPlace
                    FourthPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = false;
                    client0.GetComponent<Items>().fourthPlace = true;
                }
            }
        }

        // PLAYER 3, FirstPlace
        if (rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value
            && rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value
            && rankManager.GetComponent<RankManager>().client3Points.Value > rankManager.GetComponent<RankManager>().client2Points.Value)
        {
            FirstPlaceText = "<color=yellow> YELLOW " + rankManager.GetComponent<RankManager>().client3Points.Value + "</color> POINTS";
            client3.GetComponent<Items>().firstPlace = true;
            client3.GetComponent<Items>().secondPlace = false;
            client3.GetComponent<Items>().thirdPlace = false;
            client3.GetComponent<Items>().fourthPlace = false;

            // PLAYER 0, SecondPlace
            if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client1Points.Value
                && rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value)
            {
                SecondPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                client0.GetComponent<Items>().firstPlace = false;
                client0.GetComponent<Items>().secondPlace = true;
                client0.GetComponent<Items>().thirdPlace = false;
                client0.GetComponent<Items>().fourthPlace = false;

                // PLAYER 1, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value)
                {
                    ThirdPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = true;
                    client1.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 2, FourthPlace
                    FourthPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = false;
                    client2.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 2, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value)
                {
                    ThirdPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = true;
                    client2.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 1, FourthPlace
                    FourthPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = false;
                    client1.GetComponent<Items>().fourthPlace = true;
                }
            }

            // PLAYER 1, SecondPlace
            if (rankManager.GetComponent<RankManager>().client1Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value
                && rankManager.GetComponent<RankManager>().client1Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value)
            {
                SecondPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                client1.GetComponent<Items>().firstPlace = false;
                client1.GetComponent<Items>().secondPlace = true;
                client1.GetComponent<Items>().thirdPlace = false;
                client1.GetComponent<Items>().fourthPlace = false;

                // PLAYER 0, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client2Points.Value)
                {
                    ThirdPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = true;
                    client0.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 2, FourthPlace
                    FourthPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = false;
                    client2.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 2, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value)
                {
                    ThirdPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                    client2.GetComponent<Items>().firstPlace = false;
                    client2.GetComponent<Items>().secondPlace = false;
                    client2.GetComponent<Items>().thirdPlace = true;
                    client2.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 0, FourthPlace
                    FourthPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = false;
                    client0.GetComponent<Items>().fourthPlace = true;
                }
            }

            // PLAYER 2, SecondPlace
            if (rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value
                && rankManager.GetComponent<RankManager>().client2Points.Value > rankManager.GetComponent<RankManager>().client1Points.Value)
            {
                SecondPlaceText = "<color=green> GREEN " + rankManager.GetComponent<RankManager>().client2Points.Value + "</color> POINTS";
                client2.GetComponent<Items>().firstPlace = false;
                client2.GetComponent<Items>().secondPlace = true;
                client2.GetComponent<Items>().thirdPlace = false;
                client2.GetComponent<Items>().fourthPlace = false;

                // PLAYER 0, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client0Points.Value >= rankManager.GetComponent<RankManager>().client1Points.Value)
                {
                    ThirdPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = true;
                    client0.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 1, FourthPlace
                    FourthPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = false;
                    client1.GetComponent<Items>().fourthPlace = true;
                }

                // PLAYER 1, ThirdPlace
                if (rankManager.GetComponent<RankManager>().client1Points.Value > rankManager.GetComponent<RankManager>().client0Points.Value)
                {
                    ThirdPlaceText = "<color=blue> BLUE " + rankManager.GetComponent<RankManager>().client1Points.Value + "</color> Points";
                    client1.GetComponent<Items>().firstPlace = false;
                    client1.GetComponent<Items>().secondPlace = false;
                    client1.GetComponent<Items>().thirdPlace = true;
                    client1.GetComponent<Items>().fourthPlace = false;

                    // PLAYER 0, FourthPlace
                    FourthPlaceText = "<color=red> RED " + rankManager.GetComponent<RankManager>().client0Points.Value + "</color> Points";
                    client0.GetComponent<Items>().firstPlace = false;
                    client0.GetComponent<Items>().secondPlace = false;
                    client0.GetComponent<Items>().thirdPlace = false;
                    client0.GetComponent<Items>().fourthPlace = true;
                }
            }
        }
    }
}
