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

using System;

namespace Soomla.Store
{
	/// <summary>
	/// This type of purchase allows users to purchase <c>PurchasableVirtualItems</c> with other 
	/// <c>VirtualItem</c>s.
	/// 
	/// Real Game Example: Purchase a 'Sword' in exchange for 100 'Gem's. 'Sword' is the item to be purchased,
	/// 'Gem' is the target item, and 100 is the amount.
	/// </summary>
	public class PurchaseWithVirtualItem : PurchaseType
	{
		public String TargetItemId;
		public int Amount;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="itemId">The itemId of the <c>VirtualItem</c> that is used to "pay" in order 
		/// 					to make the purchase.</param>
		/// <param name="amount">The number of items (with the given item id) needed in order to make the 
		/// 					purchase.</param>
		public PurchaseWithVirtualItem (String targetItemId, int amount) :
			base()
		{
			this.TargetItemId = targetItemId;
			this.Amount = amount;
		}

#if UNITY_EDITOR
		public override void Buy(string payload)
		{
			SoomlaUtils.LogDebug("SOOMLA PurchaseWithVirtualItem", "Trying to buy a " + AssociatedItem.Name + " with "
			                     + Amount + " pieces of " + TargetItemId);

			StoreEvents.Instance.onItemPurchaseStarted(AssociatedItem.ItemId);

			VirtualItem item = null;
			try {
				item = StoreInfo.GetItemByItemId(TargetItemId);
			} catch (VirtualItemNotFoundException e) {
				SoomlaUtils.LogError(TAG, "Target virtual item doesn't exist !");
				return;
			}

			int balance = StoreInventory.GetItemBalance(TargetItemId);
			if (balance < Amount){
				throw new InsufficientFundsException(TargetItemId);
			}

			StoreInventory.TakeItem(TargetItemId, Amount);

			StoreInventory.GiveItem(AssociatedItem.ItemId, 1);

			StoreEvents.Instance.onItemPurchased(AssociatedItem.ItemId + "#SOOM#" + payload);
		}
#endif
	}
}

