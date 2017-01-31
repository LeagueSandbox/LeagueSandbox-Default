using System.Linq;
using LeagueSandbox.GameServer;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;

namespace Karthus
{
    public class R
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            foreach (var enemyTarget in ApiFunctionManager.GetChampionsInRange(owner, 20000, true).Where(x => x.Team == CustomConvert.GetEnemyTeam(owner.Team)))
            {
                ApiFunctionManager.AddParticleTarget(owner, "KarthusFallenOne", enemyTarget);
            }
        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var ap = owner.GetStats().AbilityPower.Total;
            var damage = 100 + spell.Level * 150 + ap * 0.6f;
            foreach (var enemyTarget in ApiFunctionManager.GetChampionsInRange(owner, 20000, true).Where(x => x.Team == CustomConvert.GetEnemyTeam(owner.Team)))
            {
                owner.DealDamageTo(enemyTarget, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            }
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {

        }

        public static void OnUpdate(double diff)
        {

        }
    }
}