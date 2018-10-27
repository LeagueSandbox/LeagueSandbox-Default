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
    public class AkaliMota : IGameScript
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
            spell.AddProjectileTarget("AkaliMota", target);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.Stats.AbilityPower.Total * 0.4f;
            var damage = new Damage(15 + spell.Level * 20 + ap, DamageType.DAMAGE_TYPE_PHYSICAL, 
            DamageSource.DAMAGE_SOURCE_ATTACK, false);
            target.TakeDamage(owner, damage);
            AddParticleTarget(owner, "akali_markOftheAssasin_marker_tar_02.troy", target);
            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
