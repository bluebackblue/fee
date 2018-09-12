﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。ワーク。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** Work
	*/
	public class Work
	{
		/** Mode
		*/
		private enum Mode
		{
			/** 開始。
			*/
			Start,

			/**	実行中。
			*/
			Do,
	
			/** 完了。
			*/
			End
		};

		/** mode
		*/
		private Mode mode;

		/** url
		*/
		private string url;

		/** datatype
		*/
		private DataType datatype;

		/** assetbundle_version
		*/
		private uint assetbundle_version;

		/** assetbundle_id
		*/
		private long assetbundle_id;

		/** item
		*/
		private Item item;

		/** constructor
		*/
		public Work(string a_url,DataType a_datatype,uint a_assetbundle_version,long a_assetbundle_id)
		{
			//mode
			this.mode = Mode.Start;

			//url
			this.url = a_url;

			//datatype
			this.datatype = a_datatype;

			//assetbundle_version
			this.assetbundle_version = a_assetbundle_version;

			//assetbundle_id
			this.assetbundle_id = a_assetbundle_id;

			//item
			this.item = new Item();
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** 更新。

		戻り値 = true : 完了。

		*/
		public bool Main()
		{
			MonoBehaviour_Www t_www = NDownLoad.DownLoad.GetInstance().GetWww();

			switch(this.mode){
			case Mode.Start:
				{
					AssetBundleList t_assetbundle_list = NDownLoad.DownLoad.GetInstance().GetAssetBundleList();

					AssetBundle t_assetbundle = null;

					//アセットバンドルリストから取得。
					if(this.assetbundle_id != Config.INVALID_ASSSETBUNDLE_ID){
						t_assetbundle = t_assetbundle_list.GetAssetBundle(this.assetbundle_id);
					}

					if(t_assetbundle == null){
						if(t_www.Request(this.url,this.datatype,this.assetbundle_version,this.assetbundle_id) == true){
							//開始。
							this.mode = Mode.Do;
						}
					}else{
						Tool.Log("NDownLoad.Work","GetAssetBundle From AssetBundleList");

						this.item.SetProgress(1.0f);
						this.item.SetResultAssetBundle(t_assetbundle);
						this.mode = Mode.End;
					}
				}break;
			case Mode.Do:
				{
					if(t_www.IsBusy() == false){

						this.item.SetProgress(t_www.GetProgress());

						//結果。
						switch(t_www.GetDataType()){
						case DataType.Text:
							{
								this.item.SetResultText(t_www.GetResultText());
							}break;
						case DataType.Texture:
							{		
								this.item.SetResultTexture(t_www.GetResultTexture());
							}break;
						case DataType.AssetBundle:
							{
								this.item.SetResultAssetBundle(t_www.GetResultAssetBundle());
							}break;
						default:
							{
								string t_error_string = t_www.GetResultErrorString();

								if(t_error_string == null){
									t_error_string = "";
								}

								this.item.SetResultErrorString(t_error_string);
							}break;
						}

						this.mode = Mode.End;
					}else{
						this.item.SetProgress(t_www.GetProgress());
					}
				}break;
			case Mode.End:
				{
				}return true;
			}

			return false;
		}
	}
}

