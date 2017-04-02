using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Veigar
{
    public class W : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);
            var range = to * 900;
            var trueCoords = current + range;

            if (castrange <= 900)
            {
                if (owner.Skin == 8)
                {
                    ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_W_cas.troy", spell.X, spell.Y);
                    ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_W_warning.troy", spell.X, spell.Y);
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_W_cas_hand.troy", owner);
                }
                else
                {
                    ApiFunctionManager.AddParticle(owner, "Veigar_Base_W_cas.troy", spell.X, spell.Y);
                    ApiFunctionManager.AddParticle(owner, "Veigar_Base_W_warning.troy", spell.X, spell.Y);
                }
            }
            else
            {
                if (owner.Skin == 8)
                {
                    ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_W_cas.troy", trueCoords.X, trueCoords.Y);
                    ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_W_warning.troy", trueCoords.X, trueCoords.Y);
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_W_cas_hand.troy", owner);
                }
                else
                {
                    ApiFunctionManager.AddParticle(owner, "Veigar_Base_W_cas.troy", trueCoords.X, trueCoords.Y);
                    ApiFunctionManager.AddParticle(owner, "Veigar_Base_W_warning.troy", spell.X, spell.Y);
                }
            }
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);
            var range = to * 900;
            var trueCoords = current + range;

            if (castrange <= 900)
            {
                ApiFunctionManager.CreateTimer(1.2f, () =>
                {
                    spell.AddProjectile("VeigarDarkMatter", spell.X, spell.Y);

                    if (owner.Skin == 8)
                    {
                        ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_W_aoe_explosion.troy", spell.X, spell.Y);
                    }
                    else
                    {
                        ApiFunctionManager.AddParticle(owner, "Veigar_Base_W_aoe_explosion.troy", spell.X, spell.Y);
                    }
                    ApplyDamage(owner, spell, target);
                });
            }
            else
            {
                ApiFunctionManager.CreateTimer(1.2f, () =>
                {
                    spell.AddProjectile("VeigarDarkMatter", trueCoords.X, trueCoords.Y);

                    if (owner.Skin == 8)
                    {
                        ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_W_aoe_explosion.troy", trueCoords.X, trueCoords.Y);
                    }
                    else
                    {
                        ApiFunctionManager.AddParticle(owner, "Veigar_Base_W_aoe_explosion.troy", trueCoords.X, trueCoords.Y);
                    }
                    ApplyDamage(owner, spell, target);
                });
            }
        }

        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) { }
        public void OnUpdate(double diff) { }
        public void ApplyDamage(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);
            var range = to * 900;
            var trueCoords = current + range;

            if (castrange <= 900)
            {
                Target t = new Target(spell.X, spell.Y);
                List<Unit> units = ApiFunctionManager.GetUnitsInRange(t, 150, true);
                foreach (Unit unit in units)
                {
                    //if unit is in the enemy team
                    if (unit.Team != owner.Team)
                    {
                        //100% of your own AP
                        var ap = owner.GetStats().AbilityPower.Total;
                        // 120/170/220/270/320 Damage + Your AP
                        var damage = 70 + spell.Level * 50 + ap;
                        owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_MAGICAL,
                            DamageSource.DAMAGE_SOURCE_SPELL, false);
                    }
                }
            }
            else
            {
                Target t = new Target(trueCoords.X, trueCoords.Y);
                List<Unit> units = ApiFunctionManager.GetUnitsInRange(t, 150, true);
                foreach (Unit unit in units)
                {
                    //if unit is in the enemy team
                    if (unit.Team != owner.Team)
                    {
                        //100% of your own AP
                        var ap = owner.GetStats().AbilityPower.Total;
                        // 120/170/220/270/320 Damage + Your AP
                        var damage = 70 + spell.Level * 50 + ap;
                        owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_MAGICAL,
                            DamageSource.DAMAGE_SOURCE_SPELL, false);
                    }
                }
            }
        }
    }
}