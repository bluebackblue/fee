

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。ＪＳＯＮシート。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** ConvertSheet_Json
	*/
	#if(UNITY_EDITOR)
	public class ConvertSheet_Json
	{
		/** COMMAND
		*/
		public const string COMMAND = "<json>";

		/** コンバート。
		*/
		public static void Convert(string a_convert_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet,Fee.JsonSheet.ConvertParam a_convertparam)
		{
			try{
				Fee.JsonItem.JsonItem t_jsonitem = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_IndexArray());

				if(a_sheet != null){
					for(int ii=0;ii<a_sheet.Length;ii++){
						if(a_sheet[ii] != null){
							int jj_max = a_sheet[ii].GetListMax();
							for(int jj=0;jj<jj_max;jj++){
								Fee.JsonItem.JsonItem t_list_item = a_sheet[ii].GetItem(jj);
								if(t_list_item != null){
									t_jsonitem.AddItem(t_list_item,false);
								}else{
									Tool.Assert(false);
								}
							}
						}
					}
				}else{
					Tool.Assert(false);
				}

				Fee.EditorTool.AssetTool.WriteTextFile(a_assets_path,t_jsonitem.ConvertToJsonString());
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
	#endif
}

