using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class RapidFire : GameScript
    {
        public void OnActivate(Champion owner)
        {

        }

        private void SelfWasDamaged()
        {
        }

        public void OnDeactivate(Champion owner)
        {

        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var buff = ((ObjAIBase) target).AddBuffGameScript("TristanaQ", "TristanaQ", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("RapidFire", 5.0f, 1, owner);
            ApiFunctionManager.CreateTimer(5.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
            });				
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

