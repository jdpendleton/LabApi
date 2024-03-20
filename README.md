# LabApi
An API for managing Freezer Samples

## Architecture

The GraphQL API is divided into three assemblies, dependent on the lower utilizing dependency
injection in accorrdance with SOLID principals.


### LabApi.GraphQL
Responsible for the GraphQL Schema, all queries, mutations, and subscriptions.

### LabApi.Business
Organized into services, responsible for connecting the queries, mutations, and
subscriptions to the data layer and performing business logic

### LabApi.Data
Implementation for the ISampleRepository is an in-memory singleton repository with basic CRUD operations for simplicity.

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