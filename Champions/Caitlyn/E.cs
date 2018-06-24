using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class CaitlynEntrapment : GameScript
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
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.GetStats().AbilityPower.Total * 0.8f;
            var damage = 80 + (spell.Level - 1) * 50 + ap;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            var slowDuration = new[] {0, 1, 1.25f, 1.5f, 1.75f, 2}[spell.Level];
            ApiFunctionManager.AddBuff("Slow", slowDuration, 1, (ObjAIBase) target, owner);
            ApiFunctionManager.AddParticleTarget(owner, "caitlyn_entrapment_tar.troy", target);
            ApiFunctionManager.AddParticleTarget(owner, "caitlyn_entrapment_slow.troy", target);
            projectile.setToRemove();
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
