using System;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Ezreal
{
    public class R : IGameScript
    {
        GameScriptInformation info;
        Spell spell;
        Unit owner;
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            info = scriptInfo;
            spell = info.OwnerSpell;
            owner = info.OwnerUnit;
            //Setup event listeners
            ApiEventManager.OnSpellCast.AddListener(this, spell, OnStartCasting);
            ApiEventManager.OnSpellFinishCast.AddListener(this, spell, OnFinishCasting);
            ApiEventManager.OnSpellApplyEffects.AddListener(this, spell, ApplyEffects);
        }
        public void OnDeactivate() { }
        public void OnStartCasting(Unit target){
            if (owner is Champion) ApiFunctionManager.AddParticleTarget(owner as Champion, "Ezreal_bow_huge.troy", owner, 1, "L_HAND");
        }
        public void OnFinishCasting(Unit target) {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 20000;
            var trueCoords = current + range;

            spell.AddProjectile("EzrealTrueshotBarrage", trueCoords.X, trueCoords.Y, true);
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
            var reduc = Math.Min(projectile.ObjectsHit.Count, 7);
            var bonusAd = owner.GetStats().AttackDamage.Total - owner.GetStats().AttackDamage.BaseValue;
            var ap = owner.GetStats().AbilityPower.Total * 0.9f;
            var damage = 200 + spell.Level * 150 + bonusAd + ap;
            owner.DealDamageTo(target, damage * (1 - reduc / 10), DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }
     }
}