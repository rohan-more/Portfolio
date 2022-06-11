using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AISandbox
{
    public class UIManager : MonoBehaviour
    {
        public Flocking flockingManager;
        public Slider _neighborDistanceSlider;
        public Slider _separationSlider;
        public Slider _alignmentSlider;
        public Slider _cohesionSlider;
        public Toggle toggleDraw;

        void Start()
        {
            _neighborDistanceSlider.value = flockingManager.neighbourDistance;
            _separationSlider.value = flockingManager.seperationWeight;
            _alignmentSlider.value = flockingManager.alignmentWeight;
            _cohesionSlider.value = flockingManager.cohesionWeight;

            _neighborDistanceSlider.onValueChanged.AddListener(OnNeighborDistanceSliderValueChanged);
            _separationSlider.onValueChanged.AddListener(OnSeparationSliderValueChanged);
            _alignmentSlider.onValueChanged.AddListener(OnAlignmentSliderValueChanged);
            _cohesionSlider.onValueChanged.AddListener(OnCohesionSliderValueChanged);
            toggleDraw.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnNeighborDistanceSliderValueChanged(float _neighborDistance)
        {
            flockingManager.neighbourDistance = _neighborDistance;
        }

        private void OnSeparationSliderValueChanged(float _separationWeight)
        {
            flockingManager.seperationWeight = _separationWeight;
        }

        private void OnAlignmentSliderValueChanged(float _alignmentWeight)
        {
            flockingManager.alignmentWeight = _alignmentWeight;
        }

        private void OnCohesionSliderValueChanged(float _cohesionWeight)
        {
            flockingManager.cohesionWeight = _cohesionWeight;
        }
        private void OnToggleValueChanged(bool toggle)
        {
            foreach (FlockingController actor in flockingManager.flockActorList)
            {
                actor.SetDrawVectors(toggle);
            }
        }
    }
}

