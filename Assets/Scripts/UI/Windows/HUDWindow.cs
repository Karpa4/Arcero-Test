using Features.UI.Windows.Base;
using Features.Services.UI.Factory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Features.Services.UI.Windows;
using Features.Services;
using TMPro;

public class HUDWindow : BaseWindow
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private FloatingJoystick floatingJoystick;

    private PlayerCoins playerCoins;
    private IWindowsService windowsService;

    [Inject]
    public void Construct(IWindowsService windowsService, PlayerMove playerMove, PlayerCoins playerCoins)
    {
        playerMove.Init(floatingJoystick);
        this.windowsService = windowsService;
        this.playerCoins = playerCoins;
        coinsText.text = playerCoins.Coins.ToString();
    }

    protected override void Subscribe()
    {
        pauseButton.onClick.AddListener(ActivePause);
        playerCoins.CoinsChanged += DisplayCoins;
    }

    private void ActivePause()
    {
        Time.timeScale = 0;
        windowsService.Open(WindowId.Pause);
    }

    private void DisplayCoins(int coins)
    {
        coinsText.text = "Coins: " + coins.ToString();
    }

    protected override void Cleanup()
    {
        pauseButton.onClick.RemoveListener(ActivePause);
        playerCoins.CoinsChanged -= DisplayCoins;
    }
}
