using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class RocketGrab : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.spellAnimation("SPELL1", owner);
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
            var ap = owner.GetStats().AbilityPower.Total;
            var damage = 25 + spell.Level * 55 + ap;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL,
                false);
            if (!target.IsDead)
            {
                ApiFunctionManager.AddParticleTarget(owner, "Blitzcrank_Grapplin_tar.troy", target, 1, "L_HAND");
                var current = new Vector2(owner.X, owner.Y);
                var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
                var range = to * 50;
                var trueCoords = current + range;
                ApiFunctionManager.DashToLocation((ObjAIBase)target, trueCoords.X, trueCoords.Y,
                    spell.SpellData.MissileSpeed, true);
            }

            projectile.setToRemove();
        }

        public void OnUpdate(double diff)
        {
        }
    }
}

