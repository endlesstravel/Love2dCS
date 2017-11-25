

NuGet has both a GUI and command-line interface to work with packages in your projects. 
---

### 1. Using GUI

In the Visual Studio Solution Explorer window, right-click on the project node and you will see a new context menu, Manage NuGet Packages. 

Clicking on this menu will bring up the Manage NuGet Packages dialog. The dialog defaults to show packages that are installed in your project and have updates available on the office package source, as seen in below. The package source is a publicly hosted server on the Internet that hosts both open-source and closed-source libraries and components.

![002-right-click-project](https://github.com/endlesstravel/Love2dCS/raw/master/img/002-right-click-project.png "002-right-click-project")

And click `Browse`, type `Love2dCS` in search box. And then you get result :

![003-search-result](https://github.com/endlesstravel/Love2dCS/raw/master/img/003-search-result.png "003-search-result")

Click `Install` to setup Love2dCS library.

And **REMEBER** enable native code debugging in VS : `Configuration Properties/Debugging/Enable native code debugging`

![005-enable-native-code-dubuging](https://github.com/endlesstravel/Love2dCS/raw/master/img/005-enable-native-code-dubuging.png "005-enable-native-code-dubuging")


### 2. Install from NuGet using: `Install-Package Love2dCS`

After installing the NuGet or if you are using the Visual Studio 2015, to open the `Package Manager Console`, click on Tools Menu and choose `NuGet Package Manager` and then choose Package Manager Console.

Type : `Install-Package Love2dCS` , and, for a while, you will install Love2dCS library

And **REMEBER** enable native code debugging in VS : `Configuration Properties/Debugging/Enable native code debugging`

![005-enable-native-code-dubuging](https://github.com/endlesstravel/Love2dCS/raw/master/img/005-enable-native-code-dubuging.png "005-enable-native-code-dubuging")

### Reference : 
* [Unmanaged Debugging vs. Managed Debugging vs. Mixed Debugging.](https://blogs.msdn.microsoft.com/stevejs/2004/05/05/unmanaged-debugging-vs-managed-debugging-vs-mixed-debugging/)
* [Use NuGet Package Manager In Visual Studio 2015](http://www.c-sharpcorner.com/UploadFile/8a67c0/use-nuget-package-manager-in-visual-studio-2015/)
* [Using NuGet Packages](http://www.developerfusion.com/article/131917/using-nuget-packages/)
