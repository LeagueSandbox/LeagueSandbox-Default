using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Spells
{
    public class EzrealRisingSpellForce : IGameScript
    {
        public void OnActivate(GameScriptInformation gameScriptInformation)
        {
            ApiFunctionManager.LogInfo("Ezreal Passive OnActivate");
            ApiEventManager.OnUnitDamageTaken.AddListener(this, gameScriptInformation.OwnerUnit, selfWasDamaged);
        }

        private void selfWasDamaged()
        {
            ApiFunctionManager.LogInfo("Ezreal was damaged");
        }
        public void OnDeactivate() { }
    }
}