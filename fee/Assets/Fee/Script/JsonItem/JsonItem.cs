﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。
*/


/** NJsonItem
*/
namespace NJsonItem
{
	/** JsonItem
	*/
	public class JsonItem : Config
	{
		/** ＪＳＯＮ文字列。
		*/
		private string jsonstring;

		/** バリアント型。タイプ。
		*/
		private ValueType valuetype;

		/** バリアント型。
		*/
		private Value value;

		/** constructor
		*/
		public JsonItem()
		{
			this.valuetype = ValueType.None;
			this.value.Reset();
		}

		/** constructor
		*/
		public JsonItem(string a_jsonstring)
		{
			this.SetJsonString(a_jsonstring);
		}

		/** constructor
		*/
		public JsonItem(Value_StringData a_value)
		{
			this.SetStringData(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_AssociativeArray a_value)
		{
			this.SetAssociativeArray();
		}

		/** constructor
		*/
		public JsonItem(Value_IndexArray a_value)
		{
			this.SetIndexArray();
		}

		/** constructor
		*/
		public JsonItem(Value_Integer a_value)
		{
			this.SetInteger(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Float a_value)
		{
			this.SetFloat(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_BoolData a_value)
		{
			this.SetBoolData(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_BinaryData a_value)
		{
			this.SetBinaryData(a_value.value);
		}

		/** ディープコピー。
		*/
		public JsonItem DeepCopy()
		{
			JsonItem t_new_jsonitem = new JsonItem(this.ConvertJsonString());
			return t_new_jsonitem;
		}

		/** ＪＳＯＮ文字列をセット。
		*/
		public void SetJsonString(string a_jsonstring)
		{
			if(a_jsonstring.Length > 0){
				ValueType t_valuetype = Impl.GetValueTypeFromChar(a_jsonstring[0]);
				switch(t_valuetype){
				case ValueType.StringData:
				case ValueType.AssociativeArray:
				case ValueType.IndexArray:
				case ValueType.BinaryData:
				case ValueType.IntegerNumber:
				case ValueType.FloatNumber:
					{
						this.jsonstring = a_jsonstring;
						this.valuetype = t_valuetype;
						this.value.Reset();
						return;
					}//break;
				case ValueType.Calc_UnknownNumber:
					{
						if(Impl.IsInteger(a_jsonstring)){
							this.jsonstring = a_jsonstring;
							this.valuetype = ValueType.IntegerNumber;
							this.value.Reset();
							return;
						}else{
							this.jsonstring = a_jsonstring;
							this.valuetype = ValueType.FloatNumber;
							this.value.Reset();
							return;
						}
					}//break;
				case ValueType.Calc_BoolDataTrue:
					{
						//値で設定。

						this.jsonstring = null;
						this.valuetype = ValueType.BoolData;
						this.value.bool_data = true;
						return;
					}//break;
				case ValueType.Calc_BoolDataFalse:
					{
						//値で設定。

						this.jsonstring = null;
						this.valuetype = ValueType.BoolData;
						this.value.bool_data = false;
						return;
					}//break;
				case ValueType.None:
				case ValueType.BoolData:
				default:
					{
						Tool.Assert(false);

						this.jsonstring = null;
						this.valuetype = ValueType.None;
						this.value.Reset();
						return;
					}//break;
				}
			}

			Tool.Assert(false);

			this.jsonstring = null;
			this.valuetype = ValueType.None;
			this.value.Reset();
		}

		/** 値化。
		*/
		private void JsonStringToValue()
		{
			string t_jsonstring_temp = this.jsonstring;
			this.jsonstring = null;

			this.value.Reset();

			if(t_jsonstring_temp != null){
				switch(this.valuetype){
				case ValueType.StringData:
					{
						int t_index = 1;
						int t_max = t_jsonstring_temp.Length - 1;

						this.value.string_data = "";
						//this.value.string_data->reserve(16 + t_max);
				
						while(t_index < t_max){
							int t_add = Impl.GetMojiSize(t_jsonstring_temp,t_index,true);
							if(t_add == 2){
								if(t_jsonstring_temp[t_index] == '\\'){
									//エスケープシーケンス文字をシーケンス文字に変換。
									string t_add_string = Impl.ToSequenceString(t_jsonstring_temp,t_index);
									if(t_add_string != null){
										//変換後文字列を追加。
										this.value.string_data += t_add_string;
									}else{
										//不明。
										Tool.Assert(false);

										this.value.string_data = "";
										return;
									}
								}else{
									//通常文字（２文字）を追加。
									this.value.string_data += t_jsonstring_temp.Substring(t_index,t_add);
								}
							}else if(t_add > 0){
								//通常文字（複数文字）を追加。
								this.value.string_data += t_jsonstring_temp.Substring(t_index,t_add);
							}else{
								//不明なエンコード。
								Tool.Assert(false);

								this.value.string_data = "";
								return;
							}

							t_index += t_add;
						}

						if(t_index != t_max){
							//不明なエンコード。
							Tool.Assert(false);

							this.value.string_data = "";
							return;
						}

						return;
					}//break;
				case ValueType.IntegerNumber:
					{
						this.value.integer_number = long.Parse(t_jsonstring_temp);
						return;
					}//break;
				case ValueType.FloatNumber:
					{
						this.value.float_number = float.Parse(t_jsonstring_temp);
						return;
					}//break;
				case ValueType.IndexArray:
					{
						this.value.index_array = Impl.CreateIndexArrayFromJsonString(t_jsonstring_temp);
						return;
					}//break;
				case ValueType.AssociativeArray:
					{
						this.value.associative_array = Impl.CreateAssociativeArrayFromJsonString(t_jsonstring_temp);
						return;
					}//break;
				case ValueType.BoolData:
					{
						char t_char = t_jsonstring_temp[0];

						if((t_char == 't')||(t_char == 'T')){
							this.value.bool_data = true;
						}else{
							this.value.bool_data = false;
						}
						return;
					}//break;
				case ValueType.BinaryData:
					{
						this.value.binary_data = Impl.CreateBinaryDataFromJsonString(t_jsonstring_temp);
					}break;
				case ValueType.None:
				case ValueType.Calc_BoolDataFalse:
				case ValueType.Calc_BoolDataTrue:
				case ValueType.Calc_UnknownNumber:
				default:
					{
						//不明。
						Tool.Assert(false);

						this.value.Reset();
						return;
					}//break;
				}
			}

			//不明。
			Tool.Assert(false);

			this.value.Reset();
			return;
		}

		/** [取得]GetListMax
		*/
		public int GetListMax()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			switch(this.valuetype){
			case ValueType.AssociativeArray:
				{
					return this.value.associative_array.Count;
				}//break;
			case ValueType.IndexArray:
				{
					return this.value.index_array.Count;
				}//break;
			case ValueType.StringData:
				{
					return this.value.string_data.Length;
				}//break;
			case ValueType.None:
			case ValueType.IntegerNumber:
			case ValueType.FloatNumber:
			case ValueType.BoolData:
			case ValueType.BinaryData:
			case ValueType.Calc_BoolDataFalse:
			case ValueType.Calc_BoolDataTrue:
			case ValueType.Calc_UnknownNumber:
			default:
				{
				}break;
			}

			Tool.Assert(false);

			return 0;
		}

		/** [取得][値]GetStringData
		*/
		public string GetStringData()
		{
			Tool.Assert(this.valuetype == ValueType.StringData);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return this.value.string_data;
		}

		/** [取得][値]GetInteger
		*/
		public int GetInteger()
		{
			Tool.Assert(this.valuetype == ValueType.IntegerNumber);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return (int)this.value.integer_number;
		}

		/** [取得][値]GetLong
		*/
		public long GetLong()
		{
			Tool.Assert(this.valuetype == ValueType.IntegerNumber);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return (long)this.value.integer_number;
		}

		/** [取得][値]GetFloat
		*/
		public float GetFloat()
		{
			Tool.Assert(this.valuetype == ValueType.FloatNumber);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return (float)this.value.float_number;
		}

		/** [取得][値]GetDouble
		*/
		public double GetDouble()
		{
			Tool.Assert(this.valuetype == ValueType.FloatNumber);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return (double)this.value.float_number;
		}

		/** [取得][値]GetBoolData
		*/
		public bool GetBoolData()
		{
			Tool.Assert(this.valuetype == ValueType.BoolData);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return this.value.bool_data;
		}

		/** [取得][値]GetBinaryData
		*/
		public List<byte> GetBinaryData()
		{
			Tool.Assert(this.valuetype == ValueType.BinaryData);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return this.value.binary_data;
		}

		/** [取得]GetValueType
		*/
		public ValueType GetValueType()
		{
			return this.valuetype;
		}

		/** タイプチェック。文字データ。
		*/
		public bool IsStringData()
		{
			if(this.valuetype == ValueType.StringData){
				return true;
			}
			return false;
		}

		/** タイプチェック。連想配列。
		*/
		public bool IsAssociativeArray()
		{
			if(this.valuetype == ValueType.AssociativeArray){
				return true;
			}
			return false;
		}

		/** タイプチェック。インデックス配列。
		*/
		public bool IsIndexArray()
		{
			if(this.valuetype == ValueType.IndexArray){
				return true;
			}
			return false;
		}

		/** タイプチェック。整数。
		*/
		public bool IsIntegerNumber()
		{
			if(this.valuetype == ValueType.IntegerNumber){
				return true;
			}
			return false;
		}

		/** タイプチェック。少数。
		*/
		public bool IsFloatNumber()
		{
			if(this.valuetype == ValueType.FloatNumber){
				return true;
			}
			return false;
		}

		/** タイプチェック。真偽データ。
		*/
		public bool IsBoolData()
		{
			if(this.valuetype == ValueType.BoolData){
				return true;
			}
			return false;
		}

		/** タイプチェック。バイナリデータ。
		*/
		public bool IsBinaryData()
		{
			if(this.valuetype == ValueType.BinaryData){
				return true;
			}
			return false;
		}

		/** [取得]連想リストのアイテム取得。
		*/
		public JsonItem GetItem(string a_itemname)
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			{
				JsonItem t_value;
				if(this.value.associative_array.TryGetValue(a_itemname,out t_value) == true){
					return t_value;
				}
			}

			Tool.Assert(false);

			return null;
		}

		/** [取得]連想リストのアイテムチェック。
		*/
		public bool IsExistItem(string a_itemname,ValueType a_valuetype = ValueType.None)
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			{
				JsonItem t_value;
				if(this.value.associative_array.TryGetValue(a_itemname,out t_value) == true){
					if(a_valuetype == ValueType.None || t_value.GetValueType() == a_valuetype){
						return true;
					}
				}
			}
		
			return false;
		}

		/** [取得]インデックスリストのアイテム取得。
		*/
		public JsonItem GetItem(int a_index)
		{
			Tool.Assert(a_index >= 0);
			int t_index = a_index;

			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			if(t_index < this.value.index_array.Count){
				return this.value.index_array[t_index];
			}
		
			Tool.Assert(false);

			return null;
		}

		/** [取得]インデックスリストのアイテムチェック。
		*/
		public bool IsExistItem(int a_index,ValueType a_valuetype = ValueType.None)
		{
			Tool.Assert(a_index >= 0);
			int t_index = a_index;

			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			if(t_index < this.value.index_array.Count){
				if(a_valuetype == ValueType.None || this.value.index_array[t_index].GetValueType() == a_valuetype){
					return true;
				}

				return true;
			}
		
			return false;
		}

		/** [設定]連想リストにアイテム追加。削除。
		*/
		public void SetItem(string a_itemname,JsonItem a_item,bool a_deepcopy)
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			if(a_item != null){
				if(a_deepcopy == true){
					this.value.associative_array.Add(a_itemname,a_item.DeepCopy());
				}else{
					this.value.associative_array.Add(a_itemname,a_item);
				}
			}else{
				this.value.associative_array.Remove(a_itemname);
			}
		}

