using GameServerCore;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class SummonerSmite : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            AddParticleTarget(owner, "Global_SS_Smite.troy", target, 1);
            var damage = new Damage(new float[] {390, 410, 430, 450, 480, 510, 540, 570, 600,
                640, 680, 420, 760, 800, 850, 900, 950, 1000}[owner.Stats.Level - 1], DamageType.DAMAGE_TYPE_TRUE,
                DamageSource.DAMAGE_SOURCE_SPELL, false); // Smite applies spell effects.

            target.TakeDamage(owner, damage);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }
    }
}

