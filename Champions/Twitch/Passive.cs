using LeagueSandbox.GameServer.Logic.GameObjects;

namespace Twitch
{
    public class Passive
    {
        public static void OnUpdate(double diff) { }
        public static void OnDamageTaken(Unit attacker, float damage, DamageType type, DamageSource source) { }
        public static void OnAutoAttack(Unit target) { }
        public static void OnDealDamage(Unit target, float damage, DamageType damageType, DamageSource source) { }
        public static void OnSpellCast(float x, float y, Spell slot, Unit target) { }
        public static void OnDie(Unit killer) { }
        public static void OnCollideWithTerrain() { }
        public static void OnCollide(Unit collider) { }
    }
}
