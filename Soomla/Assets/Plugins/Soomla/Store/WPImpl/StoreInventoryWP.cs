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
using System;
using System.Runtime.InteropServices;

namespace Soomla.Store {

	/// <summary>
	/// <c>StoreInventory</c> for Android.
	/// This class will help you do your day to day virtual economy operations easily.
	/// You can give or take items from your users. You can buy items or upgrade them.
	/// You can also check their equipping status and change it.
	/// </summary>
	public class StoreInventoryWP : StoreInventory {

#if UNITY_WP8

		/// <summary>
		/// Buys the item with the given <c>itemId</c>.
		/// </summary>
		/// <param name="itemId">id of item to be bought</param>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item to be bought is not found.</exception>
		/// <exception cref="InsufficientFundsException">Thrown if the user does not have enough funds.</exception>
		override protected void _buyItem(string itemId, string payload) {
            SoomlaWpStore.StoreInventory.buy(itemId, payload);
		}


		/** VIRTUAL ITEMS **/
		
		/// <summary>
		/// Retrieves the balance of the virtual item with the given <c>itemId</c>.
		/// </summary>
		/// <param name="itemId">Id of the virtual item to be fetched.</param>
		/// <returns>Balance of the virtual item with the given item id.</returns>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected int _getItemBalance(string itemId) {
			int balance = 0;
            balance = SoomlaWpStore.StoreInventory.getVirtualItemBalance(itemId);
			return balance;
		}

