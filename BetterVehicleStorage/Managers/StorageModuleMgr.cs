namespace BetterVehicleStorage.Managers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using Items;
    using Utilities;

    public static class StorageModuleMgr
    {
        public static readonly IDictionary<TechType, StorageModule> Modules = new Dictionary<TechType, StorageModule>();

        public static void RegisterModules()
        {
            var storageModuleMk2TechType = RegisterModule(new StorageModuleMk2());
            var storageModuleMk3TechType = RegisterModule(new StorageModuleMk3(storageModuleMk2TechType));
            var storageModuleMk4TechType = RegisterModule(new StorageModuleMk4(storageModuleMk3TechType));
            var storageModuleMk5TechType = RegisterModule(new StorageModuleMk5(storageModuleMk4TechType));
            RegisterModule(new StorageModuleMk6(storageModuleMk5TechType));
        }

        private static TechType RegisterModule(StorageModule storageModule)
        {
            storageModule.Patch();
            Modules.Add(storageModule.TechType, storageModule);
            return storageModule.TechType;
        }

        private static bool isStorageModule(TechType techType)
        {
            return techType == TechType.VehicleStorageModule || Modules.ContainsKey(techType);
        }

        private static bool isTardisModule(TechType techType)
        {
            return Modules.ContainsKey(techType) && Modules[techType].OpenAllSlots;
        }

        public static void UpdateSeamothStorage(ref SeaMoth seaMoth, int slotId, TechType techType, bool added)
        {
            YoLog.Debug("****************************************************************");
            YoLog.Debug("UpdateSeamothStorage:Start");
            YoLog.Debug("****************************************************************");
            if (!isStorageModule(techType))
            {
                YoLog.Debug("Not a storage module.");
                return;
            }

            var isTardis = isTardisModule(techType);
            var moduleAdded = techType != TechType.VehicleStorageModule ? Modules[techType] : null;
            var physicalStorageModuleAmount = CalculateStorageModuleAmount(seaMoth.modules);
            YoLog.Debug($"added = {added.ToString()}");
            YoLog.Debug($"SlotId = {slotId.ToString()}");
            YoLog.Debug($"isTardis = {isTardis.ToString()}");
            YoLog.Debug($"physicalStorageModuleAmount = {physicalStorageModuleAmount.ToString()}");
            for (int i = 0; i < 4; i++)
            {
                YoLog.Debug($"Processing slot {i} activation...");
                //Storage activation
                var enabled = i < physicalStorageModuleAmount || (isTardis && added);
                seaMoth.storageInputs[i].SetEnabled(enabled);
                YoLog.Debug($"... slot {i} {(enabled ? "enabled" : "disabled")}.");
                YoLog.Debug($"Processing slot {i} resizing...");
                //Storage resizing
                if (added && enabled)
                {
                    ItemsContainer itemsContainer = seaMoth.GetStorageInSlot(i, techType);
                    if (itemsContainer == null)
                    {
                        YoLog.Debug($"... slot {i} itemContainer is null. Aborted.");
                        continue;
                    }

                    itemsContainer.Resize(moduleAdded?.StorageWidth ?? 4, moduleAdded?.StorageHeight ?? 4);
                    YoLog.Debug(
                        $"... slot {i} resized to ({moduleAdded?.StorageWidth ?? 4};{moduleAdded?.StorageHeight ?? 4}).");
                }

                YoLog.Debug("UpdateSeamothStorage:End");
                YoLog.Debug("****************************************************************");
            }
        }

        internal static ItemsContainer GetStorageInSlot(Vehicle vehicle, int slotId)
        {
            YoLog.Debug("GetStorageInSlot:Start");
            YoLog.Debug("****************************************************************");
            YoLog.Debug("Mapping module slots with storage position.");
            var allModules = vehicle.GetSlotBinding();
            List<int> allStorageModules = new List<int>();
            StorageModule tardisModule = null;
            int tardisModuleSlot = -1;
            for (int i = 0; i < allModules.Length; i++)
            {
                YoLog.Debug($"Mapping module slot {i.ToString()}.");
                if (!isStorageModule(allModules[i])) continue;
                YoLog.Debug($"Slot {i.ToString()} is a storage module.");
                allStorageModules.Add(i);
                YoLog.Debug($"Slot {i.ToString()} is mapped to {allStorageModules.Count - 1}.");
                if (!isTardisModule(allModules[i])) continue;
                tardisModule = Modules[allModules[i]];
                tardisModuleSlot = allStorageModules.Count - 1;
                break;
            }
            YoLog.Debug("Mapping module done with :");
            for (int i = 0; i < allStorageModules.Count; i++)
            {
                YoLog.Debug($"[{i}] = {allStorageModules[i]}");
            }
            int mappedStorageSlot = allStorageModules.IndexOf(slotId);
            YoLog.Debug($"Mapped storage module slot is {mappedStorageSlot.ToString()}.");
            if (tardisModule != null)
            {
                YoLog.Debug($"Initializing Tardis Module...");
                InventoryItem tardisInventoryItem = vehicle.GetSlotItem(tardisModuleSlot);
                if (tardisInventoryItem != null)
                {
                    Pickupable tardisPickupable = tardisInventoryItem.item;
                    SeamothStorageMultipleContainers seamothStorageMultipleContainers =
                        tardisPickupable.GetComponent<SeamothStorageMultipleContainers>();
                    if (seamothStorageMultipleContainers == null)
                    {
                        seamothStorageMultipleContainers =
                            tardisPickupable.gameObject.AddComponent<SeamothStorageMultipleContainers>();
                    }

                    return seamothStorageMultipleContainers == null || mappedStorageSlot == -1
                        ? null
                        : seamothStorageMultipleContainers.Containers[mappedStorageSlot];
                }

                YoLog.Debug("GetStorageInSlot:End");
                YoLog.Debug("****************************************************************");
                return null;
            }

            InventoryItem storageInventoryItem = vehicle.GetSlotItem(slotId);
            if (storageInventoryItem == null) return null;
            Pickupable storagePickupable = storageInventoryItem.item;
            SeamothStorageContainer seamothStorageContainer =
                storagePickupable.gameObject.GetComponent<SeamothStorageContainer>();
            YoLog.Debug("GetStorageInSlot:End");
            YoLog.Debug("****************************************************************");
            return seamothStorageContainer == null ? null : seamothStorageContainer.container;
        }

        internal static bool IsAllowedToRemove(SeaMoth seaMoth, Pickupable pickupable, bool verbose)
        {
            if (pickupable.GetTechType() != TechType.VehicleStorageModule &&
                !Modules.ContainsKey(pickupable.GetTechType())) return true;
            SeamothStorageContainer component = pickupable.GetComponent<SeamothStorageContainer>();
            if (component == null) return true;
            var flag = component.container.count == 0;
            if (verbose && !flag)
            {
                ErrorMessage.AddDebug(Language.main.Get("SeamothStorageNotEmpty"));
            }

            return flag;
        }

        internal static bool AllowedToAdd(Equipment equipment, string slot, Pickupable pickupable, bool verbose,
            ref bool __result)
        {
            var moduleToAdd = Modules.ContainsKey(pickupable.GetTechType()) ? Modules[pickupable.GetTechType()] : null;
            if (moduleToAdd == null || !moduleToAdd.OpenAllSlots) return true;
            if (CalculateStorageModuleAmount(equipment) <= 0) return true;
            __result = false;
            ErrorMessage.AddError(
                "The Tardis Module is not compatible with other storage modules.\nPlease, remove all storage modules before adding this one.");
            return false;
        }

        private static int CalculateStorageModuleAmount(Equipment equipment)
        {
            var amount = equipment.GetCount(TechType.VehicleStorageModule);
            foreach (var module in Modules.Keys)
            {
                amount += equipment.GetCount(module);
            }

            return amount;
        }
    }
}