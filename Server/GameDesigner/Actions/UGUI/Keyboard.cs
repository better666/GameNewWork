using UnityEngine;
using System.Collections;

public class Keyboard : MonoBehaviour {
    private MainUI main_ui;
	// Use this for initialization
	void Start () {
		main_ui = GameObject.FindObjectOfType<MainUI >();
	}
	
	// Update is called once per frame
	void LateUpdate() 
    {
        if (Input.GetKeyDown("i"))
        {
            main_ui.m_XGameSystem.m_ShuXingBoxList.gameObject.SetActive(true );
        }
        if (Input.GetKeyDown("o"))
        {
			main_ui.m_XGameSystem.m_WuPinBoxList.gameObject.SetActive(true);
        }
	}
}
