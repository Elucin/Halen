using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubStateEnter : StateMachineBehaviour {
    [SerializeField]
    string myBool;
    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.SetBool(myBool, true);
    }

    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.SetBool(myBool, false);
    }
}
