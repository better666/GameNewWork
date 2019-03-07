using UnityEngine;
using System;
using System.Reflection;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace GameDesigner
{
	public enum SetValueModel
	{
		Add,Cut,Null
	}

	[Serializable]
	public class SystemType
	{
		/// <summary>
		/// 解释 : 精度获得typeName字符的类型
		/// </summary>
		static public Type GetType(string typeName)
		{
			typeName = typeName.Replace( "&" , "" ); // 反射参数的 out 标示
			typeName = typeName.Replace( "*" , "" ); // 反射参数的 int*(指针) 标示
			typeName = typeName.Replace( "[]" , "" ); // 反射参数的 object[](数组) 标示

            try { //引用外部dll存放文件夹
                foreach (var file in System.IO.Directory.GetFiles(Application.dataPath.Replace("Assets", "Managed/"), "*dll")) {//获取当前文件夹的dll文件
                    try {
                        Type type = Assembly.Load(System.IO.File.ReadAllBytes(file)).GetType(typeName);
                        if (type != null) {
                            return type;
                        }
                    } catch {
                        System.IO.File.Delete(file);
                    }
                }
                return Assembly.LoadFile(Application.dataPath.Replace("Assets", "Library/ScriptAssemblies/Assembly-CSharp.dll")).GetType(typeName);
            } catch { }
            return null;
        }

        /// <summary>
        /// 获取所有在unity所引用的dll文件的类型，并通过types委托调用，types不是一次获取完所有dll文件的类型，而是一个dll文件获取完（所有类型）就执行一次委托
        /// </summary>
        /// <param name="types">单个dll文件的所有类型委托到types的参数中</param>
        public static void GetTypes(Action<Type[]> types)
        {
            try { //引用外部dll存放文件夹
                foreach (var file in System.IO.Directory.GetFiles(Application.dataPath.Replace("Assets", "Managed/"), "*dll")) {//获取当前文件夹的dll文件
                    try {
                        types(Assembly.Load(System.IO.File.ReadAllBytes(file)).GetTypes());
                    } catch {
                        System.IO.File.Delete(file);
                    }
                }
                types(Assembly.LoadFile(Application.dataPath.Replace("Assets", "Library/ScriptAssemblies/Assembly-CSharp.dll")).GetTypes());
            } catch { }
        }

        public static string dataPath;

        /// <summary>
        /// 多线程使用的获得类型，获取所有在unity所引用的dll文件的类型，并通过types委托调用，types不是一次获取完所有dll文件的类型，而是一个dll文件获取完（所有类型）就执行一次委托
        /// </summary>
        /// <param name="types">单个dll文件的所有类型委托到types的参数中</param>
        public static void GetTypes(string dataPath, Action<Type[]> types)
        {
            SystemType.dataPath = dataPath;
            try { //引用外部dll存放文件夹
                string[] paths = System.IO.Directory.GetFiles(dataPath.Replace("Assets", "Managed"), "*dll");
                foreach (var file in paths) {//获取当前文件夹的dll文件
                    try {
                        types(Assembly.Load(System.IO.File.ReadAllBytes(file)).GetTypes());
                    } catch {
                        System.IO.File.Delete(file);
                    }
                }
                types(Assembly.LoadFile(dataPath.Replace("Assets", "Library/ScriptAssemblies/Assembly-CSharp.dll")).GetTypes());
            } catch { }
        }

        /// <summary>
        /// 解释 : 判断type的基类是否是Typeof类型,是返回真,不是返回假
        /// </summary>
        static public bool IsSubclassOf( Type type , Type Typeof )
		{
			if( type == null | Typeof == null )
				return false;
			if( type.IsSubclassOf(Typeof) | type == Typeof )
				return true;
			return false;
		}

		/// <summary>
		/// 解释 : 判断type所继承的类(基类)是否是Typeof类 和 Typeof所继承的类型(基类)是否是type类 , 是返回真,不是返回假
		/// </summary>

		static public bool IsSubclassOfs( Type type , Type Typeof )
		{
			if( type == null | Typeof == null )
				return false;
			if( type.IsSubclassOf(Typeof) | type == Typeof )
				return true;
			if( Typeof.IsSubclassOf(type) | type == Typeof )
				return true;
			return false;
		}

		/// <summary>
		/// 解释 : 判断type的基类是否是Typeof类型,是返回真,不是返回假
		/// </summary>

		static public bool IsSubclassOf<T>( Type type ) where T : class
		{
			if( type == null )
				return false;
			if( type.IsSubclassOf( typeof(T) ) | type == typeof(T) )
				return true;
			return false;
		}

		/// <summary>
		/// 在监视面板显示类的值并且可视化修改 ( type 给定类型名称 , value 转换这个字符串为type类型的值 )
		/// </summary>

		static public object StringToValue ( string type = "System.Int32" , string value = "0" )
		{
			switch( type )
			{
			case "System.Int32":
				return Convert.ToInt32( value );

			case "System.Single":
				return Convert.ToSingle( value );

			case "System.String":
				return Convert.ToString( value );

			case "System.Boolean":
				return Convert.ToBoolean( value );

			case "System.Char":
				return Convert.ToChar( value );

			case "System.Int16":
				return Convert.ToInt16( value );

			case "System.Int64":
				return Convert.ToInt64( value );

			case "System.UInt16":
				return Convert.ToUInt16( value );

			case "System.UInt32":
				return Convert.ToUInt32( value );

			case "System.UInt64":
				return Convert.ToUInt64( value );

			case "System.Double":
				return Convert.ToDouble( value );

			case "System.Byte":
				return Convert.ToByte( value );

			case "System.SByte":
				return Convert.ToSByte( value );

			case "UnityEngine.Vector2":
				return ConvertUtility.ToVector2_3_4( type , value );

			case "UnityEngine.Vector3":
				return ConvertUtility.ToVector2_3_4( type , value );

			case "UnityEngine.Vector4":
				return ConvertUtility.ToVector2_3_4( type , value );

			case "UnityEngine.Rect":
				return ConvertUtility.ToRect( type , value );

			case "UnityEngine.Color":
				return ConvertUtility.ToColor( type , value );

			case "UnityEngine.Quaternion":
				return ConvertUtility.ToQuaternion( type , value );

			case "System.Void":
				return null;;
			}

			//Object判断
			return GetType( type );
		}

		/// <summary>
		/// 设置类的变量值,解决派生类的值控制父类的变量值 ( 被赋值变量对象 , 赋值变量对象 ) [尽可能的使用此方法,此方法产生GC]
		/// </summary>

		static public void SetFieldValue( object target , object setValue ) 
		{
			foreach( FieldInfo field in target.GetType().GetFields() ){
				if (field.IsStatic|field.IsPrivate|field.FieldType.IsAbstract)
					continue;
				if ( SystemType.IsSubclassOf( field.FieldType , typeof(Object) ) | field.FieldType == typeof(string) | field.FieldType.IsValueType | field.FieldType.IsEnum | field.FieldType.IsArray ) {
					field.SetValue ( target , field.GetValue( setValue ) );
				} else if ( !field.IsNotSerialized ) {
					try{For ( field.GetValue( target ) , field.GetValue( setValue ) );}catch{}
				}
			}
		}

		static void For( object target , object setValue )
		{
			if( target == null )
				return;

			foreach( FieldInfo field in target.GetType().GetFields(BindingFlags.Public|BindingFlags.Instance) ){
				if (field.IsStatic|field.IsPrivate|field.FieldType.IsAbstract)
					continue;
				if ( SystemType.IsSubclassOf( field.FieldType , typeof(Object) ) | field.FieldType == typeof(string) | field.FieldType.IsValueType | field.FieldType.IsEnum | field.FieldType.IsArray ) {
					field.SetValue ( target , field.GetValue( setValue ) );
				} else if ( !field.IsNotSerialized ) {
					try{For ( field.GetValue( target ) , field.GetValue( setValue ) );}catch{}
				}
			}
		}

		/// <summary>
		/// 设置类的变量值,解决派生类的值控制父类的变量值 ( 被赋值变量对象 , 赋值变量对象 ) [尽可能的使用此方法,此方法产生GC]
		/// </summary>

		static public void SetPropertyValue( object target , object setValue ) 
		{
			foreach( PropertyInfo property in target.GetType().GetProperties() ){
				if (!property.CanWrite)
					continue;
				if ( IsSubclassOf( property.PropertyType , typeof(Object) ) | property.PropertyType == typeof(string) | property.PropertyType == typeof(object) | property.PropertyType.IsValueType | property.PropertyType.IsEnum ) {
					property.SetValue( target , property.GetValue( setValue , null ) , null );
				} else {
					PropertyFor ( property.GetValue( target , null ) , property.GetValue( setValue , null ) );
				}
			}
		}

		static void PropertyFor( object target , object setValue )
		{
			if( target == null )
				return;

			foreach( PropertyInfo property in target.GetType().GetProperties() ){
				if (!property.CanWrite)
					continue;
				if ( IsSubclassOf( property.PropertyType , typeof(Object) ) | property.PropertyType == typeof(string) | property.PropertyType == typeof(object) | property.PropertyType.IsValueType | property.PropertyType.IsEnum ) {
					property.SetValue( target , property.GetValue( setValue , null ) , null );
				} else {
					PropertyFor ( property.GetValue( target , null ) , property.GetValue( setValue , null ) );
				}
			}
		}

		/// <summary>
		/// 设置类的变量值,解决派生类的值控制父类的变量值 ( 被赋值变量对象 , 赋值变量对象 , 不赋值的变量名数组 ) [尽可能的使用此方法,此方法产生GC]
		/// </summary>

		static public void SetFieldValue( object target , object setValue , string[] notSetValueNames , SetValueModel model = SetValueModel.Null ) 
		{
			foreach( FieldInfo field in target.GetType().GetFields() )
			{
				if (field.IsStatic)
					continue;

				bool isSetValue = true;
				foreach( string name in notSetValueNames )
				{
					if( field.Name == name )
					{
						isSetValue = false;
						break;
					}
				}
				if( isSetValue )
				{
					switch( model )
					{
					case SetValueModel.Add:
						{
							if ( SystemType.IsSubclassOf( field.FieldType , typeof(Object) ) | field.FieldType == typeof(string) | field.FieldType.IsValueType | field.FieldType.IsEnum ) 
							{
								switch( field.FieldType.FullName )
								{
								case "System.Int32":
									{
										field.SetValue ( target , (int)field.GetValue (target) + (int)field.GetValue (setValue) );
										break;
									}
								case "System.Single":
									{
										field.SetValue ( target , (float)field.GetValue (target) + (float)field.GetValue (setValue) );
										break;
									}
								default :
									{
										field.SetValue ( target , field.GetValue (setValue) );
										break;
									}
								}
							} 
							else if ( !field.IsNotSerialized ) 
							{
								For ( field.GetValue( target ) , field.GetValue( setValue ) , true );
							}
							break;
						}
					case SetValueModel.Cut:
						{
							if ( SystemType.IsSubclassOf( field.FieldType , typeof(Object) ) | field.FieldType == typeof(string) | field.FieldType.IsValueType | field.FieldType.IsEnum ) 
							{
								switch( field.FieldType.FullName )
								{
								case "System.Int32":
									{
										field.SetValue ( target , (int)field.GetValue (target) - (int)field.GetValue (setValue) );
										break;
									}
								case "System.Single":
									{
										field.SetValue ( target , (float)field.GetValue (target) - (float)field.GetValue (setValue) );
										break;
									}
								default :
									{
										field.SetValue ( target , field.GetValue (setValue) );
										break;
									}
								}
							} 
							else if ( !field.IsNotSerialized ) 
							{
								For ( field.GetValue( target ) , field.GetValue( setValue ) , false );
							}
							break;
						}
					case SetValueModel.Null:
						{
							if ( SystemType.IsSubclassOf( field.FieldType , typeof(Object) ) | field.FieldType == typeof(string) | field.FieldType.IsValueType | field.FieldType.IsEnum ) 
							{
								field.SetValue ( target , field.GetValue( setValue ) );
							} 
							else if ( !field.IsNotSerialized ) 
							{
								For ( field.GetValue( target ) , field.GetValue( setValue ) );
							}
							break;
						}
					}
				}
			}
		}

		/// <summary>
		/// 设置类的变量值,解决派生类的值控制父类的变量值 ( 被赋值变量对象 , 赋值变量对象 ) [尽可能的使用此方法,此方法产生GC]
		/// </summary>

		static public void SetFieldValue( object target , object setValue , bool AddOrCut ) 
		{
			foreach( FieldInfo field in target.GetType().GetFields() ){
				if (field.IsStatic)
					continue;
				if ( SystemType.IsSubclassOf( field.FieldType , typeof(Object) ) | field.FieldType == typeof(string) | field.FieldType.IsValueType | field.FieldType.IsEnum ) {
					switch( field.FieldType.FullName )
					{
					case "System.Int32":
						{
							if( AddOrCut )
							{
								field.SetValue ( target , (int)field.GetValue (target) + (int)field.GetValue (setValue) );
							}
							else
							{
								field.SetValue ( target , (int)field.GetValue (target) - (int)field.GetValue (setValue) );
							}
							break;
						}
					case "System.Single":
						{
							if( AddOrCut )
							{
								field.SetValue ( target , (float)field.GetValue (target) + (float)field.GetValue (setValue) );
							}
							else
							{
								field.SetValue ( target , (float)field.GetValue (target) - (float)field.GetValue (setValue) );
							}
							break;
						}
					default :
						{
							field.SetValue ( target , field.GetValue (setValue) );
							break;
						}
					}
				} 
				else if ( !field.IsNotSerialized ) 
				{
					For ( field.GetValue( target ) , field.GetValue( setValue ) , AddOrCut );
				}
			}
		}

		static void For( object target , object setValue , bool AddOrCut )
		{
			if( target == null )
				return;

			foreach( FieldInfo field in target.GetType().GetFields() ){
				if (field.IsStatic)
					continue;
				if ( SystemType.IsSubclassOf( field.FieldType , typeof(Object) ) | field.FieldType == typeof(string) | field.FieldType.IsValueType | field.FieldType.IsEnum ) {
					switch( field.FieldType.FullName )
					{
					case "System.Int32":
						{
							if( AddOrCut )
							{
								field.SetValue ( target , (int)field.GetValue (target) + (int)field.GetValue (setValue) );
							}
							else
							{
								field.SetValue ( target , (int)field.GetValue (target) - (int)field.GetValue (setValue) );
							}
							break;
						}
					case "System.Single":
						{
							if( AddOrCut )
							{
								field.SetValue ( target , (float)field.GetValue (target) + (float)field.GetValue (setValue) );
							}
							else
							{
								field.SetValue ( target , (float)field.GetValue (target) - (float)field.GetValue (setValue) );
							}
							break;
						}
					default :
						{
							field.SetValue ( target , field.GetValue (setValue) );
							break;
						}
					}
				} 
				else if ( !field.IsNotSerialized ) 
				{
					For ( field.GetValue( target ) , field.GetValue( setValue ) , AddOrCut );
				}
			}
		}
	}
}