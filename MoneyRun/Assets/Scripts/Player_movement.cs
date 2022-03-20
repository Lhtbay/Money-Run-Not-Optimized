using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _sideSpeed;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;


    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _forwardSpeed);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _sideSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _sideSpeed);
        }
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, _minX, _maxX),
            transform.localPosition.y,
            transform.localPosition.z);

    }


}
