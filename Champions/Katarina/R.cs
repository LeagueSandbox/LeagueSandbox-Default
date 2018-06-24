using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.API;
using System.Linq;
using LeagueSandbox.GameServer;
using System.Collections.Generic;

namespace Spells
{
    public class KatarinaR : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {

        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {

            spell.spellAnimation("Spell4", owner);

            ApiFunctionManager.AddParticle(owner, "katarina_deathLotus_mis.troy", owner.X, owner.Y);
            ApiFunctionManager.AddParticle(owner, "Katarina_deathLotus_cas.troy", owner.X, owner.Y);

            foreach (var enemyTarget in ApiFunctionManager.GetUnitsInRange(target, 550, true))
            {
                if (enemyTarget != owner && owner.GetDistanceTo(enemyTarget) < 550 && !ApiFunctionManager.UnitIsTurret(enemyTarget) && ApiFunctionManager.UnitIsChampion(enemyTarget))
                {
                    ApiFunctionManager.AddParticle(owner, "katarina_deathlotus_success.troy", owner.X, owner.Y);
                    ApiFunctionManager.AddParticle(owner, "katarina_deathLotus_tar.troy", enemyTarget.X, enemyTarget.Y);
                    ApiFunctionManager.CreateTimer(0.25f, () =>
                    {
                        ApiFunctionManager.AddParticle(owner, "katarina_deathlotus_success.troy", owner.X, owner.Y);
                        ApiFunctionManager.AddParticle(owner, "katarina_deathLotus_tar.troy", enemyTarget.X, enemyTarget.Y);
                    });
                }
            }

            for (float i = 0.0f; i < 2.50; i += 0.25f)
            {
                ApiFunctionManager.CreateTimer(i, () =>
                {
                    ApplyDamage(owner, spell, target);
                });
            }

        }

        private void ApplyDamage(Champion owner, Spell spell, AttackableUnit target)
        {
            var damagePerDagger = new[] { 35, 55, 75 }[spell.Level - 1] + (owner.GetStats().AbilityPower.Total * 0.25f) + (owner.GetStats().AttackDamage.Total * 0.375f);

            List<AttackableUnit> units = ApiFunctionManager.GetUnitsInRange(owner, 550, true);

            foreach (var enemyTarget in units)
            {
                if (enemyTarget != owner && owner.GetDistanceTo(enemyTarget) < 550 && !ApiFunctionManager.UnitIsTurret(enemyTarget) && ApiFunctionManager.UnitIsChampion(enemyTarget))
                {
                    enemyTarget.TakeDamage(owner, damagePerDagger, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
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

