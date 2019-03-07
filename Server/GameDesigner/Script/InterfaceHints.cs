using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class InterfaceHints : MonoBehaviour 
{
	public Text NoBulletHints = null;
	public GameDesigner.Timer time = new GameDesigner.Timer (1);
	public new AudioSource audio = null;

	public void SetTools(string message)
	{
		NoBulletHints.text = message;
	}

	void Update()
	{
		if(time.IsTimeOut){
			gameObject.SetActive (false);
		}
	}
}
