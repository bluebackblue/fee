

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。マウス。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Mouse_Position_InputManager_InputMouse
	*/
	class Mouse_Position_InputManager_InputMouse
	{
		/** Main
		*/
		public static bool Main()
		{
			try{
				//デバイス。
				int t_x;
				int t_y;
				{
					int t_pos_x = (int)UnityEngine.Input.mousePosition.x;
					int t_pos_y = UnityEngine.Screen.height - (int)UnityEngine.Input.mousePosition.y;

					//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
					Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen(t_pos_x,t_pos_y,out t_x,out t_y);
				}

				//設定。
				Fee.Input.Input.GetInstance().mouse.cursor.Set(t_x,t_y);

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.mouse_position = "Mouse_Position_InputManager_InputMouse";
				}
				#endif

				return true;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}			
			return false;
		}
	}
}

