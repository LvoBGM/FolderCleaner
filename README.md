# WPF File Sorter App

## A simple windows desktop application that can help you sort files from a messy folder

A little WPF app I made to help made to help me clean up my downloads folder. The app can be configured to take files from a source folder and sort them in different folders in a destination folder,
for example putting all .png and .jpg files in a "Pictures" folder.

## Using the app

The app consists of 3 main windows, the first is the one you see when you open the app, with an "Organize" button and 2 navigation buttons named "Folders" and "New Folder".

<img src="Images/main page.png" alt="Main Page Screenshot" width="600"/>

To first configure everything, you should use the edit menu item on the top menu bar, there you will find 2 boxes, which are:

* Edit Source Folder Path - edit this to set up the folder you want to clean up
* Edit Destination Folder Path - edit this to set up the folder where you want the programm to set up the sorted folder, like Pictures or Documents.

### The New Folder Button

This button will open a window where you can create a template for a sorted folder.

<img src="Images/add folder page.png" alt="Add Folder Page Screenshot" width="300"/>

You need to input 3 things to make a new folder template:

* Id - determines the order in which folders will be evaluated, for example if you have folders that both take in the same file extention, the one with a lower id gets priority
* Name - the unique name of the folder
* Extentions - the file extention the folder takes in, they have to be separated by a space

### The Organize Button

This button is the one you click after you have configured the source and destination folders, as well as added at least one folder tamplate.
It iterates all files in the source folder, and checks if that file can be put into one of the sorted folders in the destination folder, which takes in files with specific file extentions
and moves that file if it has found a match.

### The Folders Button

This button takes you to the folder management window, where you can view all folder templates you have made. here you can edit and delete these templates.

<img src="Images/manage page.png" alt="Manage Page Screenshot" width="600"/>
