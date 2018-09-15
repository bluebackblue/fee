﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。パッド。
*/


//The variable "" is assigned but its value is never used.
#pragma warning disable 0219


/** NInput
*/
namespace NInput
{
	/** Pad
	*/
	public class Pad
	{
		/** [シングルトン]s_instance
		*/
		private static Pad s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Pad();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Pad GetInstance()
		{
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

		/** デジタルボタン。
		*/
		public Digital_Button left;
		public Digital_Button right;
		public Digital_Button up;
		public Digital_Button down;
		public Digital_Button enter;
		public Digital_Button escape;
		public Digital_Button sub1;
		public Digital_Button sub2;
		public Digital_Button left_menu;
		public Digital_Button right_menu;

		/** アナログスティック。
		*/
		public Analog_Stick left_stick;
		public Analog_Stick right_stick;	
		public Digital_Button left_stick_button;
		public Digital_Button right_stick_button;

		/** トリガーボタン。
		*/
		public Digital_Button left_trigger1_button;
		public Digital_Button right_trigger1_button;
		public Analog_Button left_trigger2_button;
		public Analog_Button right_trigger2_button;

		/** [シングルトン]constructor
		*/
		private Pad()
		{
			//デジタルボタン。
			this.left.Reset();
			this.right.Reset();
			this.up.Reset();
			this.down.Reset();
			this.enter.Reset();
			this.escape.Reset();
			this.sub1.Reset();
			this.sub2.Reset();
			this.left_menu.Reset();
			this.right_menu.Reset();

			//アナログスティック。
			this.left_stick.Reset();
			this.right_stick.Reset();
			this.left_stick_button.Reset();
			this.right_stick_button.Reset();

			//トリガーボタン。
			this.left_trigger1_button.Reset();
			this.right_trigger1_button.Reset();
			this.left_trigger2_button.Reset();
			this.right_trigger2_button.Reset();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				UnityEngine.Experimental.Input.Gamepad t_gamepad_current = UnityEngine.Experimental.Input.Gamepad.current;

				//デジタルボタン。
				if(t_gamepad_current != null){
					//デバイス。
					bool t_left_on = t_gamepad_current.dpad.left.isPressed;
					bool t_right_on = t_gamepad_current.dpad.right.isPressed;
					bool t_up_on = t_gamepad_current.dpad.up.isPressed;
					bool t_down_on = t_gamepad_current.dpad.down.isPressed;
					bool t_enter_on = t_gamepad_current.buttonEast.isPressed;
					bool t_escape_on = t_gamepad_current.buttonSouth.isPressed;
					bool t_sub1_on = t_gamepad_current.buttonNorth.isPressed;
					bool t_sub2_on = t_gamepad_current.buttonWest.isPressed;
					bool t_left_menu_on = t_gamepad_current.selectButton.isPressed;
					bool t_right_menu_on = t_gamepad_current.startButton.isPressed;

					//設定。
					this.left.Set(t_left_on);
					this.right.Set(t_right_on);
					this.up.Set(t_up_on);
					this.down.Set(t_down_on);
					this.enter.Set(t_enter_on);
					this.escape.Set(t_escape_on);
					this.sub1.Set(t_sub1_on);
					this.sub2.Set(t_sub2_on);
					this.left_menu.Set(t_left_menu_on);
					this.right_menu.Set(t_right_menu_on);
				}else{
					//設定。
					this.left.Set(false);
					this.right.Set(false);
					this.up.Set(false);
					this.down.Set(false);
					this.enter.Set(false);
					this.escape.Set(false);
					this.sub1.Set(false);
					this.sub2.Set(false);
				}

				//アナログスティック。
				if(t_gamepad_current != null){
					//デバイス。
					float t_l_x = t_gamepad_current.leftStick.x.ReadValue();
					float t_l_y = t_gamepad_current.leftStick.y.ReadValue();
					float t_r_x = t_gamepad_current.rightStick.x.ReadValue();
					float t_r_y = t_gamepad_current.rightStick.y.ReadValue();
					bool t_l_on = t_gamepad_current.leftStickButton.isPressed;
					bool t_r_on = t_gamepad_current.rightStickButton.isPressed;

					//設定。
					this.left_stick.Set(t_l_x,t_l_y);
					this.right_stick.Set(t_r_x,t_r_y);
					this.left_stick_button.Set(t_l_on);
					this.right_stick_button.Set(t_r_on);
				}

				//トリガーボタン。
				if(t_gamepad_current != null){
					//デバイス。
					bool t_l_1 = t_gamepad_current.leftShoulder.isPressed;
					bool t_r_1 = t_gamepad_current.rightShoulder.isPressed;
					float t_l_2 = t_gamepad_current.leftTrigger.ReadValue();
					float t_r_2 = t_gamepad_current.rightTrigger.ReadValue();

					//設定。
					this.left_trigger1_button.Set(t_l_1);
					this.right_trigger1_button.Set(t_r_1);
					this.left_trigger2_button.Set(t_l_2);
					this.right_trigger2_button.Set(t_r_2);
				}

				//更新。
				{
					//デジタルボタン。
					this.left.Main();
					this.right.Main();
					this.up.Main();
					this.down.Main();
					this.enter.Main();
					this.escape.Main();
					this.sub1.Main();
					this.sub2.Main();
					this.left_menu.Main();
					this.right_menu.Main();

					//アナログスティック。
					this.left_stick.Main();
					this.right_stick.Main();
					this.left_stick_button.Main();
					this.right_stick_button.Main();

					//トリガーボタン。
					this.left_trigger1_button.Main();
					this.right_trigger1_button.Main();
					this.left_trigger2_button.Main();
					this.right_trigger2_button.Main();
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** 移動チェック。ダウン時。
		*/
		public Dir4Type DownMoveCheck()
		{
			if(this.up.down == true){
				return Dir4Type.Up;
			}else if(this.down.down == true){
				return Dir4Type.Down;
			}else if(this.left.down == true){
				return Dir4Type.Left;
			}else if(this.right.down == true){
				return Dir4Type.Right;
			}

			return Dir4Type.None;
		}

		/** 移動チェック。オン時。
		*/
		public Dir4Type OnMoveCheck()
		{
			if(this.up.on == true){
				return Dir4Type.Up;
			}else if(this.down.on == true){
				return Dir4Type.Down;
			}else if(this.left.on == true){
				return Dir4Type.Left;
			}else if(this.right.on == true){
				return Dir4Type.Right;
			}

			return Dir4Type.None;
		}
	}
}
