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

namespace Soomla {

	/// <summary>
	/// <c>StoreController</c> for iOS.
	/// </summary>
	public class StoreControllerIOS : StoreController {
#if UNITY_IOS && !UNITY_EDITOR

		///
		[DllImport ("__Internal")]
		private static extern void storeController_Init(string customSecret);
		[DllImport ("__Internal")]
		private static extern int storeController_BuyMarketItem(string productId);
		[DllImport ("__Internal")]
		private static extern void storeController_RestoreTransactions();
		[DllImport ("__Internal")]
		private static extern void storeController_RefreshInventory();
		[DllImport ("__Internal")]
		private static extern void storeController_TransactionsAlreadyRestored(out bool outResult);
		[DllImport ("__Internal")]
		private static extern void storeController_SetSoomSec(string soomSec);
		[DllImport ("__Internal")]
		private static extern void storeController_SetSSV(bool ssv, string verifyUrl);

		protected override void _initialize(IStoreAssets storeAssets) {
			storeController_SetSSV(SoomSettings.IosSSV, "https://verify.soom.la/verify_ios?platform=unity4");
			StoreInfo.Initialize(storeAssets);
			storeController_Init(SoomSettings.CustomSecret);
		}
		
		protected override void _setupSoomSec() {
			storeController_SetSoomSec(SoomSettings.SoomSecret);
		}
		
		protected override void _buyMarketItem(string productId) {
			storeController_BuyMarketItem(productId);
		}
		
		protected override void _refreshInventory() {
			storeController_RefreshInventory();
		}
		
		protected override void _restoreTransactions() {
			storeController_RestoreTransactions();
		}

		protected override bool _transactionsAlreadyRestored() {
			bool restored = false;
			storeController_TransactionsAlreadyRestored(out restored);
			return restored;
		}
#endif
	}
}
