using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimationEndBehaviour : StateMachineBehaviour
{
    private PeacefulMobComponent mobComponent;
    private bool didGetMobComponent = false;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!didGetMobComponent)
        {
            mobComponent = animator.gameObject.GetComponent<PeacefulMobComponent>();
            didGetMobComponent = true;
        }
        mobComponent.StoppedDestroyingAnimation();
    }
}
