
x = 0;
y = 0;

img = love.graphics.newImage("res/img.png");

function love.load()
    print("Isn't cool?")
end

function love.update(dt)
    x = x + dt * 10;
    y = y + dt * 10;
end

function love.draw()
    love.graphics.setColor(1, 1, 1, 1);
    love.graphics.draw(img)
    love.graphics.setColor(0, 0, 0);
    love.graphics.print("i'm lua draw() !!!", math.floor(x), math.floor(y));
end