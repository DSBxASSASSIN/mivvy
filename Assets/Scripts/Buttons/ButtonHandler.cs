using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{

    [HideInInspector] public bool EmotionIsForced = false;
    [HideInInspector] public int scaredVotes;
    [HideInInspector] public int happyVotes;
    [HideInInspector] public int angryVotes;

    public  bool scaredMostVoted;
    public bool happyMostVoted;
    public bool angryMostVoted;
    
    [HideInInspector] public static ButtonHandler instance;

    //!Base is Happy
    [Header ("Happy base")]
    [SerializeField] VideoClip happy_happy;
    [SerializeField] VideoClip happy_angry;
    [SerializeField] VideoClip happy_scared;

    //!Base is angry
    [Header("Angry base")]
    [SerializeField] VideoClip angry_happy;
    [SerializeField] VideoClip angry_angry;
    [SerializeField] VideoClip angry_scared;

    //!Base is scared
    [Header("Scared base")]
    [SerializeField] VideoClip scared_happy;
    [SerializeField] VideoClip scared_angry;
    [SerializeField] VideoClip scared_scared;

    //! idles
    [Header("idles")]
    [SerializeField] VideoClip happy_idle;
    [SerializeField] VideoClip angry_idle;
    [SerializeField] VideoClip scared_idle;

    public VideoPlayer videoPlayer;
    public GameObject[] backgrounds = new GameObject[5];
    public TextMeshProUGUI[] textjes = new TextMeshProUGUI[3];

    public AudioSource[] buttonSounds;

    bool isScared = false;
    bool isAngry = false;
    // Start is called before the first frame update
    void Start(){
        buttonSounds[0].volume = 0f;
        buttonSounds[1].volume = 0f;
        buttonSounds[2].volume = 0f;

        if(instance == null){
            instance = this;
        }
        scaredMostVoted = false;
        happyMostVoted = false;
        angryMostVoted = false;
        videoPlayer.clip = happy_idle;
    }

    // Update is called once per frame
    void Update(){
        if(!EmotionIsForced)
        SetMostVoted();

        textjes[0].text = "Laten schrikken: " + scaredVotes.ToString();
        textjes[1].text = "Kusje geven: " + happyVotes.ToString();
        textjes[2].text = "Tong uitsteken: " + angryVotes.ToString();
    }

    public void OnScaredClick(){
        scaredVotes += 1;
        buttonSounds[0].volume = 1f;
        buttonSounds[0].Play();
        StartCoroutine(ScaredAnimation());
    }
    public void OnHappyClick(){
        happyVotes += 1;
        buttonSounds[1].volume = 1f;
        buttonSounds[1].Play();
        StartCoroutine(HappyAnimation());
    }
    public void OnAngryClick(){
        angryVotes += 1;
        buttonSounds[2].volume = 1f;
        buttonSounds[2].Play();
        StartCoroutine(AngryAnimation());
    }

    private IEnumerator ScaredAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        if (isScared && !isAngry)
        {
            videoPlayer.clip = scared_scared;
            yield return new WaitForSeconds(7f);
            StartCoroutine(BackgroundImages());
            //videoPlayer.clip = scared_idle;
        }
        else if (!isScared && !isAngry)
        {
            videoPlayer.clip = happy_scared;
            yield return new WaitForSeconds(7f);
            StartCoroutine(BackgroundImages());
          //  videoPlayer.clip = happy_idle;
        }
        else if (!isScared && isAngry)
        {
            videoPlayer.clip = angry_scared;
            yield return new WaitForSeconds(7f);
            StartCoroutine(BackgroundImages());
            //videoPlayer.clip = angry_idle;
        }
    }
    private IEnumerator AngryAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        if (isScared && !isAngry)
        {
            videoPlayer.clip = scared_angry;
            yield return new WaitForSeconds(7f);
            StartCoroutine(BackgroundImages());
            //videoPlayer.clip = scared_idle;
        }
        else if (!isScared && !isAngry)
        {
            videoPlayer.clip = happy_angry;
            yield return new WaitForSeconds(7f);
            StartCoroutine(BackgroundImages());
            //videoPlayer.clip = happy_idle;
        }
        else if (!isScared && isAngry)
        {
            videoPlayer.clip = angry_angry;
            yield return new WaitForSeconds(7f);
            StartCoroutine(BackgroundImages());
           /// videoPlayer.clip = angry_idle;
        }
    }
    private IEnumerator HappyAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        if (isScared && !isAngry)
        {
            videoPlayer.clip = scared_happy;
            yield return new WaitForSeconds(7f);
            StartCoroutine(BackgroundImages());
            //videoPlayer.clip = scared_idle;
        }
        else if (!isScared && !isAngry)
        {
            videoPlayer.clip = happy_happy;
            yield return new WaitForSeconds(7f);
            StartCoroutine(BackgroundImages());
            //videoPlayer.clip = happy_idle;
        }
        else if (!isScared && isAngry)
        {
            videoPlayer.clip = angry_happy;
            yield return new WaitForSeconds(7f);
            StartCoroutine(BackgroundImages());
            //videoPlayer.clip = angry_idle;
        }
    }

    IEnumerator BackgroundImages()
    {
        backgrounds[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        backgrounds[1].SetActive(true);
        backgrounds[0].SetActive(false);
        yield return new WaitForSeconds(5f);
        backgrounds[2].SetActive(true);
        backgrounds[1].SetActive(false);
        yield return new WaitForSeconds(8f); 
        backgrounds[3].SetActive(true);
        backgrounds[2].SetActive(false);
        yield return new WaitForSeconds(8f);
        backgrounds[4].SetActive(true);
        backgrounds[3].SetActive(false);
        yield return new WaitForSeconds(5f);
        backgrounds[4].SetActive(false);
        PlayIdle();
    }

    void PlayIdle()
    {
        if (scaredVotes > angryVotes && scaredVotes >= 25)
        {
            isScared = true;
            isAngry = false;
            videoPlayer.clip = scared_idle;
        }
        else if (angryVotes > scaredVotes && angryVotes >= 25)
        {
            isScared = false;
            isAngry = true;
            videoPlayer.clip = angry_idle;
        }
        else if (angryVotes < 25 && scaredVotes < 25)
        {
            isScared = false;
            isAngry = false;
            videoPlayer.clip = happy_idle;
        }
    }

    private int CheckgreatestNumber(int i, int j, int k){
        int ret = Mathf.Max(i, j);
        ret = Mathf.Max(ret, k);
        return ret;
    }

    private void SetMostVoted(){
    int greatestNumber = CheckgreatestNumber(scaredVotes, happyVotes, angryVotes);
        switch(greatestNumber){
            case var value when value == scaredVotes:
                scaredMostVoted = true;
                happyMostVoted = false;
                angryMostVoted = false;
            break;
            case var value when value == happyVotes:
                scaredMostVoted = false;
                happyMostVoted = true;
                angryMostVoted = false;
                break;
            case var value when value == angryVotes:
                scaredMostVoted = false;
                happyMostVoted = false;
                angryMostVoted = true;
                break;
            default:
                scaredMostVoted = false;
                happyMostVoted = false;
                angryMostVoted = false;
                break;
        }

    } 

}
