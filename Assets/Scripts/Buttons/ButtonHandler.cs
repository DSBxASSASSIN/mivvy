using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    
    [HideInInspector]public bool EmotionIsForced;
    [HideInInspector]public int scaredVotes;
    [HideInInspector]public int happyVotes;
    [HideInInspector]public int angryVotes;

    [SerializeField] private bool scaredMostVoted;
    [SerializeField] private bool happyMostVoted;
    [SerializeField] private bool angryMostVoted;
    
    public ButtonHandler instance;
    
    
    // Start is called before the first frame update
    void Start(){

        if(instance == null){
            instance = this;
        }
        scaredMostVoted = false;
        happyMostVoted = false;
        angryMostVoted = false;
    }

    // Update is called once per frame
    void Update(){
        if(!EmotionIsForced)
        SetMostVoted();
    }

    public void OnScaredClick(){
        scaredVotes += 1;
    }
    public void OnHappyClick(){
        happyVotes += 1;

    }
    public void OnAngryClick(){
        angryVotes += 1;
    }

    // public void force

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
