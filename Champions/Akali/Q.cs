using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class AkaliMota : GameScript
    {
        public static Particle mark;
        static Champion owner;
        static AttackableUnit target;
        public void OnActivate(Champion owner)
        {
            AkaliMota.owner = owner;
            ApiEventManager.OnHitUnit.AddListener(this, owner, OnProc);
        }

        public static void OnProc(AttackableUnit target, bool isCrit)
        {
            if (mark == null)
                return;

            if (AkaliMota.target != target)
                return;

            ApiFunctionManager.LogInfo("Mark got procced, removing it earlier");
            ApiFunctionManager.RemoveParticle(mark);
            ApiFunctionManager.AddParticle(owner, "akali_mark_impact_tar.troy", mark.X, mark.Y);

            var ap = owner.GetStats().AbilityPower.Total * 0.5f;
            var damage = 20 + owner.Spells[0].Level * 25 + ap;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);

            var energy = 15 + owner.Spells[0].Level * 5;

            owner.GetStats().CurrentMana += energy;

            mark = null;
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(target.X, target.Y) - current);
            var range = to * 1150;
            var trueCoords = current + range;
            spell.AddProjectile("AkaliMota", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            AkaliMota.target = target;
            var ap = owner.GetStats().AbilityPower.Total * 0.4f;
            var damage = 15 + spell.Level * 20 + ap;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
            mark = ApiFunctionManager.AddParticleTarget(owner, "akali_markOftheAssasin_marker_tar_02.troy", target, 1, "");
            projectile.setToRemove();
            ApiFunctionManager.CreateTimer(6.0f, () =>
            {
                if (mark == null)
                    return;
                ApiFunctionManager.LogInfo("6 second timer finished, removing the mark of the assassin");
                ApiFunctionManager.RemoveParticle(mark);
            });
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
