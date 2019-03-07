using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISliderHPMP : MonoBehaviour 
{
    private Slider hp_slider;
    public Text hp_text;
    public GameDesigner.Player player;

	public enum HPMP
	{
		HP , MP , Buff
	}
	public HPMP hpmp = HPMP.HP;
	// Use this for initialization
	void Start () {
	    hp_slider = GetComponent<Slider >();
        if(!player)
			player = GameObject.FindObjectOfType(typeof(GameDesigner.Player ) ) as GameDesigner.Player;
        if (!hp_text)
        {
            hp_text = transform.Find("Text").GetComponent<Text >();
        }
        if( player == true )
        {
            hp_slider.maxValue = player.Hp;
        }
    }
	
	// Update is called once per frame
	void LateUpdate() 
    {
		switch( hpmp )
		{
			case HPMP.HP :
			hp_slider.maxValue = player.HpMax;
			hp_slider.value = player.Hp;
			hp_text.text = "" + player.Hp;
			break;
			case HPMP.MP :
			hp_slider.maxValue = player.mpMax;
			hp_slider.value = player.Mp;
			hp_text.text = "" + player.Mp;
			break;
		}
	}
}
