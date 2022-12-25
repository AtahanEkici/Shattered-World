using UnityEngine;
using UnityEngine.AI;

public class Unit_Controller : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private NavMeshAgent navmesh_agent;
    //[SerializeField] public Transform move_target;

    [Header("AI Behaviours")]
    [SerializeField] bool isAggressive = true;
    [SerializeField] bool isDefensive = false;
    [SerializeField] bool isCoward = false;


    private void Awake()
    {
        CheckBehaviour();
        navmesh_agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Transform move_target)
    {
        Debug.Log(move_target);
        navmesh_agent.destination = move_target.position;
    }

    private void CheckBehaviour() // Check if more than 1 behaviour is set  if so change it//
    {
        if (isAggressive) { isDefensive = false; isCoward = false; }
        else if (isDefensive) { isAggressive = false; isCoward = false; }
        else if (isCoward) { isDefensive = false; isAggressive = false; }
        else { isAggressive = true; isDefensive = false; isCoward = false; } // Default behaviour is Aggressive //
    }
    

    private void Update()
    {
        
    }
}
