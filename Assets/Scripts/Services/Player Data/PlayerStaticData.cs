namespace Features.Services
{
    public class PlayerStaticData : IPlayerStaticData
    {
        private PlayerCoins playerCoins;
        public PlayerCoins PlayerCoins => playerCoins;

        public PlayerStaticData()
        {
            playerCoins = new PlayerCoins();
        }
    }
}
