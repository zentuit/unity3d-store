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
#endif

namespace Soomla 
{

#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	/// <summary>
	/// This class holds the store's configurations. 
	/// </summary>
	public class CoreSoomlaPanel : ISoomlaPanelPainter
	{
#if UNITY_EDITOR
		bool showAndroidSettings = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android);
		bool showIOSSettings = (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iPhone);
		
		
		GUILayoutOption fieldHeight = GUILayout.Height(16);
		GUILayoutOption fieldWidth = GUILayout.Width(120);
		GUILayoutOption spaceWidth = GUILayout.Width(24);
		
		GUIContent emptyContent = new GUIContent("");
		
		GUIContent soomlaSecLabel = new GUIContent("Soomla Secret [?]:", "All the user information will be encrypted using this secret.");
		
		GUIContent playLabel = new GUIContent("Google Play");
		GUIContent amazonLabel = new GUIContent("Amazon");
		GUIContent publicKeyLabel = new GUIContent("API Key [?]:", "The API key from Google Play dev console (just in case you're using Google Play as billing provider).");
		GUIContent testPurchasesLabel = new GUIContent("Test Purchases [?]:", "Check if you want to allow purchases of Google's test product ids.");
		GUIContent packageNameLabel = new GUIContent("Package Name [?]", "Your package as defined in Unity.");
		
		GUIContent iosSsvLabel = new GUIContent("Receipt Validation [?]:", "Check if you want your purchases validated with SOOMLA Server Side Protection Service.");
		
		GUIContent debugMsgsLabel = new GUIContent("Debug Messages [?]:", "Check if you want to show debug messages in the log (iOS and Android).");
		
		GUIContent frameworkVersion = new GUIContent("Framework Version [?]", "The SOOMLA Framework version. ");
		GUIContent buildVersion = new GUIContent("Framework Build [?]", "The SOOMLA Framework build.");


		static CoreSoomlaPanel instance = new CoreSoomlaPanel();
		static CoreSoomlaPanel()
	    {
			SoomlaEditorScript.addPainter(instance);
	    }

		public void OnEnable() {
			// Generating AndroidManifest.xml
			ManifestTools.GenerateManifest();
		}

		public void OnInspectorGUI() {
			SoomlaGUI();
			EditorGUILayout.Space();
			AndroidGUI();
			EditorGUILayout.Space();
			IOSGUI();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			AboutGUI();
		}

		private static string iosRootPath = Application.dataPath + "/Soomla/compilations/ios/";
		private bool movediOSDebugLib = false;
		private void SoomlaGUI()
		{	
			EditorGUILayout.BeginHorizontal();
			string url = "file://" + Application.dataPath + @"/Soomla/Resources/soom_logo.png";
			WWW www = new WWW(url);
			while(!www.isDone){}
			GUIContent logoImgLabel = new GUIContent (www.texture);
			EditorGUILayout.LabelField(logoImgLabel, GUILayout.MaxHeight(70), GUILayout.ExpandWidth(true));
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.HelpBox("Make sure you fill out all the information below", MessageType.None);
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(soomlaSecLabel, fieldWidth, fieldHeight);
			SoomSettings.SoomlaSecret = EditorGUILayout.TextField(SoomSettings.SoomlaSecret, fieldHeight);
			EditorGUILayout.EndHorizontal();
			
			SoomSettings.DebugMessages = EditorGUILayout.Toggle(debugMsgsLabel, SoomSettings.DebugMessages);
			
			if (SoomSettings.DebugMessages && !movediOSDebugLib) {
				try {
					FileUtil.DeleteFileOrDirectory(Application.dataPath + "/Plugins/iOS/libSoomlaIOSStore.a");
					FileUtil.DeleteFileOrDirectory(Application.dataPath + "/Plugins/iOS/libSoomlaIOSCore.a");
					FileUtil.DeleteFileOrDirectory(Application.dataPath + "/Plugins/iOS/libUnityiOSStore.a");
					FileUtil.CopyFileOrDirectory(iosRootPath + "iOSStore/debug/libSoomlaIOSStore.a",
					                             Application.dataPath + "/Plugins/iOS/libSoomlaIOSStore.a");
					FileUtil.CopyFileOrDirectory(iosRootPath + "iOSStore/debug/libSoomlaIOSCore.a",
					                             Application.dataPath + "/Plugins/iOS/libSoomlaIOSCore.a");
					FileUtil.CopyFileOrDirectory(iosRootPath + "debug/libUnityiOSStore.a",
					                             Application.dataPath + "/Plugins/iOS/libUnityiOSStore.a");
				} catch {}
				movediOSDebugLib = true;
			} if (!SoomSettings.DebugMessages && movediOSDebugLib) {
				try {
					FileUtil.DeleteFileOrDirectory(Application.dataPath + "/Plugins/iOS/libSoomlaIOSStore.a");
					FileUtil.DeleteFileOrDirectory(Application.dataPath + "/Plugins/iOS/libSoomlaIOSCore.a");
					FileUtil.DeleteFileOrDirectory(Application.dataPath + "/Plugins/iOS/libUnityiOSStore.a");
					FileUtil.CopyFileOrDirectory(iosRootPath + "iOSStore/release/libSoomlaIOSStore.a",
					                             Application.dataPath + "/Plugins/iOS/libSoomlaIOSStore.a");
					FileUtil.CopyFileOrDirectory(iosRootPath + "iOSStore/release/libSoomlaIOSCore.a",
					                             Application.dataPath + "/Plugins/iOS/libSoomlaIOSCore.a");
					FileUtil.CopyFileOrDirectory(iosRootPath + "release/libUnityiOSStore.a",
					                             Application.dataPath + "/Plugins/iOS/libUnityiOSStore.a");
				} catch {}
				movediOSDebugLib = false;
			}
			
			EditorGUILayout.Space();
		}
		
