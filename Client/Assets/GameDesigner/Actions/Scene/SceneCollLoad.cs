using UnityEngine;
using System.Collections;

public class SceneCollLoad : MonoBehaviour {
    public string load_scene = "1";
    public GameObject this_scene;

    public enum Scene
    {
        InsScene,
        SetScene
    }
    public Scene scene = Scene.InsScene;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (scene)
            {
                case Scene.InsScene :
                    Instantiate ( Resources.Load ( load_scene.ToString () ) );
                    Destroy (this_scene);
                break;
                case Scene.SetScene :
                    Instantiate ( Resources.Load ( load_scene.ToString () ) );
                    this_scene.SetActive(false);
                break;
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate() {
	
	}
}
