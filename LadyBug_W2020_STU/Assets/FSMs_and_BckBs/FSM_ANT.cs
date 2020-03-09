using FSM;
using Steerings;
using UnityEngine;

[RequireComponent(typeof(PathFeeder))]
[RequireComponent(typeof(ANT_BLACKBOARD))]
public class FSM_ANT : MonoBehaviour
{
    public enum State { INITIAL, FOLLOWING, EXIT};

    public State currentState = State.INITIAL;

    private ANT_BLACKBOARD blackboard;
    private PathFeeder pathFeeder;

    private GameObject target;
    private GameObject food;

    // Start is called before the first frame update
    void Start()
    {
        pathFeeder = GetComponent<PathFeeder>();

        blackboard = gameObject.GetComponent<ANT_BLACKBOARD>();

        pathFeeder.enabled = false;

        food = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.FOLLOWING);
                break;
            case State.FOLLOWING:
                Debug.Log(food);
                if(SensingUtils.DistanceToTarget(this.gameObject, target) <= blackboard.reachedRadius)
                {
                    food.transform.parent = null;
                    ChangeState(State.EXIT);
                    break;
                }
                break;
            case State.EXIT:
                if(SensingUtils.DistanceToTarget(this.gameObject, target) <= blackboard.reachedRadius)
                {
                    Destroy(this.gameObject);
                }
                break;
        }
    }

    void ChangeState (State newState)
    {
        switch (currentState)
        {
            case State.INITIAL: break;
            case State.FOLLOWING:
                pathFeeder.enabled = false;
                target = null;
                break;
            case State.EXIT:
                pathFeeder.enabled = false;
                target = null;
                break;
        }

        switch (newState)
        {
            case State.FOLLOWING:
                pathFeeder.enabled = true;
                target = blackboard.GetRandomFoodPoints();
                pathFeeder.target = target;
                break;
            case State.EXIT:
                pathFeeder.enabled = true;
                target = blackboard.GetRandomExitPoints();
                pathFeeder.target = target;
                break;
        }

        currentState = newState;
    }
}
