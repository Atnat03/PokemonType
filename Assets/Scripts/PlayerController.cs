using UnityEngine;


    public class PlayerController : MonoBehaviour
    {
        public bool IsMoving => isMoving;
        
        [Header("Grid Settings")]
        [SerializeField] private float cellSize = 1f;

        [Header("Collision")]
        [SerializeField] private LayerMask wallLayerMask;
        [SerializeField] private float raycastDistance = 0.6f;

        [Header("Animation")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float rotationSpeed = 15f;
        [SerializeField] private Animator animator;

        private Vector3 targetPosition;
        private Quaternion targetRotation;
        private bool isMoving;
        
        private Vector3 _previousPosition;

        private void Awake()
        {
            SnapToGrid();
            targetPosition = transform.position;
            targetRotation = transform.rotation;
        }

        private void Update()
        {
            if (isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
                {
                    transform.position = targetPosition;
                    isMoving = false;
                }
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            float velocity = (transform.position - _previousPosition).magnitude / Time.deltaTime;
            _previousPosition = transform.position;

            animator.SetFloat("Moving", velocity / moveSpeed);
        }
        
        public bool TryMove(Vector3 direction)
        {
            if (isMoving) return false;

            if (IsBlocked(direction))
            {
                return false;
            }
            
            targetRotation = Quaternion.LookRotation(direction);
            targetPosition += direction * cellSize;
            isMoving = true;
            return true;
        }

        private bool IsBlocked(Vector3 direction)
        {
            Ray ray = new Ray(transform.position + Vector3.up * 0.1f, direction);
            return Physics.Raycast(ray, raycastDistance, wallLayerMask);
        }

        private void SnapToGrid()
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(Mathf.Round(pos.x / cellSize) * cellSize, pos.y, Mathf.Round(pos.z / cellSize) * cellSize);
        }
    }
