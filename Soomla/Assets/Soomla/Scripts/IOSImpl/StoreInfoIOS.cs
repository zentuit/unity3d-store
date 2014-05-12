using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace Soomla
{
	public class StoreInfoIOS : StoreInfo {
#if UNITY_IOS && !UNITY_EDITOR
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetItemByItemId(string itemId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetPurchasableItemWithProductId(string productId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetCategoryForVirtualGood(string goodItemId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetFirstUpgradeForVirtualGood(string goodItemId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetLastUpgradeForVirtualGood(string goodItemId, out IntPtr json);
		[DllImport ("__Internal")]
		private static extern int storeInfo_GetUpgradesForVirtualGood(string goodItemId, out IntPtr json);
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

		override protected void _initialize(int version, string storeAssetsJSON) {
			StoreUtils.LogDebug(TAG, "pushing data to StoreAssets on ios side");
			storeAssets_Init(version, storeAssetsJSON);
			StoreUtils.LogDebug(TAG, "done! (pushing data to StoreAssets on ios side)");
		}

		override protected VirtualItem _getItemByItemId(string itemId) {
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetItemByItemId(itemId, out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			StoreUtils.LogDebug(TAG, "Got json: " + json);

			JSONObject obj = new JSONObject(json);
			return VirtualItem.factoryItemFromJSONObject(obj);
		}

		override protected PurchasableVirtualItem _getPurchasableItemWithProductId(string productId) {
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetPurchasableItemWithProductId(productId, out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string nonConsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);

			JSONObject obj = new JSONObject(nonConsJson);
			return (PurchasableVirtualItem)VirtualItem.factoryItemFromJSONObject(obj);
		}

		override protected VirtualCategory _getCategoryForVirtualGood(string goodItemId) {
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetCategoryForVirtualGood(goodItemId, out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new VirtualCategory(obj);
		}

		override protected UpgradeVG _getFirstUpgradeForVirtualGood(string goodItemId) {
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetFirstUpgradeForVirtualGood(goodItemId, out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new UpgradeVG(obj);
		}

		override protected UpgradeVG _getLastUpgradeForVirtualGood(string goodItemId) {
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetLastUpgradeForVirtualGood(goodItemId, out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string json = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			JSONObject obj = new JSONObject(json);
			return new UpgradeVG(obj);
		}

		override protected List<UpgradeVG> _getUpgradesForVirtualGood(string goodItemId) {
			List<UpgradeVG> vgus = new List<UpgradeVG>();
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetUpgradesForVirtualGood(goodItemId, out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string upgradesJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			StoreUtils.LogDebug(TAG, "Got json: " + upgradesJson);
			
			JSONObject upgradesArr = new JSONObject(upgradesJson);
			foreach(JSONObject obj in upgradesArr.list) {
				vgus.Add(new UpgradeVG(obj));
			}
			return vgus;
		}

		override protected List<VirtualCurrency> _getVirtualCurrencies() {
			List<VirtualCurrency> vcs = new List<VirtualCurrency>();
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualCurrencies(out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string currenciesJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			StoreUtils.LogDebug(TAG, "Got json: " + currenciesJson);
			
			JSONObject currenciesArr = new JSONObject(currenciesJson);
			foreach(JSONObject obj in currenciesArr.list) {
				vcs.Add(new VirtualCurrency(obj));
			}
			return vcs;
		}

		override protected List<VirtualGood> _getVirtualGoods() {
			List<VirtualGood> virtualGoods = new List<VirtualGood>();
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualGoods(out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string goodsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			StoreUtils.LogDebug(TAG, "Got json: " + goodsJson);
			
			JSONObject goodsArr = new JSONObject(goodsJson);
			foreach(JSONObject obj in goodsArr.list) {
				virtualGoods.Add((VirtualGood)VirtualItem.factoryItemFromJSONObject(obj));
			}
			return virtualGoods;
		}

		override protected List<VirtualCurrencyPack> _getVirtualCurrencyPacks() {
			List<VirtualCurrencyPack> vcps = new List<VirtualCurrencyPack>();
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualCurrencyPacks(out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string packsJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			StoreUtils.LogDebug(TAG, "Got json: " + packsJson);
			
			JSONObject packsArr = new JSONObject(packsJson);
			foreach(JSONObject obj in packsArr.list) {
				vcps.Add(new VirtualCurrencyPack(obj));
			}
			return vcps;
		}

		override protected List<NonConsumableItem> _getNonConsumableItems() {
			List<NonConsumableItem> nonConsumableItems = new List<NonConsumableItem>();
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetNonConsumableItems(out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string nonConsumableJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			StoreUtils.LogDebug(TAG, "Got json: " + nonConsumableJson);
			
			JSONObject nonConsArr = new JSONObject(nonConsumableJson);
			foreach(JSONObject obj in nonConsArr.list) {
				nonConsumableItems.Add(new NonConsumableItem(obj));
			}
			return nonConsumableItems;
		}

		override protected List<VirtualCategory> _getVirtualCategories() {
			List<VirtualCategory> virtualCategories = new List<VirtualCategory>();
			IntPtr p = IntPtr.Zero;
			int err = storeInfo_GetVirtualCategories(out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			
			string categoriesJson = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			
			StoreUtils.LogDebug(TAG, "Got json: " + categoriesJson);
			
			JSONObject categoriesArr = new JSONObject(categoriesJson);
			foreach(JSONObject obj in categoriesArr.list) {
				virtualCategories.Add(new VirtualCategory(obj));
			}
			return virtualCategories;
		}
#endif
	}
}
