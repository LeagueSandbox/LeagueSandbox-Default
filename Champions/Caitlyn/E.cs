﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Numerics;

namespace Spells
{
    public class CaitlynEntrapment : GameScript
    {
        public void OnActivate(Champion owner) { }

        public void OnDeactivate(Champion owner) { }

        public void OnStartCasting(Champion owner, Spell spell, Unit target) {
            spell.spellAnimation("Spell3", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            // Calculate net coords
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 750;
            var trueCoords = current + range;

            // Calculate dash coords/vector
            var dash = Vector2.Negate(to) * 500;
            var dashCoords = current + dash;
            ApiFunctionManager.DashToLocation(owner, dashCoords.X, dashCoords.Y, 1000, true, "Spell3b");
            spell.AddProjectile("CaitlynEntrapmentMissile", trueCoords.X, trueCoords.Y);
            spell.spellAnimation("Spell3_fallback", owner);
        }

        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {
            var ap = owner.GetStats().AbilityPower.Total * 0.8f;
            var damage = 80 + spell.Level * 50 + ap;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

            var slowDuration = 1.0f;
            switch (spell.Level)
            {
                case 1:
                    slowDuration = 1;
                    break;
                case 2:
                    slowDuration = 1.25f;
                    break;
                case 3:
                    slowDuration = 1.5f;
                    break;
                case 4:
                    slowDuration = 1.75f;
                    break;
                case 5:
                    slowDuration = 2;
                    break;
                default:
                    break;
            }
            ApiFunctionManager.AddBuff("Slow", slowDuration, 1, target, owner);
            ApiFunctionManager.AddParticleTarget(owner, "caitlyn_entrapment_tar.troy", target);
            ApiFunctionManager.AddParticleTarget(owner, "caitlyn_entrapment_slow.troy", target);

            projectile.setToRemove();
        }
    }
}