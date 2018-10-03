using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace HealCheck
{
    internal class HealCheck : IBuffGameScript
    {
        private IBuff _healBuff;

        public void OnActivate(IObjAiBase unit, ISpell ownerSpell)
        {
            _healBuff = ApiFunctionManager.AddBuffHudVisual("SummonerHealCheck", 35.0f, 1, BuffType.COMBAT_DEHANCER, unit);
        }

        public void OnDeactivate(IObjAiBase unit)
        {
            ApiFunctionManager.RemoveBuffHudVisual(_healBuff);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
