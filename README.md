# Fondazione ITS Alto Adriatico Inventory Management System

This repository contains the code for a web-based inventory management system for the Fondazione ITS Alto Adriatico. The system has two types of users: administrators and students.

## Workflow:

1. It is advised that the administrator/s would load up the stock of PC previously of the assignment, just to have all the serial data compiled into the database. It is not strictly necessary, the application supports also the insertion of a new device "on the go", at the moment of the delivery of the device.

2. At the moment of the delivery:
    1. The student will apply the property sticker ("cespite")
  2. The student will approach the administrator with the PC with the property sticker applied
  3. The administrator fill the assignment form in the application:
    1. The administrator will scan the serial of the PC first (if the device exists it will pull the data from the database, if it does not exists, it will ask from which stock batch is it linked to)
    2. The administrator will either scan or input manually the number of the property sticker
    3. The administrator will select the student from the database (the app will feature an autocomplete to simplify the input)
    4. The administrator will choose the reason of the delivery: First delivery, temporary delivery ("muletto")
    5. Confirmation of the delivery

3. Tickets:
  1. Tickets can be opened by students about anything, link to a specific device is NOT mandatory. It is generic as to enable all kind of needs, not specifically device dependant (ex. communicating with administration).
  2. An administrator can claim the ticket to flag who's handling the ticket
  3. An administrator can flag the ticket as open/claimed/completed

4. At the moment of the return of the device from the student to the ITS:
  1. The administrator will access the student profile and will go under the "linked devices" section
  2. The administrator will select which item connected to the student is the subject of the return
  3. The administrator will compile the form putting in the date and the reason of the return: "Riparazione", "Dimissioni", "Conclusione corso"
  4. The application, depending which option has been selected, will check the date of the device's batch to handle the status of being eligible for another course cycle or will request either the status of "muletto" or disposal.

### Administrator Features

An administrator can:

- Add items (both PC and ) to the inventory manually or automatically by scanning the barcode on the object
- Assign the devices to the students via barcode scan/property sticker scan/manual input
- View a list of tickets, organized by status:
  - To be taken care of
  - In progress
  - Completed
- Take over a ticket if its status is "to be taken care of"
- View a list of students attending the institute
- Upload an Excel file containing registered data: the rows of the sheet will be automatically converted, and the data will be inserted into the database

### Student Features

A student can:

- View their personal information, the items currently in their possession, and the expected return date (i.e. the end of their academic path, which may or may not coincide with the actual return date, for example in case of early dropout from studies)
- Open a ticket for technical assistance related to a malfunctioning product through a dedicated page where they can:
  - Select from a limited list of problem types
  - Add explanatory notes to help better identify the problem
- View the history of tickets opened in the past and linked to their student profile