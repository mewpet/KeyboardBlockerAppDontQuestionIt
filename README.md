#### This is a bad idea

# Setup

If cou actually wanna use this update the window title in `Form1.cs` in line 37.

The program will block any keyboard input as long as the current active window title contains whatever value you declare the check for.

## Build for testing

```shell
dotnet build -c Release
```

## Build for production (standalone executable)

```shell
dotnet publish -c Release -r win-x64 --self-contained -p:PublishSingleFile=true
```

### In both cases the resulting executable will be saved to `<ProjectRoot>/bin/Release/netWhatever`
