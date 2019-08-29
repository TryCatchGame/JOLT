namespace GameEntity.Player {

    public class ShieldPaddle : Paddle {
        private int shieldLifeCount;

        internal void IncrementShieldLife() {
            ++shieldLifeCount;

            EnableShieldIfInactive();

            #region Local_Function

            void EnableShieldIfInactive() {
                if (!IsEnabled) {
                    EnablePaddle(true);
                }
            }

            #endregion
        }

        internal void DecreaseShieldLife() {
            --shieldLifeCount;

            ClampShieldLifeCount();

            #region Local_Function

            void ClampShieldLifeCount() {
                if (shieldLifeCount < 0) {
                    shieldLifeCount = 0;

                    DisableShieldIfActive();
                }
            }

            void DisableShieldIfActive() {
                if (IsEnabled) {
                    EnablePaddle(false);
                }
            }

            #endregion
        }

    }
}
