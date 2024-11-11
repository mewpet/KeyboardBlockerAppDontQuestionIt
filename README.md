#### This is a bad idea

# Setup

The project contains a config file at `<ProjectRoot>/config.json`.
This file contains an array of window titles the project will check against, in which all keyboard input is blocked.

Since the program checks if the title contains a string, include a value in the config file that is present at all times in whatever application you want the block to be enabled. 

**After compiling the program, copy this config file next to the executable.**

To get the active window title of any application, you may compile the application and start it. The GUI will dynamically render the title of whatever window is in focus.

## Build for testing

```shell
dotnet build -c Release
```

Find the resulting executable with dependencies in `<ProjectRoot>/bin/Release/netWhatever`

## Build for production (standalone executable)

```shell
dotnet publish -c Release -r win-x64 --self-contained -p:PublishSingleFile=true
```

Find the resulting standalone executable in `<ProjectRoot>/bin/Release/netWhatever/win/publish`
