using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace Soomla
{
	/// <summary>
	/// This class holds the store's meta data including:
	/// - Virtual Currencies definitions
	/// - Virtual Currency Packs definitions
	/// - Virtual Goods definitions
	/// - Virtual Categories definitions
	/// - Virtual Non-Consumable items definitions
	/// </summary>
	public class StoreInfo
	{
		static StoreInfo _instance = null;
		static StoreInfo instance {
			get {
				if(_instance == null) {
					#if UNITY_ANDROID && !UNITY_EDITOR
					_instance = new StoreInfoAndroid();
					#elif UNITY_IOS && !UNITY_EDITOR
					_instance = new StoreInfoIOS();
					#else
					_instance = new StoreInfo();
					#endif
				}
				return _instance;
			}
		}

		protected const string TAG = "SOOMLA StoreInfo";
			
		public static void Initialize(IStoreAssets storeAssets) {
			
//			StoreUtils.LogDebug(TAG, "Adding currency");
			JSONObject currencies = new JSONObject(JSONObject.Type.ARRAY);
			foreach(VirtualCurrency vi in storeAssets.GetCurrencies()) {
				currencies.Add(vi.toJSONObject());
			}
			
//			StoreUtils.LogDebug(TAG, "Adding packs");
			JSONObject packs = new JSONObject(JSONObject.Type.ARRAY);
			foreach(VirtualCurrencyPack vi in storeAssets.GetCurrencyPacks()) {
				packs.Add(vi.toJSONObject());
			}
			
//			StoreUtils.LogDebug(TAG, "Adding goods");
		    JSONObject suGoods = new JSONObject(JSONObject.Type.ARRAY);
		    JSONObject ltGoods = new JSONObject(JSONObject.Type.ARRAY);
		    JSONObject eqGoods = new JSONObject(JSONObject.Type.ARRAY);
		    JSONObject upGoods = new JSONObject(JSONObject.Type.ARRAY);
		    JSONObject paGoods = new JSONObject(JSONObject.Type.ARRAY);
		    foreach(VirtualGood g in storeAssets.GetGoods()){
		        if (g is SingleUseVG) {
		            suGoods.Add(g.toJSONObject());
		        } else if (g is EquippableVG) {
		            eqGoods.Add(g.toJSONObject());
			    } else if (g is UpgradeVG) {
   		            upGoods.Add(g.toJSONObject());
   		        } else if (g is LifetimeVG) {
		            ltGoods.Add(g.toJSONObject());
		        } else if (g is SingleUsePackVG) {
		            paGoods.Add(g.toJSONObject());
		        }
		    }
			JSONObject goods = new JSONObject(JSONObject.Type.OBJECT);
			goods.AddField(JSONConsts.STORE_GOODS_SU, suGoods);
			goods.AddField(JSONConsts.STORE_GOODS_LT, ltGoods);
			goods.AddField(JSONConsts.STORE_GOODS_EQ, eqGoods);
			goods.AddField(JSONConsts.STORE_GOODS_UP, upGoods);
			goods.AddField(JSONConsts.STORE_GOODS_PA, paGoods);
			
//			StoreUtils.LogDebug(TAG, "Adding categories");
			JSONObject categories = new JSONObject(JSONObject.Type.ARRAY);
			foreach(VirtualCategory vi in storeAssets.GetCategories()) {
				categories.Add(vi.toJSONObject());
			}
			
//			StoreUtils.LogDebug(TAG, "Adding nonConsumables");
			JSONObject nonConsumables = new JSONObject(JSONObject.Type.ARRAY);
			foreach(NonConsumableItem vi in storeAssets.GetNonConsumableItems()) {
				nonConsumables.Add(vi.toJSONObject());
			}
			
//			StoreUtils.LogDebug(TAG, "Preparing StoreAssets  JSONObject");
			JSONObject storeAssetsObj = new JSONObject(JSONObject.Type.OBJECT);
			storeAssetsObj.AddField(JSONConsts.STORE_CATEGORIES, categories);
			storeAssetsObj.AddField(JSONConsts.STORE_CURRENCIES, currencies);
			storeAssetsObj.AddField(JSONConsts.STORE_CURRENCYPACKS, packs);
			storeAssetsObj.AddField(JSONConsts.STORE_GOODS, goods);
			storeAssetsObj.AddField(JSONConsts.STORE_NONCONSUMABLES, nonConsumables);
			
			string storeAssetsJSON = storeAssetsObj.print();
			instance._initialize(storeAssets.GetVersion(), storeAssetsJSON);
		}

		virtual protected void _initialize(int version, string storeAssetsJSON) {
		}
		
		public static VirtualItem GetItemByItemId(string itemId) {
			StoreUtils.LogDebug(TAG, "Trying to fetch an item with itemId: " + itemId);
			return instance._getItemByItemId(itemId);
		}
		public static PurchasableVirtualItem GetPurchasableItemWithProductId(string productId) {
			return instance._getPurchasableItemWithProductId(productId);
		}
		public static VirtualCategory GetCategoryForVirtualGood(string goodItemId) {
			return instance._getCategoryForVirtualGood(goodItemId);
		}
		public static UpgradeVG GetFirstUpgradeForVirtualGood(string goodItemId) {
			return instance._getFirstUpgradeForVirtualGood(goodItemId);
		}
		public static UpgradeVG GetLastUpgradeForVirtualGood(string goodItemId) {
			return instance._getLastUpgradeForVirtualGood(goodItemId);
		}
		public static List<UpgradeVG> GetUpgradesForVirtualGood(string goodItemId) {
			StoreUtils.LogDebug(TAG, "Trying to fetch upgrades for " + goodItemId);
			return instance._getUpgradesForVirtualGood(goodItemId);
		}
		public static List<VirtualCurrency> GetVirtualCurrencies() {
			StoreUtils.LogDebug(TAG, "Trying to fetch currencies");
			return instance._getVirtualCurrencies();
		}
		public static List<VirtualGood> GetVirtualGoods() {
			StoreUtils.LogDebug(TAG, "Trying to fetch goods");
			return instance._getVirtualGoods();
		}
		public static List<VirtualCurrencyPack> GetVirtualCurrencyPacks() {
			StoreUtils.LogDebug(TAG, "Trying to fetch packs");
			return instance._getVirtualCurrencyPacks();
		}
		public static List<NonConsumableItem> GetNonConsumableItems() {
			StoreUtils.LogDebug(TAG, "Trying to fetch noncons");
			return instance._getNonConsumableItems();
		}
		public static List<VirtualCategory> GetVirtualCategories() {
			StoreUtils.LogDebug(TAG, "Trying to fetch categories");
			return instance._getVirtualCategories();
		}

		virtual protected VirtualItem _getItemByItemId(string itemId) {
			return null;
		}

		virtual protected PurchasableVirtualItem _getPurchasableItemWithProductId(string productId) {
			return null;
		}

		virtual protected VirtualCategory _getCategoryForVirtualGood(string goodItemId) {
			return null;
		}

		virtual protected UpgradeVG _getFirstUpgradeForVirtualGood(string goodItemId) {
			return null;
		}

		virtual protected UpgradeVG _getLastUpgradeForVirtualGood(string goodItemId) {
			return null;
		}

		virtual protected List<UpgradeVG> _getUpgradesForVirtualGood(string goodItemId) {
			return new List<UpgradeVG>();
		}

		virtual protected List<VirtualCurrency> _getVirtualCurrencies() {
			return new List<VirtualCurrency>();
		}

		virtual protected List<VirtualGood> _getVirtualGoods() {
			return new List<VirtualGood>();
		}

		virtual protected List<VirtualCurrencyPack> _getVirtualCurrencyPacks() {
			return new List<VirtualCurrencyPack>();
		}
		
		virtual protected List<NonConsumableItem> _getNonConsumableItems() {
			return new List<NonConsumableItem>();
		}

		virtual protected List<VirtualCategory> _getVirtualCategories() {
			return new List<VirtualCategory>();
		}
	}
}

