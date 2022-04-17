using UnityEngine;
using System.Collections;
using Rubber;
public class ParticleManager : MonoBehaviour {

    public GameObject particle;

    private void OnEnable()
    {
        if (this.gameObject.tag == "particle")
        {
            StartCoroutine(DestroyerParticle());
        }
    }

    private IEnumerator DestroyerParticle()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 particlePos = collision.contacts[0].point;
        if (collision.gameObject.CompareTag("player") && !DetectCollisions.LockCollision )
        {
            Instantiate(particle,particlePos,Quaternion.identity);
        }
    }
}
