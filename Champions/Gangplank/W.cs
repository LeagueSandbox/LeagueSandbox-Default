using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Gangplank
{
    public class W : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApplyHeal(owner, spell);
            owner.ClearAllCrowdControl();

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {

        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {
            
        }

        public void OnUpdate(double diff) {

        }

        private void ApplyHeal(Champion owner, Spell spell)
        {
            var heal = new[] {80, 150, 220, 290, 360}[spell.Level - 1] + owner.GetStats().AbilityPower.Total;
            var newHealth = owner.GetStats().CurrentHealth + heal;
            var maxHealth = owner.GetStats().HealthPoints.Total;


            if (newHealth >= maxHealth)
            {
                owner.GetStats().CurrentHealth = maxHealth;
            }
            else
            {
                owner.GetStats().CurrentHealth = newHealth;
            }

        }
     }
}