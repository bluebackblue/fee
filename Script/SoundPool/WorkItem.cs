

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。ワークアイテム。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** WorkItem
	*/
	public class WorkItem
	{
		/** Mode
		*/
		private enum Mode
		{
			/** 開始。
			*/
			Start,

			/** Do_File
			*/
			Do_File,

			/** 完了。
			*/
			End
		};

		/** RequestType
		*/
		private enum RequestType
		{
			/** None
			*/
			None,

			/** ロードローカル。パック。
			*/
			LoadLocalPack,

			/** ロードストリーミングアセット。パック。
			*/
			LoadStreamingAssetsPack,

			/** ロードＵＲＬ。パック。
			*/
			LoadUrlPack,
		};

		/** mode
		*/
		private Mode mode;

		/** request_type
		*/
		private RequestType request_type;

		/** request_path
		*/
		private File.Path request_path;

		/** request_post_data
		*/
		private System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> request_post_data;

		/** request_certificate_handler
		*/
		private Fee.File.CustomCertificateHandler request_certificate_handler;

		/** request_data_version
		*/
		private uint request_data_version;

		/** item
		*/
		private Item item;

		/** constructor
		*/
		public WorkItem()
		{
			//mode
			this.mode = Mode.Start;

			//request_type
			this.request_type = RequestType.None;

			//request_path
			this.request_path = null;

			//request_post_data
			this.request_post_data = null;

			//request_certificate_handler
			this.request_certificate_handler = null;

			//request_data_version
			this.request_data_version = 0;

			//item
			this.item = new Item();
		}

		/** リセット。
		*/
		public void Reset()
		{
			//mode
			this.mode = Mode.Start;

			//request_type
			this.request_type = RequestType.None;

			//request_path
			this.request_path = null;

			//request_post_data
			this.request_post_data = null;

			//request_certificate_handler
			this.request_certificate_handler = null;

			//request_data_version
			this.request_data_version = 0;

			//item
			this.item.Reset();
		}

		/** リクエスト。ロードローカル。パック。
		*/
		public void RequestLoadLocalPack(File.Path a_relative_path)
		{
			this.request_type = RequestType.LoadLocalPack;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードストリーミングアセット。パック。
		*/
		public void RequestLoadStreamingAssetsPack(File.Path a_relative_path,uint a_data_version)
		{
			this.request_type = RequestType.LoadStreamingAssetsPack;
			this.request_path = a_relative_path;
			this.request_data_version = a_data_version;
		}

		/** リクエスト。ロードＵＲＬ。パック。
		*/
		public void RequestLoadUrlBinaryFile(File.Path a_url_path,System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler,uint a_data_version)
		{
			this.request_type = RequestType.LoadUrlPack;
			this.request_path = a_url_path;
			this.request_post_data = a_post_data;
			this.request_certificate_handler = a_certificate_handler;
			this.request_data_version = a_data_version;
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** 更新。

			return == true : 完了。

		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Start:
				{
					switch(this.request_type){
					case RequestType.LoadLocalPack:
						{
							if(Fee.SoundPool.SoundPool.GetInstance().GetMainFile().RequestLoadLocalPack(this.request_path) == true){
								this.mode = Mode.Do_File;
							}
						}break;
					case RequestType.LoadStreamingAssetsPack:
						{
							if(Fee.SoundPool.SoundPool.GetInstance().GetMainFile().RequestLoadStreamingAssetsPack(this.request_path,this.request_data_version) == true){
								this.mode = Mode.Do_File;
							}
						}break;
					case RequestType.LoadUrlPack:
						{
							if(Fee.SoundPool.SoundPool.GetInstance().GetMainFile().RequestLoadUrlPack(this.request_path,this.request_post_data,this.request_certificate_handler,this.request_data_version) == true){
								this.mode = Mode.Do_File;
							}
						}break;
					}
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Do_File:
				{
					Main_File t_main = Fee.SoundPool.SoundPool.GetInstance().GetMainFile();

					this.item.SetResultProgress(t_main.GetResultProgress());

					if(t_main.GetResultType() != Main_File.ResultType.None){ 
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_File.ResultType.Pack:
							{
								if(t_main.GetResultPack() != null){
									this.item.SetResultResponseHeader(t_main.GetResultResponseHeader());
									this.item.SetResultPack(t_main.GetResultPack());
									t_success = true;
								}
							}break;
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_main.GetResultErrorString());
							this.item.SetResultResponseHeader(t_main.GetResultResponseHeader());
						}

						//完了。
						t_main.Fix();					

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_main.Cancel();
					}
				}break;
			}

			return false;
		}
	}
}

