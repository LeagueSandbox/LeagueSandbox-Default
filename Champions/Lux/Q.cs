using System.Numerics;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class LuxLightBinding : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "LuxLightBinding_cas.troy", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1175;
            var trueCoords = current + range;
            spell.AddProjectile("LuxLightBindingDummy", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.Stats.AbilityPower.Total * 0.7f;
            var damage = 70 + spell.Level * 20 + ap;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            ApiFunctionManager.AddParticleTarget(owner, "LuxLightBinding_tar.troy", target);
            var time = 2.0f;
            ((ObjAiBase)target).AddBuffGameScript("Root", "Root", spell, time);
            ApiFunctionManager.AddBuffHudVisual("Root", time, 1, BuffType.SNARE, (ObjAiBase)target, time);
            projectile.IncrementAttackerCount();
            if (projectile.AttackerCount >= 2)
                projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
