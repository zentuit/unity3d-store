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

using System;

namespace Soomla {

	/// <summary>
	/// This class contains all string names of the keys/vals in the JSON being parsed all around the SDK.
	/// </summary>
	public static class JSONConsts
	{


		/** Reward **/
		
		public const string BP_REWARDS           = "rewards";
		public const string BP_REWARD_REWARDID   = "rewardId";
		public const string BP_REWARD_AMOUNT     = "amount";
		public const string BP_REWARD_ICONURL    = "iconUrl";
		public const string BP_REWARD_REPEAT     = "repeatable";

		/** Global **/
		
		public const string BP_ASSOCITEMID       = "associatedItemId";
		public const string BP_ASSOCSCOREID      = "associatedScoreId";
		public const string BP_ASSOCWORLDID      = "associatedWorldId";
		public const string BP_DESIRED_RECORD    = "desiredRecord";
		public const string BP_DESIRED_BALANCE   = "desiredBalance";
		public const string BP_NAME              = "name";
		public const string BP_TYPE              = "jsonType";
	}
}

