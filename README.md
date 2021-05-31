# Xamarin.DateTimePicker

This plugin is designed to select the time and date.

## Support platforms

- [x] Android
- [x] iOS

## Initialization

Xamarin.DateTimePicker must be initialized on every platform 

## Android

```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
    //...
    base.OnCreate(savedInstanceState);
    DateTimePicker.Droid.Platform.Init(this, savedInstanceState);
    //...
}
 ```
 
 ## iOS
 
 ```csharp
 public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    DateTimePicker.IOS.Platform.Init();
    //...
}
```

## PickAsync

 ```csharp
var dateTime = await DateTimePicker.Instance.PickAsync();
```

# Screenshots

|______________|   iOS   | Android |______________|
|:------------:|:---:|:-------:|:------------:|
| |![iOS](https://raw.githubusercontent.com/andrk0p/Xamarin.DateTimePicker/main/screenshots/ios.png)|![Android](https://raw.githubusercontent.com/andrk0p/Xamarin.DateTimePicker/main/screenshots/droid.jpg)| |