		/// <summary>
		/// Gives your user the given amount of the virtual item with the given <c>itemId</c>.
		/// For example, when your user plays your game for the first time you GIVE him/her 1000 gems.
		/// NOTE: This action is different than buy -
		/// You use <c>give(int amount)</c> to give your user something for free.
		/// You use <c>buy()</c> to give your user something and you get something in return.
		/// </summary>
		/// <param name="itemId">Id of the item to be given.</param>
		/// <param name="amount">Amount of the item to be given.</param>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected void _giveItem(string itemId, int amount) {
            SoomlaWpStore.StoreInventory.giveVirtualItem(itemId, amount);
		}

		/// <summary>
		/// Takes from your user the given amount of the virtual item with the given <c>itemId</c>.
		/// For example, when your user requests a refund, you need to TAKE the item he/she is returning from him/her.
		/// </summary>
		/// <param name="itemId">Item identifier.</param>
		/// <param name="amount">Amount.</param>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected void _takeItem(string itemId, int amount) {
            SoomlaWpStore.StoreInventory.takeVirtualItem(itemId,amount);
		}


		/** VIRTUAL GOODS **/
		
		/// <summary>
		/// Equips the virtual good with the given <c>goodItemId</c>.
		/// Equipping means that the user decides to currently use a specific virtual good.
		/// For more details and examples <see cref="com.soomla.store.domain.virtualGoods.EquippableVG"/>.
		/// </summary>
		/// <param name="goodItemId">Id of the good to be equipped.</param>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		/// <exception cref="NotEnoughGoodsException"></exception>
		override protected void _equipVirtualGood(string goodItemId) {
            SoomlaWpStore.StoreInventory.equipVirtualGood(goodItemId);
		}

		/// <summary>
		/// Unequips the virtual good with the given <c>goodItemId</c>. Unequipping means that the
		/// user decides to stop using the virtual good he/she is currently using.
		/// For more details and examples <see cref="com.soomla.store.domain.virtualGoods.EquippableVG"/>.
		/// </summary>
		/// <param name="goodItemId">Id of the good to be unequipped.</param>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected void _unEquipVirtualGood(string goodItemId) {
            SoomlaWpStore.StoreInventory.unEquipVirtualGood(goodItemId);
		}

		/// <summary>
		/// Checks if the virtual good with the given <c>goodItemId</c> is currently equipped.
		/// </summary>
		/// <param name="goodItemId">Id of the virtual good who we want to know if is equipped.</param>
		/// <returns>True if the virtual good is equipped, false otherwise.</returns>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected bool _isVertualGoodEquipped(string goodItemId) {
			bool result = false;
            SoomlaWpStore.StoreInventory.isVirtualGoodEquipped(goodItemId);
			return result;
		}

		/// <summary>
		/// Retrieves the upgrade level of the virtual good with the given <c>goodItemId</c>.
		/// For Example:
		/// Let's say there's a strength attribute to one of the characters in your game and you provide
		/// your users with the ability to upgrade that strength on a scale of 1-3.
		/// This is what you've created:
		/// 1. <c>SingleUseVG</c> for "strength". 
		/// 2. <c>UpgradeVG</c> for strength 'level 1'.
		/// 3. <c>UpgradeVG</c> for strength 'level 2'.
		/// 4. <c>UpgradeVG</c> for strength 'level 3'.
		/// In the example, this function will retrieve the upgrade level for "strength" (1, 2, or 3).
		/// </summary>
		/// <param name="goodItemId">Good item identifier.</param>
		/// <returns>The good upgrade level.</returns>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected int _getGoodUpgradeLevel(string goodItemId) {
			int level = 0;
            SoomlaWpStore.StoreInventory.getGoodUpgradeLevel(goodItemId);
			return level;
		}

		/// <summary>
		/// Retrieves the current upgrade of the good with the given id.
		/// </summary>
		/// <param name="goodItemId">Id of the good whose upgrade we want to fetch. </param>
		/// <returns>The good's current upgrade.</returns>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected string _getGoodCurrentUpgrade(string goodItemId) {
			string currentItemId = "";
            currentItemId = SoomlaWpStore.StoreInventory.getGoodCurrentUpgrade(goodItemId);
			return currentItemId;
		}

		/// <summary>
		/// Upgrades the virtual good with the given <c>goodItemId</c> by doing the following:
		/// 1. Checks if the good is currently upgraded or if this is the first time being upgraded.
		/// 2. If the good is currently upgraded, upgrades to the next upgrade in the series. 
		/// In case there are no more upgrades available(meaning the current upgrade is the last available), 
		/// the function returns.
		/// 3. If the good has never been upgraded before, the function upgrades it to the first
		/// available upgrade with the first upgrade of the series.
		/// </summary>
		/// <param name="goodItemId">Good item identifier.</param>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected void _upgradeGood(string goodItemId) {
            SoomlaWpStore.StoreInventory.upgradeVirtualGood(goodItemId);
		}

		/// <summary>
		/// Removes all upgrades from the virtual good with the given <c>goodItemId</c>.
		/// </summary>
		/// <param name="goodItemId">Id of the good whose upgrades are to be removed.</param>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected void _removeGoodUpgrades(string goodItemId) {
            SoomlaWpStore.StoreInventory.removeUpgrades(goodItemId);
		}


		/** NON-CONSUMABLES **/
		
		/// <summary>
		/// Checks if the non-consumable with the given <c>nonConsItemId</c> exists.
		/// </summary>
		/// <param name="nonConsItemId">Id of the item to check if exists.</param>
		/// <returns>True if non-consumable item with nonConsItemId exists, false otherwise.</returns>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected bool _nonConsumableItemExists(string nonConsItemId) {
			bool result = false;
            SoomlaWpStore.StoreInventory.nonConsumableItemExists(nonConsItemId);
			return result;
		}

		/// <summary>
		/// Adds the non-consumable item with the given <c>nonConsItemId</c> to the non-consumable items storage.
		/// </summary>
		/// <param name="nonConsItemId">Id of the item to be added.</param>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected void _addNonConsumableItem(string nonConsItemId) {
            SoomlaWpStore.StoreInventory.addNonConsumableItem(nonConsItemId);
		}

		/// <summary>
		/// Removes the non-consumable item with the given <c>nonConsItemId</c> from the non-consumable 
		/// items storage.
		/// </summary>
		/// <param name="nonConsItemId">Id of the item to be removed.</param>
		/// <exception cref="VirtualItemNotFoundException">Thrown if the item is not found.</exception>
		override protected void _removeNonConsumableItem(string nonConsItemId) {
            SoomlaWpStore.StoreInventory.removeNonConsumableItem(nonConsItemId);
		}
#endif
	}
}
