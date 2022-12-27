using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using TMPro;

public class SCP_Ch1_CineManager : SCP_Base_CineManager
{
    private SCP_Ch1_GameManager gameManager;

    // Actors
    [SerializeField] private GameObject thankYou;
    
    private GameObject muted;
    private GameObject unmuted;
    private GameObject auntySleep;
    private GameObject auntyAwake;
    private GameObject wayHomeFade;
    private GameObject gameEndFade;
    private SCP_Ch1_WayHomeFade ch1EndFade;
    private SCP_Ch4_FadeInColor ch4EndFade;

    protected override void OnEnable()
    {
        base.OnEnable();
        gameManager = GameObject.Find("GameManager").GetComponent<SCP_Ch1_GameManager>();
        subtitle.GetComponent<Button>().onClick.AddListener(CloseSubtitle);
        bgm.clip = bgms[0];
        bgm.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find actors in screens
        //muted = screens[1].transform.Find("SPR_S1-2_Muted").gameObject;
        //unmuted = screens[1].transform.Find("SPR_S1-2_Unmuted").gameObject;
        //auntySleep = screens[1].transform.Find("SPR_S1-2_AuntySleep").gameObject;
        //auntyAwake = screens[1].transform.Find("SPR_S1-2_AuntyAwake").gameObject;
        //wayHomeFade = screens[9].transform.Find("SPR_S1-10_Fade").gameObject;
        //ch1EndFade = wayHomeFade.GetComponent<SCP_Ch1_WayHomeFade>();
        //gameEndFade = screens[15].transform.Find("SPR_S4-6_Grey").gameObject;
        //ch4EndFade = gameEndFade.GetComponent<SCP_Ch4_FadeInColor>();

        // Start from foreword
        subtitleText.text = subtitleContent[subtitleIndex];
    }

    // Everytime when a timeline finished
    protected override void OnPlayableDirectorStopped(PlayableDirector pd)
    {
        base.OnPlayableDirectorStopped(pd);
            
        if (timelineIndex == 0 || timelineIndex == 3) // Start S1-2: The basket stoped at bank
        {
            PlayNextScreen();
            PlayNextTimeline();
        }
        else if (timelineIndex == 1)
            muted.SetActive(true);
        else if (timelineIndex == 2)
            StartClearningMiniGame1();
        else if (timelineIndex == 4)
            StartCoroutine(DisplaySubtitle(2, "\"I've been hiding for so long...\" \n \n \"I thought I was the only one...\""));
    }

    protected override void CloseSubtitle()
    {
        if (screenIndex > 0 || screens[0].activeSelf)
        {
            if (subtitleText.text == subtitleContent[2])
            {
                subtitleText.text = subtitleContent[3];
                //StartCoroutine(subtitle.GetComponent<SCP_Subtitle>().ActivateClicking());
                return;
            }
            if (screenIndex == 9)
            {
                StopEndDialog();
                return;
            }
            if (screenIndex == 12)
            {
                StartTravel2();
                return;
            }
            if (screenIndex == 14)
            {
                ch4EndFade.fadeIn = true;
            }
            if (screenIndex == 15)
            {
                ThankYou();
                return;
            }
            PlayNextScreen();
        }
        else
        {
            screens[screenIndex].SetActive(true);
            director.Play(timelines[timelineIndex]);
        }
        base.CloseSubtitle();
    }

    private void PlayNextScreen()
    {
        // Switch screens
        screens[screenIndex].SetActive(false);
        screenIndex++;
        screens[screenIndex].SetActive(true);
    }

    private void PlayNextTimeline()
    {
        timelineIndex++;
        director.Play(timelines[timelineIndex]);
    }

    private IEnumerator DisplaySubtitle(float sec, string line)
    {
        yield return new WaitForSeconds(sec);
        screens[screenIndex].SetActive(false);
        subtitle.SetActive(true);
        subtitleText.text = line;
    }

    private IEnumerator DisplayMultipleSubtitles(float sec, string line, IEnumerator func)
    {
        yield return new WaitForSeconds(sec);
        subtitle.SetActive(true);
        subtitleText.text = line;
        if(func != null)
            StartCoroutine(func);
    }

