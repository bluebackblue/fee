﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief タスク。
*/


/** NTaskW
*/
namespace NTaskW
{
	/** TaskW
	*/
	public class TaskW
	{
		/** [シングルトン]s_instance
		*/
		private static TaskW s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new TaskW();
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static TaskW GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** 同期コンテキスト。
		*/
		private Task_Sync main_sync;

		/** [シングルトン]constructor
		*/
		private TaskW()
		{
			this.main_sync = new Task_Sync();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.main_sync.Delete();
			this.main_sync = null;
		}

		/** 同期コンテキスト。取得。
		*/
		public Task_Sync GetTaskSync()
		{
			return this.main_sync;
		}
	}
}

