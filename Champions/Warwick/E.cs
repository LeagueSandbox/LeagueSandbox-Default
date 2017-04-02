using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Packets;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Warwick
{
    public class E : GameScript
    {
        public void OnActivate(Champion owner)
        {
            

        }

        public void OnDeactivate(Champion owner)
        {
           
        }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
          
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            
        }
        public void ApplyEffects(Champion owner, Unit champion, Spell spell, Projectile projectile) {
            var range = 700 + spell.Level * 800;
            foreach (var enemy in ApiFunctionManager.GetChampionsInRange(owner, range, true))
            {
                if (enemy.Team != owner.Team)
                {
                    var enemyTotalHealth = enemy.GetStats().HealthPoints.Total;
                    var enemyCurrentHealth = enemyTotalHealth - enemy.GetStats().CurrentHealth;
                    if (enemyCurrentHealth < enemyTotalHealth * 0.5f)
                    {
                        owner.AddBuffGameScript("BloodScent", "BloodScent", spell);
                    }
                }
            }
        }
        public void OnUpdate(double diff) {

        }
     }
}