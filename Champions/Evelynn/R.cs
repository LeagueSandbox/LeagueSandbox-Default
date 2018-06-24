using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Numerics;
using System;

namespace Spells
{
    class EvelynnR : GameScript
    {
        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            Target ZoneCenter;
            Vector2 ownerLocation = new Vector2(owner.X, owner.Y);
            Vector2 targetLocation = new Vector2(spell.X, spell.Y);
            var spellData = spell.SpellData;
            float distance = Vector2.Distance(ownerLocation, targetLocation);

            if (distance > spellData.CastRange[0])
            {
                var to = Vector2.Normalize(targetLocation - ownerLocation);
                var range = to * 650;
                var trueCoords = ownerLocation + range;

                ZoneCenter = new Target(trueCoords.X, trueCoords.Y);
            }
            else
            {
                ZoneCenter = new Target(spell.X, spell.Y);
            }

            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Evelynn_R_cas.troy", ZoneCenter);
            spell.spellAnimation("SPELL4", owner);

            List<AttackableUnit> units = ApiFunctionManager.GetUnitsInRange(ZoneCenter, 250, true);
            var ap = owner.GetStats().AbilityPower.Total * 0.01f;
            var percentHealthDamage = ((new float[] { 0.15f, 0.20f, 0.25f })[spell.Level]) + ap;
            var damage = 0.0f;
            var monsterDamage = 0.0f;

            foreach (AttackableUnit unit in units)
            {
                if (unit.Team != owner.Team)
                {
                    if (unit is Champion || unit is Minion)
                    {
                        damage = unit.GetStats().CurrentHealth * percentHealthDamage;
                        unit.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                        ((ObjAIBase)unit).AddBuffGameScript("EveAgonySlow", "EveAgonySlow", spell, 2.0f, true);
                    }
                    else if (unit is Monster)
                    {
                        damage = unit.GetStats().CurrentHealth * percentHealthDamage;
                        monsterDamage = Math.Max(damage, 1000);
                        unit.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                        ((ObjAIBase)unit).AddBuffGameScript("EveAgonySlow", "EveAgonySlow", spell, 2.0f, true);
                    }
                }
            }

            //TODO:Give evelynn her shield per champion hit.

            ApiFunctionManager.CreateTimer(2.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
            });
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
