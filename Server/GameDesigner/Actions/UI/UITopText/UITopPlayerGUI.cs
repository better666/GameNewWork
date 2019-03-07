using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UITopPlayerGUI : MonoBehaviour 
{
	public string 				Name = "龙兄の";
	public int 					lv = 18;
	public UITopTargetPosition 	topPosition = null;
    public GameDesigner.PlayerSystem   		m_PlayerTarget = null;
    public Slider           	m_HPSlider = null;
    public Slider           	m_MPSlider = null;
	public Text					nameText = null;
	public Text					lvText = null;

    // Use this for initialization
    void Start () 
    {
		m_PlayerTarget = topPosition.target.GetComponent<GameDesigner.PlayerSystem> ();

        if (m_PlayerTarget == null) enabled = false;

		SetLvOrName ( Name , lv );
    }

    // Update is called once per frame
    void LateUpdate() 
    {
        m_HPSlider.maxValue = m_PlayerTarget.HpMax;
        m_HPSlider.value = m_PlayerTarget.Hp;

        m_MPSlider.maxValue = m_PlayerTarget.mpMax;
        m_MPSlider.value = m_PlayerTarget.Mp;

		if( m_PlayerTarget.isDeath )
		{
			Destroy ( gameObject );
		}
    }

	public void SetLvOrName( string name , int lv )
	{
		nameText.text = name;
		lvText.text = lv.ToString();
	} 
}
