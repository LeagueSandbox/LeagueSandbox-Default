using LeagueSandbox.GameServer.Logic.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trundle
{
    public class Passive
    {
        public static void onUpdate(double diff) { }
        public static void onDamageTaken(Unit attacker, float damage, DamageType type, DamageSource source) { }
        public static void onAutoAttack(Unit target) { }
        public static void onDealDamage(Unit target, float damage, DamageType damageType, DamageSource source) { }
        public static void onSpellCast(float x, float y, Spell slot, Unit target) { }
        public static void onDie(Unit killer) { }
    }
}
