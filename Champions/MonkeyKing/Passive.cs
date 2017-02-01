using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;

namespace MonkeyKing
{
    public class Passive
    {
        public static void OnUpdate(Unit owner, double diff) { }
        public static void OnDamageTaken(Unit owner, Unit attacker, float damage, DamageType type, DamageSource source) { }
        public static void OnAutoAttack(Unit owner, Unit target) { }
        public static void OnDealDamage(Unit owner, Unit target, float damage, DamageType damageType, DamageSource source) { }
        public static void OnSpellCast(Unit owner, Vector2 coords, Spell slot, Unit target) { }
        public static void OnDie(Unit owner, Unit killer) { }
        public static void OnCollideWithTerrain(Unit owner) { }
        public static void OnCollide(Unit owner, Unit collider) { }
    }
}
