using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace Soomla
{
	public class StoreInfoAndroid : StoreInfo {
#if UNITY_ANDROID && !UNITY_EDITOR
		override protected void _initialize(int version, string storeAssetsJSON) {
			StoreUtils.LogDebug(TAG, "pushing data to StoreAssets on java side");
			using(AndroidJavaClass jniStoreAssets = new AndroidJavaClass("com.soomla.unity.StoreAssets")) {
				jniStoreAssets.CallStatic("prepare", version, storeAssetsJSON);
			}
			StoreUtils.LogDebug(TAG, "done! (pushing data to StoreAssets on java side)");
		}

		override protected VirtualItem _getItemByItemId(string itemId) {
			VirtualItem vi = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualItem = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.store.data.StoreInfo"),"getVirtualItem", itemId)) {
				vi = VirtualItem.factoryItemFromJNI(jniVirtualItem);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vi;
		}

		override protected PurchasableVirtualItem _getPurchasableItemWithProductId(string productId) {
			VirtualItem vi = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualItem = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.store.data.StoreInfo"),"getPurchasableItem", productId)) {
				vi = VirtualItem.factoryItemFromJNI(jniVirtualItem);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return (PurchasableVirtualItem)vi;
		}

		override protected VirtualCategory _getCategoryForVirtualGood(string goodItemId) {
			VirtualCategory vc = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualVategory = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.store.data.StoreInfo"),"getCategory", goodItemId)) {
				vc = new VirtualCategory(jniVirtualVategory);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vc;
		}

		override protected UpgradeVG _getFirstUpgradeForVirtualGood(string goodItemId) {
			UpgradeVG vgu = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniUpgradeVG = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.store.data.StoreInfo"),"getGoodFirstUpgrade", goodItemId)) {
				vgu = new UpgradeVG(jniUpgradeVG);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vgu;
		}

		override protected UpgradeVG _getLastUpgradeForVirtualGood(string goodItemId) {
			UpgradeVG vgu = null;
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniUpgradeVG = AndroidJNIHandler.CallStatic<AndroidJavaObject>(
				new AndroidJavaClass("com.soomla.store.data.StoreInfo"),"getGoodLastUpgrade", goodItemId)) {
				vgu = new UpgradeVG(jniUpgradeVG);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vgu;
		}

		override protected List<UpgradeVG> _getUpgradesForVirtualGood(string goodItemId) {
			List<UpgradeVG> vgus = new List<UpgradeVG>();
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniUpgradeVGs = new AndroidJavaClass("com.soomla.store.data.StoreInfo").CallStatic<AndroidJavaObject>("getGoodUpgrades")) {
				for(int i=0; i<jniUpgradeVGs.Call<int>("size"); i++) {
					using(AndroidJavaObject jnivgu = jniUpgradeVGs.Call<AndroidJavaObject>("get", i)) {
						vgus.Add(new UpgradeVG(jnivgu));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vgus;
		}

		override protected List<VirtualCurrency> _getVirtualCurrencies() {
			List<VirtualCurrency> vcs = new List<VirtualCurrency>();
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrencies = new AndroidJavaClass("com.soomla.store.data.StoreInfo").CallStatic<AndroidJavaObject>("getCurrencies")) {
				for(int i=0; i<jniVirtualCurrencies.Call<int>("size"); i++) {
					using(AndroidJavaObject jnivc = jniVirtualCurrencies.Call<AndroidJavaObject>("get", i)) {
						vcs.Add(new VirtualCurrency(jnivc));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vcs;
		}

		override protected List<VirtualGood> _getVirtualGoods() {
			List<VirtualGood> virtualGoods = new List<VirtualGood>();
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualGoods = new AndroidJavaClass("com.soomla.store.data.StoreInfo").CallStatic<AndroidJavaObject>("getGoods")) {
				for(int i=0; i<jniVirtualGoods.Call<int>("size"); i++) {
					AndroidJNI.PushLocalFrame(100);
					using(AndroidJavaObject jniGood = jniVirtualGoods.Call<AndroidJavaObject>("get", i)) {
						virtualGoods.Add((VirtualGood)VirtualItem.factoryItemFromJNI(jniGood));
					}
					AndroidJNI.PopLocalFrame(IntPtr.Zero);
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return virtualGoods;
		}

		override protected List<VirtualCurrencyPack> _getVirtualCurrencyPacks() {
			List<VirtualCurrencyPack> vcps = new List<VirtualCurrencyPack>();
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCurrencyPacks = new AndroidJavaClass("com.soomla.store.data.StoreInfo").CallStatic<AndroidJavaObject>("getCurrencyPacks")) {
				for(int i=0; i<jniVirtualCurrencyPacks.Call<int>("size"); i++) {
					using(AndroidJavaObject jnivcp = jniVirtualCurrencyPacks.Call<AndroidJavaObject>("get", i)) {
						vcps.Add(new VirtualCurrencyPack(jnivcp));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return vcps;
		}

		override protected List<NonConsumableItem> _getNonConsumableItems() {
			List<NonConsumableItem> nonConsumableItems = new List<NonConsumableItem>();
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniNonConsumableItems = new AndroidJavaClass("com.soomla.store.data.StoreInfo").CallStatic<AndroidJavaObject>("getNonConsumableItems")) {
				for(int i=0; i<jniNonConsumableItems.Call<int>("size"); i++) {
					using(AndroidJavaObject jniNon = jniNonConsumableItems.Call<AndroidJavaObject>("get", i)) {
						nonConsumableItems.Add(new NonConsumableItem(jniNon));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return nonConsumableItems;
		}

		override protected List<VirtualCategory> _getVirtualCategories() {
			List<VirtualCategory> virtualCategories = new List<VirtualCategory>();
			AndroidJNI.PushLocalFrame(100);
			using(AndroidJavaObject jniVirtualCategories = new AndroidJavaClass("com.soomla.store.data.StoreInfo").CallStatic<AndroidJavaObject>("getCategories")) {
				for(int i=0; i<jniVirtualCategories.Call<int>("size"); i++) {
					using(AndroidJavaObject jniCat = jniVirtualCategories.Call<AndroidJavaObject>("get", i)) {
						virtualCategories.Add(new VirtualCategory(jniCat));
					}
				}
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
			return virtualCategories;
		}
#endif
	}
}
