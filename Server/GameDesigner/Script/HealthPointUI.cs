using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointUI : MonoBehaviour {

	public GameDesigner.PlayerSystem player = null;
	public Slider hpSlider = null;

	private float hpSave = 0;

	// Use this for initialization
	void Start () {
		if(player==null){
			player = GameObject.FindObjectOfType<GameDesigner.PlayerSystem> ();
			if(player==null){
				Debug.Log ("没有玩家组建！");
				enabled = false;
				return;
			}
		}
		hpSlider.maxValue = player.HpMax;
		hpSlider.value = player.Hp;
	}
	
	// Update is called once per frame
	void Update () {
		if(hpSave != player.Hp){
			hpSlider.value = player.Hp;
			hpSave = player.Hp;
		}
	}
}