using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Collections.Generic;

namespace Garen
{
    public class E : IGameScript
    {
        GameScriptInformation info;
        Spell spell;
        Unit owner;
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            info = scriptInfo;
            spell = info.OwnerSpell;
            owner = info.OwnerUnit;
            //Setup event listeners
            ApiEventManager.OnSpellCast.AddListener(this, spell, OnStartCasting);
        }
        public void OnDeactivate() { }
        public void OnStartCasting(Unit target)
        {
            Particle p = null;
            if (owner is Champion) p = ApiFunctionManager.AddParticleTarget(owner as Champion, "Garen_Base_E_Spin.troy", owner, 1);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("GarenE", 3.0f, 1, owner);
            ApiFunctionManager.CreateTimer(3.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                if (p != null) ApiFunctionManager.RemoveParticle(p);
            });

            for (float i = 0.0f; i < 3.0; i+= 0.5f)
            {
                ApiFunctionManager.CreateTimer(i, () =>
                {
                    ApplySpinDamage(target);
                });
            }
        }
        private void ApplySpinDamage(Unit target)
        {
            List<Unit> units = ApiFunctionManager.GetUnitsInRange(owner, 175, true);
            foreach (Unit unit in units)
            {
                if (unit.Team != owner.Team)
                {
                    //PHYSICAL DAMAGE PER SECOND: 20 / 45 / 70 / 95 / 120 (+ 70 / 80 / 90 / 100 / 110% AD)
                    float ad = new[] { .7f, .8f, .9f, 1f, 1.1f }[spell.Level - 1] * owner.GetStats().AttackDamage.Total * 0.5f;
                    float damage = new[] { 20, 45, 70, 95, 120 }[spell.Level - 1] * 0.5f + ad;
                    if (unit is Minion) damage *= 0.75f;
                    owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                }
            }
        }
    }
}
