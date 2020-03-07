using FSM;
using Steerings;
using UnityEngine;

[RequireComponent(typeof(PathFeeder))]
[RequireComponent(typeof(LADYBUG_BLACKBOARD))]
public class FSM_LADYBUG : FiniteStateMachine
{
    public enum State { INITIAL, WANDERING, GO_FOR_FOOD, GO_CHAMBRE};
    public State currentState = State.INITIAL;

    private LADYBUG_BLACKBOARD blackboard;
    private PathFeeder pathFeeder;

    private GameObject target;
    private GameObject pathFeederTarget;

    private GameObject egg;
    private GameObject seed;
    // Start is called before the first frame update
    void Start()
    {
        pathFeeder = GetComponent<PathFeeder>();

        blackboard = gameObject.GetComponent<LADYBUG_BLACKBOARD>();

        pathFeeder.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.WANDERING);
                break;
            case State.WANDERING:
                pathFeederTarget = blackboard.GetRandomWayPoint();
                pathFeeder.target = pathFeederTarget;
                if (SensingUtils.DistanceToTarget(this.gameObject, pathFeederTarget) <= blackboard.reachedRadius)
                {
                    pathFeederTarget = blackboard.GetRandomWayPoint();
                    pathFeeder.target = pathFeederTarget;
                }
                egg = SensingUtils.FindInstanceWithinRadius(this.gameObject, "EGG", blackboard.detectionClosestEggRadius);
                if(egg != null)
                {
                    target = egg;
                    ChangeState(State.GO_FOR_FOOD);
                    break;
                }
                else
                {
                    egg = SensingUtils.FindRandomInstanceWithinRadius(this.gameObject, "EGG", blackboard.detectionEggRadius);
                    if(egg != null)
                    {
                        target = egg;
                        ChangeState(State.GO_FOR_FOOD);
                        break;
                    }
                }
                seed = SensingUtils.FindInstanceWithinRadius(this.gameObject, "SEED", blackboard.detectionClosestSeedRadius);
                if (seed != null)
                {
                    target = seed;
                    ChangeState(State.GO_FOR_FOOD);
                    break;
                }
                else
                {
                    seed = SensingUtils.FindRandomInstanceWithinRadius(this.gameObject, "EGG", blackboard.detectionSeedRadius);
                    if (seed != null)
                    {
                        target = seed;
                        ChangeState(State.GO_FOR_FOOD);
                        break;
                    }
                }
                break;
            case State.GO_FOR_FOOD:
                if(target == egg)
                {
                    egg = SensingUtils.FindRandomInstanceWithinRadius(this.gameObject, "EGG", blackboard.detectionClosestEggRadius);
                    if(egg != null)
                    {
                        target = egg;
                        pathFeeder.target = target;
                    }
                }
                if(target == seed)
                {
                    egg = SensingUtils.FindRandomInstanceWithinRadius(this.gameObject, "EGG", blackboard.detectionEggWhileTranspotingSeed);
                    if (egg != null)
                    {
                        target = egg;
                        pathFeeder.target = target;
                    }
                }
                if(SensingUtils.DistanceToTarget(this.gameObject, target) <= blackboard.reachedRadius)
                {
                    if(target = egg)
                    {
                        egg.transform.parent = gameObject.transform;
                        egg.tag = "EGG_ON_ANT";
                        ChangeState(State.GO_CHAMBRE);
                        break;
                    }
                    else if (target = seed)
                    {
                        seed.transform.parent = gameObject.transform;
                        seed.tag = "SEED_ON_ANT";
                        ChangeState(State.GO_CHAMBRE);
                        break;
                    }
                }
                break;
            case State.GO_CHAMBRE:
                if(target == seed)
                {
                    egg = SensingUtils.FindRandomInstanceWithinRadius(this.gameObject, "EGG", blackboard.detectionEggWhileTranspotingSeed);
                    if(egg != null)
                    {
                        seed.transform.parent = null;
                        target = egg;
                        pathFeeder.target = target;
                        ChangeState(State.GO_FOR_FOOD);
                        break;
                    }
                }
                if (SensingUtils.DistanceToTarget(this.gameObject, pathFeederTarget) <= blackboard.reachedRadius)
                {
                    if(target == egg)
                    {
                        egg.transform.parent = null;
                        target = null;
                    }
                    else
                    {
                        seed.transform.parent = null;
                        target = null;
                    }
                    ChangeState(State.WANDERING);
                    break;
                }
                break;
        }
    }

    void ChangeState (State newState)
    {
        switch(currentState)
        {
            case State.INITIAL: break;
            case State.WANDERING:
                pathFeeder.enabled = false;
                break;
            case State.GO_FOR_FOOD:
                pathFeeder.enabled = false;
                break;
            case State.GO_CHAMBRE:
                pathFeeder.enabled = false;
                break;
        }

        switch (newState)
        {
            case State.WANDERING:
                pathFeeder.enabled = true;
                break;
            case State.GO_FOR_FOOD:
                pathFeeder.enabled = true;
                pathFeeder.target = target;
                break;
            case State.GO_CHAMBRE:
                pathFeeder.enabled = true;
                if (target == egg)
                {
                    pathFeederTarget = blackboard.GetRandomHatchingPoint();
                }
                else
                {
                    pathFeederTarget = blackboard.GetRandomStorePoint();
                }
                pathFeeder.target = pathFeederTarget;
                break;
        }

        currentState = newState;
    }
}
