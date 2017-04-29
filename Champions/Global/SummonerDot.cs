using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Spells
{
    public class SummonerDot : IGameScript
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
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerDot", 5.0f, 1, target);
            Particle p = ApiFunctionManager.AddParticleTarget((owner as Champion), "Global_SS_Ignite.troy", target, 1);
            var damage = 10 + owner.GetStats().Level * 4;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
            ApiFunctionManager.CreateTimer(1.0f, () =>
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
            });
            ApiFunctionManager.CreateTimer(2.0f, () =>
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
            });
            ApiFunctionManager.CreateTimer(3.0f, () =>
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
            });
            ApiFunctionManager.CreateTimer(4.0f, () =>
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
            });
        }
    }
}
