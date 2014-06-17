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
public class SoomlaEditorScript : ScriptableObject
{
	const string soomSettingsAssetName = "SoomlaEditorScript";
	const string soomSettingsPath = "Soomla/Resources";
	const string soomSettingsAssetExtension = ".asset";

	private static SoomlaEditorScript instance;

	static SoomlaEditorScript Instance
    {
        get
        {
            if (instance == null)
            {
				instance = Resources.Load(soomSettingsAssetName) as SoomlaEditorScript;
                if (instance == null)
                {
                    // If not found, autocreate the asset object.
					instance = CreateInstance<SoomlaEditorScript>();
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
	static CoreSoomlaPanel mCsp;
	public static void setPanel(CoreSoomlaPanel csp) {
		mCsp = csp;
	}

	public static void gui() {
		mCsp.draw();
	}
	
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
	
    public static void DirtyEditor()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(Instance);
#endif
    }

}
