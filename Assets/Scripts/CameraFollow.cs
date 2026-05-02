using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    #region Variables


    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 5, -15);
  
    [SerializeField] private float _smoothMove = 0.02f;
  
    Vector3 _wantedPosition;
    Quaternion _wantedRotation;


    #endregion


    #region Fonctions


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _smoothMove * Time.deltaTime);
    }


    #endregion
}