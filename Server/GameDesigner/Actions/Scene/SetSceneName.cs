using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SetSceneName : MonoBehaviour 
{
    public GameObject[] Scene = new GameObject[120];

    public bool Init = false;
    public bool SetName = false;
    public int SceneName = 1;
	#if UNITY_EDITOR
    public void InitEditor ()
    {
        if( Init == false ) return;
        for ( int i = 0 ; i < Scene.Length ; i++ )
        {
            Scene [ i ] = AssetDatabase.LoadAssetAtPath ( "Assets/Resources/LoadScene/Resources/" + i + ".prefab" , typeof ( GameObject ) ) as GameObject;
        }
        Init = false;
    }
	#endif 
    public void SetNameEditor ()
    {
        if ( SetName == false )
            return;
        for ( int i = 0 ; i < Scene.Length ; i++ )
        {
            if( Scene[i] == true )
            {
                Scene [ i ].name = SceneName.ToString();
                ++SceneName;
            }
        }
        SetName = false;
    }
#if UNITY_EDITOR
    public void SetAssName()
    {
        if ( SetName == false )
            return;
        for ( int i = 0 ; i < Scene.Length ; i++ )
        {
            if ( Scene [ i ] == true )
            {
                string assname = AssetDatabase.GetAssetPath ( Scene [ i ] );
                AssetDatabase.RenameAsset ( assname , SceneName.ToString() );
                ++SceneName;
            }
        }
        SetName = false;
    }
#endif
}