		/** [設定]インデックスリストにアイテム追加。
		*/
		public void AddItem(JsonItem a_item,bool a_deepcopy)
		{
			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			if(a_deepcopy == true){
				this.value.index_array.Add(a_item.DeepCopy());
			}else{
				this.value.index_array.Add(a_item);
			}
		}

		/** [削除]インデックスリストからアイテム削除。
		*/
		public void RemoveItem(int a_index)
		{
			Tool.Assert(a_index >= 0);

			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			this.value.index_array.RemoveAt(a_index);
		}

		/** [設定]文字データ。
		*/
		public void SetStringData(string a_string)
		{
			this.jsonstring = null;
			this.value.Reset();
		
			this.valuetype = ValueType.StringData;
			this.value.string_data = a_string;
		}

		/** [設定]空連想リスト。
		*/
		public void SetAssociativeArray()
		{
			this.jsonstring = null;
			this.value.Reset();
		
			this.valuetype = ValueType.AssociativeArray;
			this.value.associative_array = new Dictionary<string, JsonItem>();
		}

		/** [設定]空インデックスリスト。
		*/
		public void SetIndexArray()
		{
			this.jsonstring = null;
			this.value.Reset();
		
			this.valuetype = ValueType.IndexArray;
			this.value.index_array = new List<JsonItem>();
		}

