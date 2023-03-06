using Test;

var pub = new Publisher();
var sub1 = new Subscriber("sub1", pub);
var sub2 = new Subscriber("sub2", pub);

// Call the method that raises the event.
pub.DoSomething();

// Keep the console window open
Console.WriteLine("Press any key to continue...");
Console.ReadLine();