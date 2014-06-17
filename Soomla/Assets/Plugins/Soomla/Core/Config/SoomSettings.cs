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


namespace Soomla
{
	/// <summary>
	/// This class holds the store's configurations. 
	/// </summary>
	public class SoomSettings : ScriptableObject
	{
		const string soomSettingsAssetName = "SoomSettings";

		public static string AND_PUB_KEY_DEFAULT = "YOUR GOOGLE PLAY PUBLIC KEY";
		public static string ONLY_ONCE_DEFAULT = "SET ONLY ONCE";

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
					}
				}
				return instance;
			}
		}

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
					SoomlaEditorScript.DirtyEditor ();
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
					SoomlaEditorScript.DirtyEditor();
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
					SoomlaEditorScript.DirtyEditor ();
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
					SoomlaEditorScript.DirtyEditor ();
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
					SoomlaEditorScript.DirtyEditor();
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
					SoomlaEditorScript.DirtyEditor();
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
					SoomlaEditorScript.DirtyEditor();
				}
			}
		}

	}
}