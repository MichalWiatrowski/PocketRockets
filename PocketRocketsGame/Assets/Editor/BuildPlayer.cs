using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class BuildPlayer : MonoBehaviour {

    [MenuItem("Pocket Rocket Tools/Build/Build Server")]
    public static void BuildServerGame()
    {
        
        //Select the directory to build the server file
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Server", "", "");

        //Create build options
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        //select scenes for the build
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/server/serverManagerScene.unity", "Assets/Scenes/server/menu.unity", "Assets/Scenes/server/track.unity" };
        buildPlayerOptions.locationPathName = path + "/server.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.options = BuildOptions.None;
        // Build the player.
        BuildPipeline.BuildPlayer(buildPlayerOptions);

        
       // Run the game (Process class from System.Diagnostics).
       // Process proc = new Process();
       // proc.StartInfo.FileName = path + "/Server.exe";
       // proc.Start();
    }

    [MenuItem("Pocket Rocket Tools/Build/Build Client")]


    public static void BuildClientGame()
    {
        //Select the directory to build the client file
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Client", "", "");

        //Create build options
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        //select scenes for the build
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/client/clientManagerScene.unity", "Assets/Scenes/client/menu.unity", "Assets/Scenes/client/gameScreen.unity" };
        buildPlayerOptions.locationPathName = path + "/client.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.options = BuildOptions.None;
        // Build the player.
        BuildPipeline.BuildPlayer(buildPlayerOptions);

        // Run the game (Process class from System.Diagnostics).
        //Process proc = new Process();
        // proc.StartInfo.FileName = path + "/Client.exe";
        // proc.Start();
    }


}
