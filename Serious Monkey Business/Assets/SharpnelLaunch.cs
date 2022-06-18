using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpnelLaunch : MonoBehaviour
{
    public int min=3, max=10;
    public float minLife=1 ,maxLife=2;
    public float minImpulse=20,maxImpulse=30;
    public GameObject Shrapnel;
    // Start is called before the first frame update
    void Start()
    {
        int amt=Random.Range(min,max);
        for(int i = 0; i < amt; i++)
        {
            var rot=Random.rotation;
            if ((rot * Vector3.forward).y < 0)
                rot = rot * Quaternion.FromToRotation(Vector3.down, Vector3.up);
            var piece=Instantiate(Shrapnel, transform.position, rot, null);
            var rb = piece.GetComponent<Rigidbody>();
            rb.AddForce(rot * Vector3.forward*Random.Range(minImpulse,maxImpulse), ForceMode.VelocityChange);
            IEnumerator die() {
                yield return new WaitForSeconds(Random.Range(minLife, maxLife));
                piece.GetComponentInChildren<ParticleStopper>().StopAndDetach();
                Destroy(piece);
            }

            StartCoroutine(die());
        }
    }
}
