using UnityEngine;
using TheForest.Utils;
using SOTF_ModMenu.Utilities;

namespace SOTF_ModMenu
{
    public class Main
    {
        public class MyMonoBehaviour : MonoBehaviour
        {
            private void Start()
            {
                _isvitalsNull = vitals == null;
            }

            private void OnGUI()
            {
                UIHelper.Begin("Vitals", 10, 10, 150, 200, 2, 20, 2);

                if (UIHelper.Button("Max Health: ", Settings.Health))
                    Settings.Health = !Settings.Health;
                if (UIHelper.Button("Max Stamina: ", Settings.Stamina))
                    Settings.Stamina = !Settings.Stamina;
                if (UIHelper.Button("Max Vitality: ", Settings.Vitality))
                    Settings.Vitality = !Settings.Vitality;
                if (UIHelper.Button("Max Strength: ", Settings.Strength))
                    Settings.Strength = !Settings.Strength;
                if (UIHelper.Button("No Cold: ", Settings.Cold))
                    Settings.Cold = !Settings.Cold;
                if (UIHelper.Button("No Hunger: ", Settings.Hunger))
                    Settings.Hunger = !Settings.Hunger;
                if (UIHelper.Button("No Thirst: ", Settings.Thirst))
                    Settings.Thirst = !Settings.Thirst;
                if (UIHelper.Button("Always Rested: ", Settings.Rested))
                    Settings.Rested = !Settings.Rested;

                //UIHelper.Begin("Player", 165, 10, 150, 200, 2, 20, 2);

            }

            private void Update()
            {
                if(_isvitalsNull)
                {
                    vitals = FindObjectOfType<Vitals>();
                }

                if (!LocalPlayer.IsInWorld) return;
                
                //Vitals
                if (Settings.Health) {
                    vitals._health._currentValue = vitals._health._max;
                    LocalPlayer.FpCharacter.allowFallDamage = false;
                }
                if (Settings.Stamina)
                    vitals._stamina._currentValue = vitals._stamina._max;
                if (Settings.Vitality)
                    vitals._vitality._currentValue = vitals._vitality._max;
                if (Settings.Cold)
                    vitals._isCold = false;
                if (Settings.Hunger)
                    vitals._fullness._currentValue = vitals._fullness._max;
                if (Settings.Thirst)
                    vitals._hydration._currentValue = vitals._hydration._max;
                if (Settings.Rested)
                    vitals._rested._currentValue = vitals._rested._max;
                if (Settings.Strength)
                    vitals._strength._currentValue = vitals._strength._max;
            }
            
            private Vitals vitals;
            private bool _isvitalsNull;
        }
    }
}
