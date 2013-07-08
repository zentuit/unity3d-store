package com.soomla.unity;

import com.soomla.store.domain.NonConsumableItem;
import com.soomla.store.domain.PurchasableVirtualItem;
import com.soomla.store.domain.VirtualCategory;
import com.soomla.store.domain.VirtualItem;
import com.soomla.store.domain.virtualCurrencies.VirtualCurrency;
import com.soomla.store.domain.virtualCurrencies.VirtualCurrencyPack;
import com.soomla.store.domain.virtualGoods.VirtualGood;
import com.soomla.store.exceptions.VirtualItemNotFoundException;

import java.util.List;

public class StoreInfo {

    public static VirtualItem getVirtualItem(String itemId) throws VirtualItemNotFoundException {
        return com.soomla.store.data.StoreInfo.getVirtualItem(itemId);
    }

    public static PurchasableVirtualItem getPurchasableItem(String productId) throws VirtualItemNotFoundException {
        return com.soomla.store.data.StoreInfo.getPurchasableItem(productId);
    }

    public static VirtualCategory getCategoryForVirtualGood(String goodItemId) throws VirtualItemNotFoundException {
        return com.soomla.store.data.StoreInfo.getCategory(goodItemId);
    }

    public static List<VirtualCurrency> getCurrencies(){
        return com.soomla.store.data.StoreInfo.getCurrencies();
    }

    public static List<VirtualCurrencyPack> getCurrencyPacks() {
        return com.soomla.store.data.StoreInfo.getCurrencyPacks();
    }

    public static List<VirtualGood> getGoods() {
        return com.soomla.store.data.StoreInfo.getGoods();
    }

    public static List<VirtualCategory> getCategories() {
        return com.soomla.store.data.StoreInfo.getCategories();
    }

    public static List<NonConsumableItem> getNonConsumableItems() {
        return com.soomla.store.data.StoreInfo.getNonConsumableItems();
    }
}
