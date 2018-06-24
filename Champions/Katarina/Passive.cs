using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class KatarinaPassive : GameScript
    {
        public void OnActivate(Champion owner)
        {
            ApiEventManager.OnDealDamage.AddListener(this, owner, champ => OnUnitDamageDealt(owner, champ));
        }

        private void OnUnitDamageDealt(Champion owner, AttackableUnit target)
        {
            if (target is Champion && target.IsDead)
            {
                owner.GetSpellByName("KatarinaQ").LowerCooldown(0, 15);
                owner.GetSpellByName("KatarinaW").LowerCooldown(1, 15);
                owner.GetSpellByName("KatarinaE").LowerCooldown(2, 15);
                owner.GetSpellByName("KatarinaR").LowerCooldown(3, 15);
            }
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
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
    }
}

