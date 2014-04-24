### v1.4.3 [view commit logs](https://github.com/soomla/unity3d-store/compare/v1.4.1...v1.4.3)

* Fixes
  * Added "using System" so things will work corrctly on Android. Closes #201
  * Refreshed items were not parsed correctly. Closes #207


### v1.4.2 [view commit logs](https://github.com/soomla/unity3d-store/compare/v1.4.1...v1.4.2)

* Fixes
  * Fixed some build issues in native libraries.
  * Fixed warnings for 'save' function in VirtualItems.

### v1.4.1 [view commit logs](https://github.com/soomla/unity3d-store/compare/v1.4.0...v1.4.1)

* New Features
  * Added an option to save changed item's metadata (closes #197)
  
* Fixes
  * Fixed ios static libs to support multiple archs.


### v1.4.0 [view commit logs](https://github.com/soomla/unity3d-store/compare/v1.3.0...v1.4.0)

* General
  * Changed directory structure - dropped support for unity 3.5 and changed the main source folder name to Soomla.
  * Added a new event "OnMarketItemsRefreshed" that'll be fired when market items details (MarketPrice, MarketTitle and MarketDescription) are refreshed from the mobile (on device) store. Thanks @Whyser and @Idden
  * Added a function to StoreController called "RefreshInventory". It will refresh market items details from the mobile (on device) store.

* Fixes
  * Fixed some issues in android-store Google Play purchase flow. Thanks to @HolymarsHsieh
