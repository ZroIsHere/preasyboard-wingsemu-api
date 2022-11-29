# PreasyBoard WingsEmu Api

This api was developed to be used in conjunction with the PreasyBoard service, but you are also free to use it with other projects.

## Features

- Contains all non-risky grpc services (except for warehouses, these have to be handled with care).
- With an asymmetric encryption (you must obtain your key from the PreasyBoard page).
- Built on .NET 7 (can in a future also be compiled as native code, see [here][Net7Aot]).

## Contributors

- [@ZroIsHere][ZroUser] as lead developer.
- [@vBalatroni][BahlUser] for his encryption implementation.

## Requirements

- Logically, a version of [this][VanosillaDiscord] emulator working and running with all the consoles opened.
- [.Net 7.0 SDK][Net7SDK] or latest
- Dont be a monkey with c#.

## Installation

- Create a folder inside src folder and paste all this repo inside it.
- Add to the solution.
- Take the new key from the website and paste in to the Configuration/PreasyBoardEnvVariables.cs in the EncryptionKey variable.
- Compile and run.

## Development

Want to contribute? 

Just do a fork, modify and do a pull request.

## Docker

To put this project in a docker container, just read [this][DockerArticle] article.

## License

Copyright© 2022

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files, to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [Net7SDK]: <https://dotnet.microsoft.com/en-us/download/dotnet/7.0>
   [DockerArticle]: <https://learn.microsoft.com/en-us/dotnet/core/docker/publish-as-container>
   [VanosillaDiscord]: <https://discord.gg/jDEMcvKRfc>
   [Net7Aot]: <https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/>
   [BahlUser]: <https://github.com/vBalatroni>
   [ZroUser]: <https://github.com/ZroIsHere>
