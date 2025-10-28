# Caesar Cipher

#### A simple C# console application for encrypting and decrypting text using the Caesar cipher method (Latin letters only).


# What the program does

#### Asks the user to enter a message.

#### Encrypts the text by shifting each Latin letter by 3 positions (A→D, x→a).

#### Decrypts it back for verification.

#### Saves both the original and encrypted messages to a file called messages.txt.


# Requirements

### .NET SDK (version 6 or higher)

### Any IDE: Visual Studio, VS Code, Rider, or just the command line (dotnet CLI)

# How to Run
### Option 1 — Using .NET CLI

#### dotnet new console -n CaesarApp
#### dotnet run --project CaesarApp

### Option 2 — Using the C# Compiler (csc)

##### csc CaesarCipherApp.cs
##### ./CaesarCipherApp.exe
