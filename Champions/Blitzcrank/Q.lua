Vector2 = require 'Vector2' -- include 2d vector lib 

function onStartCasting()
    addParticleTarget(owner, "Blitzcrank_Grapplin_tar.troy", owner, 1, "L_HAND")
end

function onFinishCasting()
    local current = Vector2:new(owner.X, owner.Y)
    local to = (Vector2:new(spell.X, spell.Y) - current):normalize()
    local range = to * 925
    local trueCoords = current + range
    addProjectile("RocketGrabMissile", trueCoords.x, trueCoords.y)
    spellAnimation("SPELL1", owner)
end

function applyEffects()
    local AP = owner:GetStats().AbilityPower.Total
    local damage = 25 + (spellLevel*55) + AP
    dealMagicalDamage(damage)

    local target = getTarget()
    local speed = projectileSpeed

    if ( not isDead( target ) ) then
    	-- dash the user slightly in front of blitz
	    local current = Vector2:new(owner.X, owner.Y)
	    local to = (Vector2:new(spell.X, spell.Y) - current):normalize()
    	local range = to * 50
	    local trueCoords = current + range

    	dashToLocation( target, trueCoords.x, trueCoords.y, speed, true)
    end

    destroyProjectile()
end

function onUpdate(diff)

end
