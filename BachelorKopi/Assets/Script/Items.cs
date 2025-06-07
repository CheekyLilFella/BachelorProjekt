using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.VisualScripting;
using System.Collections;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Coherence.Connection;
using UnityEngine.Rendering.Universal;

public class Items : NetworkBehaviour
{
    [SerializeField] private NetworkObject m_spawnedNetworkObject;

    public Button button;

    public float fireballSpeed;
    public float homingSpeed;
    public float waterWaveSpeed;
    public float tsunamiSpeed;
    public float iceSpeed;
    public float boomerangSpeed;
    public float boomerangDistance;
    public float heal;
    public float pointGain;
    public float itemPicker;

    public NetworkVariable<int> itemAmount = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    //public int itemAmount;

    private NetworkVariable<float> itemIndex = new NetworkVariable<float>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public Transform itemSpawnTransform;

    public GameObject fireballPrefab;
    public GameObject earthWallPrefab;
    public GameObject homingPrefab;
    public GameObject healingPrefab;
    public GameObject windWavePrefab;
    public GameObject waterWavePrefab;
    public GameObject iceSpikePrefab;
    public GameObject teleportPrefab;
    public GameObject outTeleportPrefab;
    public GameObject tsunamiPrefab;
    public GameObject shieldPrefab;
    public GameObject lightningPrefab;
    public GameObject cloudPrefab;
    public GameObject plantPrefab;
    public GameObject meteorPrefab;
    public GameObject boomerangPrefab;
    public GameObject swordPrefab;
    public GameObject swordCollider;
    public GameObject sword;
    public GameObject slashPrefab;
    public GameObject snowballPrefab;
    public GameObject pointsPrefab;
    public GameObject target;
    public GameObject buttonGameObject;

    public Image image;

    public Sprite fireballSprite;
    public Sprite fireballSprite3;
    public Sprite fireballSprite2;
    public Sprite earthWallSprite;
    public Sprite earthWallSprite3;
    public Sprite earthWallSprite2;
    public Sprite homingSprite;
    public Sprite homingSprite3;
    public Sprite homingSprite2;
    public Sprite healSprite;
    public Sprite windSprite;
    public Sprite waterSprite;
    public Sprite waterSprite3;
    public Sprite waterSprite2;
    public Sprite iceSprite;
    public Sprite teleportSprite;
    public Sprite tsunamiSprite;
    public Sprite shieldSprite;
    public Sprite lightningSprite;
    public Sprite plantSprite;
    public Sprite fogSprite;
    public Sprite meteorSprite;
    public Sprite boomerangSprite;
    public Sprite swordSprite;
    public Sprite snowballSprite;
    public Sprite pointsSprite;
    public Sprite defaultSprite;

    public Mesh snowballMesh;
    public Mesh capsuleMesh;

    public UniversalRendererData fogPowerupMobile;
    public UniversalRendererData fogPowerupPC;
    public Material fullScreenMaterialFog;
    public Material snowballMaterial;
    public Material playerMaterial;

    public bool shieldActive = false;
    public bool snowActive = false;
    public bool itemAvailable = false;

    public bool firstPlace = false;
    public bool secondPlace = false;
    public bool thirdPlace = false;
    public bool fourthPlace = false;
    public bool fifthPlace = false;
    public bool sixthPlace = false;
    public bool seventhPlace = false;
    public bool eighthPlace = false;

