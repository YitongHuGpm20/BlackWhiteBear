/*
 * Author: Yitong Hu
 * Created: Dec 28th 2022
 * Description: Doctor trys to give baby treatment
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_PS_1_2_3 : SCP_PuzzleScreen
{
    // References
    private GameObject baby;
    private GameObject doctor;
    private Animator babyAnimator;
    private GameObject prompt;

    // Variables
    [SerializeField] private float babyMoveSpeed; //1
    [SerializeField] private float doctorMoveSpeed; //0.5
    [SerializeField] private float promptMoveSpeed; //8
    [SerializeField] private float promptMoveAmount; //0.5
    private Vector3 babyTarget;
    private Vector3 doctorTarget;
    private bool hittedButt;
    private bool givenInput;
    private float promptOriginY;

    // Initialization
    protected override void Awake()
    {
        base.Awake();

        hittedButt = false;
        givenInput = false;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Find references
        baby = transform.Find("SPR_Baby").gameObject;
        doctor = transform.Find("SPR_Doctor").gameObject;
        babyAnimator = baby.GetComponent<Animator>();
        prompt = transform.Find("SPR_TapPrompt").gameObject;

        // Set logic
        babyTarget = baby.transform.position;
        doctorTarget = babyTarget;
        promptOriginY = prompt.transform.position.y;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Display Tap-to-move prompt
        if (!givenInput)
            MoveUpMoveDown(prompt, promptOriginY, promptMoveSpeed, promptMoveAmount);

        // To make characters move after tap/click
        if (!hittedButt)
        {
            // Get click/tap location
            if (Input.GetMouseButtonDown(0))
            {
                if(!givenInput)
                {
                    givenInput = true;
                    prompt.SetActive(false);
                }
                babyTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                babyTarget.z = -1;
                if (babyTarget.y > -0.77f) babyTarget.y = -0.77f;
                if (babyTarget.y < -2.33f) babyTarget.y = -2.33f;
                if (babyTarget.x < -1f) babyTarget.x = -1f;
                if (babyTarget.x > -0.12f) babyTarget.x = -0.12f;
            }

            // Make baby move to target
            baby.transform.position = Vector3.MoveTowards(baby.transform.position, babyTarget, babyMoveSpeed * Time.deltaTime);
            if (baby.transform.position == babyTarget) babyAnimator.SetBool("isMoving", false);
            else babyAnimator.SetBool("isMoving", true);

            // Make doctor move to baby
            doctorTarget = baby.transform.position;
            doctorTarget.z = -2;
            if (doctorTarget.x < 0.58f) doctorTarget.x = 0.58f;
            if (doctorTarget.y > -1f) doctorTarget.y = -1f;
            if (doctorTarget.y < -1.96f) doctorTarget.y = -1.96f;
            doctor.transform.position = Vector3.MoveTowards(doctor.transform.position, doctorTarget, doctorMoveSpeed * Time.deltaTime);
        }
        
    }

    // Called by SCP_1_2_3_Needle, to start next screen
    public void NeedleHitButt()
    {
        hittedButt = true;
        canPlayNextScreen = true;
    }
}
