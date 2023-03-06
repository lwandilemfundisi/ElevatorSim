# ElevatorSim
# Elevator Simulation Code Challenge built using Domain Driven Design coupled with Aggregate Pattern and Event Driven Architecture and CRQS

# Before starting up the simulation please follow these steps
# - On the configuration file for Local please change the server to point to your machine (database will be built for you using code first approach of Microsoft)
# - Run the migrations to build the database on your local machine with Sql Server Express installed
# -- To run the migrations use the nuget console manager and run command Update-Database
# - If you don't have Sql Server Expressed installed you can change the persistence to InMemory (which you dont need the migrations run)

# The application is built to execute its steps using events (the events described below)
# - On ElevatorControl (Manages elevators) you request an elevator
# - The elevator control then assigns an elevator to you using a round robin selection (had to do this as I was running out of time)
# - Once an elevator is assigned the control then tells the elevator to move from its current floor to the requested floor to load
# - Once the elevator is loaded it then moves the load to the designated floor of choice
# - Once it reaches the designated floor it then unloads the elevator (updating the elevator status and current load it has all this information is stored on DB as it occurs)

# For visual representation of these events occuring I implemented the basic Event/Publish (on the Domain) for the console (found in this url https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-publish-events-that-conform-to-net-framework-guidelines)

# To add more information about the simulation as I develop it below here