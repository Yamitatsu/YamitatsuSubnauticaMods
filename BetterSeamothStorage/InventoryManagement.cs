namespace BetterSeamothStorage
{
    using System;
    using UnityEngine;
    using Utils;
    using UWE;

    public class InventoryManagement : MonoBehaviour
    {
        public InventoryManagement Instance { get; private set; }
        public Vehicle ThisVehicle;
        private PDA PdaMain;
        private bool isActive; 

        internal void Awake()
        {
            Console.WriteLine($"[BetterSeamothStorage:DEBUG] InventoryManagement Awakening.");
            Instance = GetComponent<InventoryManagement>();
            Console.WriteLine(
                $"[BetterSeamothStorage:DEBUG] Instance is now initialized with type {Instance.GetType().Name}.");
            Console.WriteLine(
                $"[BetterSeamothStorage:DEBUG] Instance has {Instance.gameObject.GetFullName()} for GameObject.");
            if (Instance.GetComponent<SeaMoth>())
            {
                ThisVehicle = Instance.GetComponent<SeaMoth>();
                Console.WriteLine(
                    $"[BetterSeamothStorage:DEBUG] We have a Seamoth here, its type is {ThisVehicle.GetType().Name}.");
            }
            else if (Instance.GetComponent<Exosuit>())
            {
                ThisVehicle = Instance.GetComponent<Exosuit>();
                Console.WriteLine(
                    $"[BetterSeamothStorage:DEBUG] We have a Prawn Suit here, its type is {ThisVehicle.GetType().Name}.");
            }
            else
            {
                Console.WriteLine($"[BetterSeamothStorage:DEBUG] We have nothing here, Instance will be destroyed.");
                Destroy(Instance);
            }
        }

        internal void Start()
        {
            Console.WriteLine($"[BetterSeamothStorage:DEBUG] Instance has just Started.");
            Player.main.playerModeChanged.AddHandler(this, new Event<Player.Mode>.HandleFunction(OnPlayerModeChanged));
            PdaMain = Player.main.GetPDA();
            PdaMain.Open();
            PdaMain.Close();
            isActive = Player.main.GetVehicle() == ThisVehicle;
        }

        private bool isPlayerInVehicle()
        {
            return Player.main.inSeamoth || Player.main.inExosuit;
        }

        internal void OnPlayerModeChanged(Player.Mode playerMode)
        {
            ErrorMessage.AddMessage($"[BetterSeamothStorage:DEBUG] Player.main.GetVehicle = {( Player.main.GetVehicle() == null ? "Null" : Player.main.GetVehicle().GetName())}.");
            ErrorMessage.AddMessage($"[BetterSeamothStorage:DEBUG] Player.main.currentMountedVehicle = {( Player.main.currentMountedVehicle == null ? "Null" : Player.main.currentMountedVehicle.GetName())}.");
            isActive = playerMode == Player.Mode.LockedPiloting && Player.main.GetVehicle() == ThisVehicle;
        }

        internal void Update()
        {
            if (!isActive)
                return;
            if (!isPlayerInVehicle())
                return;
            if (GameInput.GetButtonDown(GameInput.Button.Slot1))
            {
                ErrorMessage.AddMessage("Button 1 !");
                if (ThisVehicle.GetType() == typeof(SeaMoth))
                {
                    ErrorMessage.AddMessage("Button 1 - Seamoth !");
                    if (!VehicleMgr.StorageTechTypes.Contains(ThisVehicle.GetSlotBinding(0)))
                        return;
                    if (PdaMain.isOpen)
                    {
                        PdaMain.Close();
                        return;
                    }

                    ErrorMessage.AddMessage($"Opening for Seamoth {ThisVehicle.GetSlotBinding(0).ToString()}");
                    ThisVehicle.GetComponent<SeaMoth>().storageInputs[0].OpenFromExternal();
                    return;
                }
                else if (ThisVehicle.GetType() == typeof(Exosuit))
                {
                    ErrorMessage.AddMessage("Button 1 - Exosuit !");
                    if (!VehicleMgr.StorageTechTypes.Contains(ThisVehicle.GetSlotBinding(0)))
                        return;
                    if (PdaMain.isOpen)
                    {
                        PdaMain.Close();
                        return;
                    }

                    ErrorMessage.AddMessage($"Opening for Exosuit {ThisVehicle.GetSlotBinding(0).ToString()}");
                    ThisVehicle.GetComponent<Exosuit>().storageContainer.Open();
                }
            }

            if (GameInput.GetButtonDown(GameInput.Button.Slot2))
            {
                if (ThisVehicle.GetType() == typeof(SeaMoth))
                {
                    if (!VehicleMgr.StorageTechTypes.Contains(ThisVehicle.GetSlotBinding(1)))
                        return;
                    if (PdaMain.isOpen)
                    {
                        PdaMain.Close();
                        return;
                    }

                    ThisVehicle.GetComponent<SeaMoth>().storageInputs[1].OpenFromExternal();
                    return;
                }
                else if (ThisVehicle.GetType() == typeof(Exosuit))
                {
                    if (!VehicleMgr.StorageTechTypes.Contains(ThisVehicle.GetSlotBinding(1)))
                        return;
                    if (PdaMain.isOpen)
                    {
                        PdaMain.Close();
                        return;
                    }

                    ThisVehicle.GetComponent<Exosuit>().storageContainer.Open();
                }
            }

            if (GameInput.GetButtonDown(GameInput.Button.Slot3))
            {
                if (ThisVehicle.GetType() == typeof(SeaMoth))
                {
                    if (!VehicleMgr.StorageTechTypes.Contains(ThisVehicle.GetSlotBinding(2)))
                        return;
                    if (PdaMain.isOpen)
                    {
                        PdaMain.Close();
                        return;
                    }

                    ThisVehicle.GetComponent<SeaMoth>().storageInputs[2].OpenFromExternal();
                    return;
                }
                else if (ThisVehicle.GetType() == typeof(Exosuit))
                {
                    if (!VehicleMgr.StorageTechTypes.Contains(ThisVehicle.GetSlotBinding(2)))
                        return;
                    if (PdaMain.isOpen)
                    {
                        PdaMain.Close();
                        return;
                    }

                    ThisVehicle.GetComponent<Exosuit>().storageContainer.Open();
                }
            }

            if (GameInput.GetButtonDown(GameInput.Button.Slot4))
            {
                if (ThisVehicle.GetType() == typeof(SeaMoth))
                {
                    if (!VehicleMgr.StorageTechTypes.Contains(ThisVehicle.GetSlotBinding(3)))
                        return;
                    if (PdaMain.isOpen)
                    {
                        PdaMain.Close();
                        return;
                    }

                    ThisVehicle.GetComponent<SeaMoth>().storageInputs[3].OpenFromExternal();
                    return;
                }
                else if (ThisVehicle.GetType() == typeof(Exosuit))
                {
                    if (!VehicleMgr.StorageTechTypes.Contains(ThisVehicle.GetSlotBinding(3)))
                        return;
                    if (PdaMain.isOpen)
                    {
                        PdaMain.Close();
                        return;
                    }

                    ThisVehicle.GetComponent<Exosuit>().storageContainer.Open();
                }
            }
        }

        public void OnDestroy()
        {
            Player.main.playerModeChanged.RemoveHandler(this, OnPlayerModeChanged);
            Destroy(Instance);
        }
    }
}