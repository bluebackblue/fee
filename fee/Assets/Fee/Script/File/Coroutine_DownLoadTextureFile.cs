﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** NFile
*/
namespace NFile
{
	/** ダウンロード。テクスチャーファイル。
	*/
	public class Coroutine_DownLoadTextureFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			public Texture2D texture;
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.texture = null;
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,string a_url)
		{
			//result
			this.result = new ResultType();

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(a_url)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;
				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.result.errorstring = "webrequest_async == null";
						yield break;
					}
				}else{
					this.result.errorstring = "webrequest == null";
					yield break;
				}

				while(true){
					//プログレス。
					/*
					{
						float t_progress = t_webrequest.downloadProgress;
						if(t_progress >= 0.999f){
							t_progress = 0.999f;
						}else if(t_progress < 0.0f){
							t_progress = 0.0f;
						}
						a_instance.SetResultProgress(t_progress);
					}
					*/

					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.result.errorstring = t_webrequest.error;
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if(a_instance.OnCoroutine() == false){
						t_webrequest.Abort();
					}

					yield return null;
				}

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				//コンバート。
				Texture2D t_result = null;
				try{
					t_result = UnityEngine.Networking.DownloadHandlerTexture.GetContent(t_webrequest);
				}catch(System.Exception t_exception){
					this.result.errorstring = t_exception.Message;
					yield break;
				}

				//成功。
				if(t_result != null){
					this.result.texture = t_result;
					yield break;
				}

				//失敗。
				this.result.errorstring = "null";
				yield break;
			}
		}
	}
}
