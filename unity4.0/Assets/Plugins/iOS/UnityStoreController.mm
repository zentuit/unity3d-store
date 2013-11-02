#import "UnityStoreAssets.h"
#import "UnityStoreEventDispatcher.h"
#import "VirtualCategory.h"
#import "VirtualCurrency.h"
#import "VirtualGood.h"
#import "AppStoreItem.h"
#import "VirtualCurrencyPack.h"
#import "StoreController.h"
#import "VirtualItemNotFoundException.h"
#import "NotEnoughGoodsException.h"
#import "UnityCommons.h"
#import "StoreConfig.h"
#import "NonConsumableItem.h"
#import "StoreInfo.h"
#import "PurchaseWithMarket.h"

#import "ObscuredNSUserDefaults.h"

UnityStoreEventDispatcher * storeEventDispatcher;
extern UIViewController* UnityGetGLViewController();

extern "C"{
	    
    void storeController_SetSoomSec(const char* soomSec) {
        if (SOOM_SEC) {
            [SOOM_SEC release];
        }
        SOOM_SEC = [[NSString stringWithUTF8String:soomSec] retain];
        [ObscuredNSUserDefaults setString:@"https://highway-dev.soom.la:3002/" forKey:@"II#PHW9"];
        [ObscuredNSUserDefaults setString:@"https://driveway-dev.soom.la:3002/" forKey:@"II#OIS2"];
        [ObscuredNSUserDefaults setInt:0 forKey:@"KL#G87"];
        [ObscuredNSUserDefaults setInt:0 forKey:@"KL#G78"];
        VERIFY_URL = @"https://verify-dev.soom.la:3002/verify_ios?platform=unity4";
    }
	
    void storeController_SetSSV(bool ssv) {
		VERIFY_PURCHASES = ssv;
    }

	void storeController_Init(const char* secret){
		storeEventDispatcher = [[UnityStoreEventDispatcher alloc] init];
		[[StoreController getInstance] initializeWithStoreAssets:[UnityStoreAssets getInstance] andCustomSecret:[NSString stringWithUTF8String:secret]];
	}
	
	int storeController_BuyMarketItem(const char* productId) {
		@try {
			PurchasableVirtualItem* pvi = [[StoreInfo getInstance] purchasableItemWithProductId:[NSString stringWithUTF8String:productId]];
			if ([pvi.purchaseType isKindOfClass:[PurchaseWithMarket class]]) {
				AppStoreItem* asi = ((PurchaseWithMarket*) pvi.purchaseType).appStoreItem;
				[[StoreController getInstance] buyInAppStoreWithAppStoreItem:asi];
			} else {
				NSLog(@"The requested PurchasableVirtualItem is has no PurchaseWithMarket PurchaseType. productId: %@. Purchase is cancelled.", [NSString stringWithUTF8String:productId]);
				return EXCEPTION_ITEM_NOT_FOUND;
			}
		} 
		
        @catch (VirtualItemNotFoundException *e) {
            NSLog(@"Couldn't find a VirtualCurrencyPack with productId: %@. Purchase is cancelled.", [NSString stringWithUTF8String:productId]);
			return EXCEPTION_ITEM_NOT_FOUND;
        }
		
		return NO_ERR;
	}
		
	void storeController_StoreOpening() {
		[[StoreController getInstance] storeOpening];
	}
	
	void storeController_StoreClosing() {
		[[StoreController getInstance] storeClosing];
	}
	
	void storeController_RestoreTransactions() {
		[[StoreController getInstance] restoreTransactions];
	}
	
	void storeController_TransactionsAlreadyRestored(bool* outResult){
		*outResult = [[StoreController getInstance] transactionsAlreadyRestored];
	}
	
}