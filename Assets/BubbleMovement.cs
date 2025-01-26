using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;
using TMPro;

public class BubbleMovement : MonoBehaviour
{
    private TestButtons buttons;

    public bool keyboard1 = false;
    public bool keyboard2 = false;
    public bool keyboard3 = false;
    public bool controller1 = false;
    public bool buzz1 = false;
    public bool buzz2 = false;
    public bool buzz3 = false;
    public bool buzz4 = false;

    [SerializeField] private float maxSpeed = 5f; // Maximum velocity
    private Vector2 currentSpeed;
    [SerializeField] private float accelerationSpeed = 2f; // Acceleration rate
    [SerializeField] private float decelerationSpeed = 1f; // Deceleration rate

    private Rigidbody rb;

    [SerializeField] private float gracePeriod = 1f; // Cooldown period after taking damage
    public int bubbleHealth = 3;

    public HashSet<int> playersOnThisBubble;

    public Outline outline;

    public GameObject bubbleModel;

    [SerializeField] private float scaleIncrement;
    private Vector3 targetScale;
    private bool needNewBubble = false;

    public SphereCollisionFollower bubbleKeepMonsterInside;

    [SerializeField] private int startingPlayerTEST;

    private BubbleTouchingLogic bubbleTouchingLogic;

    private bool canTakeDamage = true; // To track if damage can be taken
    private Material bubbleMaterial; // Reference to the material of the bubble

    [SerializeField] private BlinkEffect[] hearts;

    private PlayerManager playerManager;

    public int score;
    public TextMeshProUGUI uiScore;

    private bool dead = false;

    private void Awake()
    {
        playersOnThisBubble = new HashSet<int>();
    }

    void Start()
    {
        buttons = FindAnyObjectByType<TestButtons>();
        playerManager = FindAnyObjectByType<PlayerManager>();   
        bubbleTouchingLogic = GetComponent<BubbleTouchingLogic>();
        currentSpeed = Vector2.zero;
        rb = GetComponent<Rigidbody>();

        // Get the material component from the bubble model
        if (bubbleModel != null)
        {
            bubbleMaterial = bubbleModel.GetComponent<Renderer>().material;
        }
    }

    public void DealDamage(int damage)
    {
        if (!canTakeDamage || dead) return; // Exit if in grace period

        bubbleHealth -= damage;





        if (bubbleHealth <= 0)
        {
            dead = true;
            Debug.Log("killed bubble");
            StartCoroutine(BubbleDead());
        }
        else
        {
            StartCoroutine(GracePeriodRoutine());
        }
    }

    private IEnumerator GracePeriodRoutine()
    {

        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(true);
        }

        yield return null;

        canTakeDamage = false; // Start grace period

        float elapsedTime = 0f;

        float defaultMaterialValue = bubbleMaterial.GetFloat("_Add_Alpha");

        if (bubbleHealth >= 0)
        {
            hearts[bubbleHealth].StartBlink();
        }


        while (elapsedTime < gracePeriod)
        {
            elapsedTime += Time.deltaTime;

            // Toggle AddAlpha property to create a blinking effect
            if (bubbleMaterial != null)
            {
                float alpha = Mathf.PingPong(Time.time * 10f, 1f); // PingPong oscillates between 0 and 1
                bubbleMaterial.SetFloat("_Add_Alpha", alpha);
            }

            yield return null; // Wait until the next frame
        }

        // Reset material alpha to default (assuming default is 1f)
        if (bubbleMaterial != null)
        {
            bubbleMaterial.SetFloat("_Add_Alpha", defaultMaterialValue);
        }

        canTakeDamage = true; // End grace period

        yield return new WaitForSeconds(1f);

        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(false);
        }

    }

    private IEnumerator BubbleDead()
    {

        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(true);
        }

        yield return null;

        Destroy(bubbleKeepMonsterInside.currentBubble);
        Destroy(bubbleModel);

        if (bubbleHealth >= 0)
        {
            hearts[bubbleHealth].StartBlink();
        }

        yield return new WaitForSeconds(1f);

        foreach (int playerNumber in playersOnThisBubble)
        {
            playerManager.AddToPlayerScore(playerNumber, -(playerManager.nrOfPlayers - 1));
        }

        playerManager.nrOfPlayers -= playersOnThisBubble.Count;

        Debug.Log("reduced nr of players");

        foreach (GameObject monster in bubbleTouchingLogic.monstersPivotsInside)
        {


            monster.transform.parent = null;
            Vector3 dirToCamera = Camera.main.transform.position - monster.transform.position;
            monster.GetComponent<Rigidbody>().linearVelocity = dirToCamera * UnityEngine.Random.Range(0.5f, 1.5f);
        }



        yield return new WaitForSeconds(1f);


        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(false);
        }


        yield return new WaitForSeconds(5f);

        foreach (GameObject monster in bubbleTouchingLogic.monstersPivotsInside)
        {
            Destroy(monster.gameObject);
            Destroy(gameObject, 0.1f);
        }
    }

    public void AddPlayer(int player)
    {
        playersOnThisBubble.Add(player);
        Debug.Log("playersOnThisBubble count " + playersOnThisBubble.Count);
        targetScale = Vector3.one + (Vector3.one * 0.33f * (playersOnThisBubble.Count - 1));
    }

    private void FixedUpdate()
    {
        if(!dead)
        {
            if (bubbleModel.transform.localScale.x < targetScale.x)
            {
                bubbleModel.transform.localScale += Vector3.one * Time.fixedDeltaTime * scaleIncrement;
                needNewBubble = true;
            }
            else if (needNewBubble)
            {
                needNewBubble = false;
                bubbleKeepMonsterInside.SpawnBubble();
            }

            Vector2 input = GetInput();
            rb.linearVelocity = input * maxSpeed;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            DealDamage(3);
        }
    }

    private Vector2 GetInput()
    {
        Vector2 speed = Vector2.zero;

        int numberOfInputs = 0;

        if (keyboard1)
        {
            numberOfInputs++;
            speed += buttons.keyboard1.normalized;
        }

        if (keyboard2)
        {
            numberOfInputs++;
            speed += buttons.keyboard2.normalized;
        }

        if (keyboard3)
        {
            numberOfInputs++;
            speed += buttons.keyboard3.normalized;
        }

        if (controller1)
        {
            numberOfInputs++;
            speed += buttons.joystickValues.normalized;
        }

        if (buzz1)
        {
            numberOfInputs++;
            speed += buttons.buzz1.normalized;
        }

        if (buzz2)
        {
            numberOfInputs++;
            speed += buttons.buzz2.normalized;
        }

        if (buzz3)
        {
            numberOfInputs++;
            speed += buttons.buzz3.normalized;
        }

        if (buzz4)
        {
            numberOfInputs++;
            speed += buttons.buzz4.normalized;
        }

        if (numberOfInputs == 0)
            return Vector2.zero;

        return speed / numberOfInputs;
    }
}
