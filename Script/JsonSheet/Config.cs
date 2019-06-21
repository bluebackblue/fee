

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。コンフィグ。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** Config
	*/
	public class Config
	{
		/** ログ。
		*/
		public static bool LOG_ENABLE = false;

		/** ログエラー。
		*/
		public static bool LOGERROR_ENABLE = true;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = true;

		/** リスロー。
		*/
		public static bool RETHROW_ENABLE = false;




		/** シート名。コンバート。
		*/
		public static string SHEETNAME_CONVERT = "convert";




		/** コンバートシート。コマンド。

			インデックスリストＪＳＯＮにコンバート。

		*/
		public static string CONVERTSHEET_COMMAND_JSON = "<json>";

		/** コンバートシート。コマンド。

			ＥＮＵＭにコンバート。

		*/
		public static string CONVERTSHEET_COMMAND_ENUM = "<enum>";

		/** コンバートシート。コマンド。

			ＳＥにコンバート。

		*/
		public static string CONVERTSHEET_COMMAND_SE = "<se>";

		/** コンバートシート。コマンド。

			エディターデータにコンバート。

		*/
		public static string CONVERTSHEET_COMMAND_EDITORDATA = "<editordata>";

		/** コンバートシート。コマンド。

			リリースデータにコンバート。

		*/
		public static string CONVERTSHEET_COMMAND_RELEASEDATA = "<releasedata>";



		/** ＥＮＵＭシート。コマンド。

			ネームスペース設定。

		*/
		public static string ENUMSHEET_COMMAND_NAMESPACE = "<namespace>";

		/** ＥＮＵＭシート。コマンド。

			ＥＮＵＭ名設定。

		*/
		public static string ENUMSHEET_COMMAND_ENUMNAME = "<enumname>";

		/** ＥＮＵＭシート。コマンド。

			要素設定。

		*/
		public static string ENUMSHEET_COMMAND_ITEM = "<item>";

		/** ＳＥシート。コマンド。
		
			要素設定。

		*/
		public static string SESHEET_COMMAND_ITEM = "<item>";

		/** データシート。コマンド。

			要素設定。リソース。プレハブ。

		*/
		public static string DATASHEET_COMMAND_RESOURCES_PREFAB = "<resources_prefab>";

		/** データシート。コマンド。

			要素設定。リソース。テクスチャー。

		*/
		public static string DATASHEET_COMMAND_RESOURCES_TEXTURE = "<resources_texture>";

		/** データシート。コマンド。

			要素設定。ストリーミングアセット。テクスチャー。

		*/
		public static string DATASHEET_COMMAND_STREAMINGASSETS_TEXTURE = "<streamingassets_texture>";








		/** ＥＮＵＭコンバート。テンプレート。
		*/
		public static string ENUMCONVERT_TEMPLATE_MAIN = 
@"

/** <<namespace>>
*/
namespace <<namespace>>
{
	/** <<enumcomment>>
	*/
	public enum <<enumname>>
	{
<<itemroot>>	}
}

";

		/** ＥＮＵＭコンバート。テンプレート。
		*/
		public static string ENUMCONVERT_TEMPLATE_ITEM = 
@"		/** <<itemcomment>>
		*/
		<<itemname>>,

";





		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_NAMESPACE = "<<namespace>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ENUMCOMMENT = "<<enumcomment>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ENUMNAME = "<<enumname>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ITEMROOT = "<<itemroot>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ITEMCOMMENT = "<<itemcomment>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ITEMNAME = "<<itemname>>";

	}
}

