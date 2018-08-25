using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class EvelynnPassive : IGameScript
    {
        public void OnActivate(Champion owner)
        {
            ApiEventManager.OnChampionDamageTaken.AddListener(this, owner, SelfWasDamaged);
        }

        private void SelfWasDamaged()
        {
        }

        public void OnDeactivate(Champion owner)
        {
            //Listeners are automatically removed when GameScripts deactivate
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

