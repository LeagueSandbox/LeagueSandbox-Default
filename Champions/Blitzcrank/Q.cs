using System.Numerics;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace LeagueSandbox_Default.Champions.Blitzcrank
{
    public class RocketGrab : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.SpellAnimation("SPELL1", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 925;
            var trueCoords = current + range;
            spell.AddProjectile("RocketGrabMissile", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.Stats.AbilityPower.Total;
            var damage = 25 + spell.Level * 55 + ap;
            spell.Target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL,
                false);
            if (!spell.Target.IsDead)
            {
                ApiFunctionManager.AddParticleTarget(owner, "Blitzcrank_Grapplin_tar.troy", spell.Target, 1, "L_HAND");
                var current = new Vector2(owner.X, owner.Y);
                var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
                var range = to * 50;
                var trueCoords = current + range;
                ApiFunctionManager.DashToLocation((ObjAiBase) spell.Target, trueCoords.X, trueCoords.Y,
                    spell.SpellData.MissileSpeed, true);
            }

            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }
    }
}

