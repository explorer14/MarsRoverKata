# Introduction 
The revolutionary new Mars rover that will change our lives as we know it. There are two flavours of rover implementation in this solution (_both built as .NET Core 3.1 console applications_).

- `QuickAndDirtyRover`: simple command line invocable rover. Not much in the way of clean code or tests. This was written first and therefore has no library links with the modular implementation.

- `MarsRover.ConsoleApp`: a bit cleaner implementation with domain model, ports and adapters and tests, but otherwise mostly identical in behaviour to the QAD version. The command line output might be a bit different format between the two.

# Build Solution

`cd` into `\MarsRover\` and invoke `dotnet build`

# Getting Started with QuickAndDirtyRover

## Command Line Invocation

### Single rover invocation

`cd` into the debug folder at `\MarsRover\QuickAndDirtyRover\bin\Debug\netcoreapp3.1` and invoke:

`.\QuickAndDirtyRover.exe "terrainMaxX terrainMaxY" "roverX roverY roverHeading" some-combination-of-L/M/Rs`

For e.g. 

`.\QuickAndDirtyRover.exe "5 5" "1 2 N" LML` will produce an output like this:

```
Rover starting @ (1, 2, N)
Rover turned LEFT now @ (1, 2, W)
Rover MOVED, now @ (0, 2, W)
Rover turned LEFT now @ (0, 2, S)
```

OR

`cd` into the project root folder `MarsRover\QuickAndDirtyRover` like so and invoke:

`dotnet run "5 5" "3 3 E" MMR`

## Visual Studio Debugging

Hit `F5` in Visual Studio having made sure that the `QuickAndDirtyRover` project is the startup project. Its hard coded to run with a default command sequence defined in `launchSettings.json`:

```
"profiles": {
    "QuickAndDirtyRover": {
      "commandName": "Project",
      "commandLineArgs": "\"5 5\" \"1 2 n\" lml"
    }
  }
```

### Multi rover invocation

Invoke `.\QuickAndDirtyRover.exe "5 5" "1 2 N" LML "3 3 e" mmr` 
or do `dotnet run "5 5" "3 3 E" MMR "1 2 n" mml`

# Getting Started with MarsRover.ConsoleApp

## Command Line Invocation

### Single rover invocation

`cd` into the debug folder at `\MarsRover\MarsRover.ConsoleApp\bin\Debug\netcoreapp3.1` and invoke the command of the form:

`.\MarsRover.ConsoleApp.exe "terrainMaxX terrainMaxY" "roverX roverY roverHeading" some-combination-of-L/M/Rs`

For e.g. 

`.\MarsRover.ConsoleApp.exe "5 5" "1 2 N" LML` will produce an output like this:

```
Rover a7d5ed92-d253-4421-8c1b-8824cf56b50f moved to (1, 2). Heading: West
Rover a7d5ed92-d253-4421-8c1b-8824cf56b50f moved to (0, 2). Heading: West
Rover a7d5ed92-d253-4421-8c1b-8824cf56b50f moved to (0, 2). Heading: South
```

OR

`cd` into the project root folder `\MarsRover\MarsRover.ConsoleApp` like so and invoke:

`dotnet run "5 5" "1 2 N" LML`

## Visual Studio Debugging

Hit `F5` in Visual Studio having made sure that the `MarsRover.ConsoleApp` project is the startup project. Its hard coded to run with a default command sequence defined in `launchSettings.json`:

```
"profiles": {
    "MarsRover.ConsoleApp": {
      "commandName": "Project",
      "commandLineArgs": "\"5 5\" \"1 2 n\" lml"
    }
  }
```

### Multi rover invocation

Invoke `.\MarsRover.ConsoleApp.exe "5 5" "1 2 N" LML "3 3 e" mmr` 
or do `dotnet run "5 5" "3 3 E" MMR "1 2 n" mml`

## NB:

In Either projects, if you make mistakes with command line arguments, program validation will tell you and stop. You are going to have to reinvoke it with the correct arguments.

# Test

Either run the tests from the Test Explorer window in Visual Studio or via the command line by `cd`-ing into the solution root folder and invoking `dotnet test`.

# Architectural and Design Notes:

This `MarsRover.ConsoleApp` application follows the **Hexagonal Ports and Adapters** architectural style with DDD pattern, that has the domain at the heart of the application which interacts with the outside world using **ports**. These port interfaces are implemented by technology specific **adapters**. This way the domain can not only maintain  decoupling from the outside concerns but also establish a consistent linguistic boundary within it.

There 2 primary ports: 

- For parsing input data and turning them into domain readable commands parameters
- For transmitting rover's updated position and heading after a movement has been carried out.

The domain use case takes dependency on these 2 ports and:

- Retrieves all the parsed command parameters
- Instantiates a new Rover instance from these values
- Based on the movement sequence, commands the rover to move
- Transmits the updated rover position and heading

Based on evolving needs, new parser implementations can be created and swapped in without changes to the domain. Unless, you want to add a new behaviour to the rover for e.g. rotate camera and take selfies on Mars in which there will be a few changes to the domain and the parsers to support that.

# Assumptions:

You will find all the assumptions in the form of comments in the code where they were made.