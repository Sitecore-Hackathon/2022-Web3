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
1. Preconfigured lightweight Sitecore 10.2.0 XM CM, single container with embedded SQL Server 2019 Express and custom pipelines to support the Sitecore CLI authentication.

## Video link

⟹ Provide a video highlighting your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

<!-- TODO -->

⟹ [Replace this Video link](#video-link)

## Pre-requisites and Dependencies

- Windows 10 / 11
- Some Docker engine, for example [Docker Desktop](https://desktop.docker.com/win/stable/amd64/Docker%20Desktop%20Installer.exe)
- Visual Studio 2022 and .NET 6.0 SDK

## Installation instructions

1. Make sure you have placed a valid Sitecore license file at `.\glitterfish\docker\build\cm\license.xml`.
1. Run `.\Build.ps1`.

## Usage instructions

⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

<!-- TODO -->

## Comments

If you'd like to make additional comments that is important for your module entry.
