using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    bool showHelp;

    bool showStats;

    string input;

    public static DebugCommand FORCE_EMOTION_SCRARED;
    public static DebugCommand FORCE_EMOTION_HAPPY;
    public static DebugCommand FORCE_EMOTION_ANGRY;
    public static DebugCommand TURN_FORCE_EMOTION_OFF;
    public static DebugCommand SHOW_FPS;
    public static DebugCommand HELP;

    public List<object> commandList;

    public void OnToggleDebug(InputValue value) {
        showConsole = !showConsole;
    }
    
    public void OnReturn(InputValue value) {
        if (showConsole) {
            HandleInPut();
            input = "";
        }
    }

    private void Awake() {

        FORCE_EMOTION_SCRARED = new DebugCommand("force_emotion_scared", "makes the current emotion scared.", "force_emotion_scared", () => {
            
        });

        FORCE_EMOTION_HAPPY = new DebugCommand("force_emotion_happy", "makes the current emotion happy.", "force_emotion_happy", () => {

        });

        FORCE_EMOTION_ANGRY = new DebugCommand("force_emotion_angry", "makes the current emotion angry.", "force_emotion_angry", () => {

        });

        SHOW_FPS = new DebugCommand("show_fps", "shows the current fps", "show_fps", () => {
            FPSDisplay.instance.toggleShowStats();
        });

        HELP = new DebugCommand("help", "shows a list of commands", "help", () => {
            showHelp = true;
        });



        commandList = new List<object> {
            FORCE_EMOTION_SCRARED,
            FORCE_EMOTION_HAPPY,
            FORCE_EMOTION_ANGRY,
            SHOW_FPS,
            HELP
        };
    }


    Vector2 scroll;
    private void OnGUI() {

        if(!showConsole){  showHelp = false; return; }

        float y = 0f;

        if (showHelp) {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for(int i = 0; i < commandList.Count; i++) {
                DebugCommandbase command = commandList[i] as DebugCommandbase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100f;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    private void HandleInPut() {

        string[] properties = input.Split(' ');

        for(int i=0; i < commandList.Count; i++) {
            DebugCommandbase commandbase = commandList[i] as DebugCommandbase;
            Debug.Log("commandbase.commandid: " + commandbase.commandId);
            if (input.Contains(commandbase.commandId)) {

                if(commandList[i] as DebugCommand != null) {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if(commandList[i] as DebugCommand<int> != null) {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
            }
        }
    }
}
