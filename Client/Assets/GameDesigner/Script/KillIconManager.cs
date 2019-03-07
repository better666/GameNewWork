using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillIconManager : MonoBehaviour {

	public Image 击杀图像组件 = null;

	public Sprite 爆头 = null;
	public Sprite 刀杀 = null;
	public Sprite 一杀 = null;
	public Sprite 双杀 = null;
	public Sprite 三杀 = null;
	public Sprite 四杀 = null;
	public Sprite 五杀 = null;
	public Sprite 六杀 = null;

	public AudioSource 击杀音效组件 = null;
	public AudioSource 每次击杀音效组件 = null;
	public AudioClip 爆头音效 = null;
	public AudioClip 一杀音效 = null;
	public AudioClip 双杀音效 = null;
	public AudioClip 三杀音效 = null;
	public AudioClip 四杀音效 = null;
	public AudioClip 五杀音效 = null;
	public AudioClip 六杀音效 = null;

	public bool 连杀启动 = false;
	public int 连杀数 = 0;
	public int 总杀敌人数 = 0;
	public Text 总杀敌文本组件 = null;
	public GameDesigner.Timer 连杀持续时间 = new GameDesigner.Timer(5f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(连杀启动){
			if(连杀持续时间.IsTimeOut){
				连杀启动 = false;
				连杀数 = 0;
			}
		}
	}
}
