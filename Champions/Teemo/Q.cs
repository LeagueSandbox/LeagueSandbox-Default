using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Teemo
{
    public class Q : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            float time = 1.25f + 0.25f * spell.Level;
            var buff = target.AddBuffGameScript("Blind", "Blind", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("Blind", time, 1, target);
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                target.RemoveBuffGameScript(buff);
            });
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            spell.AddProjectileTarget("BlindingDart", target);
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            var ap = owner.GetStats().AbilityPower.Total * 0.8f;
            var damage = 35 + spell.Level * 45 + ap;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            projectile.setToRemove();
        }
        public void OnUpdate(double diff) {

        }
     }
}