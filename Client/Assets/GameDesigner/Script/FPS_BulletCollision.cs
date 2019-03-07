using UnityEngine;
using System.Collections;

public class FPS_BulletCollision : MonoBehaviour
{
    public Weapon weapon;
    //射击击中目标特效
    public HitEffect[] hitEffects = new HitEffect[] {
        new HitEffect(),
        new HitEffect("Ground"),
        new HitEffect("Eneny")
    };
    public AudioClip explosionSound;
    public AudioSource aSource;
    public float explosionRadius = 5.0f;
    public float explosionPower = 10.0f;
    public float destroyTime = 0.05f;
    private new bool collider = false;

    public void Start()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        LimbCollisionManager limb = collision.gameObject.GetComponent<LimbCollisionManager>();
        if (limb != null) {
            foreach (var loca in weapon.Locations) {
                if (loca.limbsMode == limb.limbsMode) {
                    limb.player.Hp -= loca.outHP;
                }
            }
        }
        foreach (var effect in hitEffects) {
            if (collision.transform.CompareTag(effect.tag)) {
                var def = Instantiate(effect.effect, transform.position, transform.rotation);
                Destroy(def, effect.destroyTime);
                break;
            }
        }
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider)
            return;
        var colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in colliders) {
            if (hit.GetComponent<Rigidbody>())
                hit.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, transform.position, explosionRadius, 3.0f);
            LimbCollisionManager limb = hit.GetComponent<LimbCollisionManager>();
            if (limb != null) {
                foreach (var loca in weapon.Locations) {
                    if (loca.limbsMode == limb.limbsMode) {
                        limb.player.Hp -= loca.outHP;
                    }
                }
            }
        }
        aSource.clip = explosionSound;
        aSource.Play();
        Destroy(gameObject, destroyTime);
        collider = true;
    }
}
