package com.soomla.unity;

import java.util.ArrayList;

import com.soomla.store.IStoreAssets;
import com.soomla.store.StoreConfig;
import com.soomla.store.StoreController;
import com.soomla.store.StoreUtils;
import com.soomla.store.data.JSONConsts;
import com.soomla.store.domain.NonConsumableItem;
import com.soomla.store.domain.VirtualCategory;
import com.soomla.store.domain.virtualCurrencies.VirtualCurrency;
import com.soomla.store.domain.virtualCurrencies.VirtualCurrencyPack;
import com.soomla.store.domain.virtualGoods.*;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class StoreAssets implements IStoreAssets {

    public static ArrayList<VirtualCurrency> currencies = new ArrayList<VirtualCurrency>();
    public static ArrayList<VirtualGood> goods = new ArrayList<VirtualGood>();
    public static ArrayList<VirtualCurrencyPack> currencyPacks = new ArrayList<VirtualCurrencyPack>();
    public static ArrayList<VirtualCategory> categories = new ArrayList<VirtualCategory>();
    public static ArrayList<NonConsumableItem> nonConsumables = new ArrayList<NonConsumableItem>();
    public static int version = 0;

    public static void prepare(int oVersion, String storeAssetsJSON) {
        StoreUtils.LogDebug(TAG, "the storeAssets json is: " + storeAssetsJSON);

        try {
            version = oVersion;

            JSONObject jsonObject = new JSONObject(storeAssetsJSON);

            JSONArray virtualCurrencies = jsonObject.getJSONArray(JSONConsts.STORE_CURRENCIES);
            currencies = new ArrayList<VirtualCurrency>();
            for (int i=0; i<virtualCurrencies.length(); i++){
                JSONObject o = virtualCurrencies.getJSONObject(i);
                VirtualCurrency c = new VirtualCurrency(o);
                currencies.add(c);
            }

            JSONArray currencyPacks = jsonObject.getJSONArray(JSONConsts.STORE_CURRENCYPACKS);
            StoreAssets.currencyPacks = new ArrayList<VirtualCurrencyPack>();
            for (int i=0; i<currencyPacks.length(); i++){
                JSONObject o = currencyPacks.getJSONObject(i);
                VirtualCurrencyPack pack = new VirtualCurrencyPack(o);
                StoreAssets.currencyPacks.add(pack);
            }

            // The order in which VirtualGoods are created matters!
            // For example: VGU and VGP depend on other VGs
            JSONObject virtualGoods = jsonObject.getJSONObject(JSONConsts.STORE_GOODS);
            JSONArray suGoods = virtualGoods.getJSONArray(JSONConsts.STORE_GOODS_SU);
            JSONArray ltGoods = virtualGoods.getJSONArray(JSONConsts.STORE_GOODS_LT);
            JSONArray eqGoods = virtualGoods.getJSONArray(JSONConsts.STORE_GOODS_EQ);
            JSONArray upGoods = virtualGoods.getJSONArray(JSONConsts.STORE_GOODS_UP);
            JSONArray paGoods = virtualGoods.getJSONArray(JSONConsts.STORE_GOODS_PA);
            goods = new ArrayList<VirtualGood>();
            for (int i=0; i<suGoods.length(); i++){
                JSONObject o = suGoods.getJSONObject(i);
                SingleUseVG g = new SingleUseVG(o);
                goods.add(g);
            }
            for (int i=0; i<ltGoods.length(); i++){
                JSONObject o = ltGoods.getJSONObject(i);
                LifetimeVG g = new LifetimeVG(o);
                goods.add(g);
            }
            for (int i=0; i<eqGoods.length(); i++){
                JSONObject o = eqGoods.getJSONObject(i);
                EquippableVG g = new EquippableVG(o);
                goods.add(g);
            }
            for (int i=0; i<paGoods.length(); i++){
                JSONObject o = paGoods.getJSONObject(i);
                SingleUsePackVG g = new SingleUsePackVG(o);
                goods.add(g);
            }
            for (int i=0; i<upGoods.length(); i++){
                JSONObject o = upGoods.getJSONObject(i);
                UpgradeVG g = new UpgradeVG(o);
                goods.add(g);
            }

            // categories depend on virtual goods. That's why the have to be initialized after!
            JSONArray virtualCategories = jsonObject.getJSONArray(JSONConsts.STORE_CATEGORIES);
            categories = new ArrayList<VirtualCategory>();
            for(int i=0; i<virtualCategories.length(); i++){
                JSONObject o = virtualCategories.getJSONObject(i);
                VirtualCategory category = new VirtualCategory(o);
                categories.add(category);
            }

            JSONArray nonConsumables = jsonObject.getJSONArray(JSONConsts.STORE_NONCONSUMABLES);
            StoreAssets.nonConsumables = new ArrayList<NonConsumableItem>();
            for (int i=0; i<nonConsumables.length(); i++){
                JSONObject o = nonConsumables.getJSONObject(i);
                NonConsumableItem non = new NonConsumableItem(o);
                StoreAssets.nonConsumables.add(non);
            }

        } catch (JSONException e) {
            StoreUtils.LogError(TAG, "Couldn't parse storeAssetsJSON (unity)");
        } catch (Exception ex) {
            StoreUtils.LogError(TAG, "An error occurred while trying to prepare storeAssets (unity) " + ex.getMessage());
        }
    }

    @Override
    public int getVersion() {
        return version;
    }

    @Override
    public VirtualCurrency[] getCurrencies() {
        return currencies.toArray(new VirtualCurrency[currencies.size()]);
    }

    @Override
    public VirtualGood[] getGoods() {
        return goods.toArray(new VirtualGood[goods.size()]);
    }

    @Override
    public VirtualCurrencyPack[] getCurrencyPacks() {
        return currencyPacks.toArray(new VirtualCurrencyPack[currencyPacks.size()]);
    }

    @Override
    public VirtualCategory[] getCategories() {
        return categories.toArray(new VirtualCategory[categories.size()]);
    }

    @Override
    public NonConsumableItem[] getNonConsumableItems() {
        return nonConsumables.toArray(new NonConsumableItem[nonConsumables.size()]);
    }

//    public static VirtualCategory createVirtualCategory(String name, int id, int em) {
//        VirtualCategory.EquippingModel equippingModel = VirtualCategory.EquippingModel.MULTIPLE;
//        if (em == 0) {
//            equippingModel = VirtualCategory.EquippingModel.NONE;
//        } else if (em == 1) {
//            equippingModel = VirtualCategory.EquippingModel.SINGLE;
//        }
//
//        return new VirtualCategory(name, id, equippingModel);
//    }

//    public static GoogleMarketItem createGoogleMarketItem(String id, int managed, double price) {
//        if (managed == 0) {
//            return new GoogleMarketItem(id, Managed.MANAGED, price);
//        } else if (managed == 1) {
//            return new GoogleMarketItem(id, Managed.UNMANAGED, price);
//        } else if (managed == 2) {
//            return new GoogleMarketItem(id, Managed.SUBSCRIPTION, price);
//        } else {
//            return new GoogleMarketItem(id, Managed.UNMANAGED, price);
//        }
//    }

//    public static HashMap<String, Integer> createStringIntegerHashMap() {
//        return new HashMap<String, Integer>();
//    }

//    public static ArrayList<HashMap<String, Integer>> createStringIntegerHashMapArrayList() {
//        return new ArrayList<HashMap<String, Integer>>();
//    }

//    public static void voidPutIntoStringIntegerHashMap(HashMap<String, Integer> map, String key, int value) {
//        map.put(key, value);
//    }

    public static void setSoomSec(String soomSec) {
        StoreConfig.SOOM_SEC = soomSec;
    }

//    public static void setVersion(int version) {
//        StoreAssets.version = version;
//    }


    private static String TAG = "SOOMLA StoreAssets (unity)";
}