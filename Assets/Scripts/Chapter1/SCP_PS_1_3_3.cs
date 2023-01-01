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
    private GameObject panda;
    private GameObject prompt;

    // Variables
    [SerializeField] private Texture[] fallingItems;
    [SerializeField] private float pandaSpeed; //10f
    [SerializeField] private float promptMoveSpeed; //8
    [SerializeField] private float promptMoveAmount; //0.5
    private Vector3 touchPos;
    private float pandaPosY;
    private float promptOriginY;
    private bool end;
    private bool givenInput;

    // Initialization
    protected override void Awake()
    {
        base.Awake();

        givenInput = false;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Find references
        panda = transform.Find("SPR_Panda").gameObject;
        pandaPosY = panda.transform.position.y;
        prompt = transform.Find("SPR_TapPrompt").gameObject;

        // Set logic
        end = false;
        touchPos = panda.transform.position;
        promptOriginY = prompt.transform.position.y;
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
            }
            panda.transform.position = Vector3.MoveTowards(panda.transform.position, touchPos, pandaSpeed * Time.deltaTime);
        }
    }
}
