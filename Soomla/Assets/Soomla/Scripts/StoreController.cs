using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace Soomla
{
	/// <summary>
	/// You can use this class to purchase products from the native phone market, buy virtual goods, and do many other store related operations.
	/// </summary>
	public class StoreController
	{
		static StoreController _instance = null;
		static StoreController instance {
			get {
				if(_instance == null) {
				#if UNITY_EDITOR
					_instance = new StoreController();
				#elif UNITY_ANDROID
					_instance = new StoreControllerAndroid();
				#elif UNITY_IOS
					_instance = new StoreControllerIOS();
				#endif
				}
				return _instance;
			}
		}

		protected const string TAG = "SOOMLA StoreController";

		public static void Initialize(IStoreAssets storeAssets) {
			if (string.IsNullOrEmpty(SoomSettings.CustomSecret)) {
				StoreUtils.LogError(TAG, "SOOMLA/UNITY MISSING customSecret !!! Stopping here !!");
				throw new ExitGUIException();
			}

			if (SoomSettings.CustomSecret==SoomSettings.ONLY_ONCE_DEFAULT) {
				StoreUtils.LogError(TAG, "SOOMLA/UNITY You have to change customSecret !!! Stopping here !!");
				throw new ExitGUIException();
			}

			if (string.IsNullOrEmpty(SoomSettings.SoomSecret)) {
				StoreUtils.LogError(TAG, "SOOMLA/UNITY MISSING soomSec !!! Stopping here !!");
				throw new ExitGUIException();
			}

			if (SoomSettings.SoomSecret==SoomSettings.ONLY_ONCE_DEFAULT) {
				StoreUtils.LogError(TAG, "SOOMLA/UNITY You have to change soomSec !!! Stopping here !!");
				throw new ExitGUIException();
			}

			instance._setupSoomSec();
			instance._initialize(storeAssets);
		}

		public static void BuyMarketItem(string productId) { instance._buyMarketItem(productId); }
		public static void RefreshInventory() {	instance._refreshInventory(); }
		public static void RestoreTransactions() { instance._restoreTransactions();	}
		public static bool TransactionsAlreadyRestored() { return instance._transactionsAlreadyRestored(); }
		public static void StartIabServiceInBg() { instance._startIabServiceInBg(); }
		public static void StopIabServiceInBg() { instance._stopIabServiceInBg(); }

		protected virtual void _initialize(IStoreAssets storeAssets) {
			//implementation for unity editor here
		}

		protected virtual void _setupSoomSec() {
			//implementation for unity editor here
		}

		protected virtual void _buyMarketItem(string productId) {
			//implementation for unity editor here
		}

		protected virtual void _refreshInventory() {
			//implementation for unity editor here
		}

		protected virtual void _restoreTransactions() {
			//implementation for unity editor here
		}

		protected virtual bool _transactionsAlreadyRestored() {
			return true;
		}

		protected virtual void _startIabServiceInBg() {
		}

		protected virtual void _stopIabServiceInBg() {
		}

	}
}

