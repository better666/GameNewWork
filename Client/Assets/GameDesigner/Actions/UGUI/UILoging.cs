using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILoging : MonoBehaviour {

	public InputField zhanghao;//账号
	public InputField mima;//密码
	public Transform mainTarent;//主对象，点击登录 我设置关闭这个对象的物体

	public GameObject[] sceneObj;//游戏数组，场景物体
	public GameObject[] OpenScene;//打开场景
	public GameObject[] OpenSceneDesObj;//打开场景后要删除的游戏物体
    public GameObject[] OpenSceneInsObj;//打开场景后要实例化的游戏物体
	public MonoBehaviour[] OpenSceneEnabled;//打开场景设置一些脚本的开启（打开）
	public GameObject[] OpenSceneClosePlay;//打开场景关闭玩家（2个玩家选择，必须其中一个玩家）

	public GameObject	Player = null;
	public bool			PlayerBUG = false; //该角色还未完整或正在努力的制作中，敬请期待！

	/// <summary>
	/// 该角色还未设置技能或K动画事件，所以不能使用该角色
	/// </summary>

	public void PlayerBUGs ( bool b )
	{
		PlayerBUG = b;
	}

	public void OnLoading ()//点击登录函数
	{
		if(zhanghao.text == "123" & mima.text == "456" )//如果账号=123，并且密码=456，登录成功，否则失败
		{
			foreach ( GameObject i in sceneObj )//遍历游戏物体，
			{
				i.SetActive (true );//设置打开或者关闭的函数
			}

			Destroy (mainTarent.gameObject );//删除函数
		}
	}

	public void OpenScenes()//打卡场景
	{
		foreach ( GameObject i in OpenScene )
		{
			i.SetActive (true );
		}
        foreach ( GameObject i in OpenSceneInsObj )
        {
            Instantiate ( i );
        }
		foreach ( MonoBehaviour i in OpenSceneEnabled )
		{
			i.enabled = true;
		}
		foreach ( GameObject i in OpenSceneClosePlay )
		{
			i.SetActive (false );
		}
		foreach ( GameObject i in OpenSceneDesObj )
		{
			Destroy (i);
		}

		try{
			FindComponent.FindObjectsOfTypeAll<MainUI>(null,true).enabled = true;
			FindComponent.FindObjectsOfTypeAll<SaveLoading>(null,true).enabled = true;
		}catch{}
	}

	public Vector3 m_PlayPos = Vector3.zero;
	public Quaternion m_PlayQus = Quaternion.identity;

	public void SelectPlayer ( GameObject player )
	{
		GameDesigner.Player go = GameObject.FindObjectOfType <GameDesigner.Player>();
		if( go == true )
		{
			Destroy ( go.gameObject );
		}
		if( player == null )
		{
			Debug.Log ( "此英雄在制作中，敬请期待！" );
			return;
		}
		Player = Instantiate( player , m_PlayPos , m_PlayQus ) as GameObject;
	}

    public void StartGame ( )
    {
		if( PlayerBUG == true )
		{
			Debug.Log ( "此英雄正在努力的制作中，敬请期待！" );
			return ;
		}

		if( Player == null )
		{
			Debug.Log ( "请选择一个英雄后在进入游戏！" );
			return ;
		}

        OpenScenes ();
    }

	public void StartGame ( InputField inputField )
	{
		if( PlayerBUG == true )
		{
			Debug.Log ( "此英雄正在努力的制作中，敬请期待！" );
			return ;
		}

		if( Player == null )
		{
			Debug.Log ( "请选择一个英雄后在进入游戏！" );
			return ;
		}

		if( inputField.text == "" )
		{
			Debug.Log ( "给玩家起个名字吧！" );
			return;
		}

		OpenScenes ();
	}

	public void SetPlayerName ( string name )
	{
		if (!Player) {
			Debug.Log ("还未加载玩家对象！");
			return;
		}
	}

	public void SetPlayerName ( InputField inputField )
	{
		if (!Player) {
			Debug.Log ("还未加载玩家对象！");
			return;
		}
	}

}
