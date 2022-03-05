![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

# Sitecore Hackathon 2022

- MUST READ: **[Submission requirements](SUBMISSION_REQUIREMENTS.md)**
- ~~[Entry form template](ENTRYFORM.md)~~
- ~~[Starter kit instructions](STARTERKIT_INSTRUCTIONS.md)~~

## Team name

⟹ Web3

## Category

⟹ Sitecore Command Line Interface (CLI) plugin

## Description

⟹ Write a clear description of your hackathon entry.

### Module Purpose

An easy way to get running Sitecore XM CM instances for use in headless projects, also from non Windows machines - Anywhere where the Sitecore CLI runs.

### What problem was solved

- People on non Windows machines or low spec'ed Windows machines had trouble running a full Sitecore XM on VM's or directly on host. They can now request a fresh new Sitecore XM CM instance using Sitecore CLI `dotnet sitecore instance start --name 'xyz' --password 'b'`.
- Hosting the backend (operator) and instances can be done on any Windows Container capable machine (Windows 10/11, Server 2019/2022), cloud or local.

### How does this module solve it

![Overview](docs/overview.png?raw=true "Overview")

1. Sitecore CLI plugin that talks http with an operator.
1. Operator REST API that schedules containers on a Windows host.
1. Preconfigured lightweight Sitecore 10.2.0 XM CM, single container with embedded SQL Server 2019 Express.

## Video link

⟹ Provide a video highlighting your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

⟹ [Replace this Video link](#video-link)

## Pre-requisites and Dependencies

- Windows 10 / 11
- Some Docker engine, for example [Docker Desktop](https://desktop.docker.com/win/stable/amd64/Docker%20Desktop%20Installer.exe)
- [.NET 6.0 SDK](https://download.visualstudio.microsoft.com/download/pr/89f0ba2a-5879-417b-ba1d-debbb2bde208/b22a9e9e4d513e4d409d2222315d536b/dotnet-sdk-6.0.200-win-x64.exe)

## Installation instructions

1. Make sure you have placed a valid Sitecore license file at `.\glitterfish\docker\build\cm\license.xml`.
1. Run `.\Build.ps1`.

## Usage instructions

⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

Include screenshots where necessary. You can add images to the `./images` folder and then link to them from your documentation:

![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Deal With It](docs/images/deal-with-it.gif?raw=true "Deal With It")

And you can embed external images too:

![Random](https://thiscatdoesnotexist.com/)

## Comments

If you'd like to make additional comments that is important for your module entry.
