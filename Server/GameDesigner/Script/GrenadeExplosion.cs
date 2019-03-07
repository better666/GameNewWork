using UnityEngine;
using System.Collections;

public class GrenadeExplosion:MonoBehaviour
{
    public float timer = 5f;
    public GameObject explosionObj;
    public Weapon weapon;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timer);
        var obj = Instantiate(explosionObj, transform.position, transform.rotation);
        obj.GetComponent<FPS_BulletCollision>().weapon = weapon;
        Destroy(gameObject);
    }
}
