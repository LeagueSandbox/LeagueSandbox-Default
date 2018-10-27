using System.Numerics;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore;

namespace Spells
{
    public class CaitlynEntrapment : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnUpdate(double diff)
        {

        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            // Calculate net coords
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 750;
            var trueCoords = current + range;

            // Calculate dash coords/vector
            var dash = Vector2.Negate(to) * 500;
            var dashCoords = current + dash;
            DashToLocation(owner, dashCoords.X, dashCoords.Y, 1000, true, "Spell3b");
            spell.AddProjectile("CaitlynEntrapmentMissile", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.Stats.AbilityPower.Total * 0.8f;
            var damage = new Damage(80 + (spell.Level - 1) * 50 + ap, DamageType.DAMAGE_TYPE_MAGICAL, 
                DamageSource.DAMAGE_SOURCE_SPELL, false);
            target.TakeDamage(owner, damage);
            var slowDuration = new[] {0, 1, 1.25f, 1.5f, 1.75f, 2}[spell.Level];
            AddBuff("Slow", slowDuration, 1, BuffType.SLOW, (ObjAiBase) target, owner);
            AddParticleTarget(owner, "caitlyn_entrapment_tar.troy", target);
            AddParticleTarget(owner, "caitlyn_entrapment_slow.troy", target);
            projectile.SetToRemove();
        }
    }
}
