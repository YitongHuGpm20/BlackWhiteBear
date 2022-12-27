/*
 * Author: Yitong Hu
 * Created: Dec 26th 2022
 * Description: Orphan's basket reached Aunty White Bear, wake her up to be saved
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.EventSystems;

public class SCP_PS_1_1_2 : SCP_PuzzleScreen, IPointerDownHandler
{
    // References
    private GameObject aunty;
    private GameObject soundIcon;
    //private GameObject orphan;

    // Variables
    [SerializeField] private float sleepRotateSpeed; //2
    [SerializeField] private float sleepRotateAmount; //3
    [SerializeField] private float mutedIconMoveSpeed; //2
    [SerializeField] private float mutedIconMoveAmount; //0.5
    [SerializeField] private float unmutedIconZoomSpeed; //4
    [SerializeField] private float unmutedIconZoomAmount; //1
    [SerializeField] private float playNextTimer; //1
    [SerializeField] private Sprite auntyAwakeTexture;
    [SerializeField] private Sprite unmutedTexture;
    private bool isAuntySleeping;
    private float mutedIconOriginY;
    private float unmutedIconOriginScaleX;
    private float unmutedIconOriginScaleY;

    // Initialization
    protected override void Awake()
    {
        base.Awake();

        // Set variables
        isAuntySleeping = false;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Find references
        aunty = transform.Find("SPR_Aunty").gameObject;
        soundIcon = transform.Find("SPR_SoundIcon").gameObject;
        //orphan = transform.Find("SPR_OrphanBasket").gameObject;

        // Set logic
        AddPhysics2DRaycaster();
        mutedIconOriginY = soundIcon.transform.position.y;
        unmutedIconOriginScaleX = soundIcon.transform.localScale.x;
        unmutedIconOriginScaleY = soundIcon.transform.localScale.y;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if(isAuntySleeping)
        {
            ZoomBigZoomSmall(soundIcon, unmutedIconOriginScaleX, unmutedIconOriginScaleY, unmutedIconZoomSpeed, unmutedIconZoomAmount);
        }
        else
        {
            RotateLeftRotateRight(aunty, sleepRotateSpeed, sleepRotateAmount);
            MoveUpMoveDown(soundIcon, mutedIconOriginY, mutedIconMoveSpeed, mutedIconMoveAmount);
        }
    }

    // Called when a timeline finished
    protected override void OnPlayableDirectorStopped(PlayableDirector p)
    {
        base.OnPlayableDirectorStopped(p);

        soundIcon.SetActive(true);
    }

    // To add raycaster for clicking sound icon
    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
    }

    // Called when click on muted icon
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        isAuntySleeping = true;
        aunty.GetComponent<SpriteRenderer>().sprite = auntyAwakeTexture;
        aunty.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        soundIcon.GetComponent<SpriteRenderer>().sprite = unmutedTexture;
        canPlayNextScreen = true;
    }
}
