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
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

#if UNITY_WP8 && !UNITY_EDITOR
using SoomlaWpStore
using SoomlaWpStore.data
#endif


namespace Soomla.Store {

	/// <summary>
	/// <c>StoreInfo</c> for Android.
	/// This class holds the store's meta data including:
	/// virtual currencies definitions, 
	/// virtual currency packs definitions, 
	/// virtual goods definitions, 
	/// virtual categories definitions, and 
	/// virtual non-consumable items definitions
	/// </summary>
	public class StoreInfoWP : StoreInfo {

#if UNITY_WP8

		/// <summary>
		/// Initializes <c>StoreInfo</c>. 
		/// On first initialization, when the database doesn't have any previous version of the store
		/// metadata, <c>StoreInfo</c> gets loaded from the given <c>IStoreAssets</c>.
		/// After the first initialization, <c>StoreInfo</c> will be initialized from the database.
		/// 
		/// IMPORTANT: If you want to override the current <c>StoreInfo</c>, you'll have to bump
		/// the version of your implementation of <c>IStoreAssets</c> in order to remove the
		/// metadata when the application loads. Bumping the version is done by returning a higher 
		/// number in <c>IStoreAssets</c>'s <c>getVersion</c>.
		/// </summary>
		override protected void _initialize(int version, string storeAssetsJSON) {
			
            SoomlaUtils.LogDebug(TAG, "pushing data to StoreAssets on wp side");
            SoomlaWpStore.data.GenericStoreAssets.GetInstance().Prepare(version, storeAssetsJSON);
			SoomlaUtils.LogDebug(TAG, "done! (pushing data to StoreAssets on wp side)");
		}

		/// <summary>
		/// Gets the item with the given <c>itemId</c>.
		/// </summary>
		/// <param name="itemId">Item id.</param>
		/// <returns>Item with the given id.</returns>
		/// <exception cref="VirtualItemNotFoundException">Exception is thrown if item is not found.</exception>
		override protected VirtualItem _getItemByItemId(string itemId) {
			VirtualItem vi = null;
            SoomlaWpStore.domain.VirtualItem wpvi = SoomlaWpStore.data.StoreInfo.getVirtualItem(itemId);
            vi = VirtualItem.factoryItemFromWP(wpvi);
			return vi;
		}

		/// <summary>
		/// Gets the purchasable item with the given <c>productId</c>.
		/// </summary>
		/// <param name="productId">Product id.</param>
		/// <returns>Purchasable virtual item with the given id.</returns>
		/// <exception cref="VirtualItemNotFoundException">Exception is thrown if item is not found.</exception>
		override protected PurchasableVirtualItem _getPurchasableItemWithProductId(string productId) {
			VirtualItem vi = null;
            SoomlaWpStore.domain.VirtualItem wpvi = SoomlaWpStore.data.StoreInfo.getPurchasableItem(productId);
            vi = VirtualItem.factoryItemFromWP(wpvi);
			
			return (PurchasableVirtualItem)vi;
		}

		/// <summary>
		/// Gets the category that the virtual good with the given <c>goodItemId</c> belongs to.
		/// </summary>
		/// <param name="goodItemId">Item id.</param>
		/// <returns>Category that the item with given id belongs to.</returns>
		/// <exception cref="VirtualItemNotFoundException">Exception is thrown if category is not found.</exception>
		override protected VirtualCategory _getCategoryForVirtualGood(string goodItemId) {
			VirtualCategory vc = null;
            SoomlaWpStore.domain.VirtualCategory wpCat = SoomlaWpStore.data.StoreInfo.getCategory(goodItemId);
            vc = new VirtualCategory(wpCat);
			return vc;
		}

		/// <summary>
		/// Gets the first upgrade for virtual good with the given <c>goodItemId</c>.
		/// </summary>
		/// <param name="goodItemId">Item id.</param>
		/// <returns>The first upgrade for virtual good with the given id.</returns>
		override protected UpgradeVG _getFirstUpgradeForVirtualGood(string goodItemId) {
			UpgradeVG vgu = null;
            SoomlaWpStore.domain.virtualGoods.UpgradeVG wpUVG = SoomlaWpStore.data.StoreInfo.getGoodFirstUpgrade(goodItemId);
            vgu = new UpgradeVG(wpUVG);
			return vgu;
		}

