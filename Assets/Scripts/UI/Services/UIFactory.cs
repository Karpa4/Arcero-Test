using Features.Services.Assets;
using Features.Services.StaticData;
using Features.Services.UI.Windows;
using Features.StaticData.Windows;
using Features.UI.Windows.Base;
using UnityEngine;
using Zenject;

namespace Features.Services.UI.Factory.BaseUI
{
    public class UIFactory : PlaceholderFactory<BaseWindow>, IUIFactory
    {
        private readonly DiContainer container;
        private readonly IAssetProvider assets;
        private readonly IStaticDataService staticData;

        private Transform uiRoot;

        private Camera mainCamera;

        [Inject]
        public UIFactory(DiContainer container, IAssetProvider assets, IStaticDataService staticData, IWindowsService windowsService)
        {
            this.container = container;
            this.assets = assets;
            this.staticData = staticData;
            windowsService.Register(this);
        }

        public BaseWindow Create(WindowId id)
        {
            WindowInstantiateData config = LoadWindowInstantiateData(id);

            if (uiRoot == null)
            {
                CreateUIRoot();
            }

            return CreateWindow(config, id);
        }

        private void CreateUIRoot()
        {
            if (uiRoot != null)
                return;

            GameObject prefab = assets.Instantiate(GlobalConst.UIRootPath);
            uiRoot = prefab.transform;
        }

        private BaseWindow CreateWindow(WindowInstantiateData config, WindowId id)
        {
            BaseWindow window = InstantiateWindow(config);
            return window;
        }

        private BaseWindow InstantiateWindow(WindowInstantiateData config)
        {
            BaseWindow window = container.InstantiatePrefab(config.Window, uiRoot).GetComponent<BaseWindow>();
            window.SetID(config.ID);
            return window;
        }


        private WindowInstantiateData LoadWindowInstantiateData(WindowId id) =>
          staticData.ForWindow(id);

        private Camera GetCamera()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;
            return mainCamera;
        }
    }
}