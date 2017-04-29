using System;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class CaitlynPiltoverPeacemaker : IGameScript
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
            ApiEventManager.OnSpellFinishCast.AddListener(this, spell, OnFinishCasting);
            ApiEventManager.OnSpellApplyEffects.AddListener(this, spell, ApplyEffects);
        }
        public void OnDeactivate() { }
        public void OnFinishCasting(Unit target) {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1150;
            var trueCoords = current + range;

            spell.AddProjectile("CaitlynPiltoverPeacemaker", trueCoords.X, trueCoords.Y, true);
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
            var reduc = Math.Min(projectile.ObjectsHit.Count, 5);
            var baseDamage = new[] {20, 60, 100, 140, 180}[spell.Level - 1] + 1.3f * owner.GetStats().AttackDamage.Total;
            var damage = baseDamage * (1 - reduc / 10);
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }
     }
}