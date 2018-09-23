﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。オブジェクト化。
*/


/** NJsonItem
*/
namespace NJsonItem
{
	/** JsonToObject_SystemObject
	*/
	public class JsonToObject_SystemObject
	{
		/** Convert
		*/
		public static void Convert(ref System.Object a_to_object,JsonItem a_jsonitem,List<JsonToObject_Work> a_workpool = null)
		{
			List<JsonToObject_Work> t_workpool = a_workpool;

			if(t_workpool == null){
				t_workpool = new List<JsonToObject_Work>();
			}

			if(a_to_object != null){
				System.Type t_type = a_to_object.GetType();

				if(a_jsonitem.IsStringData() == true){
					if(t_type == typeof(string)){
						//string
						a_to_object = a_jsonitem.GetStringData();
					}
				}else if(a_jsonitem.IsIntegerNumber() == true){
					if((t_type == typeof(int))||(t_type == typeof(long))){
						//int
						//long
						a_to_object = a_jsonitem.GetInteger();
					}
				}else if(a_jsonitem.IsFloatNumber() == true){
					if((t_type == typeof(float))||(t_type == typeof(double))){
						//float
						//double
						a_to_object = a_jsonitem.GetFloat();
					}
				}else if(a_jsonitem.IsIndexArray() == true){
					if(t_type.IsGenericType == true){
						if(t_type.GetGenericTypeDefinition() == typeof(List<>)){
							//List

							IList t_list = a_to_object as IList;
							System.Type t_type_member = t_type.GetGenericArguments()[0];

							for(int ii=0;ii<a_jsonitem.GetListMax();ii++){
								JsonItem t_jsonitem_member = a_jsonitem.GetItem(ii);

								System.Object t_object_member = null;

								try{
									t_object_member = System.Activator.CreateInstance(t_type_member);
								}catch(System.Exception t_exception){
									//引数なしconstructorの呼び出しに失敗。
									Tool.LogError(t_exception);
								}

								if(t_object_member != null){
									if(t_type_member.IsClass == true){
										t_workpool.Add(new JsonToObject_Work(t_object_member,t_jsonitem_member));
									}else{
										JsonToObject_SystemObject.Convert(ref t_object_member,t_jsonitem_member);
									}
									t_list.Add(t_object_member);
								}
							}
						}
					}
				}else if(a_jsonitem.IsAssociativeArray() == true){
					//struct,class,Dictionary
					bool t_search_member = true;

					if(t_type.IsGenericType == true){
						if(t_type.GetGenericTypeDefinition() == typeof(Dictionary<,>)){
							//Dictionary

							IDictionary t_list = a_to_object as IDictionary;
							System.Type t_type_member = t_type.GetGenericArguments()[1];

							List<string> t_key_list = a_jsonitem.CreateAssociativeKeyList();
							for(int ii=0;ii<t_key_list.Count;ii++){
								JsonItem t_jsonitem_member = a_jsonitem.GetItem(t_key_list[ii]);

								System.Object t_object_member = null;

								try{
									t_object_member = System.Activator.CreateInstance(t_type_member);
								}catch(System.Exception t_exception){
									//引数なしconstructorの呼び出しに失敗。
									Tool.LogError(t_exception);
								}

								if(t_object_member != null){
									if(t_type_member.IsClass == true){
										t_workpool.Add(new JsonToObject_Work(t_object_member,t_jsonitem_member));
									}else{
										JsonToObject_SystemObject.Convert(ref t_object_member,t_jsonitem_member);
									}
									t_list.Add(t_key_list[ii],t_object_member);
								}
							}

							//no support dictionary
							t_search_member = false;
						}
					}

					if(t_search_member == true){
						//struct,class

						System.Reflection.MemberInfo[] t_member = t_type.GetMembers(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);

						for(int ii=0;ii<t_member.Length;ii++){
							if(t_member[ii].MemberType == System.Reflection.MemberTypes.Field){
								System.Reflection.FieldInfo t_fieldinfo = t_member[ii] as System.Reflection.FieldInfo;
								if(t_fieldinfo != null){
									if(t_fieldinfo.IsDefined(typeof(NJsonItem.Ignore),false) == true){
										//無視する。
									}else{
										if((t_fieldinfo.Attributes == System.Reflection.FieldAttributes.Public)||(t_fieldinfo.Attributes == System.Reflection.FieldAttributes.Private)){
											if(a_jsonitem.IsExistItem(t_fieldinfo.Name) == true){
												System.Type t_type_member = t_fieldinfo.FieldType;

												JsonItem t_jsonitem_member = a_jsonitem.GetItem(t_fieldinfo.Name);

												System.Object t_object_member = null;

												try{
													t_object_member = System.Activator.CreateInstance(t_type_member);
												}catch(System.Exception t_exception){
													//引数なしconstructorの呼び出しに失敗。
													Tool.LogError(t_exception);
												}

												if(t_object_member != null){
													if(t_type_member.IsClass == true){
														t_workpool.Add(new JsonToObject_Work(t_object_member,t_jsonitem_member));
													}else{
														JsonToObject_SystemObject.Convert(ref t_object_member,t_jsonitem_member);
													}
													t_fieldinfo.SetValue(a_to_object,t_object_member);
												}
											}else{
												//ＪＳＯＮ側には存在しない。
											}
										}else{
											//オブジェクト化しない方型。
										}
									}
								}else{
									Tool.Assert(false);
								}
							}
						}
					}
				}
			}

			if(a_workpool == null){
				while(true){
					int t_count = t_workpool.Count;
					if(t_count > 0){
						JsonToObject_Work t_current_work = t_workpool[t_count - 1];
						t_workpool.RemoveAt(t_count - 1);
						t_current_work.Do(t_workpool);
					}else{
						break;
					}
				}
			}
		}
	}
}

