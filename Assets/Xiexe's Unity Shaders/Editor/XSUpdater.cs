using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System.Collections;
using System.IO;

public class XSUpdater : EditorWindow
{
    [MenuItem("Tools/Xiexe/About - Updater")]
    // Use this for initialization
    static public void Init()
    {
        XSUpdater window = EditorWindow.GetWindow<XSUpdater>(false, "XSToon: About & Updater", true);
        window.minSize = new Vector2(300, 345);
        window.maxSize = new Vector2(300, 346);


    }




    static string url = "https://api.github.com/repos/Xiexe/Xiexes-Unity-Shaders/releases/latest";
    static UnityWebRequest www;

    public void OnGUI()
    {
        //Do updatery things
        XSStyles.Separator();
        if (GUILayout.Button("Fetch Update"))
        {
            req();
            EditorApplication.update += EditorUpdate;
        }

        XSStyles.HelpBox("Your current version is: v" + XSStyles.ver + "\n\nTo check for updates, use the update button. If you choose to download an update, you will need to manually overwrite the old install by extracting the .zip into the project using the windows explorer. \n\nDo not drag the update directly into Unity - it won't ask to overwrite - it'll just create a duplicate and break.", MessageType.Info);


        //
        XSStyles.Separator();
        XSStyles.doLabel("Thank you to my patreon supporters, and the people who have helped me along the way, you guys are great! \n\n Patrons as of " + XSStyles.ver + "\n - Wandering Youth \n - SaltQueen \n - Your name could be here!");

        XSStyles.Separator();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        XSStyles.discordButton(50, 50);
        GUILayout.Space(8);
        XSStyles.patreonButton(50, 50);
        XSStyles.githubButton(50, 50);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }


    static void req()
    {
        www = UnityWebRequest.Get(url);
        www.Send();
        Debug.Log("Checking for updates...");
    }

    static void EditorUpdate()
    {
        while (!www.isDone)
            return;

        if (www.isNetworkError)
            Debug.Log(www.error);
        else
            updateHandler(www.downloadHandler.text);

        EditorApplication.update -= EditorUpdate;
    }

    static void updateHandler(string apiResult)
    {
        gitAPI git = JsonUtility.FromJson<gitAPI>(apiResult);


        bool option = EditorUtility.DisplayDialog("XSToon: Updater",
                                    "You are on version: \nv" + XSStyles.ver + "\n\nThe latest version is: \n" + git.tag_name + "\n\n You can view the changelog either on my Discord, or at the Github page for this release." + "\n\nWould you like to update?",
                                    "Download", "Cancel");

        switch (option)
        {
            case true:
                Application.OpenURL(git.zipball_url);
                break;

            case false:
                Debug.Log("Cancelled Update.");
                break;
        }

        // Debug.Log("Latest Release Information:");
        // Debug.Log("Name: " + git.name);
        // Debug.Log("URL: " + git.html_url);
        // Debug.Log("Release Date: " + git.published_at);
    }


    public class gitAPI
    {
        public string name;
        public string tag_name;
        public string html_url;
        public string published_at;
        public string zipball_url;
        public string body;
    }

}