# LabTec
A Domain Driven Application for managing Samples 

## Architecture

The application is divided into five types of assemblies:
- A Domain Layer/Core Application responsible for business logic
- A Common/Shared Kernel that stores utilities scoped across multiple concerns
- An Infrastructure Layer responsible for data persistence, logging, etc.
- Applications such as APIs and Web Applications responsible for presenting data
- A Test Assembly responsible for Unit Tests, Integration Tests, and Quality Assurance

## Context

In a life science laboratory, scientist store their samples in a freezer. These samples are used
in their research. The freezer is shared in use between a group of researchers. When performing
their research, scientists can take the samples out of the freezer (checking the sample out).

When their research is complete, samples are archived for the sake of reference.

## Assignment

Regard a Sample that has the following properties:

- name
- owner
- checked out
- position: Position in the freezer
- archived

For the sake of scope, we limit the position to a fixed freezer.

Create an API that allows for listing samples and supports the following mutations:

- checkout / check-in
Marks a sample as checked out (taken out of the freezer). This should block any other mutation on the sample
until it is checked in again (by the user that performed the check-out)
- archive
If a sample is archived, no other mutations are allowed on the sample from that time on
- move
Move the sample to another position in the freezer