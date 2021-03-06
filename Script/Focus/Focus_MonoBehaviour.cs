

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フォーカス。
*/


/** Fee.Focus
*/
namespace Fee.Focus
{
	/** Focus_MonoBehaviour
	*/
	public class Focus_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** callbackparam_focuscheck
		*/
		private Fee.Focus.OnFocusCheck_CallBackParam callbackparam_focuscheck;

		/** コールバックインターフェイス。設定。
		*/
		public void SetOnFocusCheck<T>(Fee.Focus.OnFocusCheck_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callbackparam_focuscheck = new Fee.Focus.OnFocusCheck_CallBackParam_Generic<T>(a_callback_interface,a_id);
		}

		/** 呼び出し。
		*/
		public void CallOnFocusCheck()
		{
			if(this.callbackparam_focuscheck != null){
				this.callbackparam_focuscheck.Call();
			}
		}
	}
}

