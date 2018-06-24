using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.API;
using System.Linq;
using LeagueSandbox.GameServer;

namespace Spells
{
    public class KatarinaE : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            if (target == null)
            {
                return;
            }

            spell.spellAnimation("SPELL3", owner);
            ApiFunctionManager.AddParticleTarget(owner, "katarina_shadowStep_cas.troy", owner);
            ApiFunctionManager.TeleportTo(owner, target.X + 80, target.Y + 80);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            if (target == null)
            {
                return;
            }

            ApiFunctionManager.AddParticleTarget(owner, "katarina_shadowStep_cas.troy", owner);

            var damage = new[] { 60, 85, 110, 135, 160 }[spell.Level - 1] + owner.GetStats().AbilityPower.Total * 0.4f;

            if (target.Team != owner.Team)
            {
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
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
