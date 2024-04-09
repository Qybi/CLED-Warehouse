# Fondazione ITS Alto Adriatico Inventory Management System

This repository contains the code for a web-based inventory management system for the Fondazione ITS Alto Adriatico. The system has two types of users: administrators and students.

## Administrator Features

An administrator can:

- View a list of tickets, organized by status:
  - To be taken care of
  - In progress
  - Completed
- Take over a ticket if its status is "to be taken care of"
- View a list of students attending the institute
- Add items to the inventory manually or automatically by scanning the barcode on the object
- Upload an Excel file containing registered data: the rows of the sheet will be automatically converted, and the data will be inserted into the database

## Student Features

A student can:

- View their personal information, the items currently in their possession, and the expected return date (i.e. the end of their academic path, which may or may not coincide with the actual return date, for example in case of early dropout from studies)
- Open a ticket for technical assistance related to a malfunctioning product through a dedicated page where they can:
  - Select from a limited list of problem types
  - Add explanatory notes to help better identify the problem
- View the history of tickets opened in the past and linked to their student profile
