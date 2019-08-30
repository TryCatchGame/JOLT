using GameManager;

namespace GameEntity.Collectables {
    public class TimeSlowPowerUp : Collectable {
        protected override void OnCollectedEvent() {
            TimeSlowManager.Instance.AddTimeSlowUsableCount();
        }
    }
}
