using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace com.soomla.unity
{
	/// <summary>
	/// This class holds the store's meta data including:
	/// - Virtual Currencies definitions
	/// - Virtual Currency Packs definitions
	/// - Virtual Goods definitions
	/// - Virtual Categories definitions
	/// - Virtual Non-Consumable items definitions
	/// </summary>
	public static class StoreInfo
	{
#if UNITY_IOS
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetItemByItemId(string itemId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetPurchasableItemWithProductId(string productId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetCategoryForVirtualGood(string goodItemId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetVirtualCurrencies(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetVirtualGoods(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetVirtualCurrencyPacks(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetNonConsumableItems(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetVirtualCategories(out IntPtr json);
		[DllImport ("__Internal")]
		private static extern void storeAssets_Init(int version, string storeAssetsJSON);
#endif
		
#if UNITY_ANDROID
//		private static AndroidJavaClass jniStoreInfo = new AndroidJavaClass("com.soomla.unity.StoreInfo");
#endif
			
		public static void Initialize(IStoreAssets storeAssets) {
			
			JSONObject currencies = new JSONObject(JSONObject.Type.ARRAY);
			foreach(VirtualCurrency vi in storeAssets.GetVirtualCurrencies) {
				currencies.Add(vi.toJSONObject());
			}
			
			JSONObject packs = new JSONObject(JSONObject.Type.ARRAY);
			foreach(VirtualCurrencyPack vi in storeAssets.GetVirtualCurrencyPacks) {
				packs.Add(vi.toJSONObject());
			}
			
		    JSONObject suGoods = new JSONObject(JSONObject.Type.ARRAY);
		    JSONObject ltGoods = new JSONObject(JSONObject.Type.ARRAY);
		    JSONObject eqGoods = new JSONObject(JSONObject.Type.ARRAY);
		    JSONObject upGoods = new JSONObject(JSONObject.Type.ARRAY);
		    JSONObject paGoods = new JSONObject(JSONObject.Type.ARRAY);
		    foreach(VirtualGood g in storeAssets.GetVirtualGoods){
		        if (g is SingleUseVG) {
		            suGoods.Add(g.toJSONObject);
		        } else if (g is EquippableVG) {
		            eqGoods.Add(g.toJSONObject);
		        } else if (g is LifetimeVG) {
		            ltGoods.Add(g.toJSONObject);
		        } else if (g is SingleUsePackVG) {
		            paGoods.Add(g.toJSONObject);
		        } else if (g is UpgradeVG) {
		            upGoods.Add(g.toJSONObject);
		        }
		    }
			JSONObject goods = new JSONObject(JSONObject.Type.OBJECT);
			goods.AddField(JSONConsts.STORE_GOODS_SU, suGoods);
			goods.AddField(JSONConsts.STORE_GOODS_LT, ltGoods);
			goods.AddField(JSONConsts.STORE_GOODS_EQ, eqGoods);
			goods.AddField(JSONConsts.STORE_GOODS_UP, upGoods);
			goods.AddField(JSONConsts.STORE_GOODS_PA, paGoods);
			
			JSONObject categories = new JSONObject(JSONObject.Type.ARRAY);
			foreach(VirtualCategory vi in storeAssets.GetVirtualCategories) {
				categories.Add(vi.toJSONObject());
			}
			
			JSONObject nonConsumables = new JSONObject(JSONObject.Type.ARRAY);
			foreach(NonConsumableItem vi in storeAssets.GetNonConsumableItems) {
				nonConsumables.Add(vi.toJSONObject());
			}
			
			JSONObject storeAssetsObj = new JSONObject(JSONObject.Type.OBJECT);
			storeAssetsObj.AddField(JSONConsts.STORE_CATEGORIES, categories);
			storeAssetsObj.AddField(JSONConsts.STORE_CURRENCIES, currencies);
			storeAssetsObj.AddField(JSONConsts.STORE_CURRENCYPACKS, packs);
			storeAssetsObj.AddField(JSONConsts.STORE_GOODS, goods);
			storeAssetsObj.AddField(JSONConsts.STORE_NONCONSUMABLES, nonConsumables);
			
			string storeAssetsJSON = storeAssetsObj.print();
			
#if UNITY_ANDROID
			
#elif UNITY_IOS
			Debug.Log("pushing data to StoreAssets on ios side");
			storeAssets_Init(storeAssets.GetVersion(), storeAssetsJSON);
			Debug.Log("done! (pushing data to StoreAssets on ios side)");
#endif
			
#if UNITY_ANDROID
			Debug.Log("pushing data to StoreAssets on java side");
			
			AndroidJNI.PushLocalFrame(100);
			
			using(AndroidJavaClass jniStoreAssets = new AndroidJavaClass("com.soomla.unity.StoreAssets")) {
			
				//storeAssets version
				jniStoreAssets.CallStatic("setVersion", storeAssets.GetVersion());
				
				Dictionary<int, AndroidJavaObject> jniCategories = new Dictionary<int, AndroidJavaObject>();
				//virtual categories
				using(AndroidJavaObject jniVirtualCategories = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualCategories")) {
					jniVirtualCategories.Call("clear");
					AndroidJNI.PushLocalFrame(100);
					foreach(VirtualCategory vc in storeAssets.GetVirtualCategories()){
						jniCategories[vc.Id] = vc.toAndroidJavaObject(jniStoreAssets);
						jniVirtualCategories.Call<bool>("add", jniCategories[vc.Id]);
					}
					AndroidJNI.PopLocalFrame(IntPtr.Zero);
				}

				//non consumable items
				using(AndroidJavaObject jniNonConsumableItems = jniStoreAssets.GetStatic<AndroidJavaObject>("nonConsumableItems")) {
					jniNonConsumableItems.Call("clear");
					AndroidJNI.PushLocalFrame(100);
					foreach(NonConsumableItem non in storeAssets.GetNonConsumableItems()){
						using(AndroidJavaObject obj = non.toAndroidJavaObject()) {
							jniNonConsumableItems.Call<bool>("add", obj);
						}
					}
					AndroidJNI.PopLocalFrame(IntPtr.Zero);
				}
				
				Dictionary<string, AndroidJavaObject> jniCurrencies = new Dictionary<string, AndroidJavaObject>();
				//Virtual currencies
				using(AndroidJavaObject jniVirtualCurrencies = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualCurrencies")) {
					jniVirtualCurrencies.Call("clear");
					AndroidJNI.PushLocalFrame(100);
					foreach(VirtualCurrency vc in storeAssets.GetVirtualCurrencies()){
						jniCurrencies[vc.ItemId] = vc.toAndroidJavaObject();
						jniVirtualCurrencies.Call<bool>("add", jniCurrencies[vc.ItemId]);
					}
					AndroidJNI.PopLocalFrame(IntPtr.Zero);
				}
				
				//Virtual currency packs
				using(AndroidJavaObject jniVirtualCurrencyPacks = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualCurrencyPacks")) {
					jniVirtualCurrencyPacks.Call("clear");
					AndroidJNI.PushLocalFrame(100);
					foreach(VirtualCurrencyPack vcp in storeAssets.GetVirtualCurrencyPacks()){
						using(AndroidJavaObject obj = vcp.toAndroidJavaObject(jniCurrencies[vcp.Currency.ItemId])) {
							jniVirtualCurrencyPacks.Call<bool>("add", obj);
						}
					}
					AndroidJNI.PopLocalFrame(IntPtr.Zero);
				}

				//Virtual goods
				using(AndroidJavaObject jniVirtualGoods = jniStoreAssets.GetStatic<AndroidJavaObject>("virtualGoods")) {
					jniVirtualGoods.Call("clear");
					foreach(VirtualGood vg in storeAssets.GetVirtualGoods()){
						AndroidJNI.PushLocalFrame(100);
						using(AndroidJavaObject obj = vg.toAndroidJavaObject(jniStoreAssets, jniCategories[vg.Category.Id])) {
							jniVirtualGoods.Call<bool>("add", obj);
						}
						AndroidJNI.PopLocalFrame(IntPtr.Zero);
					}
				}
				
				foreach(KeyValuePair<int, AndroidJavaObject> kvp in jniCategories) {
					kvp.Value.Dispose();
				}

				foreach(KeyValuePair<string, AndroidJavaObject> kvp in jniCurrencies) {
					kvp.Value.Dispose();
				}
			}
			
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			Debug.Log("done! (pushing data to StoreAssets on java side)");
#elif UNITY_IOS
			
#endif
		}
		
		public static VirtualItem GetItemByItemId(string itemId) {
#if UNITY_ANDROID
			VirtualCurrency vc = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrency = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.unity.StoreInfo"),"getVirtualCurrencyByItemId", itemId)) {
				vc = new VirtualCurrency(jniVirtualCurrency);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vc;
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetItemByItemId(itemId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return VirtualItem.factoryItemFromJSONObject(obj);
#else
			return null;
#endif
		}
		
		public static PurchasableVirtualItem GetPurchasableItemWithProductId(string productId) {
#if UNITY_ANDROID
			NonConsumableItem non = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniNonConsumableItem = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.unity.StoreInfo"), "getNonConsumableByProductId", productId)) {
				non = new NonConsumableItem(jniNonConsumableItem);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return non;
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetPurchasableItemWithProductId(productId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string nonConsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(nonConsJson);
			return VirtualItem.factoryItemFromJSONObject(obj);
#else
			return null;
#endif
		}
		
		public static VirtualCategory GetCategoryForVirtualGood(string goodItemId) {
#if UNITY_ANDROID
			VirtualCategory vc = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCategory = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.unity.StoreInfo"),"getVirtualCategoryById", id)) {
				vc = new VirtualCategory(jniVirtualCategory);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vc;
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCategoryForVirtualGood(goodItemId, out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCategory(obj);
#else
			return null;
#endif
		}
		
		public static List<VirtualCurrency> GetVirtualCurrencies() {
			List<VirtualCurrency> vcs = new List<VirtualCurrency>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrencies = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getVirtualCurrencies")) {
				for(int i=0; i<jniVirtualCurrencies.Call<int>("size"); i++) {
					using(AndroidJavaObject jnivc = jniVirtualCurrencies.Call<AndroidJavaObject>("get", i)) {
						vcs.Add(new VirtualCurrency(jnivc));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualCurrencies(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string currenciesJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject currenciesArr = new JSONObject(currenciesJson);
			foreach(JSONObject obj in currenciesArr.list) {
				vcs.Add(new VirtualCurrency(obj));
			}
#endif
			return vcs;
		}
		
		public static List<VirtualGood> GetVirtualGoods() {
			List<VirtualGood> virtualGoods = new List<VirtualGood>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualGoods = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getVirtualGoods")) {
				for(int i=0; i<jniVirtualGoods.Call<int>("size"); i++) {
					AndroidJNI.PushLocalFrame(100);
					using(AndroidJavaObject jniGood = jniVirtualGoods.Call<AndroidJavaObject>("get", i)) {
						virtualGoods.Add(new VirtualGood(jniGood));
					}
					AndroidJNI.PopLocalFrame(IntPtr.Zero);
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualGoods(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string goodsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject goodsArr = new JSONObject(goodsJson);
			foreach(JSONObject obj in goodsArr.list) {
				virtualGoods.Add((VirtualGood)VirtualItem.factoryItemFromJSONObject(obj));
			}
#endif
			return virtualGoods;
		}
		
		public static List<VirtualCurrencyPack> GetVirtualCurrencyPacks() {
			List<VirtualCurrencyPack> vcps = new List<VirtualCurrencyPack>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrencyPacks = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getCurrencyPacks")) {
				for(int i=0; i<jniVirtualCurrencyPacks.Call<int>("size"); i++) {
					using(AndroidJavaObject jnivcp = jniVirtualCurrencyPacks.Call<AndroidJavaObject>("get", i)) {
						vcps.Add(new VirtualCurrencyPack(jnivcp));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualCurrencyPacks(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string packsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject packsArr = new JSONObject(packsJson);
			foreach(JSONObject obj in packsArr.list) {
				vcps.Add(new VirtualCurrencyPack(obj));
			}
#endif
			return vcps;
		}
		
		public static List<NonConsumableItem> GetNonConsumableItems() {
			List<NonConsumableItem> nonConsumableItems = new List<NonConsumableItem>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniNonConsumableItems = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getNonConsumableItems")) {
				for(int i=0; i<jniNonConsumableItems.Call<int>("size"); i++) {
					using(AndroidJavaObject jniNon = jniNonConsumableItems.Call<AndroidJavaObject>("get", i)) {
						nonConsumableItems.Add(new NonConsumableItem(jniNon));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetNonConsumableItems(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string nonConsumableJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject nonConsArr = new JSONObject(nonConsumableJson);
			foreach(JSONObject obj in nonConsArr.list) {
				nonConsumableItems.Add(new NonConsumableItem(obj));
			}
#endif
			return nonConsumableItems;
		}
		
		public static List<VirtualCategory> GetVirtualCategories() {
			List<VirtualCategory> virtualCategories = new List<VirtualCategory>();
#if UNITY_ANDROID
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCategories = new AndroidJavaClass("com.soomla.unity.StoreInfo").CallStatic<AndroidJavaObject>("getVirtualCategories")) {
				for(int i=0; i<jniVirtualCategories.Call<int>("size"); i++) {
					using(AndroidJavaObject jniCat = jniVirtualCategories.Call<AndroidJavaObject>("get", i)) {
						virtualCategories.Add(new VirtualCategory(jniCat));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualCategories(out p);
				
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string categoriesJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject categoriesArr = new JSONObject(categoriesJson);
			foreach(JSONObject obj in categoriesArr.list) {
				virtualCategories.Add(new VirtualCategory(obj));
			}
#endif
			return virtualCategories;
		}
	}
}

