# Take Home Task

## Overview

Please spend no more than **2 hours** on this exercise.

When you're done, please either:

- Upload your solution to a public GitHub repository and share the link, **or**
- Zip your solution and send it back via email.

## The task

Using the provided solution, create the following

- A page to list and search for products
- A responsive UI (it does not need to look pretty)
- A way to view which products come from what category
- Any validation, error handling, or business rules you think make sense for this kind of data

For the UI, pick whatever you're comfortable with — vanilla JS or a framework.

## What's provided

This repository is a basic .NET solution containing some seed data and a couple of data entities to get you started. Feel free to change anything to suit your own style of coding — moving files, renaming/restructuring projects, adding packages, etc. is all fair game.

There are **two web projects** — one MVC, one API — both wired up to the same in-memory `Product`/`Category` data via `Infrastructure`. **Pick one** to build your solution in, based on your strengths. Do not use both.

## Getting started

Requires the .NET SDK matching the target framework in the `.csproj` files.

```bash
dotnet restore
dotnet run --project Interview.API    # or Interview.Web
```

## Bonus points

- Application resilience
- Extendability of the design
- Good programming practices

## Time limit

**2 hours.** We're far more interested in your approach, code quality, and reasoning than in a fully "complete" feature set — **don't feel you need to rush to cover everything above**.
