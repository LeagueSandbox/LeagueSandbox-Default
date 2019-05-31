using System.Collections.Generic;
using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using GameServerCore;

namespace Spells
{
    public class CannonBarrage : GameScript
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
            Target ZoneCenter2;
            Target ZoneCenter3;
            Target ZoneCenter4;
            Target ZoneCenter5;
            Target ZoneCenter6;
            Target ZoneCenter7;
            Target ZoneCenter8;
            Target ZoneCenter9;
            Target ZoneCenter10;
            Vector2 ownerLocation = new Vector2(owner.X, owner.Y);
            Vector2 targetLocation = new Vector2(spell.X, spell.Y);
            var spellData = spell.SpellData;
            float distance = Vector2.Distance(ownerLocation, targetLocation);
            if (distance > spellData.CastRange[0])
            {
                var to = Vector2.Normalize(targetLocation - ownerLocation);
                var range = to * 30000;
                var trueCoords = ownerLocation + range;
                ZoneCenter = new Target(trueCoords.X, trueCoords.Y); 
                ZoneCenter2 = new Target(trueCoords.X+225, trueCoords.Y+225); 
                ZoneCenter3 = new Target(trueCoords.X-225, trueCoords.Y+225); 
                ZoneCenter4 = new Target(trueCoords.X-125, trueCoords.Y-325); 
                ZoneCenter5 = new Target(trueCoords.X+125, trueCoords.Y-425); 
                ZoneCenter6 = new Target(trueCoords.X-332, trueCoords.Y+43); 
                ZoneCenter7 = new Target(trueCoords.X+335, trueCoords.Y-192); 
                ZoneCenter8 = new Target(trueCoords.X-125, trueCoords.Y+267); 
                ZoneCenter9 = new Target(trueCoords.X+125, trueCoords.Y-372); 
                ZoneCenter10 = new Target(trueCoords.X-355, trueCoords.Y-411); 
            }
            else
            {
                ZoneCenter = new Target(spell.X, spell.Y);
                ZoneCenter2 = new Target(spell.X+225, spell.Y+225); 
                ZoneCenter3 = new Target(spell.X-225, spell.Y+225); 
                ZoneCenter4 = new Target(spell.X-125, spell.Y-325); 
                ZoneCenter5 = new Target(spell.X+125, spell.Y-425); 
                ZoneCenter6 = new Target(spell.X-332, spell.Y+431); 
                ZoneCenter7 = new Target(spell.X+335, spell.Y-192); 
                ZoneCenter8 = new Target(spell.X-125, spell.Y+267); 
                ZoneCenter9 = new Target(spell.X+125, spell.Y-372); 
                ZoneCenter10 = new Target(spell.X-355, spell.Y-411); 
            }
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter3);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter7);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter6);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            Particle p2 = AddParticleTarget(owner,"pirate_cannonBarrage_glow.troy", ZoneCenter);
            Particle p3 = AddParticleTarget(owner,"pirate_cannonBarrage_point.troy", ZoneCenter);
            Particle p4 = AddParticleTarget(owner,"pirate_cannonBarrage_aoe_indicator_red.troy", ZoneCenter);
			ApiFunctionManager.CreateTimer(0.5f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter3);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter4);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(1.0f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter8);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter4);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter9);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(1.5f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter10);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter3);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter7);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(2.0f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter8);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter9);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter4);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter6);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(2.5f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter3);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter4);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(3.0f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter6);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter8);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter10);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(3.5f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter3);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter4);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(4.0f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter10);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter3);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter7);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(4.5f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter8);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter4);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter9);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(5.0f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter3);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter4);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(5.5f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter3);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter7);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter6);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(6.0f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter9);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter3);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter8);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter4);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(6.5f, () =>
            {
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter2);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter8);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter4);
            AddParticleTarget(owner,"pirate_cannonBarrage_tar.troy", ZoneCenter5);
			DamageTargetsInZone(owner, spell, target, ZoneCenter);
            });
            CreateTimer(7.0f, () =>
            {
                RemoveParticle(p2);
                RemoveParticle(p3);
                RemoveParticle(p4);
            });
        }

        public void DamageTargetsInZone(Champion owner, Spell spell, AttackableUnit target, Target ZoneCenter)
        {
            List<AttackableUnit> units = ApiFunctionManager.GetUnitsInRange(ZoneCenter, 525, true);
            var ap = owner.Stats.AbilityPower.Total * 0.2f;
            var damage = ((new float[] { 78.125f, 115.875f, 138.75f })[spell.Level - 1]) + ap;
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
