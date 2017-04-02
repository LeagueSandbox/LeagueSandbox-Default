using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Veigar
{
    public class E : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);
            var range = to * 700;
            var trueCoords = current + range;

            if (castrange <= 700)
            {
                if (owner.Skin == 8)
                {
                    ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_E_cas.troy", spell.X, spell.Y);
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_W_cas_hand.troy", owner);
                }
                else
                {
                    ApiFunctionManager.AddParticle(owner, "Veigar_Base_E_cas.troy", spell.X, spell.Y);
                }
            }
            else
            {
                if (owner.Skin == 8)
                {
                    ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_E_cas.troy", trueCoords.X, trueCoords.Y);
                    ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_W_cas_hand.troy", owner);
                }
                else
                {
                    ApiFunctionManager.AddParticle(owner, "Veigar_Base_E_cas.troy", trueCoords.X, trueCoords.Y);
                }
            }
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {

            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);
            var range = to * 700;
            var trueCoords = current + range;

            if (castrange <= 700)
            {
                    spell.AddProjectile("VeigarEventHorizon", spell.X, spell.Y);

                    if (owner.Skin == 8)
                    {
                        ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_E_cage_green.troy", spell.X, spell.Y);
                    }
                    else
                    {
                        ApiFunctionManager.AddParticle(owner, "Veigar_Base_E_cage_green.troy", spell.X, spell.Y);
                    }
            }
            else
            {
                    spell.AddProjectile("VeigarEventHorizon", trueCoords.X, trueCoords.Y);

                    if (owner.Skin == 8)
                    {
                        ApiFunctionManager.AddParticle(owner, "Veigar_Skin08_E_cage_green.troy", trueCoords.X, trueCoords.Y);
                    }
                    else
                    {
                        ApiFunctionManager.AddParticle(owner, "Veigar_Base_E_cage_green.troy", trueCoords.X, trueCoords.Y);
                    }
            }
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) { }
        public void OnUpdate(double diff) { }
     }
}