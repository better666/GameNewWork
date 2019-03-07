using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FondRenreadAddMesh : MonoBehaviour 
{
    public string fondren = "3v3_b_plant";
    public bool StartFond = false;
    public bool DesMeshColl = false;
    public Component componentType = null;

    public void AddMeshCollider()
    {
        MeshRenderer[] mrs = transform.GetComponentsInChildren<MeshRenderer>();

        for( int i = 0 ; i < mrs.Length ; i ++ )
        {
            if( mrs[i].sharedMaterial.name == fondren )
            {
                mrs[i].gameObject.AddComponent( componentType.GetType() );
                Debug.Log( mrs[i].gameObject.name + "成功添加!" );
            }
        }
        StartFond = false;
    }

    public void DesMeshCollider()
    {
        MeshRenderer[] mrs = transform.GetComponentsInChildren<MeshRenderer>();

        for( int i = 0 ; i < mrs.Length ; i ++ )
        {
            if( mrs[i].sharedMaterial.name == fondren )
            {
                DestroyImmediate( mrs[i].gameObject.GetComponent( componentType.GetType() ) , true );
                Debug.Log( mrs[i].gameObject.name + "成功删除!" );
            }
        }

        DesMeshColl = false;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate() 
    {
        if( StartFond == true )
        {
            AddMeshCollider();
        }

        if( DesMeshColl == true )
        {
            DesMeshCollider();
        }
	}
}
