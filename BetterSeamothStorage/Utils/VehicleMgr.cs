namespace BetterSeamothStorage.Utils
{
    using System.Collections.Generic;
    using Modules;
    using UnityEngine;

    internal class VehicleMgr
    {
        public static readonly List<TechType> StorageTechTypes = new List<TechType>()
        {
            TechType.VehicleStorageModule,
            Main.StorageModuleMk2TechType,
            Main.StorageModuleMk3TechType,
            Main.StorageModuleMk4TechType,
            Main.StorageModuleMk5TechType
        };

        internal static bool IsAllowedToRemove(SeaMoth seaMoth, Pickupable pickupable, bool verbose)
        {
            if (StorageTechTypes.Contains(pickupable.GetTechType()))
            {
                SeamothStorageContainer component = pickupable.GetComponent<SeamothStorageContainer>();
                if (component != null)
                {
                    bool flag = component.container.count == 0;
                    if (verbose && !flag)
                    {
                        ErrorMessage.AddDebug(Language.main.Get("SeamothStorageNotEmpty"));
                    }

                    return flag;
                }

                Debug.LogError("No SeamothStorageContainer found on SeamothStorageModule item");
            }

            return true;
        }

        internal static bool IsAllowedToRemove(Exosuit exosuit, Pickupable pickupable, bool verbose)
        {
            if (StorageTechTypes.Contains(pickupable.GetTechType()))
            {
                bool flag = exosuit.storageContainer.container.count == 0;
                if (verbose && !flag)
                {
                    ErrorMessage.AddDebug(Language.main.Get("ExosuitStorageShrinkError"));
                }

                return flag;
            }

            return true;
        }

        internal static ItemsContainer GetStorageInSlot(Vehicle vehicle, int slotID)
        {
            InventoryItem slotItem = vehicle.GetSlotItem(slotID);
            if (slotItem == null)
            {
                return null;
            }

            Pickupable item = slotItem.item;
            if (!StorageTechTypes.Contains(item.GetTechType()))
            {
                return null;
            }

            SeamothStorageContainer component = item.GetComponent<SeamothStorageContainer>();
            if (component == null)
            {
                return null;
            }

            if (item.GetTechType() == Main.StorageModuleMk2TechType)
            {
                component.container.Resize(StorageUpgradeMk2.StorageWidth, StorageUpgradeMk2.StorageHeight);
            }
            else if (item.GetTechType() == Main.StorageModuleMk3TechType)
            {
                component.container.Resize(StorageUpgradeMk3.StorageWidth, StorageUpgradeMk3.StorageHeight);
            }
            else if (item.GetTechType() == Main.StorageModuleMk4TechType)
            {
                component.container.Resize(StorageUpgradeMk4.StorageWidth, StorageUpgradeMk4.StorageHeight);
            }
            else if (item.GetTechType() == Main.StorageModuleMk5TechType)
            {
                component.container.Resize(StorageUpgradeMk5.StorageWidth, StorageUpgradeMk5.StorageHeight);
            }

            return component.container;
        }
    }
}