		private void IOSGUI()
		{
			showIOSSettings = EditorGUILayout.Foldout(showIOSSettings, "iOS Build Settings");
			if (showIOSSettings)
			{
				SoomSettings.IosSSV = EditorGUILayout.Toggle(iosSsvLabel, SoomSettings.IosSSV);
			}
			EditorGUILayout.Space();
		}
		
		private bool playUpdate = false;
		private bool amazonUpdate = false;
		private void AndroidGUI()
		{
			showAndroidSettings = EditorGUILayout.Foldout(showAndroidSettings, "Android Settings");
			if (showAndroidSettings)
			{
				EditorGUILayout.BeginHorizontal();
				SelectableLabelField(packageNameLabel, PlayerSettings.bundleIdentifier);
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.Space();
				EditorGUILayout.HelpBox("Billing Service Selection", MessageType.None);
				
				if (!SoomSettings.GPlayBP && !SoomSettings.AmazonBP) {
					SoomSettings.GPlayBP = true;
				}
				
				SoomSettings.GPlayBP = EditorGUILayout.ToggleLeft(playLabel, SoomSettings.GPlayBP);
				
				if (SoomSettings.GPlayBP) {
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.Space();
					EditorGUILayout.LabelField(publicKeyLabel, fieldWidth, fieldHeight);
					SoomSettings.AndroidPublicKey = EditorGUILayout.TextField(SoomSettings.AndroidPublicKey, fieldHeight);
					EditorGUILayout.EndHorizontal();
					
					EditorGUILayout.Space();
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(emptyContent, spaceWidth, fieldHeight);
					SoomSettings.AndroidTestPurchases = EditorGUILayout.Toggle(testPurchasesLabel, SoomSettings.AndroidTestPurchases);
					EditorGUILayout.EndHorizontal();
				}
				
				if (SoomSettings.GPlayBP && !playUpdate) {
					playUpdate = true;
					amazonUpdate = false;
					
					SoomSettings.AmazonBP = false;
					ManifestTools.GenerateManifest();
					SoomlaAndroidUtil.handlePlayBPJars(false);
					SoomlaAndroidUtil.handleAmazonBPJars(true);
				}
				
				
				SoomSettings.AmazonBP = EditorGUILayout.ToggleLeft(amazonLabel, SoomSettings.AmazonBP);
				
				if (SoomSettings.AmazonBP && !amazonUpdate) {
					playUpdate = false;
					amazonUpdate = true;
					
					SoomSettings.GPlayBP = false;
					ManifestTools.GenerateManifest();
					SoomlaAndroidUtil.handlePlayBPJars(true);
					SoomlaAndroidUtil.handleAmazonBPJars(false);
				}
				
				
				
				if (!SoomlaAndroidUtil.IsSetupProperly())
				{
					var msg = "You have errors in your Android setup. More info in the SOOMLA docs.";
					switch (SoomlaAndroidUtil.SetupError)
					{
					case SoomlaAndroidUtil.ERROR_NO_SDK:
						msg = "You need to install the Android SDK!  Set the location of Android SDK in: " + (Application.platform == RuntimePlatform.OSXEditor ? "Unity" : "Edit") + "->Preferences->External Tools";
						break;
					case SoomlaAndroidUtil.ERROR_NO_KEYSTORE:
						msg = "Your defined keystore doesn't exist! You'll need to create a debug keystore or point to your keystore in 'Publishing Settings' from 'File -> Build Settings -> Player Settings...'";
						break;
					}
					
					EditorGUILayout.HelpBox(msg, MessageType.Error);
				}
			}
			EditorGUILayout.Space();
		}
		
		private void AboutGUI()
		{
			EditorGUILayout.HelpBox("SOOMLA SDK Info", MessageType.None);
			SelectableLabelField(frameworkVersion, "1.5.0");
			SelectableLabelField(buildVersion, "1");
			EditorGUILayout.Space();
		}
		
		private void SelectableLabelField(GUIContent label, string value)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(label, GUILayout.Width(140), fieldHeight);
			EditorGUILayout.SelectableLabel(value, fieldHeight);
			EditorGUILayout.EndHorizontal();
		}
#endif
	}
}