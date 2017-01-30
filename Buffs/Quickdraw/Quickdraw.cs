using LeagueSandbox.GameServer.Logic.GameObjects;

namespace Quickdraw
{
    class Quickdraw
    {
        static void OnAddBuff(Champion owner, Spell spell)
        {
            owner.GetStats().AttackSpeedMultiplier.PercentBonus = (spell.Level + 2) * 10;
        }
        static void OnUpdate(double diff) { }
        static void OnBuffEnd() { }
    }
}