    /// For Certain Screens //---------------------------------------------------------------------------

    // S1-2: The baby cried out loud; Called by Muted sign's script
    public void UnmuteCrying()
    {
        Debug.Log("The baby cried out loud!");
        muted.SetActive(false);
        unmuted.SetActive(true);
        auntySleep.SetActive(false);
        auntyAwake.SetActive(true);
        StartCoroutine(StartLookAtBasket());
    }

    // Start S1-3: Aunty looked inside the basket
    private IEnumerator StartLookAtBasket()
    {
        yield return new WaitForSeconds(3);
        PlayNextScreen();
        PlayNextTimeline();
    }

    // Start S1-4: Cleaning mini game 1
    private void StartClearningMiniGame1()
    {
        StartCoroutine(DisplaySubtitle(2, subtitleContent[1]));
        gameManager.miniGameCleaning = true;
    }

    // Start S1-5: Realized this is a special-color baby; Called by GameManager
    public IEnumerator StartRealizedBWB()
    {
        yield return new WaitForSeconds(2);
        
        screens[3].SetActive(false);
        screenIndex = 4;
        screens[screenIndex].SetActive(true);
        timelineIndex = 2;

        StartCoroutine(DisplaySubtitle(3, subtitleContent[2]));
    }

    // Start S1-7: Left doctor and visit bishop
    public void LeftDoctor()
    {
        StartCoroutine(DisplaySubtitle(1, subtitleContent[4]));
    }

    // Start S1-8: Left bishop and visit scientists
    public void LeftBishop()
    {
        StartCoroutine(DisplaySubtitle(1, subtitleContent[5]));
    }

    // Start S1-9: Left scientists and visit headmaster
    public void LeftScientists()
    {
        StartCoroutine(DisplaySubtitle(1, subtitleContent[6]));
    }

    // Start S1-10: Left headmaster and go home
    public void LeftHeadmaster()
    {
        screens[8].SetActive(false);
        screens[9].SetActive(true);
        screenIndex = 9;
        StartCoroutine(ch1EndFade.FadeIn());
    }

    public void DisplayEndDialog()
    {
        subtitle.SetActive(true);
        subtitleText.text = "\"So...mom...who am I?\" \n \n \"All I know is that you are always my baby.\"";
    }

    private void StopEndDialog()
    {
        subtitle.SetActive(false);
        StartCoroutine(ch1EndFade.FadeOut());
    }

    /// Temp: Ch4 --------------------------------------------------------------------------
    
    // Start S4-1: Drove around the world
    public void StartTravel()
    {
        bgm.clip = bgms[1];
        bgm.Play();

        subtitle.SetActive(false);
        screenIndex = 9;
        PlayNextScreen();
        StartCoroutine(StartWaitForBus());
    }

    // Start S4-2: Waited for bus in rain
    private IEnumerator StartWaitForBus()
    {
        yield return new WaitForSeconds(4);
        PlayNextScreen();
        timelineIndex = 2;
        PlayNextTimeline();
    }

    // Start S4-4: Drove around the world with cube panda
    public void StartTravel2()
    {
        Debug.Log("travel 2");
        subtitle.SetActive(false);
        PlayNextScreen();
        StartCoroutine(StartTalkToZoologist());
    }


    // Start S4-5: Found the zoologist
    private IEnumerator StartTalkToZoologist()
    {
        yield return new WaitForSeconds(4);
        Debug.Log("talk to zoologist");
        PlayNextScreen();
    }

    // Asked zoologist a question
    public void AskZoologist()
    {
        StartCoroutine(DisplaySubtitle(0, "\"Hey zoologist! Have you seen any bear as strange-looking as us?\" \n \n" +
            "\"Strange? You are pandas, a different species of bears.\" \n \n" +
            "\"Wait, what?\""));
    }

    // Game Ending
    public void DisplayEndDialog4()
    {
        StartCoroutine(DisplaySubtitle(0, "\"See? There isn't a NORMAL bear. Everyone is unique. " +
            "The reason why people think you are abnormal is because they never see anyone as cool " +
            "as you are.\""));
    }

    // Temp: Thank you for playing!
    private void ThankYou()
    {
        thankYou.SetActive(true);
    }
}
