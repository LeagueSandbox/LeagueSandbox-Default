using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace RisingSpell
{
    internal class RisingSpell : IBuffGameScript
    {
        private StatsModifier _statMod;
        private IBuff _visualBuff;

        public void OnActivate(IObjAiBase unit, ISpell ownerSpell)
        {
            var stacks = Spells.EzrealMysticShot.stacks;
            _statMod = new StatsModifier();            
            _statMod.AttackSpeed.PercentBonus += (10f / 100f)* stacks;
            unit.AddStatModifier(_statMod);
            _visualBuff = AddBuffHudVisual("EzrealRisingSpellForce", 6.0f, stacks, BuffType.COMBAT_ENCHANCER, unit);
            //Immunity to slowness not added
        }

        public void OnDeactivate(IObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
            RemoveBuffHudVisual(_visualBuff);
        }

        private void OnAutoAttack(AttackableUnit target, bool isCrit)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
