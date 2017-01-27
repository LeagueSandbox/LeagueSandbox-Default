using LeagueSandbox.GameServer.Logic.GameObjects;

namespace Quickdraw
{
    class Quickdraw
    {
        static void onAddBuff(Champion owner, Spell spell)
        {
            owner.GetStats().AttackSpeedMultiplier.PercentBonus = (spell.Level + 2) * 10;
        }
        static void onUpdate(double diff) { }
        static void onBuffEnd() { }
    }
}
