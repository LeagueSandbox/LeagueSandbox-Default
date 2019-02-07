using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;

namespace Spells
{
    public class Swipe : IGameScript
    {
        public void OnActivate(IChampion owner)
        {
        }

        public void OnDeactivate(IChampion owner)
        {
        }

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {            
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {                        
                var current = new Vector2(owner.X, owner.Y);
                var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
                var range = to * 300;
                var trueCoords = current + range;

                spell.AddCone("Swipe", trueCoords.X, trueCoords.Y, 90f);
                var p1 = AddParticle(owner, "Nidalee_Base_Cougar_E_swipe.troy", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
            var ap = owner.Stats.AbilityPower.Total * 0.45f;
            var damage = 10f + owner.Spells[3].Level * 60 + ap;

            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            //W CD refund
            if (target.IsDead)
            {
                AddParticleTarget(owner, "Nidalee_Base_Cougar_W_Enhanced.troy", owner);
                AddParticleTarget(owner, "Nidalee_Base_Cougar_W_Marked_Cas.troy", owner);
                var refund = (30f + owner.Spells[3].Level * 10) / 100f;
                owner.Spells[1].LowerCooldown(refund);
            };
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
