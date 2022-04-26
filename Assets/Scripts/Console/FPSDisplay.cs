using UnityEngine;
public class FPSDisplay : MonoBehaviour {
    bool showStats;


    // fps stats
    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    private int frameRate;

    public static FPSDisplay instance;

    private void Awake(){
        if(instance == null) instance = this;
    }


    private void Update() {
        if(!showStats) { return; }

        time += Time.deltaTime;

        frameCount++;

        if(time >= pollingTime){
            frameRate = Mathf.RoundToInt(frameCount / time);

            time -= pollingTime;
            frameCount = 0;
        }
        
    }

    private void OnGUI(){

        if(!showStats) { return; }

        GUI.Box(new Rect(Screen.width - 65, 5, 60, 30), "");
        GUI.Label(new Rect(Screen.width - 60, 10, 50, 20), "FPS: " + frameRate.ToString());
    }

    public void toggleShowStats(){
        showStats = !showStats;
    }
}
