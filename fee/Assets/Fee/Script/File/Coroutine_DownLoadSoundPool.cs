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
	/** ダウンロード。サウンドプール。
	*/
	public class Coroutine_DownLoadSoundPool
	{
		/** ResultType
		*/
		public class ResultType
		{
			public NAudio.Pack_SoundPool soundpool;
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.soundpool = null;
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** ＵＲＬをファイル名とパスに分ける。
		*/
		public static bool ParseUrl(string a_url,ref string a_filename,ref string a_url_path)
		{
			if(a_url != null){
				if(a_url.Length > 0){
					string t_filename = System.IO.Path.GetFileName(a_url);
					string t_url_path = a_url.Substring(0,a_url.Length - t_filename.Length);

					if(System.Text.RegularExpressions.Regex.IsMatch(t_filename,"[0-9a-zA-Z][0-9a-zA-Z\\.\\-_]*") == true){
						a_filename = t_filename;
						a_url_path = t_url_path;
						return true;
					}
				}
			}

			return false;
		}

		/** CoroutineMain
		*/
		public IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,string a_url,uint a_data_version)
		{
			//result
			this.result = new ResultType();

			//ファイル名。
			string t_filename = null;
			string t_url_path = null;
			if(ParseUrl(a_url,ref t_filename,ref t_url_path) == false){
				//失敗。
				this.result.errorstring = "null";
				yield break;
			}

			/* TODO:
			yield return Coroutine_LoadLocalTextFile.CoroutineMain(a_instance,t_filename);

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(a_url)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;
				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.result.errorstring = "webrequest_async == null");
						yield break;
					}
				}else{
					this.result.errorstring = "webrequest == null");
					yield break;
				}

				while(true){
					//プログレス。
					{
						float t_progress = t_webrequest.downloadProgress;
						if(t_progress >= 0.999f){
							t_progress = 0.999f;
						}else if(t_progress < 0.0f){
							t_progress = 0.0f;
						}
						a_instance.SetResultProgress(t_progress);
					}

					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.result.errorstring = t_webrequest.error);
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if((a_instance.IsCancel() == true)||(a_instance.IsDeleteRequest() == true)){
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
					this.result.errorstring = t_exception.Message);
					yield break;
				}

				//成功。
				if(t_result != null){
					a_instance.SetResultTexture(t_result);
					yield break;
				}

				//失敗。
				this.result.errorstring = "null");
				yield break;
			}
			*/
		}
	}
}
