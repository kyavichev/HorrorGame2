using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGhostAI : MonoBehaviour
{
    public enum State { Idle, Track, Shake, Attack, Disappear }
    private State _state;

    // Behavior Settings
    public float hoverHeight = 3.5f;
    public float observeDistance = 5;
    public float attackDistance = 15;

    // Movement settings
    public float speed = 3;
    public float rotSpeed = 10;
    public LayerMask terrainMask;

    // Attack settings
    public float attackSpeed = 25;

    // Disappear settings
    public float hideDuration = 5;
    private float _hideTimer = 0;

    // Shake settings
    public float shakeDuration = 3;
    private float _shakeTimer = 0;
    [Range(0f, 2f)]
    public float shakeDistance = 0.1f;
    [Range(0f, 0.1f)]
    public float delayBetweenShakes = 0f;
    private Vector3 _startShakePos;
    private Vector3 _randomPos;

    // Other settings
    private Vector3 _startPosition;

    public GameObject visualRoot;

    public GameObject targetObject;


    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        // If there is no target currently, try to grab one from the GameManager
        if(targetObject == null)
        {
            // Grab from the GameManager
            targetObject = GameManager.GetInstance().hero;

            // Check again in case GameManager also didn't have a hero
            if (targetObject == null)
            {
                return;
            }
        }

        Vector3 dir = (targetObject.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        if (_state == State.Idle)
        {
            _state = State.Track;
        }
        else if (_state == State.Track)
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
                _state = State.Shake;
            }
        }
        else if (_state == State.Shake)
        {
            _shakeTimer += Time.deltaTime;
            if(_shakeTimer > shakeDuration)
            {
                _shakeTimer = 0;
                _state = State.Attack;
            }
            else
            {
                _randomPos = _startShakePos + (Random.insideUnitSphere * shakeDistance);
                transform.position = _randomPos;
            }
        }
        else if (_state == State.Attack)
        {
            if (distance < 0.1f)
            {
                visualRoot.SetActive(false);
                _state = State.Disappear;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, Time.deltaTime * attackSpeed);
            }
        }
        else if (_state == State.Disappear)
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

                _state = State.Idle;
                visualRoot.SetActive(true);
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
    }
}
