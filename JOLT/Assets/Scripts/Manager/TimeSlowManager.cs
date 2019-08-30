using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MyBox;

namespace GameManager {

    public class TimeSlowManager : Singleton<TimeSlowManager> {
        [Separator("Display properties")]
        [SerializeField, Tooltip("The background effect to show when time is slowed"), MustBeAssigned]
        private Image timeSlowedImage;
    }
}
