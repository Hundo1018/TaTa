using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpingForce;
    [SerializeField] private float _rayLength;
    [SerializeField] private bool _isGround;
    [SerializeField] private float _jumpingCoolDown;
    [SerializeField] private float _jumpLastTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var ori = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.size.y / 2);
        var end = ori + Vector2.down * _rayLength;
        _isGround = Physics2D.Raycast(ori, Vector2.down, _rayLength, 1 << 6).transform != null;
        Debug.DrawLine(ori, end, Color.blue);
        if (_isGround && Input.GetMouseButton(0))
        {
            if (Time.time - _jumpLastTime > _jumpingCoolDown)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpingForce, ForceMode2D.Impulse);
                _jumpLastTime = Time.time;
            }
        }

    }
}
