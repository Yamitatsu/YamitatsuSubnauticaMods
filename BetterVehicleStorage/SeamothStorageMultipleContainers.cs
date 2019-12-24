namespace BetterVehicleStorage
{
    using System.Collections.Generic;
    using UnityEngine;
    using Utilities;

    public class SeamothStorageMultipleContainers : MonoBehaviour
    {
        public List<ItemsContainer> Containers { get; private set; }

        public void Awake()
        {
            YoLog.Debug($"SeamothStorageMultipleContainers just Awaken!");
            this.Init();
        }
        
        private void Init()
        {
            YoLog.Debug($"SeamothStorageMultipleContainers just Initialized!");
            if (this.Containers.Count > 0) return;
            
            YoLog.Debug($"SeamothStorageMultipleContainers : Adding Containers");
            for (int i = 0; i < 4; i++)
            {
                var itemsContainer = new ItemsContainer(4, 4, this.gameObject.GetComponent<Transform>(), "VehicleStorageLabel", null);
                this.Containers.Add(itemsContainer);
            }
        }

    }
}