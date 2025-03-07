using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LazerChaseState1 : StateMachineBehaviour
{ 
    NavMeshAgent agent;
    Transform player;
    GameObject lazerPoint;
    GameObject TargetChange;
    MonsterInfo monsterInfo;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        lazerPoint = GameObject.FindGameObjectWithTag("Lazer_point");
        monsterInfo  = animator.GetComponent<MonsterInfo>();
        TargetChange = monsterInfo.GetRandomGameObject();
        if(TargetChange  != null ) {
            if (lazerPoint != null)
            {
                player = lazerPoint.transform;
                // Proceed with your logic using 'player'
            }
            else
            {
                player = TargetChange.transform;
            }
            agent.speed = 3.5f;
        } else
        {
            Debug.Log("에러처리해야함");
            return;
        }
        

    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (TargetChange != null)
        {
            if (lazerPoint != null && agent != null)
            {
                agent.SetDestination(player.position);
                float distance = Vector3.Distance(player.position, animator.transform.position);
                if (distance > 3)
                    animator.SetBool("isChasing", false);
                if (distance < 1.5f)
                    animator.SetBool("isAttacking", true);
            }
            else if (TargetChange != null && agent != null)
            {
                agent.SetDestination(player.position);
                float distance = Vector3.Distance(player.position, animator.transform.position);
                if (distance > 3)
                    animator.SetBool("isChasing", false);
                if (distance < 1.5f)
                    animator.SetBool("isAttacking", true);
            }
        } else
        {
            Debug.Log("에러처리 해야함");
            return;
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (TargetChange != null)
        {
            agent.SetDestination(animator.transform.position);
        } else
        {
            Debug.Log("에러처리 해야함");
        }
            

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
