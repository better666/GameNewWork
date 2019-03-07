using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;

public enum KeyMode
{
	KeyTrue,KeyFalse,Down,Up
}

/// <summary>
/// 状态机链接输入事件
/// </summary>

public class InputEvents : TransitionBehaviour 
{
	[Header( "按键 模式 " )]
	public KeyMode modele =  KeyMode.KeyTrue;
	[Header( "并且 和 或者 ， 当And_Or为真keys数组都必须被按下才能进入下一个状态" )]
	public bool And_Or = false;//或者和并且控制,真时为bq并且(和)，假则为或者
	[Header( "键值 数组 " )]
	public string[] keys = new string[] {"w" , "s" , "a" , "d"};
	bool condition = false;

	public override void OnTransitionUpdate (StateManager manager , State CurrState, State DstState, Transition transition, ref bool isEnterNextState)
	{
		switch(modele){
		case KeyMode.KeyTrue:
			foreach(string key in keys){
				if (And_Or) {
					if (Input.GetKey (key) == true) {//如果conditionA是true的话，那么给condition一个标记为真,这时候并不能跳出
						condition = true;//记住一个标记为真，这时候还不能break跳出，因为还要判断conditionB条件B，知道判断整个数组完成都为真也就是自动退出，那么condition标记为真，则可以执行if语句内
					} else {//如果conditionA是false的话,那么if ( conditionA && conditionB )就不用判断了,直接为false...跳出判断
						condition = false;
						break;
					}
				} else {
					if ( Input.GetKey (key) == true ) {//如果conditionA是ture的话,那么if ( conditionA || conditionB )就不用判断...直接是ture...跳出循环
						condition = true;
						break;
					}
				}
			}
			if( condition ){
				transition.isEnterNextState = true;
				condition = false;
			}
			break;
		case KeyMode.KeyFalse:
			foreach(string key in keys){
				if (And_Or) {
					if (Input.GetKey (key) == false) {//如果conditionA是true的话，那么给condition一个标记为真,这时候并不能跳出
						condition = true;//记住一个标记为真，这时候还不能break跳出，因为还要判断conditionB条件B，知道判断整个数组完成都为真也就是自动退出，那么condition标记为真，则可以执行if语句内
					} else {//如果conditionA是false的话,那么if ( conditionA && conditionB )就不用判断了,直接为false...跳出判断
						condition = false;
						break;
					}
				} else {
					if ( Input.GetKey (key) == false ) {//如果conditionA是ture的话,那么if ( conditionA || conditionB )就不用判断...直接是ture...跳出循环
						condition = true;
						break;
					}
				}
			}
			if( condition ){
				transition.isEnterNextState = true;
				condition = false;
			}
			break;
		case KeyMode.Down:
			foreach(string key in keys){
				if (And_Or) {
					if (Input.GetKeyDown (key) == true) {//如果conditionA是true的话，那么给condition一个标记为真,这时候并不能跳出
						condition = true;//记住一个标记为真，这时候还不能break跳出，因为还要判断conditionB条件B，知道判断整个数组完成都为真也就是自动退出，那么condition标记为真，则可以执行if语句内
					} else {//如果conditionA是false的话,那么if ( conditionA && conditionB )就不用判断了,直接为false...跳出判断
						condition = false;
						break;
					}
				} else {
					if ( Input.GetKeyDown (key) == true ) {//如果conditionA是ture的话,那么if ( conditionA || conditionB )就不用判断...直接是ture...跳出循环
						condition = true;
						break;
					}
				}
			}
			if( condition ){
				transition.isEnterNextState = true;
				condition = false;
			}
			break;
		case KeyMode.Up:
			foreach(string key in keys){
				if (And_Or) {
					if (Input.GetKeyUp (key) == true) {//如果conditionA是true的话，那么给condition一个标记为真,这时候并不能跳出
						condition = true;//记住一个标记为真，这时候还不能break跳出，因为还要判断conditionB条件B，知道判断整个数组完成都为真也就是自动退出，那么condition标记为真，则可以执行if语句内
					} else {//如果conditionA是false的话,那么if ( conditionA && conditionB )就不用判断了,直接为false...跳出判断
						condition = false;
						break;
					}
				} else {
					if ( Input.GetKeyUp (key) == true ) {//如果conditionA是ture的话,那么if ( conditionA || conditionB )就不用判断...直接是ture...跳出循环
						condition = true;
						break;
					}
				}
			}
			if( condition ){
				transition.isEnterNextState = true;
				condition = false;
			}
			break;
		}
	}
}
