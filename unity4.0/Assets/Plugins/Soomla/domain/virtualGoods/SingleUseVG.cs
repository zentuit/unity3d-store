/*
 * Copyright (C) 2012 Soomla Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using UnityEngine;
using System.Collections;


namespace com.soomla.unity{	
	
	/// <summary>
	/// This is an abstract representation of the application's virtual good.
	/// Your game's virtual economy revolves around virtual goods. This class defines the abstract
	/// and most common virtual good while the descendants of this class defines specific definitions of VirtualGood.
	/// </summary>
	public abstract class VirtualGood : PurchasableVirtualItem{
		
		public VirtualGood(string name, string description, string itemId, PurchaseType purchaseType)
			: base(name, description, itemId, purchaseType)
		{
		}
		
#if UNITY_ANDROID
		public VirtualGood(AndroidJavaObject jniVirtualGood) 
			: base(jniVirtualGood)
		{
			// Virtual Category
			using(AndroidJavaObject jniVirtualCategory = jniVirtualGood.Call<AndroidJavaObject>("getCategory")) {
				this.Category = new VirtualCategory(jniVirtualCategory);
			}

			// Price Model
			using(AndroidJavaObject jniPriceModel = jniVirtualGood.Call<AndroidJavaObject>("getPriceModel")) {
				this.PriceModel = AbstractPriceModel.CreatePriceModel(jniPriceModel);
			}
		}
		
		public AndroidJavaObject toAndroidJavaObject(AndroidJavaObject jniUnityStoreAssets, AndroidJavaObject jniVirtualCategory) {
			return new AndroidJavaObject("com.soomla.store.domain.data.VirtualGood", this.Name, this.Description, 
				this.PriceModel.toAndroidJavaObject(jniUnityStoreAssets), this.ItemId, jniVirtualCategory);
		}
#endif
		/// <summary>
		/// see parent
		/// </summary>
		public VirtualGood(JSONObject jsonVg)
			: base(jsonVg)
		{
		}

		/// <summary>
		/// see parent
		/// </summary>
		public override JSONObject toJSONObject() {
			return base.toJSONObject();
		}

	}
}
