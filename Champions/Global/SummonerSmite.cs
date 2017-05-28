using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Spells
{
    public class SummonerSmite : IGameScript
    {
        Unit owner;
        public void OnActivate(GameScriptInformation gameScriptInformation)
        {
            owner = gameScriptInformation.OwnerUnit;
            ApiEventManager.OnSpellCast.AddListener(this, gameScriptInformation.OwnerSpell, OnStartCasting);
        }

        public void OnDeactivate()
        {
        }
        public void OnStartCasting(Unit target)
        {
            ApiFunctionManager.AddParticleTarget((owner as Champion), "Global_SS_Smite.troy", target, 1);
            var damage = 370 + owner.GetStats().Level * 20;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }
    }
}
