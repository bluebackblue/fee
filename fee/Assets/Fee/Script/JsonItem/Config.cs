﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。コンフィグ。
*/


/** NJsonItem
*/
namespace NJsonItem
{
	/** Config
	*/
	public class Config
	{
		/** ログ。
		*/
		public static bool LOG_ENABLE = true;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = false;

		/** Double To String
		*/
		public string DOUBLE_TO_STRING_FORMAT = "{0:0.0#########}";
	}
}

