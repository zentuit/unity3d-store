using System;
using System.Collections.Generic;

namespace Soomla.Example
{
	public class ExampleEventHandler
	{
		
		public ExampleEventHandler ()
		{
			StoreEvents.OnMarketPurchase += onMarketPurchase;
			StoreEvents.OnMarketRefund += onMarketRefund;
			StoreEvents.OnItemPurchased += onItemPurchased;
			StoreEvents.OnGoodEquipped += onGoodEquipped;
			StoreEvents.OnGoodUnEquipped += onGoodUnequipped;
			StoreEvents.OnGoodUpgrade += onGoodUpgrade;
			StoreEvents.OnBillingSupported += onBillingSupported;
			StoreEvents.OnBillingNotSupported += onBillingNotSupported;
			StoreEvents.OnMarketPurchaseStarted += onMarketPurchaseStarted;
			StoreEvents.OnItemPurchaseStarted += onItemPurchaseStarted;
			StoreEvents.OnUnexpectedErrorInStore += onUnexpectedErrorInStore;
			StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
			StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
			StoreEvents.OnMarketPurchaseCancelled += onMarketPurchaseCancelled;
			StoreEvents.OnRestoreTransactionsStarted += onRestoreTransactionsStarted;
			StoreEvents.OnRestoreTransactionsFinished += onRestoreTransactionsFinished;
			StoreEvents.OnStoreControllerInitialized += onStoreControllerInitialized;
#if UNITY_ANDROID && !UNITY_EDITOR
			StoreEvents.OnIabServiceStarted += onIabServiceStarted;
			StoreEvents.OnIabServiceStopped += onIabServiceStopped;
#endif
		}
		
		public void onMarketPurchase(PurchasableVirtualItem pvi) {
			
		}
		
		public void onMarketRefund(PurchasableVirtualItem pvi) {

		}
		
		public void onItemPurchased(PurchasableVirtualItem pvi) {

		}
		
		public void onGoodEquipped(EquippableVG good) {
			
		}
		
		public void onGoodUnequipped(EquippableVG good) {
			
		}
		
		public void onGoodUpgrade(VirtualGood good, UpgradeVG currentUpgrade) {
			
		}
		
		public void onBillingSupported() {
			
		}
		
		public void onBillingNotSupported() {
			
		}
		
		public void onMarketPurchaseStarted(PurchasableVirtualItem pvi) {
			
		}
		
		public void onItemPurchaseStarted(PurchasableVirtualItem pvi) {
			
		}
		
		public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi) {
			
		}
		
		public void onUnexpectedErrorInStore(string message) {
			
		}
		
		public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance, int amountAdded) {
			ExampleLocalStoreInfo.UpdateBalances();
		}
		
		public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded) {
			ExampleLocalStoreInfo.UpdateBalances();
		}
		
		public void onRestoreTransactionsStarted() {
			
		}
		
		public void onRestoreTransactionsFinished(bool success) {
			
		}
		
		public void onStoreControllerInitialized() {
			ExampleLocalStoreInfo.Init();
			
			// some usage examples for add/remove currency
            // some examples
            if (ExampleLocalStoreInfo.VirtualCurrencies.Count>0) {
                try {
                    StoreInventory.GiveItem(ExampleLocalStoreInfo.VirtualCurrencies[0].ItemId,4000);
                    StoreUtils.LogDebug("SOOMLA ExampleEventHandler", "Currency balance:" + StoreInventory.GetItemBalance(ExampleLocalStoreInfo.VirtualCurrencies[0].ItemId));
                } catch (VirtualItemNotFoundException ex){
                    StoreUtils.LogError("SOOMLA ExampleEventHandler", ex.Message);
                }
            }
		}
		
#if UNITY_ANDROID && !UNITY_EDITOR
		public void onIabServiceStarted() {
			
		}
		public void onIabServiceStopped() {
			
		}
#endif
	}
}

