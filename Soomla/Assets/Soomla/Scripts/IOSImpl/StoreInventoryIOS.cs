using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace Soomla
{
	public class StoreInventoryIOS : StoreInventory {
#if UNITY_IOS && !UNITY_EDITOR
		[DllImport ("__Internal")]
		private static extern int storeInventory_BuyItem(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeInventory_GetItemBalance(string itemId, out int outBalance);
		[DllImport ("__Internal")]
		private static extern int storeInventory_GiveItem(string itemId, int amount);
		[DllImport ("__Internal")]
		private static extern int storeInventory_TakeItem(string itemId, int amount);
		[DllImport ("__Internal")]
		private static extern int storeInventory_EquipVirtualGood(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeInventory_UnEquipVirtualGood(string itemId)	;
		[DllImport ("__Internal")]
		private static extern int storeInventory_IsVirtualGoodEquipped(string itemId, out bool outResult);
		[DllImport ("__Internal")]
		private static extern int storeInventory_GetGoodUpgradeLevel(string itemId, out int outResult);
		[DllImport ("__Internal")]
		private static extern int storeInventory_GetGoodCurrentUpgrade(string itemId, out IntPtr outResult);
		[DllImport ("__Internal")]
		private static extern int storeInventory_UpgradeGood(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeInventory_RemoveGoodUpgrades(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeInventory_NonConsumableItemExists(string itemId, out bool outResult);
		[DllImport ("__Internal")]
		private static extern int storeInventory_AddNonConsumableItem(string itemId);
		[DllImport ("__Internal")]
		private static extern int storeInventory_RemoveNonConsumableItem(string itemId);

		override protected void _buyItem(string itemId) {
			int err = storeInventory_BuyItem(itemId);
			IOS_ErrorCodes.CheckAndThrowException(err);
		}

		override protected int _getItemBalance(string itemId) {
			int balance = 0;
			int err = storeInventory_GetItemBalance(itemId, out balance);
			IOS_ErrorCodes.CheckAndThrowException(err);
			return balance;
		}

		override protected void _giveItem(string itemId, int amount) {
			int err = storeInventory_GiveItem(itemId, amount);
			IOS_ErrorCodes.CheckAndThrowException(err);
		}

		override protected void _takeItem(string itemId, int amount) {
			int err = storeInventory_TakeItem(itemId, amount);
			IOS_ErrorCodes.CheckAndThrowException(err);
		}

		override protected void _equipVirtualGood(string goodItemId) {
			int err = storeInventory_EquipVirtualGood(goodItemId);
			IOS_ErrorCodes.CheckAndThrowException(err);
		}

		override protected void _unEquipVirtualGood(string goodItemId) {
			int err = storeInventory_UnEquipVirtualGood(goodItemId);
			IOS_ErrorCodes.CheckAndThrowException(err);
		}

		override protected bool _isVertualGoodEquipped(string goodItemId) {
			bool result = false;
			int err = storeInventory_IsVirtualGoodEquipped(goodItemId, out result);
			IOS_ErrorCodes.CheckAndThrowException(err);
			return result;
		}

		override protected int _getGoodUpgradeLevel(string goodItemId) {
			int level = 0;
			int err = storeInventory_GetGoodUpgradeLevel(goodItemId, out level);
			IOS_ErrorCodes.CheckAndThrowException(err);
			return level;
		}

		override protected string _getGoodCurrentUpgrade(string goodItemId) {
			IntPtr p = IntPtr.Zero;
			int err = storeInventory_GetGoodCurrentUpgrade(goodItemId, out p);
			IOS_ErrorCodes.CheckAndThrowException(err);
			string result = Marshal.PtrToStringAnsi(p);
			Marshal.FreeHGlobal(p);
			return result;
		}

		override protected void _upgradeGood(string goodItemId) {
			int err = storeInventory_UpgradeGood(goodItemId);
			IOS_ErrorCodes.CheckAndThrowException(err);
		}

		override protected void _removeGoodUpgrades(string goodItemId) {
			int err = storeInventory_RemoveGoodUpgrades(goodItemId);
			IOS_ErrorCodes.CheckAndThrowException(err);
		}

		override protected bool _nonConsumableItemExists(string nonConsItemId) {
			bool result = false;
			int err = storeInventory_NonConsumableItemExists(nonConsItemId, out result);
			IOS_ErrorCodes.CheckAndThrowException(err);
			return result;
		}

		override protected void _addNonConsumableItem(string nonConsItemId) {
			int err = storeInventory_AddNonConsumableItem(nonConsItemId);
			IOS_ErrorCodes.CheckAndThrowException(err);
		}

		override protected void _removeNonConsumableItem(string nonConsItemId) {
			int err = storeInventory_RemoveNonConsumableItem(nonConsItemId);
			IOS_ErrorCodes.CheckAndThrowException(err);
		}
#endif
	}
}
