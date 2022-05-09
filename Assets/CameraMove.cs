using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float _leftBound;
    float _rightBound;
    float _upperBound;
    float _bottomBound;
    [SerializeField] int _horizontalSplitFactor;
    [SerializeField] int _verticalSplitFactor;
    [SerializeField] float _movement;
    // Start is called before the first frame update
    void Start()
    {
        _leftBound = Camera.main.pixelWidth / _horizontalSplitFactor;
        _rightBound = _leftBound * (_horizontalSplitFactor - 1);
        _upperBound = Camera.main.pixelHeight / _verticalSplitFactor;
        _bottomBound = _upperBound * (_verticalSplitFactor - 1);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        if (mouseY > _upperBound && mouseY < _bottomBound)
        {
            if (mouseX < _leftBound)
                transform.Translate(-_movement, 0, 0);
            if (mouseX > _rightBound)
                transform.Translate(_movement, 0, 0);
        }
    }
}