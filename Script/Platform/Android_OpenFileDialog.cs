

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プラットフォーム。オープンファイルダイアログ。
*/


/** Fee.Platform
*/
namespace Fee.Platform
{
	#if((!UNITY_EDITOR)&&(UNITY_ANDROID))

	/** Android_OpenFileDialog
	*/
	class Android_OpenFileDialog
	{
		/** OpenFileDialog
		*/
		public static void OpenFileDialog(MonoBehaviour_Root a_root_instance)
		{
			a_root_instance.openfiledialog_result = null;

			UnityEngine.AndroidJavaClass t_class_openfiledialog = new UnityEngine.AndroidJavaClass("fee.platform.Android_OpenFileDialogActivity");
			t_class_openfiledialog.CallStatic("DO","*/*");
		}
	}

	#endif
}

