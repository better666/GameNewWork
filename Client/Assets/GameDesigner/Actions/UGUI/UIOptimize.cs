using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIOptimize : MonoBehaviour 
{
	#if UNITY_EDITOR
	[MenuItem( "XGAME/UI/系统栏/UIOptimize(UI性能优化类)" )]
	static void init()
	{
		Selection.activeGameObject.AddComponent<UIOptimize>();
	}
	#endif
	
	protected List<Object> GetComChis1 = new List<Object>();

	public void set_OffUIEffect( GameObject main )
	{
		GetComChis1 = new List<Object>();
		for( int i = 0 ; i < main.transform.childCount ; i ++ )
		{
			if ( main.transform.GetChild ( i ).GetComponent<ParticleSystem>() )
			{
				GetComChis1.Add ( main.transform.GetChild ( i ).GetComponent<ParticleSystem>() );
			}
			GetChilds ( main.transform.GetChild ( i ).transform , typeof(ParticleSystem) );
		}
		foreach( Object o in GetComChis1 )
		{
			ParticleSystem p = o as ParticleSystem;
			p.GetType().GetProperty("enableEmission").SetValue( p , false , null );
		}
	}

	public void set_OpenUIEffect( GameObject main )
	{
		GetComChis1 = new List<Object>();
		for( int i = 0 ; i < main.transform.childCount ; i ++ )
		{
			if ( main.transform.GetChild ( i ).GetComponent<ParticleSystem>() )
			{
				GetComChis1.Add ( main.transform.GetChild ( i ).GetComponent<ParticleSystem>() );
			}
			GetChilds ( main.transform.GetChild ( i ).transform , typeof(ParticleSystem) );
		}
		foreach( Object o in GetComChis1 )
		{
			ParticleSystem p = o as ParticleSystem;
			p.GetType().GetProperty("enableEmission").SetValue( p , true , null );
		}
	}
	
	void GetChilds ( Component target , System.Type t ) 
	{
		for ( int i = 0 ; i < target.transform.childCount ; i++ )
		{
			if ( target.transform.GetChild ( i ).GetComponent( t ) )
			{
				GetComChis1.Add ( target.transform.GetChild ( i ).GetComponent( t ) );
			}
			GetChilds ( target.transform.GetChild ( i ).transform , t );
		}
	}

	// 开始初始化！
	void Start () {
	
	}
	
	// 每一帧运行!
	void LateUpdate() {
	
	}
}
