using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System;
using GameServerCore;

namespace Spells
{
    public class GarenE : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var p = AddParticleTarget(owner, "Garen_Base_E_Spin.troy", owner, 1);
            var visualBuff = AddBuffHudVisual("GarenE", 3.0f, 1,
                BuffType.COMBAT_ENCHANCER, owner, 3.0f);
            CreateTimer(3.0f, () =>
            {
                RemoveParticle(p);
            });
            for (var i = 0.0f; i < 3.0; i += 0.5f)
            {
                CreateTimer(i, () => { ApplySpinDamage(owner, spell, target); });
            }
        }

        private void ApplySpinDamage(Champion owner, Spell spell, AttackableUnit target)
        {
            var units = GetUnitsInRange(owner, 500, true);
            var isCrit = new Random().Next(0, 100) < (owner.Stats.CriticalChance.Total * 100);
            var bonusCritDamage = 1.0f;
            if(isCrit)
            {
                bonusCritDamage = (owner.Stats.CriticalDamage.Total - 0.5f);
            }
            foreach (var unit in units)
            {
                if (unit.Team != owner.Team)
                {
                    //PHYSICAL DAMAGE PER SECOND: 20 / 45 / 70 / 95 / 120 (+ 70 / 80 / 90 / 100 / 110% AD)
                    var ad = new[] {.7f, .8f, .9f, 1f, 1.1f}[spell.Level - 1] * owner.Stats.AttackDamage.Total *
                               0.5f * bonusCritDamage;
                    var damage = new[] {20, 45, 70, 95, 120}[spell.Level - 1] * 0.5f + ad;
                    if (unit is Minion) damage *= 0.75f;
                    var finalDamage = new Damage(damage, DamageType.DAMAGE_TYPE_PHYSICAL, 
                        DamageSource.DAMAGE_SOURCE_SPELL, isCrit);
                    unit.TakeDamage(owner, finalDamage);
                }
            }
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}