		/// <summary>
		/// Gets the last upgrade for the virtual good with the given <c>goodItemId</c>.
		/// </summary>
		/// <param name="goodItemId">item id</param>
		/// <returns>last upgrade for virtual good with the given id</returns>
		override protected UpgradeVG _getLastUpgradeForVirtualGood(string goodItemId) {
            UpgradeVG vgu = null;
            SoomlaWpStore.domain.virtualGoods.UpgradeVG wpUVG = SoomlaWpStore.data.StoreInfo.getGoodLastUpgrade(goodItemId);
            vgu = new UpgradeVG(wpUVG);
            return vgu;
		}

		/// <summary>
		/// Gets all the upgrades for the virtual good with the given <c>goodItemId</c>.
		/// </summary>
		/// <param name="goodItemId">Item id.</param>
		/// <returns>All upgrades for virtual good with the given id.</returns>
		override protected List<UpgradeVG> _getUpgradesForVirtualGood(string goodItemId) {
            List<UpgradeVG> vgus = new List<UpgradeVG>();
            foreach(SoomlaWpStore.domain.virtualGoods.UpgradeVG wpUVG in SoomlaWpStore.data.StoreInfo.getGoodUpgrades(goodItemId))
            {
                vgus.Add(new UpgradeVG(wpUVG));
            }
			return vgus;
		}

		/// <summary>
		/// Fetches the virtual currencies of your game.
		/// </summary>
		/// <returns>The virtual currencies.</returns>
		override protected List<VirtualCurrency> _getVirtualCurrencies() {
			List<VirtualCurrency> vcs = new List<VirtualCurrency>();
            foreach (SoomlaWpStore.domain.virtualCurrencies.VirtualCurrency wpVC in SoomlaWpStore.data.StoreInfo.getCurrencies())
            {
                vcs.Add(new VirtualCurrency(wpVC));
            }
			return vcs;
		}

		/// <summary>
		/// Fetches the virtual goods of your game.
		/// </summary>
		/// <returns>All virtual goods.</returns>
		override protected List<VirtualGood> _getVirtualGoods() {
			List<VirtualGood> virtualGoods = new List<VirtualGood>();
            foreach (SoomlaWpStore.domain.virtualGoods.VirtualGood wpVG in SoomlaWpStore.data.StoreInfo.getGoods())
            {
                virtualGoods.Add((VirtualGood)VirtualItem.factoryItemFromWP(wpVG));
            }
			return virtualGoods;
		}

		/// <summary>
		/// Fetches the virtual currency packs of your game.
		/// </summary>
		/// <returns>All virtual currency packs.</returns>
		override protected List<VirtualCurrencyPack> _getVirtualCurrencyPacks() {
			List<VirtualCurrencyPack> vcps = new List<VirtualCurrencyPack>();
            foreach (SoomlaWpStore.domain.virtualCurrencies.VirtualCurrencyPack wpVCP in SoomlaWpStore.data.StoreInfo.getCurrencyPacks())
            {
                vcps.Add(new VirtualCurrencyPack(wpVCP));
            }
			return vcps;
		}

		/// <summary>
		/// Fetches the non consumable items of your game.
		/// </summary>
		/// <returns>All non consumable items.</returns>
		override protected List<NonConsumableItem> _getNonConsumableItems() {
			List<NonConsumableItem> nonConsumableItems = new List<NonConsumableItem>();
            foreach (SoomlaWpStore.domain.NonConsumableItem wpNCI in SoomlaWpStore.data.StoreInfo.getNonConsumableItems())
            {
                nonConsumableItems.Add(new NonConsumableItem(wpNCI));
            }
			return nonConsumableItems;
		}

		/// <summary>
		/// Fetches the virtual categories of your game.
		/// </summary>
		/// <returns>All virtual categories.</returns>
		override protected List<VirtualCategory> _getVirtualCategories() {
			List<VirtualCategory> virtualCategories = new List<VirtualCategory>();
            foreach (SoomlaWpStore.domain.VirtualCategory wpVC in SoomlaWpStore.data.StoreInfo.getCategories())
            {
                virtualCategories.Add(new VirtualCategory(wpVC));
            }
			return virtualCategories;
		}
#endif
                                         }
}
