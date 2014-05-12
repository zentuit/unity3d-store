using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace Soomla
{
	/// <summary>
	/// This class allows some convinience operations on Virtual Goods and Virtual Currencies.
	/// </summary>
	public class StoreInventory
	{
		static StoreInventory _instance = null;
		static StoreInventory instance {
			get {
				if(_instance == null) {
					#if UNITY_EDITOR
					_instance = new StoreInventory();
					#elif UNITY_ANDROID
					_instance = new StoreInventoryAndroid();
					#elif UNITY_IOS
					_instance = new StoreInventoryIOS();
					#endif
				}
				return _instance;
			}
		}

		protected const string TAG = "SOOMLA StoreInventory";
		
		public static void BuyItem(string itemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling BuyItem with: " + itemId);
			instance._buyItem(itemId);
		}

		virtual protected void _buyItem(string itemId) {
		}



		/** Virtual Items **/

		public static int GetItemBalance(string itemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling GetItemBalance with: " + itemId);
			return instance._getItemBalance(itemId);
		}
		public static void GiveItem(string itemId, int amount) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling GiveItem with itedId: " + itemId + " and amount: " + amount);
			instance._giveItem(itemId, amount);
		}
		public static void TakeItem(string itemId, int amount) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling TakeItem with itedId: " + itemId + " and amount: " + amount);
			instance._takeItem(itemId, amount);
		}

		virtual protected int _getItemBalance(string itemId) {
			return 0;
		}

		virtual protected void _giveItem(string itemId, int amount) {
		}

		virtual protected void _takeItem(string itemId, int amount) {
		}



		/** Virtual Goods **/
		
		public static void EquipVirtualGood(string goodItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling EquipVirtualGood with: " + goodItemId);
			instance._equipVirtualGood(goodItemId);
		}
		public static void UnEquipVirtualGood(string goodItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling UnEquipVirtualGood with: " + goodItemId);
			instance._unEquipVirtualGood(goodItemId);
		}
		public static bool IsVirtualGoodEquipped(string goodItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling IsVirtualGoodEquipped with: " + goodItemId);
			return instance._isVertualGoodEquipped(goodItemId);
		}
		public static int GetGoodUpgradeLevel(string goodItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling GetGoodUpgradeLevel with: " + goodItemId);
			return instance._getGoodUpgradeLevel(goodItemId);
		}
		public static string GetGoodCurrentUpgrade(string goodItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling GetGoodCurrentUpgrade with: " + goodItemId);
			return instance._getGoodCurrentUpgrade(goodItemId);
		}
		public static void UpgradeGood(string goodItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling UpgradeGood with: " + goodItemId);
			instance._upgradeGood(goodItemId);
		}
		public static void RemoveGoodUpgrades(string goodItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling RemoveGoodUpgrades with: " + goodItemId);
			instance._removeGoodUpgrades(goodItemId);
		}

		virtual protected void _equipVirtualGood(string goodItemId) {
		}

		virtual protected void _unEquipVirtualGood(string goodItemId) {
		}

		virtual protected bool _isVertualGoodEquipped(string goodItemId) {
			return false;
		}

		virtual protected int _getGoodUpgradeLevel(string goodItemId) {
			return 0;
		}

		virtual protected string _getGoodCurrentUpgrade(string goodItemId) {
			return null;
		}

		virtual protected void _upgradeGood(string goodItemId) {
		}

		virtual protected void _removeGoodUpgrades(string goodItemId) {
		}



		/** NonConsumables **/
		
		public static bool NonConsumableItemExists(string nonConsItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling NonConsumableItemExists with: " + nonConsItemId);
			return instance._nonConsumableItemExists(nonConsItemId);
		}
		public static void AddNonConsumableItem(string nonConsItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling AddNonConsumableItem with: " + nonConsItemId);
			instance._addNonConsumableItem(nonConsItemId);
		}
		public static void RemoveNonConsumableItem(string nonConsItemId) {
			StoreUtils.LogDebug(TAG, "SOOMLA/UNITY Calling RemoveNonConsumableItem with: " + nonConsItemId);
			instance._removeNonConsumableItem(nonConsItemId);
		}

		virtual protected bool _nonConsumableItemExists(string nonConsItemId) {
			return false;
		}

		virtual protected void _addNonConsumableItem(string nonConsItemId) {
		}

		virtual protected void _removeNonConsumableItem(string nonConsItemId) {
		}
	}
}



