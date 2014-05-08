using UnityEngine;

namespace Soomla {
	public class StoreControllerIOS : StoreController {
#if UNITY_IOS && !UNITY_EDITOR
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
