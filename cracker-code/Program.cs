using static Cracker;

Init(() =>
{
    MoveTo(0, 0);
    var pos = GetPosition();

    while (pos.x < 500 && pos.y < 500)
    {
        Print(pos);
        Wait(100);
        MoveTo(pos.x + 10, pos.y + 10);
        pos = GetPosition();
    }
});