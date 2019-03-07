using UnityEngine;
using System.Collections;

public class AddDesMesh : MonoBehaviour
{
    public bool isAdd_Mesh = false;
    void Start()
    {
        MeshCollider[] ren = transform.GetComponentsInChildren<MeshCollider>();
        Animation[] mAnimation = GameObject.FindObjectsOfType( typeof(Animation) ) as Animation[];
        Animator[] anims = GameObject.FindObjectsOfType(typeof(Animator)) as Animator[];
        Light[] light = GameObject.FindObjectsOfType(typeof(Light)) as Light[];
        //GameObject[] allgameobj = GameObject.FindObjectsOfType(typeof(GameObject ) ) as GameObject[];
        if(isAdd_Mesh )
        foreach (MeshCollider i in ren)
        {
            Destroy(i);
        }
        foreach (Animation i in mAnimation )
        {
            Destroy(i);
        }
        foreach (Animator i in anims )
        {
            Destroy(i);
        }
        foreach (Light i in light )
        {
            Destroy(i);
        }
        //foreach (GameObject i in allgameobj )
        //{
        //    i.transform.parent = transform;
        //}

        gameObject.AddComponent<SceneLoad >().enabled = false;

        var chushendian = GameObject.Find("chushendian");
        Destroy(chushendian.GetComponent<Collider>() );
        Destroy(chushendian.GetComponent<Renderer>() );
        Destroy(chushendian.GetComponent<MeshFilter >() );
        chushendian.AddComponent<LoadPlayPos >();

        var guaiwu1 = GameObject.Find("guaiwu 01");
        var guaiwu2 = GameObject.Find("guaiwu 02");
        var guaiwu3 = GameObject.Find("guaiwu 03");
        var guaiwu4 = GameObject.Find("guaiwu 04");
        var guaiwu5 = GameObject.Find("guaiwu 05");

        if(guaiwu1 )
        {
            Destroy(guaiwu1.GetComponent<Collider>());
            Destroy(guaiwu1.GetComponent<Renderer>());
            Destroy(guaiwu1.GetComponent<MeshFilter>());
            guaiwu1.AddComponent<SceneInsEnemy>().enabled = false;
        }
        if (guaiwu2)
        {
            Destroy(guaiwu2.GetComponent<Collider>());
            Destroy(guaiwu2.GetComponent<Renderer>());
            Destroy(guaiwu2.GetComponent<MeshFilter>());
            guaiwu2.AddComponent<SceneInsEnemy>().enabled = false;
        }
        if (guaiwu3)
        {
            Destroy(guaiwu3.GetComponent<Collider>());
            Destroy(guaiwu3.GetComponent<Renderer>());
            Destroy(guaiwu3.GetComponent<MeshFilter>());
            guaiwu3.AddComponent<SceneInsEnemy>().enabled = false;
        }
        if (guaiwu4)
        {
            Destroy(guaiwu4.GetComponent<Collider>());
            Destroy(guaiwu4.GetComponent<Renderer>());
            Destroy(guaiwu4.GetComponent<MeshFilter>());
            guaiwu4.AddComponent<SceneInsEnemy>().enabled = false;
        }
        if (guaiwu5)
        {
            Destroy(guaiwu5.GetComponent<Collider>());
            Destroy(guaiwu5.GetComponent<Renderer>());
            Destroy(guaiwu5.GetComponent<MeshFilter>());
            guaiwu5.AddComponent<SceneInsEnemy>().enabled = false;
        }

        Debug.Log("Ok");
        Destroy(GetComponent<AddDesMesh >() );
    }
}