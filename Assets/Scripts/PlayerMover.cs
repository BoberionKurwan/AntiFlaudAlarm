using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private const string Horizontal = "Horizontal"; 
    private const string Vertical = "Vertical"; 

    private void Update()
    {
        float moveX = Input.GetAxis(Horizontal) * _moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);

        float moveZ = Input.GetAxis(Vertical) * _moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, moveZ);
    }
}