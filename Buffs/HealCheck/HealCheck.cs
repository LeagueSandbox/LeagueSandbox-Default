using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace HealCheck
{
    internal class HealCheck : IBuffGameScript
    {
        private Buff _healBuff;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _healBuff = ApiFunctionManager.AddBuffHudVisual("SummonerHealCheck", 35.0f, 1, unit);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            ApiFunctionManager.RemoveBuffHudVisual(_healBuff);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
