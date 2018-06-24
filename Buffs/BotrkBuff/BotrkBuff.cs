using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace BotrkBuff
{
    internal class BotrkBuff : BuffGameScript
    {
        private ChampionStatModifier _statMod;
        private ObjAIBase _owningUnit;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _owningUnit = unit;
            _statMod = new ChampionStatModifier();
            _statMod.LifeSteel.FlatBonus = 0.1f;
            unit.AddStatModifier(_statMod);
            ApiEventManager.OnHitUnit.AddListener(this, unit, OnHitEffect);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            ApiEventManager.OnHitUnit.RemoveListener(this);
            unit.RemoveStatModifier(_statMod);
        }

        private void OnHitEffect(AttackableUnit target, bool isCrit)
        {
            if (!(target is ObjAIBase)) // Blade of the Ruined King will only damage minions, monsters, and champions.
            {
                return;
            }

            var damage = System.Math.Max(target.GetStats().CurrentHealth * 0.08f, 15); // 8% of the target's current health (15 minimum)
            if (target is Minion || target is Monster) // Bonus Damage from Blade of the Ruined King is capped at 60 for Minions and Monsters.
            {
                damage = System.Math.Min(damage, 60);
            }
            target.TakeDamage(_owningUnit, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}