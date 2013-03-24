using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace com.soomla.unity
{
	/// <summary>
	/// This class allows some convinience operations on Virtual Goods and Virtual Currencies.
	/// </summary>
	public class StoreInventory
	{
		
#if UNITY_IOS
		[DllImport ("__Internal")]
		private static extern int storeInventory_GetCurrencyBalance(string itemId, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_AddCurrencyAmount(string itemId, int amount, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_RemoveCurrencyAmount(string itemId, int amount, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_GetGoodBalance(string itemId, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_AddGoodAmount(string itemId, int amount, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_RemoveGoodAmount(string itemId, int amount, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_EquipVirtualGood(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeInventory_UnEquipVirtualGood(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeInventory_IsVirtualGoodEquipped(string itemId, out bool outResult);
		[DllImport ("__Internal")]
		private static extern int storeInventory_NonConsumableItemExists(string productId, out bool outResult);
		[DllImport ("__Internal")]
		private static extern int storeInventory_AddNonConsumableItem(string productId);
		[DllImport ("__Internal")]
		private static extern int storeInventory_RemoveNonConsumableItem(string productId);
#endif
		
		public static int GetCurrencyBalance(string currencyItemId) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling GetCurrencyBalance with: " + currencyItemId);
#if UNITY_ANDROID
				AndroidJNI.PushLocalFrame(100);
				int balance = 0;
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = AndroidJNIHandler.CallStatic<int>(jniStoreInventory, "getCurrencyBalance", currencyItemId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
#elif UNITY_IOS
				int balance = 0;
				int err = storeInventory_GetCurrencyBalance(currencyItemId, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
		
		public static int AddCurrencyAmount(string currencyItemId, int amount) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling AddCurrencyAmount with: " + currencyItemId);
#if UNITY_ANDROID
				AndroidJNI.PushLocalFrame(100);
				int balance = 0;
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = AndroidJNIHandler.CallStatic<int>(jniStoreInventory, "addCurrencyAmount", currencyItemId, amount);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
#elif UNITY_IOS
				int balance = 0;
				int err = storeInventory_AddCurrencyAmount(currencyItemId, amount, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
		
		public static int RemoveCurrencyAmount(string currencyItemId, int amount) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling RemoveCurrencyAmount with: " + currencyItemId);
#if UNITY_ANDROID
				int balance = 0;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = AndroidJNIHandler.CallStatic<int>(jniStoreInventory, "removeCurrencyAmount", currencyItemId, amount);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
#elif UNITY_IOS
				int balance = 0;
				int err = storeInventory_RemoveCurrencyAmount(currencyItemId, amount, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
				
#endif
			}
			return 0;
		}
		
		public static int GetGoodBalance(string goodItemId) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling GetGoodBalance with: " + goodItemId);
#if UNITY_ANDROID
				int balance = 0;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = AndroidJNIHandler.CallStatic<int>(jniStoreInventory, "getGoodBalance", goodItemId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
#elif UNITY_IOS
				int balance = 0;
				int err = storeInventory_GetGoodBalance(goodItemId, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
		
		public static int AddGoodAmount(string goodItemId, int amount) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling AddGoodAmount with: " + goodItemId);
#if UNITY_ANDROID
				int balance = 0;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = AndroidJNIHandler.CallStatic<int>(jniStoreInventory, "addGoodAmount", goodItemId, amount);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
#elif UNITY_IOS
				int balance = 0;
				int err = storeInventory_AddGoodAmount(goodItemId, amount, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
		
		public static int RemoveGoodAmount(string goodItemId, int amount) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling RemoveGoodAmount with: " + goodItemId);
#if UNITY_ANDROID
				int balance = 0;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					balance = AndroidJNIHandler.CallStatic<int>(jniStoreInventory, "removeGoodAmount", goodItemId, amount);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return balance;
#elif UNITY_IOS
				int balance = 0;
				int err = storeInventory_RemoveGoodAmount(goodItemId, amount, out balance);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return balance;
#endif
			}
			return 0;
		}
		
		public static void EquipVirtualGood(string goodItemId) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling EquipVirtualGood with: " + goodItemId);
#if UNITY_ANDROID
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					AndroidJNIHandler.CallStaticVoid(jniStoreInventory, "equipVirtualGood", goodItemId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
				int err = storeInventory_EquipVirtualGood(goodItemId);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
#endif
			}
		}
		
		public static void UnEquipVirtualGood(string goodItemId) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling UnEquipVirtualGood with: " + goodItemId);
#if UNITY_ANDROID
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					AndroidJNIHandler.CallStaticVoid(jniStoreInventory, "unEquipVirtualGood", goodItemId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
				int err = storeInventory_UnEquipVirtualGood(goodItemId);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
#endif
			}
		}
		
		public static bool IsVirtualGoodEquipped(string goodItemId) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling IsVirtualGoodEquipped with: " + goodItemId);
#if UNITY_ANDROID
				bool result = false;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					result = AndroidJNIHandler.CallStatic<bool>(jniStoreInventory, "isVirtualGoodEquipped", goodItemId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return result;
#elif UNITY_IOS
				bool result = false;
				int err = storeInventory_IsVirtualGoodEquipped(goodItemId, out result);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return result;
#endif
			}
			return false;
		}
		
		public static bool NonConsumableItemExists(string nonConsProductId) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling NonConsumableItemExists with: " + nonConsProductId);
#if UNITY_ANDROID
				bool result = false;
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					result = AndroidJNIHandler.CallStatic<bool>(jniStoreInventory, "nonConsumableItemExists", nonConsProductId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
				return result;
#elif UNITY_IOS
				bool result = false;
				int err = storeInventory_NonConsumableItemExists(nonConsProductId, out result);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
				
				return result;
#endif
			}
			return false;
		}
		
		public static void AddNonConsumableItem(string nonConsProductId) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling AddNonConsumableItem with: " + nonConsProductId);
#if UNITY_ANDROID
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					AndroidJNIHandler.CallStaticVoid(jniStoreInventory, "addNonConsumableItem", nonConsProductId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
				int err = storeInventory_AddNonConsumableItem(nonConsProductId);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
#endif
			}
		}
		
		public static void RemoveNonConsumableItem(string nonConsProductId) {
			if(!Application.isEditor){
				Debug.Log("SOOMLA/UNITY Calling RemoveNonConsumableItem with: " + nonConsProductId);
#if UNITY_ANDROID
				AndroidJNI.PushLocalFrame(100);
				using(AndroidJavaClass jniStoreInventory = new AndroidJavaClass("com.soomla.store.StoreInventory")) {
					AndroidJNIHandler.CallStaticVoid(jniStoreInventory, "removeNonConsumableItem", nonConsProductId);
				}
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
				int err = storeInventory_RemoveNonConsumableItem(nonConsProductId);
				
				IOS_ErrorCodes.CheckAndThrowException(err);
#endif
			}
		}
	}
}



