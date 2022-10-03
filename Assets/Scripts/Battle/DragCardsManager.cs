using System;
using System.Collections;
using TripleTriad.Engine;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

namespace TripleTriad.Battle
{
    /// <summary>
    /// Script de gestion des effets de Drag & Drop des cartes
    /// </summary>
    [DefaultExecutionOrder(1)]
    public class DragCardsManager : MonoBehaviour
    {
        [SerializeField] private float cardDragPhysicsSpeed = 10;
        [SerializeField] private float cardDragSpeed = .1f;
        [SerializeField] private LayerMask BoardLayer;

        [SerializeField] private BattleManager _battleManager;

        public static Action<CardController, Position> OnCardRelease; 

        private PlayerControls controls;

        [HideInInspector] public CardView CurrentCard;

        [SerializeField] Camera _mainCamera;
        private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        private void Awake()
        {
            controls = new PlayerControls();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        public void Start()
        {
            controls.Touch.PrimaryContact.performed += CardPressed;
        }

        private Vector3 GetControlPosition()
        {
            return GetWorldPositionFromTouch(controls.Touch.PrimaryPosition.ReadValue<Vector2>());
        }

        private Vector3 GetWorldPositionFromTouch(Vector2 touchPosition)
        {
            Vector3 screenCoordinates = new Vector3(touchPosition.x, touchPosition.y, _mainCamera.nearClipPlane);
            return screenCoordinates;
        }

        private void CardPressed(InputAction.CallbackContext obj)
        {
            if (!_battleManager.PlayerTurn)
                return;

            var controlPosition = GetControlPosition();
            var ray = _mainCamera.ScreenPointToRay(controlPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider != null && hit.collider.tag.Contains("Card"))
                {
                    CurrentCard = hit.collider.GetComponent<CardView>();
                    // hit.collider.GetComponent<Rigidbody>().isKinematic = false;
                    StartCoroutine(DragUpdate(hit.collider.gameObject));
                }
            }
        }

        private IEnumerator DragUpdate(GameObject clickedObject)
        {
            // float initialDistance = Vector3.Distance(clickedObject.transform.position, _mainCamera.transform.position);
            float initialDistance =_mainCamera.transform.position.y;
            clickedObject.TryGetComponent<Rigidbody>(out var rb);
            while (controls.Touch.PrimaryContact.ReadValue<float>() != 0)
            {
                var controlPosition = GetControlPosition();
                var ray = _mainCamera.ScreenPointToRay(controlPosition);
                //if (rb != null)
                //{
                //   Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;

                //  Vector3 speed = direction * cardDragPhysicsSpeed;
                // rb.velocity = speed;
                // rb.rotation = Quaternion.Euler(new Vector3(rb.velocity.z, 0, -speed.x));
                // yield return waitForFixedUpdate;
                //}
                //else
                //{
                clickedObject.transform.parent.position = ray.GetPoint(initialDistance);
                yield return waitForFixedUpdate;
                // }
            }
            DropCardOnBoard(CurrentCard);
        }

        private void DropCardOnBoard(CardView card)
        {
            var controlPosition =  GetControlPosition(); 
            var ray = _mainCamera.ScreenPointToRay(controlPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Single.PositiveInfinity, BoardLayer)) {
                if (hit.collider != null)
                {
                    var slot = hit.collider.GetComponent<CardSlotBehaviour>();
                    if (slot != null )
                    {
                        if (!slot.IsEmpty)
                        {
                            BackInHand();
                            return;
                        }
                        OnCardRelease?.Invoke(CurrentCard.Card, slot.Position);
                        // TODO - subscribe to the event on the Current player
                        _battleManager.Player.FreeSlot(CurrentCard.handIndex);
                        Destroy(CurrentCard.gameObject.transform.parent.gameObject);
                    }
                }
            }
            else
            {
                BackInHand();
            }
        }

        private void BackInHand()
        {
            CurrentCard.transform.parent.position = _battleManager.Player.GetPositionOfHand(CurrentCard.handIndex);
        }
    }
}
