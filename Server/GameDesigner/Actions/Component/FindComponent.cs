using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindComponent
{
	/// <summary>
	/// 查找当前运行场景的游戏物体组建T 查找类型( findName 查找名称-查找名称可以忽略,忽略后获得的是第一个先查到的对象组建 )
	/// </summary>
	public static Object FindObjectsOfTypeAll( System.Type type , string findName = null )
	{
		Object[] objs = Resources.FindObjectsOfTypeAll(type);

		if( findName == null & objs.Length != 0 ){
			return objs [0];
		}

		foreach( Object o in objs ){
			if( o.name == findName ){
				return o;
			}
		}
		return null;
	}

	/// <summary>
	/// 查找当前运行场景的游戏物体组建T 查找类型( findName 查找名称-查找名称可以忽略,忽略后获得的是第一个先查到的对象组建 )
	/// </summary>
	static public T FindObjectsOfTypeAll<T>( string findName ) where T : class
	{
		Object[] objs = Resources.FindObjectsOfTypeAll(typeof(T));

		if( findName == null & objs.Length != 0 ){
			return objs [0] as T;
		}

		foreach( Object o in objs ){
			if( o.name == findName ){
				return o as T;
			}
		}
		return null;
	}

	/// <summary>
	/// 查找当前运行场景的游戏物体组建T 查找类型( findName 查找名称-查找名称可以忽略,忽略后获得的是第一个先查到的对象组建 , isFindInRuntimeScene 只查找在当前运行的场景物体上的组件 , 预制物体被忽略 )
	/// </summary>
	static public T FindObjectsOfTypeAll<T>( string findName = "" , bool isFindInRuntimeScene = true ) where T : class
	{
		Component[] objs = (Component[])Resources.FindObjectsOfTypeAll(typeof(T));

		foreach( var o in objs ){
			if( findName == "" & !isFindInRuntimeScene ){
				return o as T;
			}else if( o.gameObject.name == findName & !isFindInRuntimeScene ){
				return o as T;
			}else if( o.gameObject.scene.name == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name ){
				return o as T;
			}
		}
		return null;
	}

	/// <summary>
	/// 查找当前运行场景的游戏物体组建T 查找类型( findName 查找名称-查找名称可以忽略,忽略后获得的是第一个先查到的对象组建 , isFindInRuntimeScene 只查找在当前运行的场景物体上的组件 , 预制物体被忽略 )
	/// </summary>
	static public T[] FindObjectsOfTypeAll<T>( bool isFindInRuntimeScene ) where T : class
	{
		Component[] objs = (Component[])Resources.FindObjectsOfTypeAll(typeof(T));
		List<T> cpms = new List<T> ();
		foreach( var o in objs ){
			if( o.gameObject.scene.name == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name ){
				cpms.Add(o as T);
			}
		}
		return cpms.ToArray();
	}
}
