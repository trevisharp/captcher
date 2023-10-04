using System;
using System.IO;
using System.Collections.Generic;

var defaultJsons = new string[]
{
    "user-data 10.json",
    "user-data 16.json",
};
var file = args.Length == 0 || !File.Exists(args[0]) ?
      defaultJsons[Random.Shared.Next(2)] : args[0];
List<UserData> data = UserData.Read(file);

// add code here

// deafult implementation example
// defeat instaclick bot
if (data.Count < 5)
    isCracker();
else isUser();

void isCracker()
    => Console.WriteLine("Cracker");

void isUser()
    => Console.WriteLine("User");