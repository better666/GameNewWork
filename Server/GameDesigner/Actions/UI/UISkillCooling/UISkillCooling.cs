using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class UISkillCooling : MonoBehaviour 
{
	public Image 		m_SkillImage = null;
	public Text 		m_CoolingText = null;
	public KeyCode 		m_Key = KeyCode.A;//静态识别按钮
	public bool  		m_LengQueDone = true;
	public float        m_LengQueTime = 0;
	public float        m_LengQueTimeMax = 5f;
	public Sprite 		m_Texture = null;

	void OnEnable()
	{
		Awake ();
	}

	void OnDisable()
	{
		Awake ();
		enabled = true;
	}

	public void UpdateCooling ( float timeMax , Sprite skillTexture )
	{
		m_LengQueTime = 0;
		m_LengQueTimeMax = timeMax;
		m_LengQueDone = false;
		m_Texture = skillTexture;
	}

	// Use this for initialization
	void Awake () 
	{
		m_SkillImage = GetComponent<Image> ();
		m_CoolingText = transform.GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void LateUpdate() 
	{
		if (m_SkillImage == null | m_CoolingText == null | m_LengQueDone)
			return;

		m_SkillImage.sprite = m_Texture;
		m_LengQueTime += Time.deltaTime;
		m_SkillImage.fillAmount = m_LengQueTime / m_LengQueTimeMax;
		m_CoolingText.text = "" + (m_LengQueTimeMax - m_LengQueTime).ToString("f1");
		m_LengQueDone = m_LengQueTime > m_LengQueTimeMax;
		m_LengQueTime = m_LengQueDone ? 0 : m_LengQueTime;
	}
}
