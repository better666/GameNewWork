using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour 
{
	public Transform tarent;
	private Transform thisTran;
    public Camera    MainCamera;
    public float targetHeight = 15f;
    public bool PlayMini = false;
	public int  Layer = 6;
	public bool rotation = true;
	public Transform UIRoto = null;

	// Use this for initialization
	void Start () 
	{
		thisTran = transform;
        if( tarent == false )
			tarent = GameObject.FindObjectOfType<GameDesigner.Player>().transform;
        MainCamera = Camera.main;
        if ( PlayMini )
        {
            thisTran.SetParent ( tarent );
            thisTran.localPosition = new Vector3 ( 0 , targetHeight , 0 );
            thisTran.localRotation = new Quaternion ( 0 , 180f , 0 , 0 );
            enabled = false;
        }
		gameObject.layer = Layer;
	}
	
	// Update is called once per frame
	void LateUpdate() 
	{
        thisTran.position = new Vector3 ( tarent.position.x , tarent.position.y + targetHeight , tarent.position.z );
		if( rotation ){
			thisTran.rotation = Quaternion.Euler ( 90.0F , MainCamera.transform.eulerAngles.y , 0 );
		}
		UIRoto.rotation = Quaternion.Euler ( 0 , 0 , MainCamera.transform.eulerAngles.y );
    }
}
