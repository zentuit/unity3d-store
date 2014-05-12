using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace Soomla {
	public class StoreControllerAndroid : StoreController {
#if UNITY_ANDROID && !UNITY_EDITOR
		private static AndroidJavaObject jniStoreController = null;

		protected override void _initialize(IStoreAssets storeAssets) {
			if (string.IsNullOrEmpty(SoomSettings.AndroidPublicKey)) {
				StoreUtils.LogError(TAG, "SOOMLA/UNITY MISSING publickKey !!! Stopping here !!");
				throw new ExitGUIException();
			}
			
			if (SoomSettings.AndroidPublicKey==SoomSettings.AND_PUB_KEY_DEFAULT) {
				StoreUtils.LogError(TAG, "SOOMLA/UNITY You have to change android publicKey !!! Stopping here !!");
				throw new ExitGUIException();
			}

			StoreInfo.Initialize(storeAssets);

			AndroidJNI.PushLocalFrame(100);
			//init EventHandler
			using(AndroidJavaClass jniEventHandler = new AndroidJavaClass("com.soomla.unity.EventHandler")) {
				jniEventHandler.CallStatic("initialize");
			}
			using(AndroidJavaObject jniStoreAssetsInstance = new AndroidJavaObject("com.soomla.unity.StoreAssets")) {
				using(AndroidJavaClass jniStoreControllerClass = new AndroidJavaClass("com.soomla.store.StoreController")) {
					jniStoreController = jniStoreControllerClass.CallStatic<AndroidJavaObject>("getInstance");
					jniStoreController.Call<bool>("initialize", jniStoreAssetsInstance, SoomSettings.AndroidPublicKey, SoomSettings.CustomSecret);
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
		}

		protected override void _setupSoomSec() {
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaClass jniStoreAssets = new AndroidJavaClass("com.soomla.unity.StoreAssets")) {
				jniStoreAssets.CallStatic("setSoomSec", SoomSettings.SoomSecret);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
		}

		protected override void _buyMarketItem(string productId) {
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniPurchasableItem = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.store.data.StoreInfo"),"getPurchasableItem", productId)) {
				AndroidJNIHandler.CallVoid(jniStoreController, "buyWithMarket", 
				                           jniPurchasableItem.Call<AndroidJavaObject>("getPurchaseType").Call<AndroidJavaObject>("getMarketItem"), 
				                           "");
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
		}

		protected override void _refreshInventory() {
			AndroidJNI.PushLocalFrame(100);
			jniStoreController.Call("refreshInventory", true);
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
		}

		protected override void _restoreTransactions() {
			AndroidJNI.PushLocalFrame(100);
			jniStoreController.Call("refreshInventory", false);
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
		}

		protected override void _startIabServiceInBg() {
			AndroidJNI.PushLocalFrame(100);
			jniStoreController.Call("startIabServiceInBg");
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
		}
		
		protected override void _stopIabServiceInBg() {
			AndroidJNI.PushLocalFrame(100);
			jniStoreController.Call("stopIabServiceInBg");
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
		}
#endif
	}
}
