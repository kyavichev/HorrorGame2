using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGhostAI : MonoBehaviour
{
    public enum State { Idle, Track, Shake, Attack, Disappear }
    public State state;

    // Behavior Settings
    public float hoverHeight = 1;
    public float observeDistance = 5;
    public float attackDistance = 10;

    // Movement settings
    public float speed = 1;
    public float rotSpeed = 10;
    public LayerMask terrainMask;

    // Attack settings
    public float attackSpeed = 3;

    // Disappear settings
    public float hideDuration = 5;
    public float _hideTimer = 0;

    // Shake settings
    public float shakeDuration = 3;
    public float _shakeTimer = 0;
    [Range(0f, 2f)]
    public float shakeDistance = 0.1f;
    [Range(0f, 0.1f)]
    public float delayBetweenShakes = 0f;
    private Vector3 _startShakePos;
    private Vector3 _randomPos;

    public GameObject visualRoot;

    public GameObject targetObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObject == null)
        {
            return;
        }

        Vector3 dir = (targetObject.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        if (state == State.Idle)
        {
            state = State.Track;
        }
        else if (state == State.Track)
        {
            if (distance > observeDistance)
            {
                Vector3 moveStep = dir * speed * Time.deltaTime;
                Vector3 newPosition = transform.position + moveStep;

                Vector3 checkPosition = newPosition + new Vector3(0, 100, 0);
                RaycastHit hitInfo;
                if (Physics.Raycast(checkPosition, new Vector3(0, -1, 0), out hitInfo, 200, terrainMask))
                {
                    float y = hitInfo.point.y + hoverHeight;
                    newPosition.y = y;
                }

                transform.position = newPosition;
            }
            else
            {
                _startShakePos = transform.position;
                state = State.Shake;
            }
        }
        else if (state == State.Shake)
        {
            _shakeTimer += Time.deltaTime;
            if(_shakeTimer > shakeDuration)
            {
                _shakeTimer = 0;
                state = State.Attack;
            }
            else
            {
                _randomPos = _startShakePos + (Random.insideUnitSphere * shakeDistance);
                transform.position = _randomPos;
            }
        }
        else if (state == State.Attack)
        {
            if (distance < 0.1f)
            {
                visualRoot.SetActive(false);
                state = State.Disappear;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, Time.deltaTime * attackSpeed);
            }
        }
        else if (state == State.Disappear)
        {
            _hideTimer += Time.deltaTime;
            if(_hideTimer > hideDuration)
            {
                _hideTimer = 0;

                Vector3 offset = Random.insideUnitCircle * 30;
                Vector3 newPosition = transform.position + offset;
                Vector3 checkPosition = newPosition + new Vector3(0, 100, 0);
                RaycastHit hitInfo;
                if (Physics.Raycast(checkPosition, new Vector3(0, -1, 0), out hitInfo, 200, terrainMask))
                {
                    float y = hitInfo.point.y + hoverHeight;
                    newPosition.y = y;
                }

                transform.position = newPosition;

                state = State.Idle;
                visualRoot.SetActive(true);
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
    }
}
