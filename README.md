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
- Visual Studio 2022 .NET 6.0 SDK

## Installation instructions

1. Make sure you have placed a valid Sitecore license file at `.\glitterfish\docker\build\cm\license.xml`
1. Make sure you don't have anything else running on `localhost:80`
1. Run `.\Build.ps1` (builds the cm, operator and plugin)
1. Run `.\Start.ps1` (starts the operator)

## Usage instructions

⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

<!-- TODO -->

## Comments
Of course we have important comments.

As an extra benefit and in the spirit of Hackathon and Community fun the CLI has been extended with a couple of entertainment features as well:
1. dotnet sitecore joke tell [--sj / --sitecore-joke] : get a random dad joke displayed pulled from an external and free dad joke service or specify the option to get a curated Sitecore joke instead.
1. dotnet sitecore steve says : get insights related to Sitecore's upcoming roadmap based on input from an external content service.
1. you can use aliases such as blockchain, nft and wallet - we are a real web 3.0 team!
