/*
 * Author: Yitong Hu
 * Created: Dec 31st 2022
 * Description: Panda dodges the dialog bubbles falling down
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_PS_1_3_3 : SCP_PuzzleScreen
{
    // References
    [SerializeField] private GameObject[] fallingItems;
    [SerializeField] private GameObject pandaDown;
    private GameObject panda;
    private GameObject prompt;
    private GameObject spawner;
    private GameObject healthBar;

    // Variables
    [SerializeField] private float pandaSpeed; //10f
    [SerializeField] private float promptMoveSpeed; //8
    [SerializeField] private float promptMoveAmount; //0.5
    [SerializeField] private float spawnGap; //2
    [SerializeField] private float health; //5
    private Vector3 touchPos;
    private Vector3 healthBarScale;
    private float pandaPosY;
    private float promptOriginY;
    private float spawnTimer;
    private float curHealth;
    private bool end;
    private bool givenInput;

    // Initialization
    protected override void Awake()
    {
        base.Awake();

        end = false;
        givenInput = false;
        spawnTimer = spawnGap;
        curHealth = health;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Find references
        panda = transform.Find("SPR_Panda").gameObject;
        healthBar = panda.transform.Find("SPR_Health").gameObject;
        prompt = transform.Find("SPR_TapPrompt").gameObject;
        spawner = transform.Find("Spawner").gameObject;

        // Set logic
        touchPos = panda.transform.position;
        pandaPosY = panda.transform.position.y;
        promptOriginY = prompt.transform.position.y;
        healthBarScale = healthBar.transform.localScale;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Display Tap-to-move prompt
        if (!givenInput)
            MoveUpMoveDown(prompt, promptOriginY, promptMoveSpeed, promptMoveAmount);

        // Press to controll panda moves horizontally
        if (!end)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!givenInput)
                {
                    givenInput = true;
                    prompt.SetActive(false);
                }
                touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchPos.z = -1;
                touchPos.y = pandaPosY;
                if (touchPos.x < -2.2f) touchPos.x = -2.2f;
                if (touchPos.x > 2.2f) touchPos.x = 2.2f;
            }
            panda.transform.position = Vector3.MoveTowards(panda.transform.position, touchPos, pandaSpeed * Time.deltaTime);

            // Spawn items at random positions
            if(spawnTimer <= 0)
            {
                spawnGap -= 0.1f;
                spawnTimer = spawnGap;
                float lastRandX = -100f;
                float randomX = Random.Range(-2.0f, 2.0f);
                if (randomX == lastRandX)
                    randomX = Random.Range(-2.0f, 2.0f);
                lastRandX = randomX;
                Vector3 spawnPos = new Vector3(randomX, spawner.transform.position.y, -1);
                int randomFallIndex = Random.Range(0, 6);
                GameObject fallitem = Instantiate(fallingItems[randomFallIndex], spawnPos, Quaternion.identity, spawner.transform);
                fallitem.transform.localScale = new Vector3(0.52f, 0.52f, 1);
            }
            else    
                spawnTimer -= Time.deltaTime;
        }
    }

    public void HurtPanda()
    {
        curHealth--;
        healthBar.transform.localScale = new Vector3(healthBarScale.x * (curHealth / health), healthBarScale.y, healthBarScale.z);
        if (curHealth <= 0)
        {
            end = true;
            canPlayNextScreen = true;
            float pandaX = panda.transform.position.x;
            Vector3 pandaSpawnPos = new Vector3(pandaX, -3.28f, -1);
            Instantiate(pandaDown, pandaSpawnPos, Quaternion.identity, transform);
            panda.SetActive(false);
        }  
    }
}
