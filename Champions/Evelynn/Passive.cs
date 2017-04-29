using LeagueSandbox.GameServer.Logic.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class EvelynnPassive : IGameScript
    {

        public void OnActivate(GameScriptInformation gameScriptInformation)
        {
            ApiEventManager.OnUnitDamageTaken.AddListener(this, gameScriptInformation.OwnerUnit, selfWasDamaged);
        }

        public void OnDeactivate()
        {
        }

        private void selfWasDamaged()
        {

        }
    }
}