		/** [設定]整数セット。
		*/
		public void SetInteger(int a_integer)
		{
			this.jsonstring = null;
			this.value.Reset();
		
			this.valuetype = ValueType.IntegerNumber;
			this.value.integer_number = a_integer;
		}

		/** [設定]整数セット。
		*/
		public void SetLong(long a_integer)
		{
			this.jsonstring = null;
			this.value.Reset();
		
			this.valuetype = ValueType.IntegerNumber;
			this.value.integer_number = a_integer;
		}

		/** [設定]少数セット。
		*/
		public void SetFloat(float a_float)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.FloatNumber;
			this.value.float_number = a_float;
		}

		/** [設定]少数セット。
		*/
		public void SetDouble(double a_float)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.FloatNumber;
			this.value.float_number = a_float;
		}

		/** [設定]真偽データセット。
		*/
		public void SetBoolData(bool a_bool)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.BoolData;
			this.value.bool_data = a_bool;
		}	

		/** SetBinaryData
		*/
		public void SetBinaryData(List<byte> a_binarydata)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.BinaryData;
			this.value.binary_data = a_binarydata;
		}

		/** 連想配列キーリスト作成。
		*/
		public List<string> CreateAssociativeKeyList()
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			List<string> t_ret_keylist = new List<string>();

			foreach(KeyValuePair<string,JsonItem> t_pair in this.value.associative_array){
				t_ret_keylist.Add(t_pair.Key);
			}

			return t_ret_keylist;
		}

		/** JsonStringへコンバート。
		*/
		public string ConvertJsonString()
		{
			if(this.jsonstring != null){
				return this.jsonstring;
			}

			switch(this.valuetype){
			case ValueType.StringData:
				{
					string t_jsonstring = "\"";
					//t_jsonstring.reserve(64);
					{
						int t_index = 0;
						int t_max = this.value.string_data.Length;

						while(t_index < t_max){
							//１文字取得。
							int t_add = Impl.GetMojiSize(this.value.string_data,t_index,false);
							if(t_add == 1){
								string t_add_string = Impl.CheckEscapeSequence(this.value.string_data[t_index]);
								if(t_add_string == null){
									//通常文字。
								}else{
									t_jsonstring += t_add_string;
									t_index++;
									continue;
								}
							}else if(t_add <= 0){
								//不明。
								Tool.Assert(false);

								return "";
							}

							t_jsonstring += this.value.string_data.Substring(t_index,t_add);
							t_index += t_add;
						}
					}
					t_jsonstring += "\"";

					return t_jsonstring;
				}//break;
			case ValueType.IntegerNumber:
				{
					return this.value.integer_number.ToString();
				}//break;
			case ValueType.FloatNumber:
				{
					return this.value.float_number.ToString();
				}//break;
			case ValueType.IndexArray:
				{
					string t_jsonstring = "[";
					//t_jsonstring.reserve(64);
					{
						int t_count = this.value.index_array.Count;
						int t_index = 0;

						//一つ目。
						if(t_count > 0){
							t_jsonstring += this.value.index_array[0].ConvertJsonString();
							t_index++;

							//二つ目以降。
							for(;t_index<t_count;t_index++){
								t_jsonstring += ",";
								t_jsonstring += this.value.index_array[t_index].ConvertJsonString();
							}
						}
					}
					t_jsonstring += "]";
					return t_jsonstring;
				}//break;
			case ValueType.AssociativeArray:
				{
					string t_jsonstring = "{";
					//t_jsonstring.reserve(64);
					{
						bool t_first = true;

						foreach(KeyValuePair<string,JsonItem> t_pair in this.value.associative_array){
							if(t_first == true){
								t_first = false;

								//一つ目。
								t_jsonstring += "\"";
								t_jsonstring += t_pair.Key;
								t_jsonstring += "\":";
								t_jsonstring += t_pair.Value.ConvertJsonString();
							}else{
								//二つ目以降。
								t_jsonstring += ",\"";
								t_jsonstring += t_pair.Key;
								t_jsonstring += "\":";
								t_jsonstring += t_pair.Value.ConvertJsonString();
							}
						}
					}
					t_jsonstring += "}";
					return t_jsonstring;
				}//break;
			case ValueType.BoolData:
				{
					if(this.value.bool_data){
						return "true";
					}
					return "false";
				}//break;
			case ValueType.BinaryData:
				{
					string t_jsonstring = "<";
					//t_jsonstring.reserve(64);
					{
						//TODO:
					}
					t_jsonstring += ">";
					return t_jsonstring;
				}//break;
			case ValueType.None:
			case ValueType.Calc_UnknownNumber:
			case ValueType.Calc_BoolDataTrue:
			case ValueType.Calc_BoolDataFalse:
			default:
				{
					//不明。
					Tool.Assert(false);

					return "";
				}//break;
			}

			//不明。
			//Tool.Assert(false);

			//return "";
		}
	}
}
