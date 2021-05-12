using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SETUP

//1. Add collider to crown gameobject
//2. Set collider to isTrigger
//3. Add script and initialize _health to a integer that seems appropiate
//4. add NullBalls scripts to all ball gameobjects


public class HealthBarCrown : MonoBehaviour
{
    public int _health;
    public bool isDead;
    //public AkEvent _hitSound;
    [SerializeField] private AK.Wwise.Event _hitSound;

    public HapticsAsset hapticClip;
    
    public float cooldownTime = 1f;
    private bool _cooling;
    
    private void OnCollisionEnter(Collision other)
    {
         
        if (!_cooling && other.gameObject.GetComponent<NullBalls>() != null)
        {
            _health--;
            _hitSound.Post(gameObject);
            GlobalHapticsPlayer.Instance.PlayHaptic(hapticClip);
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator StartCooldown()
    {
        _cooling = true;
        yield return new WaitForSeconds(cooldownTime);
        _cooling = false;
    }
}
