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
using System.Runtime.InteropServices;
using System;

namespace Soomla.Store {

	/// <summary>
	/// This is the parent class of all virtual items in the application.
	/// Almost every entity in your virtual economy will be a virtual item. There are many types
	/// of virtual items, each one will extend this class. Each one of the various types extends
	/// <c>VirtualItem</c> and adds its own behavior to it.
	/// </summary>
	public abstract class VirtualItem : SoomlaEntity<VirtualItem> {

		private const string TAG = "SOOMLA VirtualItem";

		public string ItemId {
			get { return this._id; }
			set { this._id = value; }

		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="description">Description.</param>
		/// <param name="itemId">Item id.</param>
		protected VirtualItem (string name, string description, string itemId)
			: base(itemId, name, description)
		{
		}

#if UNITY_ANDROID && !UNITY_EDITOR
		protected VirtualItem(AndroidJavaObject jniVirtualItem)
			: base(jniVirtualItem)
		{
		}
#endif
		/// <summary>
		/// Constructor.
		/// Generates an instance of <c>VirtualItem</c> from the given <c>JSONObject</c>.
		/// </summary>
		/// <param name="jsonItem">A JSONObject representation of the wanted <c>VirtualItem</c>.</param>
		protected VirtualItem(JSONObject jsonItem)
			: base(jsonItem)
		{
		}

		/// <summary>
		/// Creates relevant virtual item according to given JSON object's className.
		/// </summary>
		/// <returns>The relevant item according to given JSON object's className.</returns>
		/// <param name="jsonItem">Json item.</param>
		public static VirtualItem factoryItemFromJSONObject(JSONObject jsonItem) {
			string className = jsonItem["className"].str;
			switch(className) {
			case "SingleUseVG":
				return new SingleUseVG((JSONObject)jsonItem[@"item"]);
			case "LifetimeVG":
				return new LifetimeVG((JSONObject)jsonItem[@"item"]);
			case "EquippableVG":
				return new EquippableVG((JSONObject)jsonItem[@"item"]);
			case "SingleUsePackVG":
				return new SingleUsePackVG((JSONObject)jsonItem[@"item"]);
			case "VirtualCurrency":
				return new VirtualCurrency((JSONObject)jsonItem[@"item"]);
			case "VirtualCurrencyPack":
				return new VirtualCurrencyPack((JSONObject)jsonItem[@"item"]);
			case "UpgradeVG":
				return new UpgradeVG((JSONObject)jsonItem[@"item"]);
			}

			return null;
		}

#if UNITY_ANDROID && !UNITY_EDITOR

		public static VirtualItem factoryItemFromJNI(AndroidJavaObject jniItem) {
			SoomlaUtils.LogDebug(TAG, "Trying to create VirtualItem with itemId: " + jniItem.Call<string>("getItemId"));

			if (isInstanceOf(jniItem, "com/soomla/store/domain/virtualGoods/SingleUseVG")) {
				return new SingleUseVG(jniItem);
			} else if (isInstanceOf(jniItem, "com/soomla/store/domain/virtualGoods/EquippableVG")) {
				return new EquippableVG(jniItem);
			} else if (isInstanceOf(jniItem, "com/soomla/store/domain/virtualGoods/UpgradeVG")) {
				return new UpgradeVG(jniItem);
			} else if (isInstanceOf(jniItem, "com/soomla/store/domain/virtualGoods/LifetimeVG")) {
				return new LifetimeVG(jniItem);
			} else if (isInstanceOf(jniItem, "com/soomla/store/domain/virtualGoods/SingleUsePackVG")) {
				return new SingleUsePackVG(jniItem);
			} else if (isInstanceOf(jniItem, "com/soomla/store/domain/virtualCurrencies/VirtualCurrency")) {
				return new VirtualCurrency(jniItem);
			} else if (isInstanceOf(jniItem, "com/soomla/store/domain/virtualCurrencies/VirtualCurrencyPack")) {
				return new VirtualCurrencyPack(jniItem);
			} else {
				SoomlaUtils.LogError(TAG, "Couldn't determine what type of class is the given jniItem.");
			}

			return null;
		}
#endif

		public override bool Equals(object obj)	{
			return (obj != null) &&
				(obj.GetType() == this.GetType()) &&
					(((VirtualItem)obj).ItemId == ItemId);
		}

		public override int GetHashCode () {
			return base.GetHashCode ();
		}

		/// <summary>
		/// Gives your user the given amount of the specific virtual item.
		/// For example, when your users play your game for the first time you GIVE them 1000 gems.
		///
		/// NOTE: This action is different than <code>PurchasableVirtualItem</code>'s <code>buy()</code>:
     	/// You use <code>give(int amount)</code> to give your user something for free.
        /// You use <code>buy()</code> to give your user something and get something in return.
		/// 
		/// </summary>
		/// <param name="amount">amount the amount of the specific item to be given</param>
		public int Give(int amount) {
			return Give(amount, true);
		}

		/// <summary>
		/// Works like "give" but receives an argument, notify, to indicate
		/// </summary>
		/// <param name="amount">amount the amount of the specific item to be given.</param>
		/// <param name="notify">notify of change in user's balance of current virtual item.</param>
		public abstract int Give(int amount, bool notify);

		/// <summary>
		/// Takes from your user the given amount of the specific virtual item.
		/// For example, when your user requests a refund, you need to TAKE the item he/she is returning.
		/// </summary>
		/// <param name="amount">The amount of the specific item to be taken.</param>
		public int Take(int amount) {
			return Take(amount, true);
		}

		/// <summary>
		/// Works like "take" but receives an argument, notify, to indicate
		/// if there has been a change in the balance of the current virtual item.
		/// </summary>
		/// <param name="amount">the amount of the specific item to be taken.</param>
		/// <param name="notify">notify of change in user's balance of current virtual item.</param>
		public abstract int Take(int amount, bool notify);

		/// <summary>
		/// Resets this <code>VirtualItem</code>'s balance to the given balance.
		/// </summary>
		/// <returns>The balance of the current virtual item.</returns>
		/// <param name="balance">Balance.</param>
		public int ResetBalance(int balance) {
			return ResetBalance(balance, true);
		}

		/// <summary>
		/// Works like "resetBalance" but receives an argument, notify, to indicate
		/// if there has been a change in the balance of the current virtual item.
		/// </summary>
		/// <returns>The balance after the reset process.</returns>
		/// <param name="balance">The balance of the current virtual item.</param>
		/// <param name="notify">Notify of change in user's balance of current virtual item.</param>
		public abstract int ResetBalance(int balance, bool notify);

		public abstract int GetBalance();

		/// <summary>
		/// Save this instance with changes that were made to it.
		/// The saving is done in the metadata in StoreInfo and being persisted to the local DB.
		/// </summary>
		public void Save() {
			StoreInfo.Save(this);
		}
	}
}
