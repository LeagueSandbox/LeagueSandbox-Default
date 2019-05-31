using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace GangplankE
{
    internal class GangplankE : IBuffGameScript
    {
        private ChampionStatModifier _statMod;
        private IBuff _hudvisual;

        public void OnActivate(IObjAIBase unit, ISpell ownerSpell)
        {
            _statMod = new ChampionStatModifier();
            _statMod.AttackSpeed.PercentBonus = _statMod.AttackSpeed.PercentBonus + (10f + 20f * ownerSpell.Level) / 100f;
            _statMod.MoveSpeed.PercentBonus = _statMod.MoveSpeed.PercentBonus + (10f + 5f * ownerSpell.Level) / 100f;
            _statMod.AttackDamage.PercentBonus = _statMod.AttackDamage.PercentBonus + (10f + 10f * ownerSpell.Level) / 100f;
            unit.AddStatModifier(_statMod);
            
            var time = 7.0f;
            _hudvisual = AddBuffHUDVisual("RaiseMorale", time, 1, unit);
            
            AddParticleTarget(owner, "pirate_raiseMorale_cas.troy", unit, 1);
            AddParticleTarget(owner, "pirate_raiseMorale_mis.troy", unit, 1);
            AddParticleTarget(owner, "pirate_raiseMorale_tar.troy", unit, 1);
        }

        public void OnDeactivate(IObjAIBase unit)
        {
            unit.RemoveStatModifier(_statMod);
            
            RemoveBuffHudVisual(_hudvisual);
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}
