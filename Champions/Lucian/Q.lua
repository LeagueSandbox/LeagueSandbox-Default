Vector2 = require 'Vector2' -- include 2d vector lib 

function onStartCasting()
	local current = Vector2:new(owner.X, owner.Y)
    local to = (Vector2:new(spell.X, spell.Y) - current):normalize()
    local range = to * 1100
    local trueCoords = current + range
	
	addLaser(trueCoords.x, trueCoords.y, true)
	spellAnimation("SPELL1", owner)
	addParticle(owner, "Lucian_Q_laser.troy", trueCoords.x, trueCoords.y)
	addParticleTarget(owner, "Lucian_Q_tar.troy", owner)

end
function onFinishCasting()

end

function applyEffects()
	local damage = (owner:GetStats().AttackDamage.Total*(0.45 + (spellLevel*0.15))) + (50 + (spellLevel*30))
	dealPhysicalDamage(damage)
	
end

function onUpdate(diff)

end