    public float[] first = new float[] { 1, 2, 3, 4, 5, 6 };
    public float[] second = new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 18, 19, 20 };
    public float[] third = new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 18, 19, 20 };
    public float[] fourth = new float[] { 1, 3, 6, 7, 9, 10, 11, 12, 17, 18, 19, 20 };
    public float[] fifth = new float[] { 3, 6, 11, 12, 13, 14, 15, 17, 21 };
    public float[] sixth = new float[] { 6, 11, 12, 13, 14, 15, 16, 21 };
    public float[] seventh = new float[] { 1, 11, 13, 14, 15, 16, 21 };
    public float[] eighth = new float[] { 3, 11, 13, 14, 16, 21 };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (!IsOwner) return;

        target = gameObject;

        buttonGameObject = GameObject.FindGameObjectWithTag("ItemBtn");
        button = buttonGameObject.GetComponent<Button>();
        button.onClick.AddListener(ShootRpc);

        image = buttonGameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Rpc(SendTo.ClientsAndHost)]
    void ShootRpc()
    {
        if (NetworkObject.OwnerClientId == 0)
        {
            if (itemAvailable == true & itemIndex.Value == 1)
            {
                GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);

                MultiTagsHelperMethods.AddTag(fireball, "0Fireball");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 2)
            {
                GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 3)
            {
                GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                StartCoroutine(Homing(homing));

                MultiTagsHelperMethods.AddTag(homing, "0Homing");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 4)
            {
                GameObject healing = Instantiate(healingPrefab, gameObject.transform.position, Quaternion.identity);
                Heal();

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 5)
            {
                GameObject windWave = Instantiate(windWavePrefab, gameObject.transform.position, Quaternion.identity);

                MultiTagsHelperMethods.AddTag(windWave, "0WindWave");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 6)
            {
                GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);

                //MultiTagsHelperMethods.AddTag(fireball, "0Fireball");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 7)
            {
                StartCoroutine(Ice());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 8)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 1;
                    image.sprite = earthWallSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 2;
                    image.sprite = earthWallSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 9)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "0Fireball");
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "0Fireball");
                    itemAmount.Value = 1;
                    image.sprite = fireballSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "0Fireball");
                    itemAmount.Value = 2;
                    image.sprite = fireballSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 10)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "0Homing");
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "0Homing");
                    itemAmount.Value = 1;
                    image.sprite = homingSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "0Homing");
                    itemAmount.Value = 2;
                    image.sprite = homingSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 11)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 1;
                    image.sprite = waterSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 2;
                    image.sprite = waterSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 12)
            {
                StartCoroutine(Teleport());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 13)
            {
                GameObject tsunami = Instantiate(tsunamiPrefab, itemSpawnTransform.position, transform.rotation);
                tsunami.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * tsunamiSpeed, ForceMode.Impulse);

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 14)
            {
                GameObject shield = Instantiate(shieldPrefab, gameObject.transform.position, Quaternion.identity);

                MultiTagsHelperMethods.AddTag(shield, "0Shield");

                StartCoroutine(ShieldActive());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 15)
            {
                StartCoroutine(LightningStrike());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 16)
            {
                GameObject plant1 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant1, "1Plant");

                GameObject plant2 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant2, "2Plant");

                GameObject plant3 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant3, "3Plant");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 17)
            {
                StartCoroutine(FogofWar());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 18)
            {
                Vector3 meteorPos = itemSpawnTransform.position + transform.forward * 15;
                GameObject meteor = Instantiate(meteorPrefab, meteorPos, Quaternion.identity);
                MultiTagsHelperMethods.AddTag(meteor, "0Meteor");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 19)
            {
                GameObject boomerang = Instantiate(boomerangPrefab, itemSpawnTransform.position, boomerangPrefab.transform.rotation);
                boomerang.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * boomerangSpeed, ForceMode.Impulse);
                MultiTagsHelperMethods.AddTag(boomerang, OwnerClientId + "Boomerang");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 20)
            {
                //StartCoroutine(Sword());
                StartCoroutine(SwordSwingAnimation());
                //MultiTagsHelperMethods.AddTag(swordCollider, "0Sword");
                //MultiTagsHelperMethods.AddTag(slashPrefab, "0Slash");

                GetComponent<PlayerMovement>().runSpeed = 7;
                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 21)
            {
                GameObject points = Instantiate(pointsPrefab, gameObject.transform.position, Quaternion.identity);
                PointsPowerUp();

                itemAvailable = false;
                image.sprite = defaultSprite;
            }
        }

        if (NetworkObject.OwnerClientId == 1)
        {
            if (itemAvailable == true & itemIndex.Value == 1)
            {
                GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, Quaternion.identity);
                fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);

                MultiTagsHelperMethods.AddTag(fireball, "1Fireball");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 2)
            {
                GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 3)
            {
                GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                StartCoroutine(Homing(homing));

                MultiTagsHelperMethods.AddTag(homing, "1Homing");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 4)
            {
                GameObject healing = Instantiate(healingPrefab, gameObject.transform.position, Quaternion.identity);
                Heal();

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 5)
            {
                GameObject windWave = Instantiate(windWavePrefab, gameObject.transform.position, Quaternion.identity);

                MultiTagsHelperMethods.AddTag(windWave, "1WindWave");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 6)
            {
                GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);

                waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);

                //MultiTagsHelperMethods.AddTag(fireball, "0Fireball");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 7)
            {
                StartCoroutine(Ice());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 8)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 1;
                    image.sprite = earthWallSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 2;
                    image.sprite = earthWallSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 9)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "1Fireball");
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "1Fireball");
                    itemAmount.Value = 1;
                    image.sprite = fireballSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "1Fireball");
                    itemAmount.Value = 2;
                    image.sprite = fireballSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 10)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "1Homing");
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "1Homing");
                    itemAmount.Value = 1;
                    image.sprite = homingSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "1Homing");
                    itemAmount.Value = 2;
                    image.sprite = homingSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 11)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 1;
                    image.sprite = waterSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 2;
                    image.sprite = waterSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 12)
            {
                StartCoroutine(Teleport());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 13)
            {
                GameObject tsunami = Instantiate(tsunamiPrefab, itemSpawnTransform.position, transform.rotation);
                tsunami.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * tsunamiSpeed, ForceMode.Impulse);

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 14)
            {
                GameObject shield = Instantiate(shieldPrefab, gameObject.transform.position, Quaternion.identity);

                MultiTagsHelperMethods.AddTag(shield, "1Shield");

                StartCoroutine(ShieldActive());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 15)
            {
                StartCoroutine(LightningStrike());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 16)
            {
                GameObject plant0 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant0, "0Plant");

                GameObject plant2 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant2, "2Plant");

                GameObject plant3 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant3, "3Plant");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 17)
            {
                StartCoroutine(FogofWar());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 18)
            {
                Vector3 meteorPos = itemSpawnTransform.position + transform.forward * 15;
                GameObject meteor = Instantiate(meteorPrefab, meteorPos, Quaternion.identity);
                MultiTagsHelperMethods.AddTag(meteor, "1Meteor");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 19)
            {
                GameObject boomerang = Instantiate(boomerangPrefab, itemSpawnTransform.position, boomerangPrefab.transform.rotation);
                boomerang.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * boomerangSpeed, ForceMode.Impulse);
                MultiTagsHelperMethods.AddTag(boomerang, OwnerClientId + "Boomerang");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 20)
            {
                //StartCoroutine(Sword());
                StartCoroutine(SwordSwingAnimation());
                //MultiTagsHelperMethods.AddTag(swordCollider, "0Sword");

                GetComponent<PlayerMovement>().runSpeed = 7;
                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 21)
            {
                GameObject points = Instantiate(pointsPrefab, gameObject.transform.position, Quaternion.identity);
                PointsPowerUp();

                itemAvailable = false;
                image.sprite = defaultSprite;
            }
        }

        if (NetworkObject.OwnerClientId == 2)
        {
            if (itemAvailable == true & itemIndex.Value == 1)
            {
                GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, Quaternion.identity);
                fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);

                MultiTagsHelperMethods.AddTag(fireball, "2Fireball");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 2)
            {
                GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 3)
            {
                GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                StartCoroutine(Homing(homing));

                MultiTagsHelperMethods.AddTag(homing, "2Homing");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 4)
            {
                GameObject healing = Instantiate(healingPrefab, gameObject.transform.position, Quaternion.identity);
                Heal();

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 5)
            {
                GameObject windWave = Instantiate(windWavePrefab, gameObject.transform.position, Quaternion.identity);

                MultiTagsHelperMethods.AddTag(windWave, "2WindWave");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 6)
            {
                GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);

                waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);

                //MultiTagsHelperMethods.AddTag(fireball, "0Fireball");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 7)
            {
                StartCoroutine(Ice());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 8)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 1;
                    image.sprite = earthWallSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 2;
                    image.sprite = earthWallSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 9)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "2Fireball");
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "2Fireball");
                    itemAmount.Value = 1;
                    image.sprite = fireballSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "2Fireball");
                    itemAmount.Value = 2;
                    image.sprite = fireballSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 10)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "2Homing");
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "2Homing");
                    itemAmount.Value = 1;
                    image.sprite = homingSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "2Homing");
                    itemAmount.Value = 2;
                    image.sprite = homingSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 11)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 1;
                    image.sprite = waterSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 2;
                    image.sprite = waterSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 12)
            {
                StartCoroutine(Teleport());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 13)
            {
                GameObject tsunami = Instantiate(tsunamiPrefab, itemSpawnTransform.position, transform.rotation);
                tsunami.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * tsunamiSpeed, ForceMode.Impulse);

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 14)
            {
                GameObject shield = Instantiate(shieldPrefab, gameObject.transform.position, Quaternion.identity);

                MultiTagsHelperMethods.AddTag(shield, "2Shield");

                StartCoroutine(ShieldActive());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 15)
            {
                StartCoroutine(LightningStrike());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 16)
            {
                GameObject plant0 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant0, "0Plant");

                GameObject plant1 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant1, "1Plant");

                GameObject plant3 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant3, "3Plant");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 17)
            {
                StartCoroutine(FogofWar());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 18)
            {
                Vector3 meteorPos = itemSpawnTransform.position + transform.forward * 15;
                GameObject meteor = Instantiate(meteorPrefab, meteorPos, Quaternion.identity);
                MultiTagsHelperMethods.AddTag(meteor, "2Meteor");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 19)
            {
                GameObject boomerang = Instantiate(boomerangPrefab, itemSpawnTransform.position, boomerangPrefab.transform.rotation);
                boomerang.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * boomerangSpeed, ForceMode.Impulse);
                MultiTagsHelperMethods.AddTag(boomerang, OwnerClientId + "Boomerang");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 20)
            {
                //StartCoroutine(Sword());
                StartCoroutine(SwordSwingAnimation());
                //MultiTagsHelperMethods.AddTag(swordCollider, "0Sword");

                GetComponent<PlayerMovement>().runSpeed = 7;
                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 21)
            {
                GameObject points = Instantiate(pointsPrefab, gameObject.transform.position, Quaternion.identity);
                PointsPowerUp();

                itemAvailable = false;
                image.sprite = defaultSprite;
            }
        }

        if (NetworkObject.OwnerClientId == 3)
        {
            if (itemAvailable == true & itemIndex.Value == 1)
            {
                GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, Quaternion.identity);
                fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);

                MultiTagsHelperMethods.AddTag(fireball, "3Fireball");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 2)
            {
                GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 3)
            {
                GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                StartCoroutine(Homing(homing));

                MultiTagsHelperMethods.AddTag(homing, "3Homing");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 4)
            {
                GameObject healing = Instantiate(healingPrefab, gameObject.transform.position, Quaternion.identity);
                Heal();

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 5)
            {
                GameObject windWave = Instantiate(windWavePrefab, gameObject.transform.position, Quaternion.identity);

                MultiTagsHelperMethods.AddTag(windWave, "3WindWave");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 6)
            {
                GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);

                waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);

                //MultiTagsHelperMethods.AddTag(fireball, "0Fireball");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 7)
            {
                StartCoroutine(Ice());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 8)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 1;
                    image.sprite = earthWallSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject earthWall = Instantiate(earthWallPrefab, itemSpawnTransform.position, Quaternion.identity);
                    itemAmount.Value = 2;
                    image.sprite = earthWallSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 9)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "3Fireball");
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "3Fireball");
                    itemAmount.Value = 1;
                    image.sprite = fireballSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject fireball = Instantiate(fireballPrefab, itemSpawnTransform.position, transform.rotation);
                    fireball.GetComponentInChildren<Rigidbody>().AddForce(itemSpawnTransform.forward * fireballSpeed, ForceMode.Impulse);
                    MultiTagsHelperMethods.AddTag(fireball, "3Fireball");
                    itemAmount.Value = 2;
                    image.sprite = fireballSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 10)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "3Homing");
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "3Homing");
                    itemAmount.Value = 1;
                    image.sprite = homingSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject homing = Instantiate(homingPrefab, itemSpawnTransform.position, Quaternion.identity);
                    StartCoroutine(Homing(homing));
                    MultiTagsHelperMethods.AddTag(homing, "3Homing");
                    itemAmount.Value = 2;
                    image.sprite = homingSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 11)
            {
                if (itemAmount.Value == 1)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 0;
                    itemAvailable = false;
                    image.sprite = defaultSprite;
                }

                if (itemAmount.Value == 2)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 1;
                    image.sprite = waterSprite;
                }

                if (itemAmount.Value == 3)
                {
                    GameObject waterWave = Instantiate(waterWavePrefab, itemSpawnTransform.position, transform.rotation);
                    waterWave.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * waterWaveSpeed, ForceMode.Impulse);
                    itemAmount.Value = 2;
                    image.sprite = waterSprite2;
                }
            }

            if (itemAvailable == true & itemIndex.Value == 12)
            {
                StartCoroutine(Teleport());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 13)
            {
                GameObject tsunami = Instantiate(tsunamiPrefab, itemSpawnTransform.position, transform.rotation);
                tsunami.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * tsunamiSpeed, ForceMode.Impulse);

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 14)
            {
                GameObject shield = Instantiate(shieldPrefab, gameObject.transform.position, Quaternion.identity);

                MultiTagsHelperMethods.AddTag(shield, "3Shield");

                StartCoroutine(ShieldActive());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 15)
            {
                StartCoroutine(LightningStrike());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 16)
            {
                GameObject plant0 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant0, "0Plant");

                GameObject plant1 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant1, "1Plant");

                GameObject plant2 = Instantiate(plantPrefab);
                MultiTagsHelperMethods.AddTag(plant2, "2Plant");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 17)
            {
                StartCoroutine(FogofWar());

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 18)
            {
                Vector3 meteorPos = itemSpawnTransform.position + transform.forward * 15;
                GameObject meteor = Instantiate(meteorPrefab, meteorPos, Quaternion.identity);
                MultiTagsHelperMethods.AddTag(meteor, "3Meteor");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 19)
            {
                GameObject boomerang = Instantiate(boomerangPrefab, itemSpawnTransform.position, boomerangPrefab.transform.rotation);
                boomerang.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * boomerangSpeed, ForceMode.Impulse);
                MultiTagsHelperMethods.AddTag(boomerang, OwnerClientId + "Boomerang");

                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 20)
            {
                //StartCoroutine(Sword());
                StartCoroutine(SwordSwingAnimation());
                //MultiTagsHelperMethods.AddTag(swordCollider, "0Sword");

                GetComponent<PlayerMovement>().runSpeed = 7;
                itemAvailable = false;
                image.sprite = defaultSprite;
            }

            if (itemAvailable == true & itemIndex.Value == 21)
            {
                GameObject points = Instantiate(pointsPrefab, gameObject.transform.position, Quaternion.identity);
                PointsPowerUp();

                itemAvailable = false;
                image.sprite = defaultSprite;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ItemBox"))
        {
            itemAvailable = true;
            ItemSelection();

        }
    }

    public void ItemSelection()
    {
        //itemIndex.Value = Random.Range(1, 22);
        //itemIndex.Value = 21;

        //ALGORITME FOR ITEMS
        if (firstPlace == true)
        {
            itemPicker = Random.Range(1, 8);

            if (itemPicker == 1)
            {
                itemIndex.Value = 1;
            }
            if (itemPicker == 2)
            {
                itemIndex.Value = 2;
            }
            if (itemPicker == 3)
            {
                itemIndex.Value = 3;
            }
            if (itemPicker == 4)
            {
                itemIndex.Value = 4;
            }
            if (itemPicker == 5)
            {
                itemIndex.Value = 5;
            }
            if (itemPicker == 6)
            {
                itemIndex.Value = 6;
            }
            if (itemPicker == 7)
            {
                itemIndex.Value = 12;
            }
            //itemIndex.Value = Random.Range(0, first.Length);
        }
        if (secondPlace == true)
        {
            itemPicker = Random.Range(1, 13);

            if (itemPicker == 1)
            {
                itemIndex.Value = 1;
            }
            if (itemPicker == 2)
            {
                itemIndex.Value = 2;
            }
            if (itemPicker == 3)
            {
                itemIndex.Value = 3;
            }
            if (itemPicker == 4)
            {
                itemIndex.Value = 4;
            }
            if (itemPicker == 5)
            {
                itemIndex.Value = 5;
            }
            if (itemPicker == 6)
            {
                itemIndex.Value = 6;
            }
            if (itemPicker == 7)
            {
                itemIndex.Value = 7;
            }
            if (itemPicker == 8)
            {
                itemIndex.Value = 8;
            }
            if (itemPicker == 9)
            {
                itemIndex.Value = 9;
            }
            if (itemPicker == 10)
            {
                itemIndex.Value = 18;
            }
            if (itemPicker == 11)
            {
                itemIndex.Value = 19;
            }
            if (itemPicker == 12)
            {
                itemIndex.Value = 20;
            }
            //itemIndex.Value = Random.Range(0, second.Length);
        }
        if (thirdPlace == true)
        {
            itemPicker = Random.Range(1, 12);

            if (itemPicker == 1)
            {
                itemIndex.Value = 1;
            }
            if (itemPicker == 2)
            {
                itemIndex.Value = 3;
            }
            if (itemPicker == 3)
            {
                itemIndex.Value = 6;
            }
            if (itemPicker == 4)
            {
                itemIndex.Value = 7;
            }
            if (itemPicker == 5)
            {
                itemIndex.Value = 9;
            }
            if (itemPicker == 6)
            {
                itemIndex.Value = 10;
            }
            if (itemPicker == 7)
            {
                itemIndex.Value = 11;
            }
            if (itemPicker == 8)
            {
                itemIndex.Value = 17;
            }
            if (itemPicker == 9)
            {
                itemIndex.Value = 18;
            }
            if (itemPicker == 10)
            {
                itemIndex.Value = 19;
            }
            if (itemPicker == 11)
            {
                itemIndex.Value = 20;
            }
            //itemIndex.Value = Random.Range(0, third.Length);
        }
        if (fourthPlace == true)
        {
            itemPicker = Random.Range(1, 9);

            if (itemPicker == 1)
            {
                itemIndex.Value = 9;
            }
            if (itemPicker == 2)
            {
                itemIndex.Value = 10;
            }
            if (itemPicker == 3)
            {
                itemIndex.Value = 13;
            }
            if (itemPicker == 4)
            {
                itemIndex.Value = 14;
            }
            if (itemPicker == 5)
            {
                itemIndex.Value = 15;
            }
            if (itemPicker == 6)
            {
                itemIndex.Value = 16;
            }
            if (itemPicker == 7)
            {
                itemIndex.Value = 19;
            }
            if (itemPicker == 8)
            {
                itemIndex.Value = 21;
            }
            //itemIndex.Value = Random.Range(0, fourth.Length);
        }
        /*if (fifthPlace == true)
        {
            itemIndex.Value = Random.Range(0, fifth.Length);
        }
        if (sixthPlace == true)
        {
            itemIndex.Value = Random.Range(0, sixth.Length);
        }
        if (seventhPlace == true)
        {
            itemIndex.Value = Random.Range(0, seventh.Length);
        }
        if (eighthPlace == true)
        {
            itemIndex.Value = Random.Range(0, eighth.Length);
        }*/

        if (itemIndex.Value == 1)
        {
            image.sprite = fireballSprite;
        }

        if (itemIndex.Value == 2)
        {
            image.sprite = earthWallSprite;
        }

        if (itemIndex.Value == 3)
        {
            image.sprite = homingSprite;
        }

        if (itemIndex.Value == 4)
        {
            image.sprite = healSprite;
        }

        if (itemIndex.Value == 5)
        {
            image.sprite = windSprite;
        }

        if (itemIndex.Value == 6)
        {
            image.sprite = waterSprite;
        }

        if (itemIndex.Value == 7)
        {
            image.sprite = iceSprite;
        }

        if (itemIndex.Value == 8)
        {
            image.sprite = earthWallSprite3;
            itemAmount.Value = 3;
        }

        if (itemIndex.Value == 9)
        {
            image.sprite = fireballSprite3;
            itemAmount.Value = 3;
        }

        if (itemIndex.Value == 10)
        {
            image.sprite = homingSprite3;
            itemAmount.Value = 3;
        }

        if (itemIndex.Value == 11)
        {
            image.sprite = waterSprite3;
            itemAmount.Value = 3;
        }

        if (itemIndex.Value == 12)
        {
            image.sprite = teleportSprite;
        }

        if (itemIndex.Value == 13)
        {
            image.sprite = tsunamiSprite;
        }

        if (itemIndex.Value == 14)
        {
            image.sprite = shieldSprite;
        }

        if (itemIndex.Value == 15)
        {
            image.sprite = lightningSprite;
        }

        if (itemIndex.Value == 16)
        {
            image.sprite = plantSprite;
        }

        if (itemIndex.Value == 17)
        {
            image.sprite = fogSprite;
        }

        if (itemIndex.Value == 18)
        {
            image.sprite = meteorSprite;
        }

        if (itemIndex.Value == 19)
        {
            image.sprite = boomerangSprite;
        }

        if (itemIndex.Value == 20)
        {
            image.sprite = swordSprite;
            swordPrefab.SetActive(true);
            GetComponent<PlayerMovement>().runSpeed = 10;
        }

        if (itemIndex.Value == 21)
        {
            image.sprite = pointsSprite;
        }
    }

    public IEnumerator Homing(GameObject homing)
    {
        target = GameObject.FindGameObjectsWithTag("Player").Where(d=> d.gameObject != gameObject).Aggregate((prev, next) =>
        Vector3.Distance(prev.transform.position, transform.position) < Vector3.Distance(next.transform.position, transform.position) 
        ? prev : next);

            while (Vector3.Distance(target.transform.position, homing.transform.position) > 0.3f)
            {

                homing.transform.position += (target.transform.position - homing.transform.position).normalized * homingSpeed * Time.deltaTime;
                yield return null;
            }
        
    }

    public void Heal()
    {
        gameObject.GetComponent<PlayerHealth>().health += heal;
    }

    public IEnumerator Ice() 
    {
        IceInstantiate();
        yield return new WaitForSeconds(1f);

        IceInstantiate();
        yield return new WaitForSeconds(1f);

        IceInstantiate();
        yield return new WaitForSeconds(1f);

        IceInstantiate();
        yield return new WaitForSeconds(1f);

        IceInstantiate();
    }

    public void IceInstantiate()
    {
        GameObject iceSpike = Instantiate(iceSpikePrefab, itemSpawnTransform.position, transform.rotation);
        iceSpike.GetComponent<Rigidbody>().AddForce(itemSpawnTransform.forward * iceSpeed, ForceMode.Impulse);

        MultiTagsHelperMethods.AddTag(iceSpike, OwnerClientId + "Ice");
    }

    public IEnumerator Teleport()
    {
        Vector3 telePos = transform.position + transform.forward * 10;

        GameObject teleport = Instantiate(teleportPrefab, gameObject.transform.position, Quaternion.identity);
        GameObject outTeleport = Instantiate(outTeleportPrefab, telePos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.position = telePos;
    }

    public IEnumerator LightningStrike()
    {
        /*var client0 = NetworkManager.Singleton.ConnectedClients[0].PlayerObject;
        var client1 = NetworkManager.Singleton.ConnectedClients[1].PlayerObject;
        var client2 = NetworkManager.Singleton.ConnectedClients[2].PlayerObject;
        var client3 = NetworkManager.Singleton.ConnectedClients[3].PlayerObject;*/

        if (NetworkManager.Singleton.ConnectedClients[0].PlayerObject.GetComponent<Items>().firstPlace == true)
        {
            GameObject cloud = Instantiate(cloudPrefab, NetworkManager.Singleton.ConnectedClients[0].PlayerObject.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(2f);
            GameObject lightning = Instantiate(lightningPrefab, NetworkManager.Singleton.ConnectedClients[0].PlayerObject.transform.position, Quaternion.identity);
            MultiTagsHelperMethods.AddTag(lightning, OwnerClientId + "Lightning");
        }

        if (NetworkManager.Singleton.ConnectedClients[1].PlayerObject.GetComponent<Items>().firstPlace == true)
        {
            GameObject cloud = Instantiate(cloudPrefab, NetworkManager.Singleton.ConnectedClients[1].PlayerObject.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(2f);
            GameObject lightning = Instantiate(lightningPrefab, NetworkManager.Singleton.ConnectedClients[1].PlayerObject.transform.position, Quaternion.identity);
            MultiTagsHelperMethods.AddTag(lightning, OwnerClientId + "Lightning");
        }

        if (NetworkManager.Singleton.ConnectedClients[2].PlayerObject.GetComponent<Items>().firstPlace == true)
        {
            GameObject cloud = Instantiate(cloudPrefab, NetworkManager.Singleton.ConnectedClients[2].PlayerObject.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(2f);
            GameObject lightning = Instantiate(lightningPrefab, NetworkManager.Singleton.ConnectedClients[2].PlayerObject.transform.position, Quaternion.identity);
            MultiTagsHelperMethods.AddTag(lightning, OwnerClientId + "Lightning");
        }

        if (NetworkManager.Singleton.ConnectedClients[3].PlayerObject.GetComponent<Items>().firstPlace == true)
        {
            GameObject cloud = Instantiate(cloudPrefab, NetworkManager.Singleton.ConnectedClients[3].PlayerObject.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(2f);
            GameObject lightning = Instantiate(lightningPrefab, NetworkManager.Singleton.ConnectedClients[3].PlayerObject.transform.position, Quaternion.identity);
            MultiTagsHelperMethods.AddTag(lightning, OwnerClientId + "Lightning");
        }

        /*target = GameObject.FindGameObjectsWithTag("Player").Where(d => d.gameObject != gameObject).Aggregate((prev, next) =>
        Vector3.Distance(prev.transform.position, transform.position) < Vector3.Distance(next.transform.position, transform.position)
        ? prev : next);

        GameObject cloud = Instantiate(cloudPrefab, target.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        GameObject lightning = Instantiate(lightningPrefab, target.transform.position, Quaternion.identity);
        MultiTagsHelperMethods.AddTag(lightning, OwnerClientId + "Lightning");*/
    }

    public IEnumerator FogofWar()
    {
        fullScreenMaterialFog.SetFloat("_VignettePower", 1);
        fullScreenMaterialFog.SetFloat("_VignetteIntensity", 8);

        yield return new WaitForSeconds(5f);
        fullScreenMaterialFog.SetFloat("_VignettePower", 0);
        fullScreenMaterialFog.SetFloat("_VignetteIntensity", 0);
    }

    public IEnumerator ShieldActive()
    {
        shieldActive = true;
        yield return new WaitForSeconds(7.5f);
        shieldActive = false;
    }

    IEnumerator SwordSwingAnimation()
    {
        swordPrefab.GetComponent<Animator>().Play("SwordSwing");
        yield return new WaitForSeconds(0.5f);
        GameObject sword = Instantiate(slashPrefab, swordPrefab.transform.position, slashPrefab.transform.rotation);
        MultiTagsHelperMethods.AddTag(sword, OwnerClientId + "Slash");
        yield return new WaitForSeconds(1f);
        swordPrefab.GetComponent<Animator>().Play("New State");
        yield return new WaitForSeconds(1f);
        swordPrefab.SetActive(false);
    }

    public void PointsPowerUp()
    {
        gameObject.GetComponent<PointManager>().points += pointGain;
    }

    IEnumerator Snowball()
    {
        //Vector3 snowballPos = transform.position + transform.up * 1.5f;
        snowActive = true;
        GameObject snowball = Instantiate(snowballPrefab, gameObject.transform.position, gameObject.transform.rotation);
        //GetComponent<PlayerMovement>().runSpeed = 12;

        /*GetComponent<MeshFilter>().mesh = snowballMesh;
        GetComponent<Transform>().localScale = new Vector3(3, 3, 3);
        GetComponent<MeshRenderer>().material = snowballMaterial;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = true;*/
        GetComponent<PlayerMovement>().runSpeed = 12;
        //GetComponent<Rigidbody>().mass = 10000;
        //buttonGameObject.SetActive(false);*/

        yield return new WaitForSeconds(8.1f);
        //GetComponent<MeshRenderer>().enabled = true;
        //GetComponent<CapsuleCollider>().enabled = true;
        //GetComponent<PlayerMovement>().runSpeed = 7;
        snowActive = false;

       /* GetComponent<MeshFilter>().mesh = capsuleMesh;
        GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        GetComponent<MeshRenderer>().material = playerMaterial;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = true;*/
        GetComponent<PlayerMovement>().runSpeed = 7;
        //GetComponent<Rigidbody>().mass = 1;
        //buttonGameObject.SetActive(true);

       /* if (OwnerClientId == 0)
        {
            var localMaterial = gameObject.GetComponent<MeshRenderer>().material;
            localMaterial.color = Color.red;
            //playerMaterial.color = Color.red;
        }

        if (OwnerClientId == 1)
        {
            var localMaterial = gameObject.GetComponent<MeshRenderer>().material;
            localMaterial.color = Color.blue;
        }

        if (OwnerClientId == 2)
        {
            var localMaterial = gameObject.GetComponent<MeshRenderer>().material;
            localMaterial.color = Color.green;
        }

        if (OwnerClientId == 3)
        {
            var localMaterial = gameObject.GetComponent<MeshRenderer>().material;
            localMaterial.color = Color.magenta;
        }*/
    }
}
