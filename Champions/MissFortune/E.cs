using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Numerics;
using LeagueSandbox.GameServer.Logic;

namespace Spells
{
    public class MissFortuneScattershot : GameScript
    {

        public void OnActivate(Champion owner)
        {

        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
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
                var range = to * 800;
                var trueCoords = ownerLocation + range;
                ZoneCenter = new Target(trueCoords.X, trueCoords.Y); 
            }
            else
            {
                ZoneCenter = new Target(spell.X, spell.Y);
            }
            Particle p = ApiFunctionManager.AddParticleTarget(owner,"missFortune_makeItRain_incoming.troy", ZoneCenter);
            for (byte i = 0; i < 8; ++i)
            {
                ApiFunctionManager.CreateTimer(0.25f * i, () =>
                {
                    DamageTargetsInZone(owner, spell, target, ZoneCenter);
                });
            }
            ApiFunctionManager.CreateTimer(2.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
            });
        }

        public void DamageTargetsInZone(Champion owner, Spell spell, AttackableUnit target, Target ZoneCenter)
        {
            List<AttackableUnit> units = ApiFunctionManager.GetUnitsInRange(ZoneCenter, 500, true);
            var ap = owner.GetStats().AbilityPower.Total * 0.1f;
            var damage = ((new float[] { 11.25f, 18.125f, 25f, 31.875f, 38.75f })[spell.Level - 1]) + ap;
            foreach (AttackableUnit unit in units)
            {
                if (unit.Team != owner.Team)
                {
                    if (unit is Champion || unit is Minion || unit is Monster)
                    {
                        unit.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    }
                }
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
