package com.soomla.unity;

import com.soomla.store.BusProvider;
import com.soomla.store.events.*;
import com.squareup.otto.Subscribe;
import com.unity3d.player.UnityPlayer;

public class EventHandler {
    private static EventHandler mLocalEventHandler;

    public static void initialize() {
        mLocalEventHandler = new EventHandler();

    }

    public EventHandler() {
        BusProvider.getInstance().register(this);
    }

    @Subscribe
    public void onBillingSupported(BillingSupportedEvent billingSupportedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onBillingSupported", "");
    }

    @Subscribe
    public void onBillingNotSupported(BillingNotSupportedEvent billingNotSupportedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onBillingNotSupported", "");
    }

    @Subscribe
    public void onClosingStore(ClosingStoreEvent closingStoreEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onClosingStore", "");
    }

    @Subscribe
    public void onCurrencyBalanceChanged(CurrencyBalanceChangedEvent currencyBalanceChangedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onCurrencyBalanceChanged",
                currencyBalanceChangedEvent.getCurrency().getItemId() + "#SOOM#" +
                currencyBalanceChangedEvent.getBalance() + "#SOOM#" +
                currencyBalanceChangedEvent.getAmountAdded());
    }

    @Subscribe
    public void onGoodBalanceChanged(GoodBalanceChangedEvent goodBalanceChangedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onGoodBalanceChanged",
                goodBalanceChangedEvent.getGood().getItemId() + "#SOOM#" +
                        goodBalanceChangedEvent.getBalance() + "#SOOM#" +
                        goodBalanceChangedEvent.getAmountAdded());
    }

    @Subscribe
    public void onGoodEquipped(GoodEquippedEvent goodEquippedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onGoodEquipped", goodEquippedEvent.getGood().getItemId());
    }

    @Subscribe
    public void onGoodUnequipped(GoodUnEquippedEvent goodUnEquippedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onGoodUnequipped", goodUnEquippedEvent.getGood().getItemId());
    }

    @Subscribe
    public void onGoodUpgrade(GoodUpgradeEvent goodUpgradeEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onGoodUpgrade",
                goodUpgradeEvent.getGood().getItemId()  + "#SOOM#" +
                goodUpgradeEvent.getCurrentUpgrade().getItemId());
    }

    @Subscribe
    public void onItemPurchased(ItemPurchasedEvent itemPurchasedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onItemPurchased", itemPurchasedEvent.getPurchasableVirtualItem().getItemId());
    }

    @Subscribe
    public void onItemPurchaseStarted(ItemPurchaseStartedEvent itemPurchaseStartedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onItemPurchaseStarted", itemPurchaseStartedEvent.getPurchasableVirtualItem().getItemId());
    }

    @Subscribe
    public void onOpeningStore(OpeningStoreEvent openingStoreEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onOpeningStore", "");
    }

    @Subscribe
    public void onMarketPurchaseCancelled(PlayPurchaseCancelledEvent playPurchaseCancelledEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onMarketPurchaseCancelled",
                playPurchaseCancelledEvent.getPurchasableVirtualItem().getItemId());
    }

    @Subscribe
    public void onMarketPurchase(PlayPurchaseEvent playPurchaseEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onMarketPurchase", playPurchaseEvent.getPurchasableVirtualItem().getItemId());
    }

    @Subscribe
    public void onMarketPurchaseStarted(PlayPurchaseStartedEvent playPurchaseStartedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onMarketPurchaseStarted", playPurchaseStartedEvent.getPurchasableVirtualItem().getItemId());
    }

    @Subscribe
    public void onMarketRefund(PlayRefundEvent playRefundEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onMarketRefund", playRefundEvent.getPurchasableVirtualItem().getItemId());
    }

    @Subscribe
    public void onRestoreTransactions(RestoreTransactionsEvent restoreTransactionsEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onRestoreTransactions", (restoreTransactionsEvent.isSuccess() ? 1 : 0) + "");
    }

    @Subscribe
    public void onRestoreTransactionsStarted(RestoreTransactionsStartedEvent restoreTransactionsStartedEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onRestoreTransactionsStarted", "");
    }

    @Subscribe
    public void onUnexpectedStoreError(UnexpectedStoreErrorEvent unexpectedStoreErrorEvent) {
        UnityPlayer.UnitySendMessage("Soomla", "onUnexpectedStoreError", "");
    }

}
