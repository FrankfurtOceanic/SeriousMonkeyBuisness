 using UnityEngine;
 using System.Collections;
 
 [RequireComponent( typeof( ParticleSystem )) ]
 public class ParticleStopper : MonoBehaviour {

     ParticleSystem ps;
 
     public float autoDestructCheckInterval = 0.5f;

    public bool autoStop=false;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if(autoStop)
            StartCoroutine( TickDelay() );

    }

    public void StopAndDetach () {
        transform.parent = null;
        ps.Stop(false, ParticleSystemStopBehavior.StopEmitting);

        StartCoroutine( TickDelay() );
     }
 
     private IEnumerator TickDelay()
     {
         yield return new WaitForSeconds( ps.main.duration );
         StartCoroutine( AliveCheck() );
     }
 
     private IEnumerator AliveCheck()
     {
         while( ps.IsAlive() )
         {
             yield return new WaitForSeconds( autoDestructCheckInterval );
         }
 
         Destroy( gameObject );
     }
 }