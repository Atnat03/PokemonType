using UnityEngine;
using UnityEngine.InputSystem;

namespace CommandPattern
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerController playerController;

        [Header("Hold Settings")]
        [SerializeField] private float holdDelay = 0.2f;

        private PlayerInput _playerInput;

        private InputAction _moveUpAction;
        private InputAction _moveDownAction;
        private InputAction _moveLeftAction;
        private InputAction _moveRightAction;
        private InputAction _undoAction;
        private InputAction _redoAction;

        private Vector3 _heldDirection = Vector3.zero;
        private float _holdTimer = 0f;
        private bool _firstMoveDone = false;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            
            BindActions();
        }

        private void OnEnable()
        {
            _moveUpAction.performed += OnMoveUp;
            _moveDownAction.performed += OnMoveDown;
            _moveLeftAction.performed += OnMoveLeft;
            _moveRightAction.performed += OnMoveRight;

            _moveUpAction.canceled += OnMoveUpCanceled;
            _moveDownAction.canceled += OnMoveDownCanceled;
            _moveLeftAction.canceled += OnMoveLeftCanceled;
            _moveRightAction.canceled += OnMoveRightCanceled;

            _undoAction.performed += OnUndo;
            _redoAction.performed += OnRedo;
        }

        private void OnDisable()
        {
            _moveUpAction.performed -= OnMoveUp;
            _moveDownAction.performed -= OnMoveDown;
            _moveLeftAction.performed -= OnMoveLeft;
            _moveRightAction.performed -= OnMoveRight;

            _moveUpAction.canceled -= OnMoveUpCanceled;
            _moveDownAction.canceled -= OnMoveDownCanceled;
            _moveLeftAction.canceled -= OnMoveLeftCanceled;
            _moveRightAction.canceled -= OnMoveRightCanceled;

            _undoAction.performed -= OnUndo;
            _redoAction.performed -= OnRedo;
        }

        private void OnMoveUp(InputAction.CallbackContext ctx) => StartHold(Vector3.forward);
        private void OnMoveDown(InputAction.CallbackContext ctx) => StartHold(Vector3.back);
        private void OnMoveLeft(InputAction.CallbackContext ctx) => StartHold(Vector3.left);
        private void OnMoveRight(InputAction.CallbackContext ctx) => StartHold(Vector3.right);
        private void OnMoveUpCanceled(InputAction.CallbackContext ctx) => StopHold();
        private void OnMoveDownCanceled(InputAction.CallbackContext ctx) => StopHold();
        private void OnMoveLeftCanceled(InputAction.CallbackContext ctx) => StopHold();
        private void OnMoveRightCanceled(InputAction.CallbackContext ctx) => StopHold();
        private void OnUndo(InputAction.CallbackContext ctx) => CommandInvoker.Undo();
        private void OnRedo(InputAction.CallbackContext ctx) => CommandInvoker.Redo();

        private void Update()
        {
            if (_heldDirection == Vector3.zero) return;

            if (!_firstMoveDone)
            {
                ExecuteMove(_heldDirection);
                _firstMoveDone = true;
                _holdTimer = 0f;
                return;
            }

            _holdTimer += Time.deltaTime;
            if (_holdTimer >= holdDelay && !playerController.IsMoving)
            {
                ExecuteMove(_heldDirection);
                _holdTimer = 0f;
            }
        }

        private void StartHold(Vector3 direction)
        {
            _heldDirection = direction;
            _firstMoveDone = false;
            _holdTimer = 0f;
        }

        private void StopHold()
        {
            _heldDirection = Vector3.zero;
            _firstMoveDone = false;
            _holdTimer = 0f;
        }

        private void ExecuteMove(Vector3 direction)
        {
            CommandInvoker.ExecuteCommand(new MoveCommand(playerController, direction));
        }

        private void BindActions()
        {
            _moveUpAction = _playerInput.actions["MoveUp"];
            _moveDownAction = _playerInput.actions["MoveDown"];
            _moveLeftAction = _playerInput.actions["MoveLeft"];
            _moveRightAction = _playerInput.actions["MoveRight"];
            _undoAction = _playerInput.actions["Undo"];
            _redoAction = _playerInput.actions["Redo"];
        }
    }
}