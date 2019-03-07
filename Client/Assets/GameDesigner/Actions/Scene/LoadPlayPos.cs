using UnityEngine;
using System.Collections;

public class LoadPlayPos : MonoBehaviour 
{
	public Transform mPlayTarget;
    public GameObject[] mPlayTargets;
	// Use this for initialization
	void Start () 
    {
        mPlayTargets = GameObject.FindGameObjectsWithTag ("Player");
       
        if (GetComponent<BoxCollider >())
            Destroy(GetComponent<BoxCollider>() );

        transform.localScale = new Vector3(1,1,1);

        foreach(GameObject i in mPlayTargets )
        {
            i.transform.position = transform.position;
        }
	}
	
    public void Load_Play_Pos()
    {
        foreach ( GameObject i in mPlayTargets )
        {
            i.transform.position = transform.position;
        }
    }
	// Update is called once per frame
	void LateUpdate() {
	
	}
}
