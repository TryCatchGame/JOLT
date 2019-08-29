namespace GameEntity.Collectables {

    public class ShieldPowerUp : Collectable {
        protected override void OnCollectedEvent() {
            PlayerCore.IncreaseShieldPaddleLife();
        }
    }
}
