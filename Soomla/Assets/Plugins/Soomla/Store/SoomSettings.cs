/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.

using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif

/// <summary>
/// This class holds the store's configurations. 
/// </summary>
public class SoomSettings : ScriptableObject
{
	public static string AND_PUB_KEY_DEFAULT = "YOUR GOOGLE PLAY PUBLIC KEY";
	public static string ONLY_ONCE_DEFAULT = "SET ONLY ONCE";

	const string soomSettingsAssetName = "SoomSettings";
	const string soomSettingsPath = "Soomla/Resources";
	const string soomSettingsAssetExtension = ".asset";

	private static SoomSettings instance;

	static SoomSettings Instance
    {
        get
        {
            if (instance == null)
            {
				instance = Resources.Load(soomSettingsAssetName) as SoomSettings;
                if (instance == null)
                {
                    // If not found, autocreate the asset object.
					instance = CreateInstance<SoomSettings>();
#if UNITY_EDITOR
                    string properPath = Path.Combine(Application.dataPath, soomSettingsPath);
                    if (!Directory.Exists(properPath))
                    {
                        AssetDatabase.CreateFolder("Assets/Soomla", "Resources");
                    }

                    string fullPath = Path.Combine(Path.Combine("Assets", soomSettingsPath),
                                                   soomSettingsAssetName + soomSettingsAssetExtension
                                                  );
                    AssetDatabase.CreateAsset(instance, fullPath);
#endif
                }
            }
            return instance;
        }
    }

#if UNITY_EDITOR
	[MenuItem("Window/Soomla/Edit Settings")]
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }

	[MenuItem("Window/Soomla/Framework Page")]
    public static void OpenFramework()
    {
        string url = "https://www.github.com/soomla/unity3d-store";
        Application.OpenURL(url);
    }

	[MenuItem("Window/Soomla/Report an issue")]
    public static void OpenIssue()
    {
		string url = "https://answers.soom.la";
        Application.OpenURL(url);
    }
#endif
	

	[SerializeField]
	private bool debugMsgs = false;
    [SerializeField]
    private bool iosSSV = false;
	[SerializeField]
	private bool gPlayBP = false;
	[SerializeField]
	private bool amazonBP = false;
	[SerializeField]
	private bool androidTestPurchases = false;
	[SerializeField]
	private string androidPublicKey = "GOOGLE PLAY PUBLIC KEY";
    [SerializeField]
	private string soomlaSecret = "SET ONLY ONCE";


	public static string SoomlaSecret
	{
		get { return Instance.soomlaSecret; }
		set 
		{
			if (Instance.soomlaSecret != value)
			{
				Instance.soomlaSecret = value;
				DirtyEditor ();
			}
		}
	}

	public static bool DebugMessages
	{
		get { return Instance.debugMsgs; }
		set
		{
			if (Instance.debugMsgs != value)
			{
				Instance.debugMsgs = value;
				DirtyEditor();
			}
		}
	}

	public static string AndroidPublicKey
	{
		get { return Instance.androidPublicKey; }
		set 
		{
			if (Instance.androidPublicKey != value)
			{
				Instance.androidPublicKey = value;
				DirtyEditor ();
			}
		}
	}

	public static bool AndroidTestPurchases
	{
		get { return Instance.androidTestPurchases; }
		set 
		{
			if (Instance.androidTestPurchases != value)
			{
				Instance.androidTestPurchases = value;
				DirtyEditor ();
			}
		}
	}

	public static bool IosSSV
	{
		get { return Instance.iosSSV; }
		set
		{
			if (Instance.iosSSV != value)
            {
				Instance.iosSSV = value;
				DirtyEditor();
            }
        }
    }

	public static bool GPlayBP
	{
		get { return Instance.gPlayBP; }
		set
		{
			if (Instance.gPlayBP != value)
			{
				Instance.gPlayBP = value;
				DirtyEditor();
			}
		}
	}

	public static bool AmazonBP
	{
		get { return Instance.amazonBP; }
		set
		{
			if (Instance.amazonBP != value)
			{
				Instance.amazonBP = value;
				DirtyEditor();
			}
		}
	}

//    public static bool Logging
//    {
//        get { return Instance.logging; }
//        set
//        {
//            if (Instance.logging != value)
//            {
//                Instance.logging = value;
//                DirtyEditor();
//            }
//        }
//    }




    private static void DirtyEditor()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(Instance);
#endif
    }

}
