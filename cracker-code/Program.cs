ICracker cracker = 
    args.Length > 0 && args[0] == "fake" ?
    new FakeCracker() : new Cracker();

cracker.Init(() =>
{
    // Add code here
    // Example of a dumb bot
    cracker.MoveTo(0, 0);
    var pos = cracker.GetPosition();

    while (pos.x < 920 && pos.y < 680)
    {
        cracker.Print(pos);
        cracker.Wait(100);
        cracker.MoveTo(pos.x + 10, pos.y + 10);
        pos = cracker.GetPosition();
    }

    while (pos.x < 920)
    {
        cracker.Print(pos);
        cracker.Wait(100);
        cracker.MoveTo(pos.x + 10, pos.y);
        pos = cracker.GetPosition();
    }

    cracker.MouseLeftDown();
    cracker.Wait(50);
    cracker.MouseLeftUp();

    // Final version of all program may
    // contains exit function
    cracker.Exit();
});