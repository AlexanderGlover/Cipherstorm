using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMgr : MonoBehaviour {
    private GameObject healthManagingObject;
    private GameObject player;

    private StateMgr stateMgr;
    private Animator playerAnimator;
    private bool mIsEnabled = false;
    
	void Start () {
        //Should only ever be one player avatar and one main camera!
        healthManagingObject = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        player = GameObject.FindGameObjectsWithTag("PlayerAvatar")[0];

		stateMgr = healthManagingObject.gameObject.GetComponent(typeof(StateMgr)) as StateMgr;
        playerAnimator = player.gameObject.GetComponent(typeof(Animator)) as Animator;
    }
	
    void OnTriggerEnter(Collider other)
    {   
        if (!mIsEnabled)
        {   
            return;
        }
        stateMgr.DamagePlayer();
        playerAnimator.Play("Damaged");
    }
    
    public void SetEnabled(bool isEnabled)
    {
        mIsEnabled = isEnabled;
    }
}
