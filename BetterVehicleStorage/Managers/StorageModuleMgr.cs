namespace BetterVehicleStorage.Managers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using Items;
    using UnityEngine;
    using Utilities;

    public static class StorageModuleMgr
    {
        public static readonly IDictionary<TechType, StorageModule> Modules = new Dictionary<TechType, StorageModule>();

        public static readonly IList<string> NeedMappingMethods = new List<string>()
        {
            "OnHandClick",
            "OpenFromExternal",
            "OpenPDA"
        };

        public static readonly IList<string> NeedFakeTechTypeMethods = new List<string>()
        {
            "RebuildVehicleScreen"
        };

        public static void RegisterModules()
        {
            var storageModuleMk2TechType = RegisterModule(new StorageModuleMk2());
            var storageModuleMk3TechType = RegisterModule(new StorageModuleMk3(storageModuleMk2TechType));
            var storageModuleMk4TechType = RegisterModule(new StorageModuleMk4(storageModuleMk3TechType));
            RegisterModule(new StorageModuleMk5(storageModuleMk4TechType));
        }

        private static TechType RegisterModule(StorageModule storageModule)
        {
            storageModule.Patch();
            Modules.Add(storageModule.TechType, storageModule);
            return storageModule.TechType;
        }

        public static bool IsStorageModule(TechType techType)
        {
            return techType == TechType.VehicleStorageModule || Modules.ContainsKey(techType);
        }

        public static bool IsTorpedoModule(TechType techType)
        {
            return techType == TechType.ExosuitTorpedoArmModule || techType == TechType.SeamothTorpedoModule;
        }

        public static void UpdateSeamothStorage(ref SeaMoth seaMoth, int slotId, TechType techType, bool added)
        {
            if (!IsStorageModule(techType)) return;
            var physicalStorageModuleAmount = CalculateStorageModuleAmount(seaMoth.modules);
            for (int i = 0; i < 4; i++)
            {
                //Storage activation
                var enabled = i < physicalStorageModuleAmount;
                seaMoth.storageInputs[i].SetEnabled(enabled);
            }
        }

        public static void UpdateExosuitStorage(ref Exosuit exosuit, int slotId, TechType techType, bool added)
        {
            if (!IsStorageModule(techType)) return;
            UpdateExosuitStorageSize(ref exosuit);
        }

        public static void UpdateExosuitStorageSize(ref Exosuit exosuit)
        {
            var allModules = exosuit.GetSlotBinding();
            TechType storageTechType = TechType.None;
            for (int i = 0; i < allModules.Length; i++)
            {
                if (!IsStorageModule(allModules[i])) continue;
                storageTechType = allModules[i];
                break;
            }

            if (storageTechType == TechType.None)
            {
                exosuit.storageContainer.Resize(4, 4);
                return;
            }

            ;
            var module = Modules.ContainsKey(storageTechType) ? Modules[storageTechType] : null;
            exosuit.storageContainer.Resize(module?.StorageWidth ?? 6, module?.StorageHeight ?? 4);
        }

        internal static ItemsContainer GetStorageInSlot(ref Vehicle vehicle, int slotId, TechType techType,
            string callingMethodName)
        {
            var allModules = vehicle.GetSlotBinding();
            int mappedStorageSlot = slotId;
            if (NeedMappingMethods.Contains(callingMethodName))
            {
                List<int> allStorageModules = new List<int>();
                for (int i = 0; i < allModules.Length; i++)
                {
                    if (!IsStorageModule(allModules[i])) continue;
                    allStorageModules.Add(i);
                }

                mappedStorageSlot = allStorageModules[slotId];
            }

            InventoryItem inventoryItem = vehicle.GetSlotItem(mappedStorageSlot);
            return GetItemsContainerFromIventoryItem(inventoryItem, allModules[mappedStorageSlot]);
        }

        public static void fixOnResize(ref uGUI_ItemsContainer itemsContainer, int width, int height)
        {
            if (height == 10)
                itemsContainer.rectTransform.anchoredPosition =
                    new Vector2(itemsContainer.rectTransform.anchoredPosition.x, -55f);
        }

        private static ItemsContainer GetItemsContainerFromIventoryItem(InventoryItem inventoryItem, TechType techType)
        {
            if (inventoryItem == null) return null;
            var module = Modules.ContainsKey(techType) ? Modules[techType] : null;
            Pickupable pickupable = inventoryItem.item;
            SeamothStorageContainer storageContainer = pickupable.GetComponent<SeamothStorageContainer>();
            if (storageContainer == null) return null;
            ItemsContainer itemsContainer = storageContainer.container;
            itemsContainer.Resize(module?.StorageWidth ?? 4, module?.StorageHeight ?? 4);
            return itemsContainer;
        }

        internal static bool IsAllowedToRemove(SeaMoth seaMoth, Pickupable pickupable, bool verbose)
        {
            if (!IsStorageModule(pickupable.GetTechType())) return true;
            SeamothStorageContainer component = pickupable.GetComponent<SeamothStorageContainer>();
            if (component == null) return true;
            var flag = component.container.count == 0;
            if (verbose && !flag)
            {
                ErrorMessage.AddDebug(Language.main.Get("SeamothStorageNotEmpty"));
            }

            return flag;
        }

        internal static bool IsAllowedToRemoveFromExosuit(Exosuit exosuit, Pickupable pickupable, bool verbose)
        {
            if (!IsStorageModule(pickupable.GetTechType())) return true;
            var flag = exosuit.storageContainer.container.count == 0;
            if (verbose && !flag)
            {
                ErrorMessage.AddDebug("Storage must be empty in order to upgrade it.");
            }

            return flag;
        }

        internal static bool AllowedToAdd(Equipment equipment, string slot, Pickupable pickupable, bool verbose,
            ref bool __result)
        {
            var isSeaMoth = equipment.owner.GetComponent<SeaMoth>() != null;
            var isExosuit = equipment.owner.GetComponent<Exosuit>() != null;
            if (!IsStorageModule(pickupable.GetTechType())) return true;
            if (isSeaMoth && CalculateStorageModuleAmount(equipment) < 4) return true;
            if (isExosuit && CalculateStorageModuleAmount(equipment) == 0) return true;
            __result = false;
            if (verbose && isSeaMoth)
            {
                ErrorMessage.AddError(
                    "You can only equip up to 4 storage modules.");
            }
            else if (verbose && isExosuit)
            {
                ErrorMessage.AddError(
                    "You can only equip 1 storage module.");
            }

            return false;
        }

        internal static bool GetTechTypeInSlot(Equipment equipment, string slot, ref TechType __result,
            string callingMethodName)
        {
            if (!NeedFakeTechTypeMethods.Contains(callingMethodName)) return true;
            var isSeaMoth = equipment.owner.GetComponent<SeaMoth>() != null;
            if (!isSeaMoth) return true;
            var allModules = equipment.GetEquipment();
            List<string> slotNames = new List<string>()
            {
                "SeamothModule1",
                "SeamothModule2",
                "SeamothModule3",
                "SeamothModule4"
            };
            List<string> allStorageModules = new List<string>();
            while (allModules.MoveNext())
            {
                var module = allModules.Current;
                if (module.Value == null || module.Value.item == null ||
                    !IsStorageModule(module.Value.item.GetTechType())) continue;
                allStorageModules.Add(module.Key);
            }

            if (slotNames.IndexOf(slot) >= allStorageModules.Count)
            {
                __result = TechType.None;
                return false;
            }

            string mappedStorageSlot = allStorageModules[slotNames.IndexOf(slot)];
            InventoryItem itemInSlot = equipment.GetItemInSlot(mappedStorageSlot);
            if (itemInSlot == null || itemInSlot.item == null)
            {
                __result = TechType.None;
                return false;
            }

            Pickupable item = itemInSlot.item;
            if (IsStorageModule(item.GetTechType()))
            {
                __result = TechType.VehicleStorageModule;
                return false;
            }

            __result = TechType.None;
            return false;
        }

        internal static void OnUpgradeModuleUse(SeaMoth seaMoth, TechType techType, int slotId)
        {
            if (!IsStorageModule(techType)) return;
            var slotItem = seaMoth.GetSlotItem(slotId);
            var itemsContainer = GetItemsContainerFromIventoryItem(slotItem, techType);
            PDA pda = Player.main.GetPDA();
            Inventory.main.SetUsedStorage(itemsContainer);
            pda.Open(PDATab.Inventory);
        }

        internal static void OnUpgradeModuleUseFromExosuit(Exosuit exosuit, TechType techType, int slotId)
        {
            if (!IsStorageModule(techType)) return;
            var slotItem = exosuit.GetSlotItem(slotId);
            var itemsContainer = exosuit.storageContainer.container;
            PDA pda = Player.main.GetPDA();
            Inventory.main.SetUsedStorage(itemsContainer);
            pda.Open(PDATab.Inventory);
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