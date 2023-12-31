using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.LiveObjects
{
    public class Crate : MonoBehaviour
    {
        [SerializeField] private float _punchDelay;
        [SerializeField] private GameObject _wholeCrate, _brokenCrate;
        [SerializeField] private Rigidbody[] _pieces;
        [SerializeField] private BoxCollider _crateCollider;
        [SerializeField] private InteractableZone _interactableZone;
        private bool _isReadyToBreak = false;

        private List<Rigidbody> _brakeOff = new List<Rigidbody>();

        private void OnEnable()
        {
            InteractableZone.onZoneInteractionComplete += InteractableZone_onZoneInteractionComplete;
            InteractableZone.onZoneInteractionFullComplete += InteractableZone_onZoneInteractionFullComplete;
        }

        private void InteractableZone_onZoneInteractionFullComplete(InteractableZone zone)
        {
            DammageCreate(zone, true);
        }

        private void InteractableZone_onZoneInteractionComplete(InteractableZone zone)
        {
            DammageCreate(zone, false);
        }

        private void DammageCreate(InteractableZone zone, bool useFullPower)
        {
            if (_isReadyToBreak == false && _brakeOff.Count > 0)
            {
                _wholeCrate.SetActive(false);
                _brokenCrate.SetActive(true);
                _isReadyToBreak = true;
            }

            if (_isReadyToBreak && zone.GetZoneID() == 6) //Crate zone
            {
                if (_brakeOff.Count > 0)
                {
                    if (useFullPower == true)
                    {
                        BreakAllPart();
                    }
                    else
                    {
                        BreakPart();
                    }
                    StartCoroutine(PunchDelay());
                }
                else if (_brakeOff.Count == 0)
                {
                    _isReadyToBreak = false;
                    _crateCollider.enabled = false;
                    _interactableZone.CompleteTask(6);
                    Debug.Log("Completely Busted");
                }
            }

        }


        private void Start()
        {
            _brakeOff.AddRange(_pieces);
            
        }

        public void BreakPart()
        {
            int rng = Random.Range(0, _brakeOff.Count);
            _brakeOff[rng].constraints = RigidbodyConstraints.None;
            _brakeOff[rng].AddForce(new Vector3(1f, 1f, 1f), ForceMode.Force);
            _brakeOff.Remove(_brakeOff[rng]);            
        }

        public void BreakAllPart()
        {
            for (int i = 0; i < _brakeOff.Count; i++)
            {
                _brakeOff[i].constraints = RigidbodyConstraints.None;
                _brakeOff[i].AddForce(new Vector3(1f, 1f, 1f), ForceMode.Force);
                _brakeOff.Remove(_brakeOff[i]);
            }
        }


        IEnumerator PunchDelay()
        {
            float delayTimer = 0;
            while (delayTimer < _punchDelay)
            {
                yield return new WaitForEndOfFrame();
                delayTimer += Time.deltaTime;
            }

            _interactableZone.ResetAction(6);
        }

        private void OnDisable()
        {
            InteractableZone.onZoneInteractionComplete -= InteractableZone_onZoneInteractionComplete;
        }
    }
}
