using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class shurelyascrest : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var targets = ApiFunctionManager.GetUnitsInRange(owner, 600, true);
            foreach(AttackableUnit unit in targets) {
                if (unit is Champion || unit is Minion)
                {
                    if (unit.Team == owner.Team)
                    {
                        ((ObjAIBase)unit).AddBuffGameScript("ShurelyasReverie", "ShurelyasReverie", spell, 3.0f);
                    }
                }
            }
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

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }
    }
}
