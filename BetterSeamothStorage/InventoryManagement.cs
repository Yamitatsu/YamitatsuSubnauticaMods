namespace BetterSeamothStorage
{
    using System;
    using UnityEngine;
    using Utils;
    using UWE;

    public class InventoryManagement : MonoBehaviour
    {
        public InventoryManagement Instance { get; private set; }
        private Player PlayerMain;
        private Vehicle ThisVehicle;
        private PDA PdaMain;
        internal bool isActive = false;
        private bool isInVehicle = false;

        internal void Awake()
        {
            Instance = GetComponent<InventoryManagement>();
            if (Instance.GetComponent<SeaMoth>())
            {
                ThisVehicle = Instance.GetComponent<SeaMoth>();
            }
            else if (Instance.GetComponent<Exosuit>())
            {
                ThisVehicle = Instance.GetComponent<Exosuit>();
            }
            else
            {
                Destroy(Instance);
            }
        }

        internal void Start()
        {
            PlayerMain = Player.main;
            PdaMain = PlayerMain.GetPDA();
            PdaMain.Open();
            PdaMain.Close();
            PlayerMain.playerModeChanged.AddHandler(this, new Event<Player.Mode>.HandleFunction(OnPlayerModeChanged));
            isActive = PlayerMain.GetVehicle() == ThisVehicle;
        }

        private bool isPlayerInVehicle()
        {
            var test = Player.main.inSeamoth || Player.main.inExosuit;
            if (test != isInVehicle)
            {
                if (test == true)
                {
                    ErrorMessage.AddMessage("isInVehicle is now true");
                }
                else
                {
                    ErrorMessage.AddMessage("isInVehicle is now false");
                }
            }

            isInVehicle = test;
            return test;
        }

        internal void OnPlayerModeChanged(Player.Mode playerMode)
        {
            var getVehName = Player.main != null ? (Player.main.GetVehicle() != null ? Player.main.GetVehicle().GetName() : "Player.main.GetVeh = NULL") : "Player.main = NULL";
            var thisVehName = ThisVehicle != null ? ThisVehicle.GetName() : "NULL";
            ErrorMessage.AddMessage($"Player mode changed to {playerMode.ToString()}\nThisVehicule is {thisVehName}\nGetVehicule is {getVehName}");
            var test = playerMode == Player.Mode.LockedPiloting && Player.main.GetVehicle() == ThisVehicle;
            if (test != isActive)
            {
                if (test == true)
                {
                    ErrorMessage.AddMessage("isActive is now true");
                }
                else
                {
                    ErrorMessage.AddMessage("isActive is now false");
                }
            }

            isActive = test;
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
            PlayerMain.playerModeChanged.RemoveHandler(this, OnPlayerModeChanged);
            Destroy(Instance);
        }
    }
}